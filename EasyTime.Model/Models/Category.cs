namespace EasyTime.Model.Models
{
    public class Category : BaseEntity<int>
    {
        public string Name { get; set; } = string.Empty;
        #region Relations 
        public ICollection<Business> Businesses { get; set; }
        public ICollection<Service> Services { get; set; }
        #endregion
    }
}
