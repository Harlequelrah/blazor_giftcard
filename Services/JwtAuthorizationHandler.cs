using System.Collections.Concurrent;
using System.Net.Http.Headers;
using Microsoft.JSInterop;

namespace blazor_giftcard.Services
{
    public class JwtAuthorizationHandler : DelegatingHandler
    {
        private CustomAuthenticationStateProvider _stateProvider;
        private readonly IJSRuntime _jsRuntime;
        public JwtAuthorizationHandler(CustomAuthenticationStateProvider stateProvider, IJSRuntime jsRuntime)
        {
            _stateProvider = stateProvider;
            _jsRuntime = jsRuntime;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
           // try
           // {
                //var token = await _stateProvider.GetToken();
                // var rendering = await _stateProvider.GetRendering();
                // var access_token =  await _userContextService.GetTokenAsync();
              //  Console.WriteLine($"Adding token to request: {token}");
                // Console.WriteLine($"rendering: {rendering}");

                // Console.WriteLine($"Adding Access token to request: {access_token}");
                //if (!string.IsNullOrEmpty(token))
                //{
                //    request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
                //}
           // }
           // catch (Exception ex)
           // {
            //    Console.WriteLine($"Error getting token: {ex.Message}");
            //}
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
