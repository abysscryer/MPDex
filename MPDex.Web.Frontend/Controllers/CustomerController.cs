using AutoMapper;
using GenFu;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MPDex.Models.Domain;
using MPDex.Models.ViewModels;
using System;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MPDex.Web.Frontend.Controllers
{
    [Route("api/[controller]")]
    public class CustomerController : Controller
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IRepository<Customer> repository;
        private readonly ILogger<CustomerController> logger;

        public CustomerController(IUnitOfWork unitOfWork, ILogger<CustomerController> logger)
        {
            this.repository = unitOfWork.GetRepository<Customer>();
            this.unitOfWork = unitOfWork;
            this.logger = logger;
        }

        // GET: api/<controller>
        [HttpGet]
        public async Task<IActionResult> Get(int pageIndex=0, int pageSize=20)
        {
            var customers = await this.repository
                .GetPagedListAsync(include: i => i
                        .Include(c => c.Books)
                        .ThenInclude(b => b.Coin.Name),
                    pageIndex: pageIndex, pageSize: pageSize);

            return Ok(customers);
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var customer = await this.repository.FindAsync(id);
            return Ok(customer);
        }

        // POST api/<controller>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]CustomerCreateViewModel vm)
        {
            vm = A.New<CustomerCreateViewModel>();

            if (!ModelState.IsValid) 
                return BadRequest(ModelState);

            var customer = Mapper.Map<Customer>(vm);
            
            this.repository.Insert(customer);
            var effected = await this.unitOfWork.SaveChangesAsync();

            return Ok(effected == 1);
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(string id, [FromBody]CustomerCreateViewModel vm)
        {
            Guid guid;

            if (!Guid.TryParse(id, out guid))
                return BadRequest();

            vm = A.New<CustomerCreateViewModel>();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var customer = Mapper.Map<Customer>(vm);
            customer.Id = guid;

            this.repository.Update(customer);
            var effected = await this.unitOfWork.SaveChangesAsync();

            return Ok(effected == 1);
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            Guid guid;

            if (!Guid.TryParse(id, out guid))
                return BadRequest();

            this.repository.Delete(guid);
            var effected = await this.unitOfWork.SaveChangesAsync();
            return Ok(effected==1);
        }
    }
}
