﻿using System;
using System.Collections.Generic;
using System.Web.Mvc;
using FollowTheTask.DependencyInjection;
using Ninject;

namespace FollowTheTask.Web.Infrastructure
{
    public class NinjectDependencyResolver : IDependencyResolver
    {
        private readonly IKernel _kernel;


        public NinjectDependencyResolver(IKernel kernel)
        {
            _kernel = kernel;
            kernel.ConfigurateResolverWeb();
        }


        public object GetService(Type serviceType)
        {
            return _kernel.TryGet(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return _kernel.GetAll(serviceType);
        }
    }
}