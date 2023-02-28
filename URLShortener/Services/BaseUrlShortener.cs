using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using URLShortener.Models;

namespace URLShortener.Services
{
    public abstract class BaseUrlShortener : IUrlShortener
    {
        protected IRepository<UrlData> _repository;

        public BaseUrlShortener(IRepository<UrlData> repository)
        {
            _repository = repository;
        }

        public async Task<string> GetFullUrl(string shortUrl)
        {
            return (await _repository.FirstOrDefault(u => u.ShortUrl == shortUrl))?.FullUrl ?? String.Empty;
        }

        public abstract Task<string> GetShortUrl(string fullUrl);

        public async Task<IEnumerable<UrlData>> GetUrls()
        {
            return await _repository.GetAll();
        }
    }
}
