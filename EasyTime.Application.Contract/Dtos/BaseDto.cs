namespace EasyTime.Application.Contract.Dtos
{
    public class BaseDto<Tkey> : IBaseDto<Tkey> where Tkey : struct
    {
        public Tkey Id { get; set; }
        public bool IsDelete { get; set; }
        public DateTime CreateObjectDate { get; set; }
        public DateTime UpdateObjectDate { get; set; }
    }

    public class BaseDto : IBaseDto
    {
        public bool IsDelete { get; set; }
        public DateTime CreateObjectDate { get; set; }
        public DateTime UpdateObjectDate { get; set; }
    }

    public interface IBaseDto<Tkey> where Tkey : struct
    {
        public Tkey Id { get; set; }
        public bool IsDelete { get; set; }
        public DateTime CreateObjectDate { get; set; }
        public DateTime UpdateObjectDate { get; set; }
    }

    public interface IBaseDto
    {
        public bool IsDelete { get; set; }
        public DateTime CreateObjectDate { get; set; }
        public DateTime UpdateObjectDate { get; set; }
    }
}
