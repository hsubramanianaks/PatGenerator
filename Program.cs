using Microsoft.VisualStudio.Services.Client;
using Microsoft.VisualStudio.Services.WebApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace PatGenerator
{
    internal class Program
    {
        async static Task Main()
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            var connection = new VssConnection(new Uri("https://spsprodwcus0.vssps.visualstudio.com/A850a26fd-8300-ce32-bb6e-28e032a3a0fd"), new VssClientCredentials(false));
            await connection.ConnectAsync();

            var tokenClient = connection.GetClient<Microsoft.VisualStudio.Services.DelegatedAuthorization.WebApi.TokenHttpClient>();
            var token = new Microsoft.VisualStudio.Services.DelegatedAuthorization.SessionToken()
            {
                TargetAccounts = new List<Guid>() { Guid.Parse("2663b13f-50e3-a655-a159-22f6f4725fab") },
                Scope = "vso.gallery_publish",
                DisplayName = "Market place CloudTest",
                ValidTo = DateTime.Now.AddMonths(3)
            };

            var result = await tokenClient.CreateSessionTokenAsync(token, Microsoft.VisualStudio.Services.DelegatedAuthorization.SessionTokenType.Compact, false);
            Console.WriteLine(result.Token.ToString());
            // result.Dump();
        }
    }
}
