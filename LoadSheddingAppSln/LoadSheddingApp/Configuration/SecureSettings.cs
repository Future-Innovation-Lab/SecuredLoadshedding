using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoadSheddingApp.Configuration
{
    public class SecureSettings : ISettings
    {
        private string _azureSearchEndPoint;
        private string _azureSearchkey;
        private string _azureOpenAiEndPoint;
        private string _azureOpenAiKey;
        private string _configurationEndpoint;

        public string ApplicationId { get => "<Application Id>";  set { } }

        public string[] Scopes
        {
            get => new string[] { "<Scope>"  };
            set  { }
        }
        public string AzureSearchEndPoint { get => _azureSearchEndPoint; set => _azureSearchEndPoint = value; }
        public string AzureSearchKey { get => _azureSearchkey; set => _azureSearchkey = value; }
        public string AzureOpenAiEndPoint { get => _azureOpenAiEndPoint; set => _azureOpenAiEndPoint = value; }
        public string AzureOpenAiKey { get => _azureOpenAiKey; set => _azureOpenAiKey = value; }
        public string ConfigurationEndpoint { get => "<Configuration Endpoint Url>"; set => _configurationEndpoint = value; }

        public SecureSettings()
        {

        }

        public async Task LoadSettings()
        {
            AzureSearchEndPoint = await SecureStorage.Default.GetAsync("AzureSearchEndPoint");
            AzureSearchKey = await SecureStorage.Default.GetAsync("AzureSearchKey");
            AzureOpenAiEndPoint = await SecureStorage.Default.GetAsync("AzureOpenAiEndPoint");
            AzureOpenAiKey = await SecureStorage.Default.GetAsync("AzureOpenAiKey");
        }

        public async Task SaveSettings()
        {
            await SecureStorage.Default.SetAsync("AzureSearchEndPoint", AzureSearchEndPoint);
            await SecureStorage.Default.SetAsync("AzureSearchKey", AzureSearchKey);
            await SecureStorage.Default.SetAsync("AzureOpenAiEndPoint", AzureOpenAiEndPoint);
            await SecureStorage.Default.SetAsync("AzureOpenAiKey", AzureOpenAiKey);
        }
    }
}
