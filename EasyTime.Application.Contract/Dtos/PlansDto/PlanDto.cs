namespace EasyTime.Application.Contract.Dtos.PlansDto
{
    public class PlanDto
    {
        public long PlanId { get; set; }
        public string Name { get; set; } = string.Empty;
        public float BasePrice { get; set; }
        public List<PlanInformationDto> PlansServices { get; set; }
        public string PlanTimeName { get; set; } = string.Empty;
        public long PlanTimeId { get; set; }
        public float AmountAdded { get; set; }
        public float FinalPrice { get; set; }



    }
}
