using Microsoft.AspNetCore.Mvc;
using MPDex.Models.ViewModels;
using MPDex.Services;
using System.Threading.Tasks;

namespace MPDex.Web.Frontend.Controllers
{
    [Produces("application/json")]
    [Route("api/Book")]
    public class BookController : Controller
    {
        private readonly IBookService service;

        public BookController(IBookService service)
        {
            this.service = service;
        }

        // GET: api/Book
        [HttpGet]
        public async Task<IActionResult> Get(int pageIndex = 0, int pageSize = 20, int indexFrom = 0, int itemCount = 0)
        {
            var page = await this.service.GetPagedListAsync(
                x => new BookViewModel {
                    OrderType = x.OrderType,
                    Price = x.Price,
                    Amount = x.Amount,
                    Stock = x.Stock,
                    CoinName = x.Coin.Name,
                    NickName = x.Customer.NickName },
                pageIndex: pageIndex, pageSize: pageSize, indexFrom: indexFrom, itemCount: itemCount);

            return Ok(page);
        }

        // GET: api/Book/5
        [HttpGet("{id:long}", Name = "Get")]
        public async Task<IActionResult> Get(long id)
        {
            if (id == 0)
                return BadRequest(id);

            var coin = await this.service.FindAsync(id);

            if (coin == null)
                return NotFound(id);

            return Ok(coin);
        }
        
        // POST: api/Book
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]BookCreateModel cm)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var vm = await this.service.AddAsync(cm);

            return Ok(vm);
        }
        
        // PUT: api/Book/5
        [HttpPut("{id:long}")]
        public async Task<IActionResult> Put(long id, [FromBody]BookUpdateModel um)
        {
            if (id == 0)
                return BadRequest(id);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var vm = await this.service.UpdateAsync(um, id);

            return Ok(vm);
        }
        
        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id:long}")]
        public async Task<IActionResult> Delete(long id)
        {
            if (id == 0)
                return BadRequest(id);

            var ok = await this.service.RemoveAsync(id);
            return Ok(ok);
        }
    }
}
