using EasyTime.Application.Contract.Dtos.BusinesDto;
using EasyTime.Application.Contract.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

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

        [HttpGet("Cities")]
        public async Task<IActionResult> GetAllCities()
        {
            var result = await businesService.GetAllCitiesAsync();
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> FilterBusinesByPlace(long? businessCityId,long? regionId)
        {
            var result = await businesService.FilterBusinesByPlace(businessCityId,regionId);
            return Ok(result);
        }

        [HttpGet("Bisineses")]
        public IActionResult GetBusinesesByNeighberhood(long neighberhoodId)
        {
             var result = businesService.FilterBusines(neighberhoodId);
            return Ok(result);
        } 

        [HttpPost("Reserve")]
        public async Task<IActionResult> Reserve(ReserveDto dto)
        {
          var result =  await businesService.Reserve(dto);
            return Ok(result);
        }

        [HttpGet("BusinessDetail")]
        public async Task<IActionResult> BusinessDetail(long businessId)
        {
            var result = await businesService.GetBusinessDetail(businessId);
            return Ok(result);
        }
    }
}
