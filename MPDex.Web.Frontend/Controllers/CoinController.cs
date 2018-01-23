using AutoMapper;
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
        public async Task<IActionResult> Get(int pageIndex = 0, int pageSize = 20, int indexFrom=0, int itemCount = 0)
        {
            var page = await this.service.GetPagedListAsync(
                x => new CoinViewModel { Id =  x.Id, Name = x.Name, OnCreated = x.OnCreated },
                pageIndex:pageIndex, pageSize:pageSize, indexFrom:indexFrom, itemCount:itemCount);

            return Ok(page);
        }

        // GET api/<controller>/5
        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get(short id)
        {
            if(id == 0)
                return BadRequest(id);

            var coin = await this.service.FindAsync<CoinViewModel>(id);

            if (coin == null)
                return NotFound(id);

            return Ok(coin);
        }

        // POST api/<controller>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]CoinCreateModel cm)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var vm = await this.service.AddAsync(cm); 
            
            return Ok(vm);
        }
        
        // DELETE api/<controller>/5
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(short id)
        {
            if (id == 0)
                return BadRequest(id);

            var ok = await this.service.RemoveAsync(id);
            return Ok(ok); 
        }
    }
}