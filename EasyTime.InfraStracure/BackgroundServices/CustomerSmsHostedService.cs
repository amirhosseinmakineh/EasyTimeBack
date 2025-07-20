using EasyTime.Application.Contract.IServices;
using EasyTime.Model.IRepository;
using EasyTime.Model.Models;
using Microsoft.Extensions.Hosting;

namespace EasyTime.InfraStracure.BackgroundServices
{
    public class CustomerSmsHostedService : IHostedService,IDisposable
    {
        private readonly IBaseRepository<Guid, User> userRepository;
        private readonly ICustomerService customerService;
        private  Timer timer;
        public CustomerSmsHostedService(IBaseRepository<Guid, User> userRepository, ICustomerService customerService)
        {
            this.userRepository = userRepository;
            this.customerService = customerService;
        }

        public void Dispose()
        {
            timer.Dispose();
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            var users = await userRepository.GetAllEntities();
            var userIdes = (from u in users
                            select u.Id).ToList();
            timer = new Timer(async state => await customerService.SendSmsForCustomer(userIdes), null, TimeSpan.Zero, TimeSpan.FromMinutes(5));
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            timer?.Change(Timeout.Infinite, 0);
            return Task.CompletedTask;
        }
    }
}
