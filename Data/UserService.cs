﻿using IM.Models;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Blazored.LocalStorage;
using System.Linq;

namespace ImBlazorApp.Data
{
    public interface IUserService
    {
        Task<List<User>> GetAllUsers(string token);
        Task<string> GetUserNameById(string userId);
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

        public async Task<string> GetUserNameById(string userId)
        {
            List<User> users = await localStorage.GetItemAsync<List<User>>("allUsers");

            var user = users.Where(user => user.Id == userId).FirstOrDefault();

            return user.FirstName + " " + user.LastName;
        }

    }
}