using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Services
{
    internal class RepositoryBase
    {
        protected readonly HttpClient _httpClient;
        protected readonly string _baseUrl;

        public RepositoryBase(ConnectionOptions connectionOptions)
        {
            _baseUrl = connectionOptions.ConnectionString;
            _httpClient = new HttpClient
            {
                BaseAddress = new Uri(_baseUrl)
            };
        }
    }
}
