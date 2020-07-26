using System;
using System.Threading.Tasks;
using Buxfer.Client.Responses;
using Microsoft.Extensions.Logging;
using RestSharp;
using RestSharp.Authenticators;

namespace Buxfer.Client.Security
{
    /// <summary>
    ///     IAuthenticator's implementation that use Buxfer API token.
    /// </summary>
    public class TokenAuthenticator : IAuthenticator
    {
        private readonly ILogger _logger;
        private readonly Func<string, Method, IRestRequest> m_createRequest;
        private readonly Func<IRestRequest, Task<LoginResponse>> m_executeRequest;
        private readonly string m_password;
        private readonly string m_userId;


        /// <summary>
        ///     Initializes a new instance of the <see cref="TokenAuthenticator" /> class.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="password">The password.</param>
        /// <param name="createRequest">The create request.</param>
        /// <param name="executeRequest">The execute request.</param>
        public TokenAuthenticator(string userId, string password, Func<string, Method, IRestRequest> createRequest,
            Func<IRestRequest, Task<LoginResponse>> executeRequest, ILogger logger)
        {
            _logger = logger;
            m_userId = userId;
            m_password = password;
            m_createRequest = createRequest;
            m_executeRequest = executeRequest;
        }

        public string Token { get; private set; }

        #region Properties

        /// <summary>
        ///     Gets a value indicating whether this <see cref="TokenAuthenticator" /> is authenticated.
        /// </summary>
        /// <value>
        ///     <c>true</c> if authenticated; otherwise, <c>false</c>.
        /// </value>
        public bool Authenticated { get; private set; }

        #endregion

        #region Methods

        /// <summary>
        ///     Authenticates the specified client.
        /// </summary>
        /// <param name="client">The client.</param>
        /// <param name="request">The request.</param>
        public void Authenticate(IRestClient client, IRestRequest request)
        {
            if (request.Resource.Equals("login", StringComparison.OrdinalIgnoreCase)) return;

            _logger.LogDebug("Authenticated: {status}", Authenticated);

            if (!Authenticated)
            {
                var loginRequest = m_createRequest("login", Method.GET);

                loginRequest.AddParameter("userid", m_userId);
                loginRequest.AddParameter("password", m_password);

                try
                {
                    Task.Run(async () =>
                    {
                        try
                        {
                            _logger.LogDebug("Requesting login...");
                            var response = await m_executeRequest(loginRequest);
                            _logger.LogDebug("Login requested.");
                            Token = response.Token;
                            Authenticated = true;
                        }
                        catch (Exception e)
                        {
                            _logger.LogError(e, "Login error");
                            throw;
                        }
                    }).Wait(60000);
                }
                catch (AggregateException ex)
                {
                    _logger.LogError(ex, "Login error");
                    throw ex.InnerException;
                }
            }

            request.AddParameter("token", Token);
        }

        #endregion
    }
}