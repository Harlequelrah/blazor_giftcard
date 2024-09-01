using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Net.Http.Headers;
using Microsoft.Extensions.Logging;
using blazor_giftcard.Models;
using System.Text.Json;
using System.Text;


namespace blazor_giftcard.Services
{
    public class SubscriberService
    {
        private readonly Lazy<HttpClient> _httpClient;
        private readonly HttpClient _noauthClient;
        private readonly HttpClient _authClient;
        private readonly ToastrService _toastrService;

        private readonly CustomAuthenticationStateProvider _customAuthenticationStateProvider;

        private readonly ILogger<SubscriberService> _logger;


        public SubscriberService(IHttpClientFactory httpClientFactory, ILogger<SubscriberService> logger, CustomAuthenticationStateProvider customAuthenticationStateProvider,ToastrService toastrService)
        {
            _httpClient = new Lazy<HttpClient>(() => httpClientFactory.CreateClient("authClientAPI"));
            _noauthClient = httpClientFactory.CreateClient("noauthClientAPI") ?? throw new ArgumentNullException(nameof(httpClientFactory));
            _logger = logger;
            _authClient = _httpClient.Value;
            _customAuthenticationStateProvider = customAuthenticationStateProvider;
            _toastrService = toastrService;

        }
        public async Task<List<Package>> GetPackagesAsync()
        {
            try
            {
                _logger.LogInformation("Getting packages.");
                var response = await _authClient.GetStringAsync("Package");
                using (var document = JsonDocument.Parse(response))
                {
                    var root = document.RootElement;
                    var packagesElement = root.GetProperty("$values");
                    var options = new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    };

                    var packages = JsonSerializer.Deserialize<List<Package>>(packagesElement.GetRawText(), options);
                    _logger.LogInformation("Successfully retrieved Packages.");
                    return packages;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error retrieving Packages: {ex.Message}");
                return new List<Package>(); // Retourne une liste vide en cas d'erreur
            }


        }



        public async Task<Beneficiary> RegisterBeneficiaryAsync(int idsubscriber, double montant, BeneficiaryDto beneficiaryDto)
        {

            try
            {
                _logger.LogInformation($"Registering beneficiary for subscriber ID {idsubscriber}.");
                var response = await _authClient.PostAsJsonAsync($"User/register/beneficiary/bysubscriber/{idsubscriber}/value/{montant}", beneficiaryDto);
                response.EnsureSuccessStatusCode();
                var registeredBeneficiary = await response.Content.ReadFromJsonAsync<Beneficiary>();
                _logger.LogInformation($"Successfully registered beneficiary for subscriber ID {idsubscriber}.");
                _toastrService.Success("L'enregistrement s'est déroulé avec succès", "SUCCESS");
                return registeredBeneficiary;
            }
            catch (HttpRequestException ex )
            {
                _logger.LogError($"Error registering beneficiary for subscriber ID {idsubscriber}: {ex.Message}");
                if (ex.Data.Contains("ResponseMessage"))
                {
                    var response = ex.Data["ResponseMessage"] as HttpResponseMessage;
                    if (response != null)
                    {
                        string errorContent = await response.Content.ReadAsStringAsync();
                        _toastrService.Error($"Erreur lors de l'enregistrement du beneficiaire : {errorContent}");
                        Console.WriteLine("Contenu de l'erreur : " + errorContent);
                    }
                }
                return null;
            }
        }

        public async Task<List<Subscription>> GetSubscriptionsBySubscriberAsync(int subscriberId)
        {
            try
            {
                _logger.LogInformation($"Getting subscription with ID {subscriberId}.");
                var response = await _authClient.GetStringAsync($"Subscription/ForSubscriber/{subscriberId}");
                using (var document = JsonDocument.Parse(response))
                {
                    var root = document.RootElement;
                    var subscriptionsElement = root.GetProperty("$values");
                    var options = new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    };
                    var subscriptions = JsonSerializer.Deserialize<List<Subscription>>(subscriptionsElement.GetRawText(), options);
                    _logger.LogInformation($"Successfully retrieved subscription with ID {subscriberId}.");
                    return subscriptions;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error retrieving subscriptions for subscriber ID {subscriberId}: {ex.Message}");
                return new List<Subscription>();
            }
        }
        public async Task<List<Beneficiary>> GetSubscriberBeneficiariesAsync(int subscriberId)
        {
            try
            {
                _logger.LogInformation($"Getting beneficiaries for subscriber with ID {subscriberId}.");
                var response = await _authClient.GetStringAsync($"Subscriber/beneficiaries/{subscriberId}");
                using (var document = JsonDocument.Parse(response))
                {
                    var root = document.RootElement;
                    var beneficiariesElement = root.GetProperty("$values");
                    var options = new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    };
                    var beneficiaries = JsonSerializer.Deserialize<List<Beneficiary>>(beneficiariesElement.GetRawText(), options);
                    _logger.LogInformation($"Successfully retrieved beneficiaries for subscriber with ID {subscriberId}.");
                    return beneficiaries;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error retrieving beneficiaries for subscriber ID {subscriberId}: {ex.Message}");
                return new List<Beneficiary>();
            }
        }


        public async Task<Subscription> PostSubscriptionAsync(int IdPackage, double? MontantParCarte, int IdSubscriber)
        {
            try
            {
                var response = await _authClient.PostAsJsonAsync("Subscription", new { IdPackage = IdPackage, IdSubscriber = IdSubscriber, MontantParCarte = MontantParCarte });
                var responseContent = await response.Content.ReadAsStringAsync();
                _logger.LogInformation($"Response content: {responseContent}");

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



        public async Task<List<SubscriberHistory>> GetSubscriberHistoriesAsync(int idSubscriber)
        {
            try
            {
                _logger.LogInformation($"Getting history for subscriber ID {idSubscriber}.");
                var response = await _authClient.GetStringAsync($"Subscriber/history/{idSubscriber}");
                using (var document = JsonDocument.Parse(response))
                {
                    var root = document.RootElement;
                    var historiesElement = root.GetProperty("$values");
                    var options = new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    };
                    var histories = JsonSerializer.Deserialize<List<SubscriberHistory>>(historiesElement.GetRawText(), options);
                    _logger.LogInformation($"Successfully retrieved history for subscriber ID {idSubscriber}.");
                    return histories;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error retrieving history for subscriber ID {idSubscriber}: {ex.Message}");
                return new List<SubscriberHistory>();
            }
        }
    }

}
