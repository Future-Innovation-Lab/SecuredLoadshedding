using Loadshedding.Core.Models;
using LoadSheddingApp.Configuration;
using LoadSheddingApp.Services.Interfaces;
using Microsoft.Datasync.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace LoadSheddingApp.Services
{
    public class LoadsheddingRemoteDataService : ILoadSheddingDataService
    {
        private ISettings _settings;
        private DatasyncClient _client = null;
        private IIdentityService _identityService = null;
        public Func<Task<AuthenticationToken>> TokenRequestor;

        public LoadsheddingRemoteDataService(IIdentityService identityService, ISettings settings)
        {
            _identityService = identityService;
            _settings = settings;
            TokenRequestor = _identityService.GetAuthenticationToken;
        }
        public async Task GetConfigurationAsync()
        {
            var options = new DatasyncClientOptions
            {
            };

            _client = new DatasyncClient(new Uri(_settings.ConfigurationEndpoint), new GenericAuthenticationProvider(TokenRequestor), options);

            var configString = await _client.HttpClient.GetStringAsync(new Uri(_settings.ConfigurationEndpoint));

            try
            {
                var jsonOptions = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };
                var config = JsonSerializer.Deserialize<ConfigStore>(configString,jsonOptions);
                if (config != null)
                {
                    _settings.AzureSearchKey = config.AzureSearchKey;
                    _settings.AzureOpenAiEndPoint = config.AzureOpenAiEndPoint;
                    _settings.AzureSearchEndPoint = config.AzureSearchEndPoint;
                    _settings.AzureOpenAiKey = config.AzureOpenAiKey;

                    _settings.SaveSettings();
                }
            }
            catch (Exception ex) { }
        }

        public async Task SyncSettings()
        {
            await _settings.LoadSettings();

            if (string.IsNullOrEmpty(_settings.AzureSearchKey))
            {
                await GetConfigurationAsync();
            }

        }
    }
}
