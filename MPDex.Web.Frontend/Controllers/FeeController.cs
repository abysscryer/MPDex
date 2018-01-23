using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MPDex.Models.ViewModels;
using MPDex.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MPDex.Web.Frontend.Controllers
{
    [Route("api/[controller]")]
    public class FeeController : Controller
    {
        private readonly IFeeService service;

        public FeeController(IFeeService service)
        {
            this.service = service;
        }

        // GET: api/<controller>
        [HttpGet]
        public async Task<IActionResult> Get(int pageIndex = 0, int pageSize = 20, int indexFrom = 0, int itemCount = 0)
        {
            var page = await this.service.GetPagedListAsync(
                x => new FeeViewModel
                {
                    Id = x.Id, CoinName = x.Coin.Name,
                    Percent = x.Percent,
                    OnCreated = x.OnCreated
                }, pageIndex: pageIndex, pageSize: pageSize, indexFrom: indexFrom, itemCount: itemCount);

            return Ok(page);
        }

        // GET api/<controller>/5
        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get(short id)
        {
            if (id == 0)
                return BadRequest(id);

            var fee = await this.service.FindAsync<FeeViewModel>(id);

            if (fee == null)
                return NotFound(id);

            return Ok(fee);
        }

        // POST api/<controller>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]FeeCreateModel cm)
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
