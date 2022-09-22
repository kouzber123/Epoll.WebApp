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




//array Object that stores options > need noSQL
/*
 [0] = [{id:1, title: "yes", "votes: 1",  
 [1] = [{id:1, title: "yes", "votes: 2",  
 [2] = [{id:1, title: "yes", "votes: 3",  
 */

/*
    "options": [
        {
            "id": 1,
            "title": "Yes",
            "votes": 0
        },
        {
            "id": 2,
            "title": "No",
            "votes": 1
        },
        {
            "id": 3,
            "title": "Cool, another option",
            "votes": 0
        }
 
 
 
 */