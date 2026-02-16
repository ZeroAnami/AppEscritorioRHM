using System;
using System.Collections.Generic;
using System.Text;

namespace AppEscritorioRHM.Core.Interfaces.Infrastructure
{
    public interface ICheckConnection
    {
        Task<bool> CheckConnectionAsync();
    }
}
