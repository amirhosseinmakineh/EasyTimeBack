using EasyTime.Application.Contract.Dtos.Customer;

namespace EasyTime.Application.Contract.IServices
{
    public interface ICustomerService
    {
        Task<List<CustomerDto>> GetAllCustomer(Guid businessOwnerId);
        Task SendSmsForCustomer(List<Guid> customerIdes);
    }
}
