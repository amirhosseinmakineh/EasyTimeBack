using Castle.DynamicProxy;
using IInterceptor = Castle.DynamicProxy.IInterceptor;

namespace EasyTime.InfraStracure.UnitOfWork
{
    public class UnitOfWorkInterCeptor : IInterceptor
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly UnitOfWorkAttributeManager unitOfWorkAttributeManager;

        public UnitOfWorkInterCeptor(IUnitOfWork unitOfWork, UnitOfWorkAttributeManager unitOfWorkAttributeManager)
        {
            this.unitOfWork = unitOfWork;
            this.unitOfWorkAttributeManager = unitOfWorkAttributeManager;
        }

        public void Intercept(IInvocation invocation)
        {
            try
            {
                var targetName = invocation.Method.DeclaringType.Name + "/" + invocation.Method.Name;

                if (unitOfWorkAttributeManager.HasValue(targetName))
                {
                    unitOfWork.Begin();
                    invocation.Proceed();
                    unitOfWork.Commit();
                }
                else
                {
                    invocation.Proceed();
                }
            }
            catch (Exception ex)
            {

            }
        }
    }
}
