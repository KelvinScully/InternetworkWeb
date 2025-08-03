using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Services
{
    internal class RepositoryBase(ConnectionOptions connectionOptions)
    {
        protected readonly string _baseUrl = connectionOptions.ConnectionString;
    }
}
