namespace EasyTime.Model.Models
{
    public class Plan : BaseEntity<long>
    {
        public string Name { get; set; } = string.Empty;
        public float BasePrice { get; set; }

        #region Relation
        public ICollection<PlansInformation> PlansInformation { get; set; }
        #endregion
    }
}
