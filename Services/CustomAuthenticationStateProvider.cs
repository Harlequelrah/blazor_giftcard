using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Authentication;
using Microsoft.JSInterop;
using System.Net.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Net.Http.Json;
using System.Security.Claims;
using System.Text.Json;
using System.IdentityModel.Tokens.Jwt;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Collections.Concurrent;
using System.Threading;

namespace blazor_giftcard.Services
{
    public class ErrorResponseModel
    {
        public string ErrorMessage { get; set; }
        // Ajoute d'autres propriétés si nécessaire
    }

    public class CustomAuthenticationStateProvider : AuthenticationStateProvider, IAsyncDisposable
    {

        private readonly HttpClient _httpClient;
        private readonly IJSRuntime _jsRuntime;
        private readonly NavigationManager _navigation;
        private readonly IConfiguration _configuration;
        private readonly ILogger<CustomAuthenticationStateProvider> _logger;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private string _token;
        private bool _tokenStored;
        private bool _isPrerendering = true;
        private ConcurrentQueue<Func<Task>> _afterRenderActions;
        private readonly IHttpClientFactory _httpClientFactory;
        public CustomAuthenticationStateProvider(
            HttpClient httpClient,
            IJSRuntime jsRuntime,
            NavigationManager navigation,
            IHttpContextAccessor httpContextAccessor,
            ILogger<CustomAuthenticationStateProvider> logger,
            IConfiguration configuration,
            IHttpClientFactory httpClientFactory
             )
        {
            _httpClient = httpClient;
            _jsRuntime = jsRuntime;
            _navigation = navigation;
            _afterRenderActions = new ConcurrentQueue<Func<Task>>();
            _httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
            _logger = logger;
            _configuration = configuration;
            _httpClientFactory = httpClientFactory ?? throw new ArgumentNullException(nameof(httpClientFactory));

        }



        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            _logger.LogInformation("Getting authentication state...");

            if (_isPrerendering)
            {
                _logger.LogInformation("Prerendering in progress, returning unauthenticated state.");
                return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
            }
            _token = await GetTokenAsync();
            var identity = new ClaimsIdentity();
            if (!string.IsNullOrEmpty(_token))
            {
                try
                {
                    identity = new ClaimsIdentity(JwtParser.ParseClaimsFromJwt(_token), "jwt");
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error parsing JWT claims.");
                }
            }


            var current_user = new ClaimsPrincipal(identity);
            _logger.LogInformation($"User authenticated: {current_user.Identity.IsAuthenticated}");
            return new AuthenticationState(current_user);
        }



