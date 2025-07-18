using EasyTime.Model.Models;

public class BusinessOwnerTime : BaseEntity<long>
{
    public long BusinessOwnerDayId { get; set; }
    public long BusinessId { get; set; }
    public TimeSpan From { get; set; }
    public TimeSpan To { get; set; }
    public bool IsReserved { get; set; } = false;
    public BusinessOwnerDay BusinessOwnerDay { get; set; }
    public Business Business { get; set; }
    public ICollection<Reserve> Reserves { get; set; }
    public ICollection<BusinessTime> BusinessTimes { get; set; }
}
