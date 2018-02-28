using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace MPDex.Web.Client.Controllers
{
    [Produces("application/json")]
    [Route("api/Coin")]
    public class CoinController : Controller
    {
        // GET: api/<controller>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var accessToken = await HttpContext.GetTokenAsync("access_token");
            var content = string.Empty;

            using (var client = new HttpClient())
            {
                client.SetBearerToken(accessToken);
                try
                {
                    content = await client.GetStringAsync($"http://localhost:5001/coin/all");
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