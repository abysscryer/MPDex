using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MPDex.Models.ViewModels;
using MPDex.Services;

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
            var result = await this.service.GetPagedListAsync(
                x => new BookViewModel { BookType = x.BookType, Price = x.Price, Amount = x.Amount, Stock = x.Stock, CoinName = x.Coin.Name, NickName = x.Customer.NickName },
                pageIndex: pageIndex, pageSize: pageSize, indexFrom: indexFrom, itemCount: itemCount);

            return Ok(result);
        }

        // GET: api/Book/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return "value";
        }
        
        // POST: api/Book
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }
        
        // PUT: api/Book/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }
        
        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
