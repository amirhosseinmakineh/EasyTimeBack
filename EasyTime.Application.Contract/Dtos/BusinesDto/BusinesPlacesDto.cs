using EasyTime.Application.Contract.Dtos.Comments;
using EasyTime.Application.Contract.Dtos.Service;
using EasyTime.Model.Enums;
using EasyTime.Model.Models;

namespace EasyTime.Application.Contract.Dtos.BusinesDto
{
    public class BusinessDto:BaseDto<long>
    {
        public string BusinessLogo { get; set; } = string.Empty;
        public string BusinessName { get; set; } = string.Empty;
        public string BusinessOwnerName { get; set; } = string.Empty;
        public string Description { get; set; }
        public List<string> AchevmentNames { get; set; }
        public decimal Amount { get; set; }
        public float UserRate { get; set; }
        public List<BusinessDayTimeDto> BusinessDayTimeDtos { get; set; }
        public List<ServiceDto> ServiceDtos { get; set; }
        public List<CommentDto> Comments { get; set; }
    }

    public class BusinessDayTimeDto
    {
        public long BusinessOwnerDayId { get; set; }
        public long? BusinessOwnerTimeId { get; set; }   // nullable
        public DayOfWeek DayOfWeek { get; set; }
        public TimeSpan? From { get; set; }             // nullable
        public TimeSpan? To { get; set; }               // nullable
        public bool? IsReserved { get; set; }           // nullable
    }

}
