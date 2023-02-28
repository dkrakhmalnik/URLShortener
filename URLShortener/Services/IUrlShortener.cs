using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using URLShortener.Models;

namespace URLShortener.Services
{
    public interface IUrlShortener
    {
        Task<string> GetShortUrl(string fullUrl);
        Task<string> GetFullUrl(string shortUrl);
        Task<IEnumerable<UrlData>> GetUrls();
    }
}
