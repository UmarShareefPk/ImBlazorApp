using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components;
using System.Net;

namespace ImBlazorApp.Helper
{
    public class Common : ICommon
    {
       private readonly NavigationManager navigationManager;
      private readonly ILocalStorageService localStorage;
        public Common(NavigationManager _navigationManager, ILocalStorageService _localStorage)
        {
            navigationManager = _navigationManager;
            localStorage = _localStorage;
        }

        public string GetStatusNameByCode(string code)
        {
            switch (code)
            {
                case "N":
                    return "New";
                case "C":
                    return "Closed";
                case "A":
                    return "Approved";
                case "I":
                    return "In Progress";
                default:
                    return code;
            }
        }
        
        public async Task HandleUnauthorizedRequests(string method)
        {
            await localStorage.RemoveItemAsync("token");
            
            navigationManager.NavigateTo("/", forceLoad: true);
        }

        public void HandleFailedRequests(string method, HttpStatusCode statusCode)
        {

        }


    }// end of class
}
