using System;
using System.Collections.Generic;
using System.Text;

namespace AppEscritorioRHM.Core.Utilities
{
    public class SynchronousProgress<T> : IProgress<T>
    {
        private readonly Action<T> _callback;
        private readonly SynchronizationContext _context;

        public SynchronousProgress(Action<T> callback)
        {
            _callback = callback;
            // Capturamos el contexto de la UI (donde se crea la instancia)
            _context = SynchronizationContext.Current;
        }

        public void Report(T value)
        {
            if (_context != null)
            {
                // Bloquea el hilo actual hasta que la UI
                // haya ejecutado el callback. Garantiza el orden.
                _context.Send(_ => _callback(value), null);
            }
            else
            {
                // Si no hay contexto de UI, ejecutamos directo
                _callback(value);
            }
        }
    }
}
