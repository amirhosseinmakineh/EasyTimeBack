using EasyTime.Application.Contract.IServices;
using System.Reflection;

namespace EasyTime.InfraStracure.UnitOfWork
{
    public class UnitOfWorkAttributeManager
    {
        private readonly HashSet<string> _unitOfWorkNames;
        public UnitOfWorkAttributeManager()
        {
            _unitOfWorkNames = new HashSet<string>();
            SetValue();
        }
        public void SetValue()
        {
            var targets = Assembly.GetExecutingAssembly().GetTypes().Where(x => x.IsAssignableTo(typeof(IService)) && !x.IsInterface).SelectMany(x => x.GetMethods().Where(x => x.GetCustomAttributes().Any(x => x is UnitOfWorkAttribute)));
            foreach (var target in targets)
            {
                var targetName = "I" + target.DeclaringType.Name + "/" + target.Name;
                _unitOfWorkNames.Add(targetName);
            }
        }
        public bool HasValue(string targetName)
        {
            return _unitOfWorkNames.TryGetValue(targetName, out _);
        }
    }
}
