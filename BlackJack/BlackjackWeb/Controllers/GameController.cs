using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;

namespace BlackjackWeb.Controllers
{
    [Route("[controller]")]
    public class GameController : Controller
    {
       [HttpGet()]
       public string Get()
       {
            Response.Headers.Add("Content-Type", "application/json");
            return "{'var':'hello'}";
       }
    }
}
