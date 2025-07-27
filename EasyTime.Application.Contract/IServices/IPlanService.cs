using EasyTime.Application.Contract.Dtos.PlansDto;
using EasyTime.Utilities.Convertor;

namespace EasyTime.Application.Contract.IServices
{
    public interface IPlanService
    {
        Task<Result<List<PlanDto>>> GetAllPlans();
    }
}
