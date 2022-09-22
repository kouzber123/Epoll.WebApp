using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epoll.Core
{
    //interface we need for DbClient
    public interface IDbClient 
    {
        IMongoCollection<EpollModel> GetEpollCollection();
    }
}
