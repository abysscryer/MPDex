using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using MPDex.Models.Base;
using MPDex.Models.ViewModels;
using MPDex.Services;
using MPDex.Web.Frontend.Hubs;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace MPDex.Web.Frontend.Controllers
{
    [Produces("application/json")]
    [Route("api/Book")]
    public class BookController : Controller
    {
        private readonly IBookService service;
        private readonly IHubContext<BookHub> bookHub;
        private readonly IBookCache bookCache;

        public BookController(IBookService service, IBookCache bookCache, IHubContext<BookHub> bookHub)
        {
            this.service = service;
            this.service.BookChanged += OnBookChanged;
            this.bookCache = bookCache;
            this.bookHub = bookHub;
        }

        /// <summary>
        /// Book changed callback
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnBookChanged(object sender, BookChangedEventArgs e)
        {
            // cache update
            if (e.IsIncrease)
            {
                bookCache.IncreaseAsync(e.Book).Wait();
            }
            else
            {
                bookCache.DecreaseAsync(e.Book).Wait();
            }

            // hub call
            var sm = Mapper.Map<BookSummaryModel>(e.Book);
            this.bookHub.Clients.All.InvokeAsync("updateBook", sm).Wait();
        }
        
        // GET: api/Book
        [HttpGet]
        public async Task<IActionResult> Get(int pageIndex = 0, int pageSize = 20, int indexFrom = 0, int itemCount = 0)
        {
            var page = await this.service.GetPagedListAsync(pageIndex, pageSize, indexFrom, itemCount);

            return Ok(page);
        }

        [HttpGet]
        [Route("Summary")]
        public async Task<IActionResult> Summary(short currencyId = 1, short coinId = 2, BookType bookType =BookType.Buy)
        {
            //var summary = await this.service.SumAsync(currencyId, coinId, bookType);
            var summary = await this.bookCache.GetAsync(bookType, currencyId, coinId, 10);
            
            return Ok(summary);
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

            cm.IPAddress = HttpContext.Connection.RemoteIpAddress.ToString();

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
