using Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IDataSessionHelper
    {
        public DataSession DataSession { get; }
        Task LoadSession(string token);
        Task<bool> RestoreSessionAsync(string json);
        Task ClearAsync();
    }
}
