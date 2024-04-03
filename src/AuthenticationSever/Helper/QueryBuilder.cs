using System.Security.Cryptography;
using System.Text;

namespace AuthenticationSever.Helper
{
    public class QueryBuilder
    {
        public static string GetQuery(Dictionary<string, string> values, ProviderType provider)
        {
            switch (provider)
            {
                case ProviderType.Google:

                    var google_access_token = values["token"];
                    return $"?access_token={google_access_token}";

                default:
                    return null;
            }
        }
    }
}
