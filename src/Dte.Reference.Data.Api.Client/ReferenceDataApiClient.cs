using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Dte.Common.Http;
using Dte.Reference.Data.Api.Client.Models;
using Dte.Reference.Data.Api.Client.Responses;
using Microsoft.Extensions.Logging;

namespace Dte.Reference.Data.Api.Client
{
    public interface IReferenceDataApiClient
    {
        // Health
        Task<HealthModel> GetHealthAsync(bool includeReady);
        
        Task<Dictionary<string, EthnicityResponse>> GetDemographicsEthnicityAsync();
        Task<string[]> GetHealthConditionsAsync();
        
        // Feature Flags
        Task<FeatureFlagResponse> GetFeatureFlagAsync(string serviceName, string featureName);
    }

    public class ReferenceDataApiClient : BaseHttpClient, IReferenceDataApiClient
    {
        private readonly ILogger<ReferenceDataApiClient> _logger;

        public ReferenceDataApiClient(HttpClient httpClient, IHeaderService headerService, ILogger<ReferenceDataApiClient> logger) 
            : base(httpClient, headerService, logger, ApiClientConfiguration.Default)
        {
            _logger = logger;
        }

        protected override string ServiceName => "ReferenceDataService";

        public async Task<HealthModel> GetHealthAsync(bool includeReady)
        {
            var httpRequest = new HttpRequestMessage
            {
                RequestUri = new Uri($"api/health/{(includeReady ? "ready" : "")}", UriKind.Relative),
                Method = HttpMethod.Get
            };
            
            var response = await SendAsync<HealthModel>(httpRequest);

            return response;
        }

        public async Task<Dictionary<string, EthnicityResponse>> GetDemographicsEthnicityAsync()
        {
            var httpRequest = new HttpRequestMessage
            {
                RequestUri = new Uri($"api/demographics/ethnicity", UriKind.Relative),
                Method = HttpMethod.Get
            };
            
            var response = await SendAsync<Dictionary<string, EthnicityResponse>>(httpRequest);

            return response;
        }

        public async Task<string[]> GetHealthConditionsAsync()
        {
            var httpRequest = new HttpRequestMessage
            {
                RequestUri = new Uri($"api/health/healthconditions", UriKind.Relative),
                Method = HttpMethod.Get
            };
            
            var response = await SendAsync<string[]>(httpRequest);

            return response;
        }

        public async Task<FeatureFlagResponse> GetFeatureFlagAsync(string serviceName, string featureName)
        {
            var httpRequest = new HttpRequestMessage
            {
                RequestUri = new Uri($"api/featureflag/service/{serviceName}/feature/{featureName}", UriKind.Relative),
                Method = HttpMethod.Get
            };
            
            var response = await SendAsync<FeatureFlagResponse>(httpRequest);

            return response;
        }
    }
}
