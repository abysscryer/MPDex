using Microsoft.AspNetCore.Mvc;
using MPDex.Models.ViewModels;
using MPDex.Services;
using System;
using System.Threading.Tasks;

namespace MPDex.Web.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/Customer")]
    public class CustomerController : Controller
    {
        private readonly ICustomerService service;

        public CustomerController(ICustomerService service)
        {
            this.service = service;
        }

        // GET: api/<controller>
        [HttpGet]
        public async Task<IActionResult> Get(int pageIndex = 0, int pageSize = 20, int indexFrom = 0, int itemCount = 0)
        {
            var result = await this.service.GetPagedListAsync(x => new CustomerViewModel
            {
                Id = x.Id,
                NickName = x.NickName,
                FamilyName = x.FamilyName,
                GivenName = x.GivenName,
                Email = x.Email,
                CellPhone = x.CellPhone
            }, pageIndex: pageIndex, pageSize: pageSize, indexFrom: indexFrom, itemCount: itemCount);

            return Ok(result);
        }

        // GET api/<controller>/5
        [HttpGet("{id:guid}")]
        public async Task<IActionResult> Get(Guid id)
        {
            if (id == Guid.Empty)
                return BadRequest(id);

            var customer = await this.service.FindAsync<CustomerViewModel>(id);

            if (customer == null)
                return NotFound(id);

            return Ok(customer);
        }

        // POST api/<controller>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]CustomerCreateModel cm)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var customer = await this.service.AddAsync(cm);

            return Ok(customer);
        }

        // PUT api/<controller>/5
        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Put(Guid id, [FromBody]CustomerUpdateModel um)
        {
            if (id == Guid.Empty)
                return BadRequest(id);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var vm = await this.service.UpdateAsync<CustomerUpdateModel, CustomerViewModel>(um, id);

            return Ok(vm);
        }
    }
}