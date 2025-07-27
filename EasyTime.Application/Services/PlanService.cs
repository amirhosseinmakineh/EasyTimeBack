using EasyTime.Application.Contract.Dtos.PlansDto;
using EasyTime.Application.Contract.IServices;
using EasyTime.Model.IRepository;
using EasyTime.Model.Models;
using EasyTime.Utilities.Convertor;
using System.Collections.Generic;

namespace EasyTime.Application.Services
{
    public class PlanService : IPlanService
    {
        private readonly IBaseRepository<long, PlansInformation> planInforReepository;
        private readonly IBaseRepository<long, Plan> planRepository;
        private readonly IBaseRepository<long, PlanTime> planTimeRepository;
        private readonly IBaseRepository<long, BusinessOwnerPlan> businessOwnerPlan;

        public PlanService(IBaseRepository<long, PlansInformation> planInforReepository, IBaseRepository<long, Plan> planRepository, IBaseRepository<long, PlanTime> planTimeRepository, IBaseRepository<long, BusinessOwnerPlan> businessOwnerPlan)
        {
            this.planInforReepository = planInforReepository;
            this.planRepository = planRepository;
            this.planTimeRepository = planTimeRepository;
            this.businessOwnerPlan = businessOwnerPlan;
        }

        public async Task<Result<List<PlanDto>>> GetAllPlans()
        {
            var plans = await planRepository.GetAllEntities();
            var plansInfos = await planInforReepository.GetAllEntities();
            var planTimes = await planTimeRepository.GetAllEntities();
            var businessOwnerPlans = await businessOwnerPlan.GetAllEntities();

            var result = (from p in plans
                          join pl in planTimes
                                on p.Id equals pl.PlanId
                          select new PlanDto
                          {
                              PlanId = p.Id,
                              Name = p.Name,
                              BasePrice = p.BasePrice,
                              PlansServices = (from pi in plansInfos
                                                    where pi.PlanId == p.Id
                                                    select new PlanInformationDto()
                                               {
                                                   PlanId = pi.PlanId,
                                                   Name = pi.PlanService
                                               }).ToList(),
                              FinalPrice = p.BasePrice + pl.AmountAdded,
                              PlanTimeId = pl.Id,
                              AmountAdded = pl.AmountAdded,
                              PlanTimeName = pl.PlanName,
                          }).ToList();

            return Result<List<PlanDto>>.Success(result);
        }
    }
}
