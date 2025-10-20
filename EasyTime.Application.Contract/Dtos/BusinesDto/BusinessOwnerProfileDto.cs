using EasyTime.Application.Contract.Dtos.Achevment;
using EasyTime.Application.Contract.Dtos.Comments;

namespace EasyTime.Application.Contract.Dtos.BusinesDto
{
    public record BusinessOwnerProfileDto
    {
        public Guid Id  { get; set; }
        public string ImageName { get; set; } = string.Empty;
        public string AboutMe { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Family { get; set; } = string.Empty;
        public string? Banner { get; set; } = string.Empty;
        public string? WorkHistory { get; set; } = string.Empty;
        public List<BusinessDayTimeDto> TakingTurns { get; set; }
        public BusinessDto BusinessInfo { get; set; }
        public  List<AchievementDto> Achievements { get; set; }
        public List<CommentDto> Comments { get; set; }
    }
}
