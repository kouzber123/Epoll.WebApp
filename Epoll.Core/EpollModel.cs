using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;
using System;

namespace Epoll.Core
{
    //model for our database what is a poll
    public class EpollModel
    {
        //get MONGODB
        [BsonId] //mongodb id = primarykey
        //assets key
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]

        public string Id { get; set; }
        public string Title { get; set; }

        public List<OptionModel> Options { set; get; }
    }
    public class OptionModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int Counts { get; set; }
    }




}



