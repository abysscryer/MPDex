using Microsoft.AspNetCore.Mvc;
using MPDex.Models.ViewModels;
using MPDex.Services;
using System;
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
                    Id = x.Id,
                    CustomerId = x.Customer.Id,
                    NickName = x.Customer.NickName,
                    CoinId = x.Coin.Id,
                    CoinName = x.Coin.Name,
                    BookType = x.BookType,
                    BookStatus = x.BookStatus,
                    Price = x.Price,
                    Amount = x.Amount,
                    Stock = x.Stock,
                    OrderCount = x.OrderCount,
                    OnCreated = x.OnCreated,
                    OnUpdated = x.OnUpdated,
                    IPAddress = x.IPAddress,
                    RowVersion = x.RowVersion
                }, pageIndex: pageIndex, pageSize: pageSize, indexFrom: indexFrom, itemCount: itemCount);

            return Ok(page);
        }

        // GET: api/Book/5
        [HttpGet("{id:guid}", Name = "Get")]
        public async Task<IActionResult> Get(Guid id)
        {
            if (id == Guid.Empty)
                return BadRequest(id);

            var coin = await this.service.FindAsync<BookViewModel>(id);

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
        [Route("order")]
        public async Task<IActionResult> Put(long id, [FromBody]BookOrderModel um)
        {
            if (id == 0)
                return BadRequest(id);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var vm = await this.service.UpdateAsync<BookOrderModel, BookViewModel>(um, id);

            return Ok(vm);
        }

        // PUT: api/Book/5
        [HttpPut("{id:long}")]
        [Route("status")]
        public async Task<IActionResult> ChangeStatus(long id, [FromBody]BookStatusModel um)
        {
            if (id == 0)
                return BadRequest(id);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var vm = await this.service.UpdateAsync<BookStatusModel, BookViewModel>(um, id);

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