        public async Task Login(string email, string password)
        {
            _logger.LogInformation("Attempting to log in user: {Email}", email);

            try
            {
                var client = _httpClientFactory.CreateClient("noauthClientAPI");

                _logger.LogInformation("Sending login request...");
                var response = await client.PostAsJsonAsync($"{_configuration["ApiBaseUrl"]}/api/User/login", new { email, password });

                _logger.LogInformation("Login response received...");
                if (!response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();

                    // Journaliser le code de statut et le contenu de la réponse
                    _logger.LogWarning("Login failed with status code: {StatusCode}. Response content: {Content}", response.StatusCode, responseContent);

                    // Dé-sérialiser le contenu JSON en un objet d'erreur
                    try
                    {
                        var errorDetails = JsonSerializer.Deserialize<ErrorResponseModel>(responseContent);
                        _logger.LogWarning("Detailed error: {ErrorMessage}", errorDetails?.ErrorMessage);
                    }
                    catch (JsonException jsonEx)
                    {
                        _logger.LogError("Failed to deserialize error response: {Message}", jsonEx.Message);
                    }

                    return;
                }

                var result = await response.Content.ReadFromJsonAsync<AuthResponse>();
                if (string.IsNullOrEmpty(result.Token))
                {
                    _logger.LogWarning("No token received for user: {Email}", email);
                    return;
                }
                _token = result.Token;
                await SecureToken();
                var identity = new ClaimsIdentity();
                if (!string.IsNullOrEmpty(_token))
                {
                    try
                    {
                        identity = new ClaimsIdentity(JwtParser.ParseClaimsFromJwt(_token), "jwt");
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex, "Error parsing JWT claims.");
                    }
                }

                var current_user = new ClaimsPrincipal(identity);
                _logger.LogInformation($"User authenticated: {current_user.Identity.IsAuthenticated}");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Login failed for user: {Email}", email);
            }
            NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
            _logger.LogInformation("User logged in successfully.");
        }

        public async Task Logout()
        {
            try
            {
                _logger.LogInformation("Logging out user...");
                _token = null;
                await SecureToken();
                _logger.LogInformation("User logged out successfully.");
            }

            catch (Exception ex)
            {
                _logger.LogError(ex, "Loggin out  failed for user");
            }
        }


        public async Task<string> GetTokenAsync()
        {
            if (_isPrerendering)
            {
                _logger.LogInformation("Prerendering, skipping token retrieval.");
                return null;
            }
            var token = await _jsRuntime.InvokeAsync<string>("localStorage.getItem", "authToken");
            _logger.LogInformation($"Token retrieved from localStorage: {token}");
            return token;
        }

        private async Task SecureToken()
        {
            if (string.IsNullOrEmpty(_token))
            {
                await _jsRuntime.InvokeVoidAsync("localStorage.removeItem", "authToken");
                _logger.LogInformation("Token removed from localStorage");
            }
            else
            {
                await _jsRuntime.InvokeVoidAsync("localStorage.setItem", "authToken", _token);
                _logger.LogInformation("Token stored in localStorage");
            }
        }

        public async ValueTask DisposeAsync()
        {
            if (_tokenStored)
            {
                await SecureToken();
            }
        }

        public async Task NotifyPostPrerender()
        {
            _logger.LogInformation("Notifying post prerender");
            _isPrerendering = false;
            while (_afterRenderActions.TryDequeue(out var action))
            {
                _logger.LogInformation("Executing post prerender action");
                await action();
            }
        }

        public async Task AddAfterRenderAction(Func<Task> action)
        {
            _afterRenderActions.Enqueue(action);
            _logger.LogInformation("Action added to post prerender queue");

            if (!_isPrerendering)
            {
                _logger.LogInformation("Prerendering complete, executing action immediately");
                await action();
            }
        }


        public async Task OnAfterRenderAsync(bool firstRender)
        {
            if (_afterRenderActions.TryDequeue(out var action))
            {
                await action();
            }
        }

        private async Task<string> GetRefreshTokenFromServer()
        {
            var response = await _httpClient.GetStringAsync("http://localhost:5107/api/User/get-refresh-token");
            return response;
        }

        public async Task<bool> TryRefreshTokenAsync()
        {
            var refreshRequest = new { RefreshToken = await GetRefreshTokenFromServer() };
            var refreshTokenResponse = await _httpClient.PostAsJsonAsync("http://localhost:5107/api/User/refresh-token", refreshRequest);

            if (refreshTokenResponse.IsSuccessStatusCode)
            {
                var result = await refreshTokenResponse.Content.ReadFromJsonAsync<AuthResponse>();
                _token = result.Token;

                await SecureToken();
                NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
                return true;
            }

            return false;
        }
        public async Task<bool> GetRendering()
        {

            return _isPrerendering;
        }




        public class AuthResponse
        {
            public string Token { get; set; }
        }

        public static class JwtParser
        {
            public static IEnumerable<Claim> ParseClaimsFromJwt(string jwt)
            {
                var claims = new List<Claim>();
                var payload = jwt.Split('.')[1];
                var jsonBytes = ParseBase64WithoutPadding(payload);
                var keyValuePairs = JsonSerializer.Deserialize<Dictionary<string, object>>(jsonBytes);

                if (keyValuePairs != null)
                {
                    foreach (var kvp in keyValuePairs)
                    {
                        claims.Add(new Claim(kvp.Key, kvp.Value.ToString()));
                    }
                }

                return claims;
            }

            private static byte[] ParseBase64WithoutPadding(string base64)
            {
                switch (base64.Length % 4)
                {
                    case 2: base64 += "=="; break;
                    case 3: base64 += "="; break;
                }
                return Convert.FromBase64String(base64);
            }
        }


    }
}
