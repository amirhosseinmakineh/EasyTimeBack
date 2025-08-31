using EasyTime.Application.Contract.Dtos.Achevment;
using EasyTime.Application.Contract.Dtos.Comments;
using EasyTime.Application.Contract.Dtos.Service;

namespace EasyTime.Application.Contract.Dtos.BusinesDto
{
    public class BusinessDetailDto
    {
        public long BusinessId { get; set; }
        public Guid UserId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Logo { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public long CityId { get; set; }
        public long RegionId { get; set; }
        public long NeighberhoodId { get; set; }
        public string CityName { get; set; } = string.Empty;
        public string RegionName { get; set; } = string.Empty;
        public string NeighberhoodName { get; set; } = string.Empty;
        public  List<BusinessOwnerDayDto> BusinessOwnerDayDtos { get; set; }
        public string? BannerName { get; set; }
        public ICollection<BusinessServiceDto> BusinessServiceDtos { get; set; }
        public ICollection<CommentDto> CommentDtos { get; set; }
        public ICollection<AchievementDto> AchievementDtos { get; set; }
    }
}
