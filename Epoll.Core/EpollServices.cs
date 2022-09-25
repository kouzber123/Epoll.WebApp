using MongoDB.Driver;
using System;
using System.Collections.Generic;

namespace Epoll.Core
{
    public class EpollServices : IEpollServices //implement interfaces 
    {
        //this collection
        private readonly IMongoCollection<EpollModel> _epollModel;
        public EpollServices(IDbClient dbClient)
        {
          _epollModel =  dbClient.GetEpollCollection();
        }

        //get by id and return by id      form mongocollection find id that matches the passed id
        public EpollModel GetEpoll(string id) => _epollModel.Find(epollmodel => epollmodel.Id == id).First();

        //gets all
        public List<EpollModel> GetEpolls() => _epollModel.Find(epollModel => true).ToList();

        //create
        public EpollModel AddEpoll(EpollModel epollModel)
        {
            _epollModel.InsertOne(epollModel);
            return epollModel;
        }

        public EpollModel UpdateEpoll(EpollModel epollModel)
        {
           //call getpoll to pass id  and if its exists then replace the one
           GetEpoll(epollModel.Id);
            _epollModel.ReplaceOne(e => e.Id == epollModel.Id, epollModel);
            return epollModel;
        }
    }
}
//Go to EpollController