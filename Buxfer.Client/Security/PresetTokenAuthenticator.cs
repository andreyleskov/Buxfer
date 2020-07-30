using RestSharp;

namespace Buxfer.Client.Security
{
    public class PresetTokenAuthenticator : ITokenAuthenticator
    {
        public string Token { get; }
        public bool Authenticated { get; } = true;

        public PresetTokenAuthenticator(string token)
        {
            Token = token;
        }
        public void Authenticate(IRestClient client, IRestRequest request)
        {
            request.AddParameter("token", Token);
        }
    }
}