using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace gcpe_azure_aks_sample.Controllers
{
    [Route("api/[controller]")]
    public class QuotesController : Controller
    {
        private static readonly string[] ListOfQuotes;

        static QuotesController()
        {
            ListOfQuotes = JsonConvert.DeserializeObject<QuoteList>(System.IO.File.ReadAllText("quotes.json")).Quotes;
        }

        [HttpGet]
        public IActionResult Get() => Ok(ListOfQuotes[new Random().Next(ListOfQuotes.Length)]);

        [HttpGet("{count}")]
        public IActionResult GetMany(int count)
        {
            if(count < 0 || count > ListOfQuotes.Length)
                return BadRequest($"number of quotes must be between 0 and {ListOfQuotes.Length}");
        
            var r = new Random();
            return Ok(ListOfQuotes.OrderBy(_ => r.Next(ListOfQuotes.Length)).Take(count).Select(x => $"{x}-{Environment.MachineName}"));;
        }
        
        private class QuoteList
        {
            public string[] Quotes { get; set; }
        }
    }
}