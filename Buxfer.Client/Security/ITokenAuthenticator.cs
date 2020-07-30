using RestSharp.Authenticators;

namespace Buxfer.Client.Security
{
    public interface ITokenAuthenticator : IAuthenticator
    {
        string Token { get; }
        bool Authenticated { get; }
    }
}