using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoadSheddingApp.Services.Interfaces
{
    public interface ILoadSheddingDataService
    {
        Task GetConfigurationAsync();
        Task SyncSettings();
    }
}
