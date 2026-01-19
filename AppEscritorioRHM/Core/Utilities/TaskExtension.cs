using AppEscritorioRHM.Core.Models.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace AppEscritorioRHM.Core.Utilities
{
    public static class TaskExtensions
    {
        public static async Task<List<TResult>> ProcessWithSemaphoreAsync<TSource, TResult>(
        this IEnumerable<TSource> source,
        SemaphoreSlim semaphore,
        Func<TSource, CancellationToken, Task<TResult>> body,
        IProgress<ProgressInfo> progress = null,
        CancellationToken ct = default,
        Func<TSource, int>? itemIdSelector = null)
        {
            var tasks = new List<Task<TResult>>();
            int total = source.Count();
            int processedCount = 0;

            foreach (var item in source)
            {
                await semaphore.WaitAsync(ct);

                int? itemId = itemIdSelector?.Invoke(item);

                var task = Task.Run(async () =>
                {
                    try
                    {
                        var result = await body(item, ct);

                        // Reporte de progreso
                        if (progress != null)
                        {
                            int current = Interlocked.Increment(ref processedCount);
                            progress.Report(new ProgressInfo(current, total, itemId));
                        }

                        return result;
                    }
                    finally
                    {
                        semaphore.Release();
                    }
                }, ct);

                tasks.Add(task);
            }

            var results = await Task.WhenAll(tasks);
            return results.ToList();
        }
    }
}
