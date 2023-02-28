using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using URLShortener.Models;

namespace URLShortener.Services
{
    public class CuttlyUrlShortener : BaseUrlShortener
    {
        private const string ApiKey = "3b9f7920b637c0920393ed17116cc60a07b3c";
        private const string ApiUrl = "https://cutt.ly/api/api.php";

        public CuttlyUrlShortener(IRepository<UrlData> repository) : base(repository)
        {
        }

        public override async Task<string> GetShortUrl(string fullUrl)
        {
            var urlData = await _repository.FirstOrDefault(u => u.FullUrl == fullUrl);
            if (urlData == null)
            {

                using var client = GetClient();
                var result = await client.GetStringAsync($"?key={ApiKey}&short={fullUrl}");
                dynamic response = JObject.Parse(result);
                if (response.url.status != 7)
                    throw new Exception("Error shortening the link!");
                urlData = new UrlData
                {
                    FullUrl = fullUrl,
                    ShortUrl = response.url.shortLink
                };
                await _repository.Insert(urlData);
            }
            return urlData.ShortUrl;
        }

        private HttpClient GetClient()
        {
            var client = new HttpClient()
            {
                BaseAddress = new Uri(ApiUrl)
            };
            client.DefaultRequestHeaders.Add("Accept", "application/json");
            return client;
        }

    }
}
