using MPDex.Domain;
using MPDex.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace MPDex.Api.Controllers
{
    [Route("api/[controller]")]
    public class TestsController : Controller
    {
        private readonly IRepository<Book> bookRepo;

        public TestsController(IRepository<Book> bookRepo)
        {
            this.bookRepo = bookRepo;
        }

        // GET api/values
        [HttpGet]
        public IEnumerable<Book> Get()
        {
            var result = bookRepo.GetAll();
            return result;
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(Guid id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]Book value)
        {
            bookRepo.Add(value);
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
