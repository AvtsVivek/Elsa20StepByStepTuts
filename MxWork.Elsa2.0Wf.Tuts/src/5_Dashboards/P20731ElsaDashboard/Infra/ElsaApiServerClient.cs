using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace P20731ElsaDashboard.Infra
{
    // Intrroduced this class based on the tye tutorial 
    // https://github.com/dotnet/tye/blob/main/docs/tutorials/hello-tye/00_run_locally.md
    // But debugging in tye is not workijng.
    // So left this approach.
    public class ElsaApiServerClient
    {
        private readonly HttpClient _client;
        
        public ElsaApiServerClient(HttpClient client)
        {
            _client = client;
        }

        public async Task<string> GetResponse(string baseAddress, string endpointString)
        {
            _client.BaseAddress = new Uri(baseAddress);
            var response = await _client.GetAsync(endpointString);
            var jsonResponseString = await response.Content.ReadAsStringAsync();
            return jsonResponseString;
        }
        public async Task<string> GetResponse(string endpointString)
        {
            var response = await _client.GetAsync(endpointString);
            var jsonResponseString = await response.Content.ReadAsStringAsync();
            return jsonResponseString;
        }
    }
}
