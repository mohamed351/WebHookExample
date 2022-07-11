using AirlineSendAgent.App;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace AirlineSendAgent.Client
{
    internal class WebHookClient : IWebHookClient
    {
        private readonly IHttpClientFactory httpClientFactory;

        public WebHookClient(IHttpClientFactory httpClientFactory)
        {
            this.httpClientFactory = httpClientFactory;
        }
        public async Task SendWebHookNotification(FlightDetailUpdateDTO flightDetailUpdate)
        {
            var serlizedPayload = JsonSerializer.Serialize(flightDetailUpdate);
            var httpClient = httpClientFactory.CreateClient();

            var request = new HttpRequestMessage(HttpMethod.Post, flightDetailUpdate.Url);
            request.Headers.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            request.Content = new StringContent(serlizedPayload);
            request.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
            try
            {
                using(var response = await httpClient.SendAsync(request))
                {
                    Console.WriteLine("success");
                    response.EnsureSuccessStatusCode();
                }
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
            }
        }
    }
}
