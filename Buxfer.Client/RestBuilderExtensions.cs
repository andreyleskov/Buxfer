using RestSharp;

namespace Buxfer.Client
{
    public static class RestBuilderExtensions
    {
        public static IRestRequest AddIfNotEmpty(this IRestRequest r, string parameterName, string value)
        {
            if (!string.IsNullOrWhiteSpace(value) && !string.IsNullOrEmpty(value))
                r.AddParameter(parameterName, value);

            return r;
        }

        public static IRestRequest AddIfNotZero(this IRestRequest r, string parameterName, int value)
        {
            if (value != 0)
                r.AddParameter(parameterName, value);

            return r;
        }
    }
}