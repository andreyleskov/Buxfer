using RestSharp;

namespace Buxfer.Client.Security
{
    public class PresetTokenAuthenticator : ITokenAuthenticator
    {
        public PresetTokenAuthenticator(string token)
        {
            Token = token;
        }

        public string Token { get; }
        public bool Authenticated { get; } = true;

        public void Authenticate(IRestClient client, IRestRequest request)
        {
            request.AddParameter("token", Token);
        }
    }
}