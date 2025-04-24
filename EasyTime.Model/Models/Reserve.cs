namespace EasyTime.Model.Models
{
    public class Reserve: BaseEntity<long>
    {
        public Guid UserId { get; set; }
        public long BusinessOwnerDayId { get; set; }
        public long BusinessOwnerTimeId { get; set; }

        public BusinessOwnerDay BusinessOwnerDay { get; set; }
        public BusinessOwnerTime BusinessOwnerTime { get; set; }
    }
}
