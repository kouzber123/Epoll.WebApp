using Epoll.Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Epoll.WebApp.Controllers
{
    //controller handles the endpoints
    [ApiController]
    [Route("[controller]")]
    public class PollsController : ControllerBase
    {
        private readonly IEpollServices _epollServices;
        //injection
        public PollsController(IEpollServices epollServices)
        {
            _epollServices = epollServices;
        }

        //requests

        //all polls in our database
        [HttpGet]
        public IActionResult GetEpolls()
        {
            return Ok(_epollServices.GetEpolls());
        }
        //get by id
        [HttpGet("{id}", Name = "GetEpoll")]
        public IActionResult GetEpoll(string id)
        {
            return Ok(_epollServices.GetEpoll(id));
        }

        //create new poll
        
        [HttpPost("add/")]
        public IActionResult AddEpoll(EpollModel epollModel)
        {
            //return 
            _epollServices.AddEpoll(epollModel);
            //201 resp and return id matching the poll
            return CreatedAtRoute("GetEpoll", new { id = epollModel.Id }, epollModel);
        }

        [HttpPut("id/vote/option/")]
        public IActionResult UpdateEpoll(EpollModel epollModel)
        {
            return Ok(_epollServices.UpdateEpoll(epollModel));
        }

   
    }
}//go to IEPOLLServices
