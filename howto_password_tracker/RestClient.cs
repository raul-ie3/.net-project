using System;
using System.Collections.Generic;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace howto_password_tracker
{
    class RestClient
    {

        private const string URL = "https://makemeapassword.ligos.net";
        private string urlParameters = "/api/v1/alphanumeric/plain?pc=1&wc=8&sp=y&maxCh=64";
        private string password = "";

        public String getPassword()
        {
            return this.password;
        }

        private async Task DownloadFileAsync(HttpResponseMessage response)
        {
            // Use HttpClient or whatever to download the file contents.
            var fileContents = await response.Content.ReadAsStringAsync();

            this.password = fileContents;
            Console.WriteLine("Your response data is: " + this.password);
        }

        public RestClient()
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(URL);

            HttpResponseMessage response = client.GetAsync(urlParameters).Result;
            Console.WriteLine(response);

            if (response.IsSuccessStatusCode)
            {
                DownloadFileAsync(response);
            }
            else
            {
                Console.WriteLine("{0} ({1})", (int)response.StatusCode, response.ReasonPhrase);
            }


            //Make any other calls using HttpClient here.

            //Dispose once all HttpClient calls are complete. This is not necessary if the containing object will be disposed of; for example in this case the HttpClient instance will be disposed automatically when the application terminates so the following call is superfluous.
            client.Dispose();
        }
    }
}
