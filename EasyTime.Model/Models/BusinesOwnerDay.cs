using EasyTime.Model.Models;

public class BusinessOwnerDay : BaseEntity<long>
{
    public Guid BusinessOwnerId { get; set; }
    public DayOfWeek DayOfWeek { get; set; }
    public long BusinessId { get; set; }
    public Business Business { get; set; }
    public ICollection<BusinessOwnerTime> BusinessOwnerTimes { get; set; }
    public ICollection<Reserve> Reserves { get; set; }
}
