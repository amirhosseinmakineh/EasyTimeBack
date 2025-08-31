using EasyTime.Application.Contract.Dtos.BusinesDto;
using EasyTime.Application.Contract.IServices;
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
        public async Task<IActionResult> FilterBusinesByPlace(long? businessCityId, long? regionId)
        {
            var result = await businesService.FilterBusinesByPlace(businessCityId, regionId);
            return Ok(result);
        }

        [HttpGet("Busineses")]
        public async Task<IActionResult> GetBusinesesByNeighberhood(long neighberhoodId,[FromQuery]List<long>? serviceIds, [FromQuery]int? categoryId, int skip = 0, int take = 6, decimal? maxAmount = 0)
        {
            var result = await businesService.FilterBusinesses(neighberhoodId, serviceIds, categoryId,skip, take,maxAmount);
            return Ok(result);
        }
        
        [HttpPost("Reserve")]
        public async Task<IActionResult> Reserve(ReserveDto dto)
        {
            var result = await businesService.Reserve(dto);
            return Ok(result);
        }

        [HttpGet("BusinessDetail")]
        public async Task<IActionResult> BusinessDetail(long businessId)
        {
            var result = await businesService.GetBusinessDetail(businessId);
            return Ok(result);
        }

        [HttpGet("GetBusinessCategories")]
        public async Task<IActionResult> GetBusinessCategories()
        {
            var result = await businesService.GetAllCategories();
            return Ok(result);
        }

        [HttpGet("GetServicesWithCategory")]
        public async Task<IActionResult> GetServicesWithCategory(int categoryId)
        {
            var result = await businesService.GetServicseWithCategory(categoryId);
            return Ok(result);
        }
        [HttpGet("GetSelectedPlaces")]
        public IActionResult GetSelectedPlaces(long cityId , long regionId , long neighberHoodId)
        {
            var result = businesService.GetSelectedPlace(cityId,regionId,neighberHoodId);
            return Ok(result);
        }
        [HttpGet("GetBusinessService")]
        public async Task<IActionResult> GetBusinessService()
        {
            var result = await businesService.GetAllServices();
            return Ok(result);
        }
        [HttpGet("GetMaxServiceAmount")]
        public async Task<IActionResult> GetMaxServiceAmount()
        {
            var result = await businesService.GetBusinessServicesAmount();
            return Ok(result);
        }
    }
}
