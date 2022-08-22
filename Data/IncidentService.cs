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
using ImBlazorApp.Helper;
using System.Net;

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
        private readonly ICommon commonService;
        private string baseUrl = "https://imwebapicore.azurewebsites.net/api";
        public IncidentService(IConfiguration _configuration, IHttpClientFactory _clientFactory, ILocalStorageService _localStorage, ICommon _commonService)
        {
            configuration = _configuration;
            clientFactory = _clientFactory;
            localStorage = _localStorage;
            commonService = _commonService;
        }

        
        public async Task<IncidentPages> GetIncidentsWithPage(string token, int pageSize, int pageNumber, string search)
        {
            var request = new HttpRequestMessage(HttpMethod.Get,
            baseUrl + "/Incidents/GetIncidentsWithPage?PageSize=" + pageSize + "&PageNumber=" + pageNumber + "&SortBy=a&SortDirection=a&Search=" + search);

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
                if(response.StatusCode == HttpStatusCode.Unauthorized)
                {
                    commonService.HandleUnauthorizedRequests("GetIncidentsWithPage");
                }                
                commonService.HandleFailedRequests("GetIncidentsWithPage", response.StatusCode);                
             
                return null;
            }     
        }

        public async Task<Incident> GetIncidentById(string token, string incidentId)
        {
            var request = new HttpRequestMessage(HttpMethod.Get,
            baseUrl + "/Incidents/IncidentById?Id=" + incidentId);

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
                if (response.StatusCode == HttpStatusCode.Unauthorized)
                {
                    commonService.HandleUnauthorizedRequests("GetIncidentById");
                }
                commonService.HandleFailedRequests("GetIncidentById", response.StatusCode);
                return null;
            }
        }

        public async Task<bool> UpdateIncident(string token, object parameters)
        {
            var request = new HttpRequestMessage(HttpMethod.Post,
            baseUrl + "/Incidents/UpdateIncident");
            request.Content = new StringContent(JsonSerializer.Serialize(parameters), Encoding.UTF8, "application/json");


            request.Headers.Add("Authorization", "Bearer " + token);


            var client = clientFactory.CreateClient();

            var response = await client.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            else
            {
                if (response.StatusCode == HttpStatusCode.Unauthorized)
                {
                    commonService.HandleUnauthorizedRequests("UpdateIncident");
                }
                commonService.HandleFailedRequests("UpdateIncident", response.StatusCode);
                return false;
            }
        }



    }
}
