using EasyTime.Application.Contract.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EasyTime.EndpointApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BusinesController : ControllerBase
    {
        private readonly IBusinesService businesService;

        public BusinesController(IBusinesService businesService)
        {
            this.businesService = businesService;
        }

        [HttpGet]
        public IActionResult FilterBusinesByPlace(long businesCityId)
        {
            var result = businesService.FilterBusinesByPlace(businesCityId);
            return Ok(result);
        }
    }
}
