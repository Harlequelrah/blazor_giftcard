using System.Collections.Concurrent;
using System.Net.Http.Headers;

namespace blazor_giftcard.Services
{
    public class JwtAuthorizationHandler : DelegatingHandler
    {
        private CustomAuthenticationStateProvider _stateProvider;
        public JwtAuthorizationHandler(CustomAuthenticationStateProvider stateProvider)
        {
            _stateProvider = stateProvider;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
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

            return await client.SendAsync(request, cancellationToken);
        }
    }
    public class AuthorizationDebugMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<AuthorizationDebugMiddleware> _logger;

        public AuthorizationDebugMiddleware(RequestDelegate next, ILogger<AuthorizationDebugMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var user = context.User;
            if (user.Identity.IsAuthenticated)
            {
                _logger.LogInformation("User is authenticated");
                foreach (var claim in user.Claims)
                {
                    _logger.LogInformation("Claim Type: {ClaimType}, Claim Value: {ClaimValue}", claim.Type, claim.Value);
                }
            }
            else
            {
                _logger.LogInformation("User is not authenticated");
            }

            await _next(context);
        }
    }

}
