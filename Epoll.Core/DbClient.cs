using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epoll.Core
{
    public class DbClient : IDbClient
    {
        private readonly IMongoCollection<EpollModel> _epoll;
        //constructor to inject options and inject EpollDbConfig and name it same
        public DbClient(IOptions<EpollDbConfig> epollDbConfig)
        {
            //get our db and collection 
            var client = new MongoClient(epollDbConfig.Value.Connection_String);
            //get dabase we get from mongodb
            var database = client.GetDatabase(epollDbConfig.Value.Database_Name);

            _epoll = database.GetCollection<EpollModel>(epollDbConfig.Value.Epoll_Collection_Name);


        }


        //this from IDbclient interface
        public IMongoCollection<EpollModel> GetEpollCollection() => _epoll; //implement the code by returning
    }
}

