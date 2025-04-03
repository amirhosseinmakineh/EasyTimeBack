namespace EasyTime.Model.Models
{
    public class BaseEntity<Tkey> where Tkey : struct
    {
        public Tkey Id { get; set; }
        public bool IsDelete { get; set; }
        public DateTime CreateObjectDate { get; set; }
        public DateTime UpdateEntityDate { get; set; }

    }
}
