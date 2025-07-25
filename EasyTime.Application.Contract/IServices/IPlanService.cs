using EasyTime.Application.Contract.Dtos.PlansDto;

namespace EasyTime.Application.Contract.IServices
{
    public interface IPlanService
    {
        Task<List<PlanDto>> GetAllPlans();
    }
}
