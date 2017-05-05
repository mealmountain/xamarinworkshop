using Conference.Frontend;
// von Paul Bets, nimmt den Nativen Http Stack der entsprechenden Umgebung
// https://www.nuget.org/packages/modernhttpclient/
using ModernHttpClient;
// Hinzufügen des Microsoft NuGet  Microsoft.Net.Http
using System.Net.Http;
using System.Threading.Tasks;

namespace Conference.Forms
{
    public class FormsHttpService : IHttpService
    {
        // Use the native HTTP handling of each platform
        private HttpClient httpClient = new HttpClient(new NativeMessageHandler());
                
        public async Task<string> GetStringAsync(string url)
        {
			// Hier wird HTTP aufgerufen 
            return await httpClient.GetStringAsync(url);
        }
    }
}
