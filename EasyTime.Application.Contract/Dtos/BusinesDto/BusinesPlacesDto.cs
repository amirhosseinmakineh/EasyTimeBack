using EasyTime.Model.Models;

namespace EasyTime.Application.Contract.Dtos.BusinesDto
{
    public class BusinessDto:BaseDto<long>
    {
        public string BusinessLogo { get; set; } = string.Empty;
        public string BusinessName { get; set; } = string.Empty;
        public string BusinessOwnerName { get; set; } = string.Empty;
        public string Description { get; set; }
    }
}
