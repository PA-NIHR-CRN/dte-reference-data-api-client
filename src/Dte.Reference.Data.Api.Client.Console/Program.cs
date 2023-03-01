using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Dte.Common.Exceptions;
using Dte.Common.Http;
using Microsoft.Extensions.Logging;

namespace Dte.Reference.Data.Api.Client.Console
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var httpClient = new HttpClient
            {
                BaseAddress = new Uri("https://localhost:2001/")
            };
            var authString = "Basic " + Convert.ToBase64String(Encoding.GetEncoding("ISO-8859-1").GetBytes("nihr-dte-study-api" + ":" + ""));
            httpClient.DefaultRequestHeaders.Add("Authorization", authString);

            var headerService = new HeaderService();
            headerService.SetHeader("ConversationId", new[] { Guid.NewGuid().ToString() });
            
            var client = new ReferenceDataApiClient(httpClient, headerService, new Logger<ReferenceDataApiClient>(new LoggerFactory()));

            try
            {
                // var addressesByPostcode = await client.GetDemographicsEthnicityAsync();
                // System.Console.WriteLine($"Success: {string.Join(", ", addressesByPostcode.Keys)} records");
                // foreach (var ethnicityResponse in addressesByPostcode)
                // {
                //     System.Console.WriteLine($"{ethnicityResponse.Value.LongName} ({ethnicityResponse.Value.ShortName})");
                // }

                // var result = await client.GetHealthConditionsAsync();
                // foreach (var r in result)
                // {
                //     System.Console.WriteLine($"{r}");
                // }

                var response = await client.GetFeatureFlagAsync("StudyService", "PrivateBetaEmailWhitelist");
                System.Console.WriteLine($"{response.Enabled} : {response.Found}");
            }
            catch (HttpServiceException ex)
            {
                System.Console.WriteLine($"HttpServiceException ({ex.ServiceName}): " + ex.Message + " : " + string.Join(", ", ex));
            }
            catch (HttpRequestException ex)
            {
                System.Console.WriteLine("HttpRequestException: " + ex);
            }
            catch (Exception ex)
            {
                System.Console.WriteLine("Exception: " + ex);
            }
        }
    }
}
