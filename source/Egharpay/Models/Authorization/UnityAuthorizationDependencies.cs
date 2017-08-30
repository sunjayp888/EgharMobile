using Microsoft.Owin.Logging;
using Microsoft.Owin.Security.Authorization;
using Microsoft.Practices.Unity;
using System;

namespace Egharpay.Models.Authorization
{
    public class UnityAuthorizationDependencies : AuthorizationDependencies
    {
        private readonly IUnityContainer _container;
        public UnityAuthorizationDependencies(IUnityContainer container)
        {
            if (container == null)
                throw new ArgumentNullException(nameof(container));

            _container = container;
        }
        
        public override ILoggerFactory LoggerFactory
        {
            get { return null; }
            set { DisallowSetMethod(); }
        }

        public override IAuthorizationPolicyProvider PolicyProvider
        {
            get { return _container.Resolve<IAuthorizationPolicyProvider>(); }
            set { DisallowSetMethod(); }
        }

        public override IAuthorizationService Service
        {
            get { return _container.Resolve<IAuthorizationService>(); }
            set { DisallowSetMethod(); }
        }

        private static void DisallowSetMethod()
        {
            throw new InvalidOperationException("Set is not a valid operation.  Instead, register the component for resolution.");
        }
    }
}