using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MPDex.Web.Client.Controllers
{
    [Route("api/[controller]")]
    public class CoinController : Controller
    {
        // GET: api/<controller>
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Get()
        {
            var accessToken = await HttpContext.GetTokenAsync("access_token");
            var content = string.Empty;

            using (var client = new HttpClient())
            {
                client.SetBearerToken(accessToken);
                try
                {
                    content = await client.GetStringAsync($"http://localhost:5001/api/coin/all");
                }
                catch (System.Exception ex)
                {

                    throw;
                }

            }
            
            return Ok(JArray.Parse(content));
        }
    }
}
