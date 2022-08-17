using IM.Models;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Blazored.LocalStorage;
using System.Linq;
using System.Text;
using ImBlazorApp.Models;

namespace ImBlazorApp.Data
{
    public interface IIncidentService
    {
        Task<IncidentPages> GetIncidentsWithPage(string token, int pageSize, int pageNumber, string search);
        Task<Incident> GetIncidentById(string token, string incidentId);
        Task<bool> UpdateIncident(string token, object parameters);
    }

    public class IncidentService : IIncidentService
    {
        private readonly IConfiguration configuration;
        private readonly IHttpClientFactory clientFactory;
        private readonly ILocalStorageService localStorage;
        public IncidentService(IConfiguration _configuration, IHttpClientFactory _clientFactory, ILocalStorageService _localStorage)
        {
            configuration = _configuration;
            clientFactory = _clientFactory;
            localStorage = _localStorage;
        }

        
        public async Task<IncidentPages> GetIncidentsWithPage(string token, int pageSize, int pageNumber, string search)
        {
            var request = new HttpRequestMessage(HttpMethod.Get,
            configuration.GetSection("APIURL").Value + "/Incidents/GetIncidentsWithPage?PageSize=" + pageSize + "&PageNumber=" + pageNumber + "&SortBy=a&SortDirection=a&Search=" + search);

            request.Headers.Add("Authorization", "Bearer " + token);


            var client = clientFactory.CreateClient();

            var response = await client.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                using var responseStream = await response.Content.ReadAsStreamAsync();
                var incidentPages = await JsonSerializer.DeserializeAsync<IncidentPages>(responseStream);
                return incidentPages;
            }
            else
            {
                return null;
            }     
        }

        public async Task<Incident> GetIncidentById(string token, string incidentId)
        {
            var request = new HttpRequestMessage(HttpMethod.Get,
            configuration.GetSection("APIURL").Value + "/Incidents/IncidentById?Id=" + incidentId);

            request.Headers.Add("Authorization", "Bearer " + token);


            var client = clientFactory.CreateClient();

            var response = await client.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                using var responseStream = await response.Content.ReadAsStreamAsync();
                var incident = await JsonSerializer.DeserializeAsync<Incident>(responseStream);
                return incident;
            }
            else
            {
                return null;
            }
        }

        public async Task<bool> UpdateIncident(string token, object parameters)
        {
            var request = new HttpRequestMessage(HttpMethod.Post,
            configuration.GetSection("APIURL").Value + "/Incidents/UpdateIncident");
            request.Content = new StringContent(JsonSerializer.Serialize(parameters), Encoding.UTF8, "application/json");


            request.Headers.Add("Authorization", "Bearer " + token);


            var client = clientFactory.CreateClient();

            var response = await client.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                //using var responseStream = await response.Content.ReadAsStreamAsync();
                //var incident = await JsonSerializer.DeserializeAsync<Incident>(responseStream);
                return true;
            }
            else
            {
                return false;
            }
        }



    }
}
