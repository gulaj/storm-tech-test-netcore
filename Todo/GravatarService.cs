using System.Net.Http;
using System.Threading.Tasks;
using System;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using System.Linq;

namespace Todo
{
    public class GravatarService
    {
        public HttpClient Client { get; }

        public GravatarService(HttpClient client)
        {
            var productValue = new ProductInfoHeaderValue("GravatarBot", "1.0");
            client.BaseAddress = new Uri("https://www.gravatar.com/");
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.Timeout = TimeSpan.FromSeconds(10);
            client.DefaultRequestHeaders.UserAgent.Add(productValue);


            Client = client;
        }

        public async Task<string> GetDisplayName(string  hash)
        {
            try
            {
                HttpResponseMessage response = await Client.GetAsync($"/{hash}.json");
                if (!response.IsSuccessStatusCode) return null;

                string responseJson = await response.Content.ReadAsStringAsync();
                var gravatar = JsonConvert.DeserializeObject<GravatarResponse>(responseJson);
                var displayName = gravatar.Entry.FirstOrDefault()?.DisplayName;
                return string.IsNullOrWhiteSpace(displayName) ? string.Empty : $"({displayName})";
            }
            catch (TaskCanceledException ex)
            {
                return string.Empty;
            }
        }
    }
}

