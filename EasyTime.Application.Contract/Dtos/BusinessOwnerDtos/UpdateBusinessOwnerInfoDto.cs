namespace EasyTime.Application.Contract.Dtos.BusinessOwnerDtos
{
    public class UpdateBusinessOwnerInfoDto : BaseDto<Guid>
    {
        public string ImageName { get; set; } = string.Empty;
        public string Family { get; set; } = string.Empty;
        public int Age { get; set; }
    }
}
