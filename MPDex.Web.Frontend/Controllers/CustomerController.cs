using AutoMapper;
using GenFu;
using Microsoft.AspNetCore.Mvc;
using MPDex.Models.Domain;
using MPDex.Models.ViewModels;
using MPDex.Services;
using System;
using System.Threading.Tasks;

namespace MPDex.Web.Frontend.Controllers
{
    [Route("api/[controller]")]
    public class CustomerController : Controller
    {
        private readonly ICustomerService service;

        public CustomerController(ICustomerService service)
        {
            this.service = service;
        }

        // GET: api/<controller>
        [HttpGet]
        public async Task<IActionResult> Get(int pageIndex=0, int pageSize=20)
        {
            var customers = await this.service.GetAsync(pageIndex, pageSize);

            return Ok(customers);
        }

        // GET api/<controller>/5
        [HttpGet("{id:guid}")]
        public async Task<IActionResult> Get(Guid id)
        {
            if (id == Guid.Empty)
                return BadRequest(id);

            var customer = await this.service.FindAsync(id);

            if (customer == null)
                return NotFound(id);

            return Ok(customer);
        }

        // POST api/<controller>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]CustomerCreateModel vm)
        {
            vm = A.New<CustomerCreateModel>();

            if (!ModelState.IsValid) 
                return BadRequest(ModelState);

            var customer = Mapper.Map<Customer>(vm);
            var id = await this.service.AddAsync(customer);
            
            return Ok(id);
        }

        // PUT api/<controller>/5
        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Put(Guid id, [FromBody]CustomerCreateModel vm)
        {
            if (id == Guid.Empty)
                return BadRequest(id);

            vm = A.New<CustomerCreateModel>();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var customer = Mapper.Map<Customer>(vm);
            customer.Id = id;

            var isSuccess = await this.service.UpdateAsync(customer);

            return Ok(isSuccess);
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            if (id == Guid.Empty)
                return BadRequest(id);

            var isSuccess = await this.service.RemoveAsync(id);
            return Ok(isSuccess);
        }
    }
}
