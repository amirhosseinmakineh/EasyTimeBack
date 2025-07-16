using EasyTime.Application.Contract.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace EasyTime.EndpointApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService customerService;

        public CustomerController(ICustomerService customerService)
        {
            this.customerService = customerService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCustomers(Guid businessOwnerId)
        {
           var result = await customerService.GetAllCustomer(businessOwnerId);
            return Ok(result);
        }
    }
}
