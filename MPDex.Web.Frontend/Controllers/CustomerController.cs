using AutoMapper;
using GenFu;
using Microsoft.AspNetCore.Mvc;
using MPDex.Models.Domain;
using MPDex.Models.ViewModels;
using MPDex.Services;
using System;
using System.Linq;
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
        public async Task<IActionResult> Get(int pageIndex=0, int pageSize=20, int indexFrom = 0, int itemCount=0)
        {
            var result = await this.service.GetPagedListAsync(x => new CustomerViewModel {
                    NickName = x.NickName,
                    FamilyName = x.FamilyName,
                    GivenName = x.GivenName,
                    Email = x.Email },
                pageIndex:pageIndex, pageSize:pageSize, indexFrom:indexFrom, itemCount:itemCount);

            return Ok(result);
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
            if (!ModelState.IsValid) 
                return BadRequest(ModelState);

            var customer = await this.service.AddAsync(vm);
            
            return Ok(customer);
        }

        // PUT api/<controller>/5
        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Put(Guid id, [FromBody]CustomerUpdateModel vm)
        {
            if (id == Guid.Empty)
                return BadRequest(id);
            
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var isSuccess = await this.service.UpdateAsync(vm, id);

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
