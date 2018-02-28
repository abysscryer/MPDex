using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MPDex.Models.Base;
using Newtonsoft.Json.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace MPDex.Web.Client.Controllers
{
    [Produces("application/json")]
    [Route("api/Book")]
    public class BookController : Controller
    {
        // GET: api/<controller>
        [HttpGet]
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
    }
}