using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoadSheddingApp.Configuration
{
    public interface ISettings
    {
        public string AzureSearchEndPoint { get; set; }
        public string AzureSearchKey { get; set; }
        public string AzureOpenAiEndPoint { get; set; }
        public string AzureOpenAiKey { get; set; }

        public string ConfigurationEndpoint { get; set; }

        public string ApplicationId { get; set; }
        public string[] Scopes { get; set; }

        Task LoadSettings();
        Task SaveSettings();
    }
}
