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
    public interface IUserService
    {
        Task<List<User>> GetAllUsers(string token);
        Task<string> GetUserNameById(string userId);
        Task<bool> Authenticate(string username, string password);
        Task<UserPages> GetUsersWithPage(string token, int pageSize, int pageNumber, string search);
        Task<string> GetLoggedInUserId();

    }

    public class UserService : IUserService
    {
        private readonly IConfiguration configuration;
        private readonly IHttpClientFactory clientFactory;
        private readonly ILocalStorageService localStorage;
        public UserService(IConfiguration _configuration, IHttpClientFactory _clientFactory, ILocalStorageService _localStorage)
        {
            configuration = _configuration;
            clientFactory = _clientFactory;
            localStorage = _localStorage;
        }

        public async Task<bool> Authenticate(string username, string password)
        {
            var request = new HttpRequestMessage(HttpMethod.Post,
          configuration.GetSection("APIURL").Value + "/Users/authenticate");
            request.Content = new StringContent(JsonSerializer.Serialize(new { Username = username, Password = password }), Encoding.UTF8, "application/json");

            var client = clientFactory.CreateClient();

            var response = await client.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                using var responseStream = await response.Content.ReadAsStreamAsync();
                var userLoginInfo = await JsonSerializer.DeserializeAsync<UserLogin>(responseStream);

                await localStorage.SetItemAsync("token", userLoginInfo.Token);

                var allUsers = await GetAllUsers(userLoginInfo.Token);
                await localStorage.SetItemAsync("allUsers", allUsers);
                await localStorage.SetItemAsync("loggedInUserId", userLoginInfo.user.Id);

                return true;
                              
            }
            else            
               return false;            
            
        }
        public async Task<List<User>> GetAllUsers(string token)
        {
            var request = new HttpRequestMessage(HttpMethod.Get,
                configuration.GetSection("APIURL").Value + "/Users/AllUsers");

            request.Headers.Add("Authorization", "Bearer " + token);

            var client = clientFactory.CreateClient();

            var response = await client.SendAsync(request);

            var users = new List<User>();

            if (response.IsSuccessStatusCode)
            {
                using var responseStream = await response.Content.ReadAsStreamAsync();
                users = await JsonSerializer.DeserializeAsync<List<User>>(responseStream);
            }
            else
            {
            }

            return users;
        }

        public async Task<UserPages> GetUsersWithPage(string token, int pageSize, int pageNumber, string search)
        {           
            var request = new HttpRequestMessage(HttpMethod.Get,
             configuration.GetSection("APIURL").Value + "/Users/GetUsersWithPage?PageSize=" + pageSize + "&PageNumber=" + pageNumber + "&SortBy=a&SortDirection=a&Search=" + search);

            request.Headers.Add("Authorization", "Bearer " + token);


            var client = clientFactory.CreateClient();

            var response = await client.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                using var responseStream = await response.Content.ReadAsStreamAsync();
                var userPages = await JsonSerializer.DeserializeAsync<UserPages>(responseStream);
                return userPages;
            }
            else
            {
                return null;
            }     
        }

        public async Task<string> GetUserNameById(string userId)
        {
            List<User> users = await localStorage.GetItemAsync<List<User>>("allUsers");
            var user = users.Where(user => user.Id == userId).FirstOrDefault();
            return user.FirstName + " " + user.LastName;
        }

        public async Task<string> GetLoggedInUserId()
        {
           return await localStorage.GetItemAsync<string>("loggedInUserId");
        }

    }
}
