using AutoMapper;
using GenFu;
using Microsoft.AspNetCore.Mvc;
using MPDex.Models.Domain;
using MPDex.Models.ViewModels;
using MPDex.Services;
using System.Threading.Tasks;

namespace MPDex.Web.Frontend.Controllers
{
    [Produces("application/json")]
    [Route("api/Coin")]
    public class CoinController : Controller
    {
        private readonly ICoinService service;

        public CoinController(ICoinService service)
        {
            this.service = service;
        }

        // GET: api/<controller>
        [HttpGet]
        public async Task<IActionResult> Get(int pageIndex = 1, int pageSize = 20)
        {
            var coins = await this.service.GetAsync(pageIndex, pageSize);
 
            return Ok(coins);
        }

        // GET api/<controller>/5
        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get(short id)
        {
            var coin = await this.service.FindAsync(id);
            return Ok(coin);
        }

        // POST api/<controller>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]CoinCreateModel vm)
        {
            vm = A.New<CoinCreateModel>();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var coin = Mapper.Map<Coin>(vm);
            var id = await this.service.AddAsync(coin);
            
            return Ok(id);
        }

        // PUT api/<controller>/5
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Put(short id, [FromBody]CoinCreateModel vm)
        {
            if (id == 0)
                return NotFound(id);

            vm = A.New<CoinCreateModel>();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var coin = Mapper.Map<Coin>(vm);
            coin.Id = id;

            var isSuccess = await this.service.UpdateAsync(coin);
            
            return Ok(isSuccess);
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(short id)
        {
            if (id == 0)
                return NotFound();

            var isSuccess = await this.service.RemoveAsync(id);
            return Ok(isSuccess); 
        }
    }
}