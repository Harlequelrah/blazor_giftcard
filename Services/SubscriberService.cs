using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using blazor_giftcard.Models;

namespace blazor_giftcard.Services
{
    public class SubscriberService
    {
        private readonly HttpClient _authClient;
        private readonly HttpClient _noauthClient;
        private readonly ILogger<SubscriberService> _logger;

        public SubscriberService(IHttpClientFactory httpClientFactory, ILogger<SubscriberService> logger)
        {
            _authClient = httpClientFactory.CreateClient("authClientAPI") ?? throw new ArgumentNullException(nameof(httpClientFactory));
            _noauthClient = httpClientFactory.CreateClient("noauthClientAPI") ?? throw new ArgumentNullException(nameof(httpClientFactory));
            _logger = logger;
        }
        public async Task<List<Package>> GetPackagesAsync()
        {
            try
            {
                _logger.LogInformation("Getting packages.");
                var packagesArray = await _authClient.GetFromJsonAsync<Package[]>("Package");
                _logger.LogInformation("Successfully retrieved packages.");
                return new List<Package>(packagesArray);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error retrieving packages: {ex.Message}");
                return new List<Package>();
            }
        }

        public async Task<Beneficiary> RegisterBeneficiaryAsync(string token, int idsubscriber, BeneficiaryDto beneficiaryDto)
        {
            try
            {
                _logger.LogInformation($"Registering beneficiary for subscriber ID {idsubscriber}.");
                var response = await _authClient.PostAsJsonAsync($"register/beneficiary/bysubscriber/{idsubscriber}", beneficiaryDto);
                response.EnsureSuccessStatusCode();
                var registeredBeneficiary = await response.Content.ReadFromJsonAsync<Beneficiary>();
                _logger.LogInformation($"Successfully registered beneficiary for subscriber ID {idsubscriber}.");
                return registeredBeneficiary;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error registering beneficiary for subscriber ID {idsubscriber}: {ex.Message}");
                return null;
            }
        }

        public async Task<List<Subscription>> GetSubscriptionsBySubscriberAsync(string token, int subscriberId)
        {
            try
            {
                _logger.LogInformation($"Getting subscriptions for subscriber ID {subscriberId}.");
                var response = await _authClient.GetFromJsonAsync<List<Subscription>>($"Subscriber/{subscriberId}");
                _logger.LogInformation($"Successfully retrieved subscriptions for subscriber ID {subscriberId}.");
                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error retrieving subscriptions for subscriber ID {subscriberId}: {ex.Message}");
                return new List<Subscription>();
            }
        }

        public async Task<Subscription> GetSubscriptionAsync(string token, int idSubscription)
        {
            try
            {
                _logger.LogInformation($"Getting subscription with ID {idSubscription}.");
                var response = await _authClient.GetFromJsonAsync<Subscription>($"{idSubscription}");
                _logger.LogInformation($"Successfully retrieved subscription with ID {idSubscription}.");
                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error retrieving subscription with ID {idSubscription}: {ex.Message}");
                return null;
            }
        }

        public async Task<Subscription> PostSubscriptionAsync(int IdPackage, double? MontantParCarte , int IdSubscriber )
        {
            try
            {
                _logger.LogInformation("Posting a new subscription.");
                var response = await _authClient.PostAsJsonAsync($"Subscription", new {IdPackage = IdPackage, IdSubscriber=IdSubscriber ,MontantParCarte=MontantParCarte});
                response.EnsureSuccessStatusCode();
                var createdSubscription = await response.Content.ReadFromJsonAsync<Subscription>();
                _logger.LogInformation("Successfully posted a new subscription.");
                return createdSubscription;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error posting a new subscription: {ex.Message}");
                return null;
            }
        }

        public async Task<List<SubscriberHistory>> GetSubscriberHistoriesAsync(string token, int idSubscriber)
        {
            try
            {
                _logger.LogInformation($"Getting history for subscriber ID {idSubscriber}.");
                var response = await _authClient.GetFromJsonAsync<List<SubscriberHistory>>($"history/{idSubscriber}");
                _logger.LogInformation($"Successfully retrieved history for subscriber ID {idSubscriber}.");
                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error retrieving history for subscriber ID {idSubscriber}: {ex.Message}");
                return new List<SubscriberHistory>();
            }
        }
    }
 

}
