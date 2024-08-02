using System.Collections.Concurrent;
using System.Net.Http.Headers;
using Microsoft.JSInterop;

namespace blazor_giftcard.Services
{
    public class JwtAuthorizationHandler : DelegatingHandler
    {
        private UserContextService _userContext;
        private readonly IJSRuntime _jsRuntime;
        private readonly CustomAuthenticationStateProvider _authenticationStateProvider;
        public JwtAuthorizationHandler( IJSRuntime jsRuntime, UserContextService userContext,CustomAuthenticationStateProvider authenticationStateProvider)
        {
            _jsRuntime = jsRuntime;
            _userContext=userContext;
            _authenticationStateProvider = authenticationStateProvider;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
           try
           {
                var token = await UserContextService.GetTokenAsync();
                //var token2 = await _authenticationStateProvider.GetToken();
                Console.WriteLine("token: " + token);
                // Console.WriteLine("token: " + token2);
                if (!string.IsNullOrEmpty(token))
                {
                   request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
                }
           }
           catch (Exception ex)
           {
               Console.WriteLine($"Error getting token: {ex.Message}");
            }
            return await base.SendAsync(request, cancellationToken);

        }

    }
    public static class HttpClientExtensions
    {
        public static async Task<HttpResponseMessage> AuthSendAsync(this HttpClient client, HttpRequestMessage request, string token, CancellationToken cancellationToken)
        {
            if (!string.IsNullOrEmpty(token))
            {
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }
            else
            {
                Console.WriteLine("Token is null or empty.");
            }

            return await client.SendAsync(request, cancellationToken);
        }
    }

}
