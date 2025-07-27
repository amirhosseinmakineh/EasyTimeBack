using EasyTime.Application.Contract.IServices;
using Microsoft.AspNetCore.Mvc;

namespace EasyTime.EndpointApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlanController : ControllerBase
    {
        private readonly IPlanService planService;

        public PlanController(IPlanService planService)
        {
            this.planService = planService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllPlans()
        {
            var result = await planService.GetAllPlans();
            return Ok(result);
        }
    }
}
