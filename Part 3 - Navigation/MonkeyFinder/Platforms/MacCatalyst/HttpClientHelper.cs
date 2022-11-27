using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonkeyFinder
{ 
    // https://docs.microsoft.com/en-us/dotnet/maui/platform-integration/invoke-platform-code
    public partial class HttpClientHelper
    {
        // This method must be in a class in a platform project, even if
        // the HttpClient object is constructed in a shared project.
        public partial HttpClientHandler GetInsecureHandler()
        {
            HttpClientHandler handler = new HttpClientHandler();
            handler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) =>
            {
                if (cert.Issuer.Equals("CN=localhost"))
                    return true;
                return errors == System.Net.Security.SslPolicyErrors.None;
            };
            return handler;
        }
    }
}
