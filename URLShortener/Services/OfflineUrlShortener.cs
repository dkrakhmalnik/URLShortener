using HashidsNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using URLShortener.Models;

namespace URLShortener.Services
{
    public class OfflineUrlShortener : BaseUrlShortener
    {
        private const string BaseUrl = "https://shrt/";
        private IHashids _hashids;
        public OfflineUrlShortener(IRepository<UrlData> repository, IHashids hashids) : base(repository)
        {
            _hashids = hashids;
        }

        public override async Task<string> GetShortUrl(string fullUrl)
        {
            if (!Uri.IsWellFormedUriString(fullUrl, UriKind.Absolute))
                throw new Exception("Url is not well formed!");
            var urlData = await _repository.FirstOrDefault(u => u.FullUrl == fullUrl);
            if (urlData == null)
            {
                urlData = new UrlData()
                {
                    FullUrl = fullUrl
                };
                await _repository.Insert(urlData);
                urlData.ShortUrl = $"{BaseUrl}{_hashids.Encode(urlData.Id)}";
                await _repository.Update(urlData);
            }
            return urlData.ShortUrl;
        }

    }
}
