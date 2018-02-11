using Ghost.AI.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Ghost.AI.API.Controllers
{
    public class NextMoveController : ApiController
    {
        // GET: api/NextMove/salt
        public GhostGameMove Get(string word)
        {
            return new GhostGameMove()
            {
                Word = word,
                NextWord = word + "j",
                Finished = false,
                Winner = 0
            };
        }
    }
}
