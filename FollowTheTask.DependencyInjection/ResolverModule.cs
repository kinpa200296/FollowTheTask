using FollowTheTask.DAL.Contexts;
using Ninject;
using Ninject.Extensions.Conventions;

namespace FollowTheTask.DependencyInjection
{
    public static class ResolverModule
    {
        public static void ConfigurateResolverWeb(this IKernel kernel)
        {
            Configure(kernel);
        }

        private static void Configure(IKernel kernel)
        {
            kernel.Bind(
                m => m.From("FollowTheTask.DAL")
                        .Select(t => t.Namespace.Contains("DAL.Repositories"))
                        .BindDefaultInterface());
            kernel.Bind(
                m => m.From("FollowTheTask.BLL")
                        .Select(t => t.Namespace.Contains("BLL.Services"))
                        .BindDefaultInterface());
            kernel.Bind<FollowTheTaskContext>().ToSelf().InSingletonScope();
        }
    }
}