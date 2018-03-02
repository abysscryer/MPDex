using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MPDex.Models.Base;
using Newtonsoft.Json.Linq;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MPDex.Web.Client.Controllers
{
    [Route("api/[controller]")]
    public class BookController : Controller
    {
        // GET: api/<controller>
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Get(short currencyId = 1, short coinId = 2, BookType bookType = BookType.Buy)
        {
            var accessToken = await HttpContext.GetTokenAsync("access_token");
            var content = string.Empty;

            using (var client = new HttpClient())
            {
                client.SetBearerToken(accessToken);

                try
                {
                    content = await client.GetStringAsync($"http://localhost:5001/api/book/summary?booktype={bookType}&currencyId={currencyId}&coinId={coinId}");
                }
                catch (System.Exception ex)
                {

                    throw;
                }

            }
            //var result = JsonConvert.DeserializeObject(content);
            return Ok(JArray.Parse(content));
        }

        // POST api/<controller>
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }
    }
}
