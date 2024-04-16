using Azure.Core;
using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using Loadshedding.Core.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LoadsheddingApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class ConfigurationController : ControllerBase
    {
        private readonly ILogger<ConfigurationController> _logger;
        private IConfiguration _configuration;

        public ConfigurationController(ILogger<ConfigurationController> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }

        [HttpGet(Name = "GetConfiguration")]
        public ConfigStore Get()
        {
            try
            {
                var keyVault = _configuration["SecureKeyVault"];

                SecretClientOptions options = new SecretClientOptions()
                {
                    Retry =
        {
            Delay= TimeSpan.FromSeconds(2),
            MaxDelay = TimeSpan.FromSeconds(16),
            MaxRetries = 5,
            Mode = RetryMode.Exponential
         }
                };
                var client = new SecretClient(new Uri(keyVault), new DefaultAzureCredential(), options);

                var configStore = new ConfigStore();
                KeyVaultSecret secret = client.GetSecret("AzureSearchEndPoint");
                string secretValue = secret.Value;

                configStore.AzureSearchEndPoint = secretValue;

                secret = client.GetSecret("AzureSearchKey");
                secretValue = secret.Value;

                configStore.AzureSearchKey = secretValue;

                secret = client.GetSecret("AzureOpenAiKey");
                secretValue = secret.Value;

                configStore.AzureOpenAiKey = secretValue;

                secret = client.GetSecret("AzureOpenAiEndPoint");
                secretValue = secret.Value;

                configStore.AzureOpenAiEndPoint = secretValue;

                return configStore;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return null;
            }
        }
    }
}
