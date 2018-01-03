using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MPDex.Models;
using System;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MPDex.Web.Frontend.Controllers
{
    [Route("api/[controller]")]
    public class CustomerController : Controller
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IRepository<Customer> repo;
        private readonly ILogger<CustomerController> logger;

        public CustomerController(IUnitOfWork unitOfWork, ILogger<CustomerController> logger)
        {
            this.repo = unitOfWork.GetRepository<Customer>();
            this.unitOfWork = unitOfWork;
            this.logger = logger;
            
            var customer = new Customer();
            repo.Insert(customer);
        }

        // GET: api/<controller>
        [HttpGet]
        public async Task<IPagedList<Customer>> Get(int pageIndex=1, int pageSize=20)
        {
            return await this.unitOfWork
                .GetRepository<Customer>()
                .GetPagedListAsync(include: source => source
                        .Include(customer => customer.Books)
                        .ThenInclude(book => book.Coin),
                    pageIndex: pageIndex, pageSize: pageSize);
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        public async Task<Customer> Get(Guid id)
        {
            return await this.repo.FindAsync(id);
        }

        // POST api/<controller>
        [HttpPost]
        public async Task<int> Post([FromBody]Customer value)
        {
            repo.Insert(value);
            return await this.unitOfWork.SaveChangesAsync();
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public async Task<int> Put(Guid id, [FromBody]Customer value)
        {
            this.repo.Update(value);
            return await this.unitOfWork.SaveChangesAsync();
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public async Task<int> Delete(Guid id)
        {
            var customer = await this.repo.FindAsync(id);
            this.repo.Delete(customer);
            return await this.unitOfWork.SaveChangesAsync();
        }
    }
}
