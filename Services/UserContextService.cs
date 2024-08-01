
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using blazor_giftcard.Models;


namespace blazor_giftcard.Services{
public class UserContextService
    {
        public string Username { get; set; }
        public string Role { get; set; }
        public int IdUser { get; set; }
        public string Token { get; set; }
        public int IdSubscriber { get; set; }
        private readonly HttpClient _authClient;
        private readonly HttpClient _noauthClient;
        private readonly ILogger<UserContextService> _logger;

        public UserContextService(IHttpClientFactory httpClientFactory, ILogger<UserContextService> logger)
        {
            _authClient = httpClientFactory.CreateClient("authClientAPI") ?? throw new ArgumentNullException(nameof(httpClientFactory));
            _noauthClient = httpClientFactory.CreateClient("noauthClientAPI") ?? throw new ArgumentNullException(nameof(httpClientFactory));
            _logger = logger;
        }

        public async Task<int> GetId(int IdUser)
        {
            try
            {
                _logger.LogInformation($"Getting Id for User ID {IdUser}.");
                var response = await _authClient.GetFromJsonAsync<int>($"User/GetIdSubscriber/{IdUser}");
                _logger.LogInformation($"Successfully retrieved Id for  User ID {IdUser}.");
                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error retrieving Id for  User ID {IdUser}: {ex.Message}");
                return 0 ;
            }

        }


    }

}
