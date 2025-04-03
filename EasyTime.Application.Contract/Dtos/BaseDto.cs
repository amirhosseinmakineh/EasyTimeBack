namespace EasyTime.Application.Contract.Dtos
{
   public class BaseDto<Tkey> where Tkey : struct
    {
        public Tkey Id { get; set; }
        public bool IsDelete { get; set; }
        public DateTime CreateObjectDate { get; set; }
        public DateTime UpdateObjectDate { get; set; }

    }
}
