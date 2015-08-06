using DataAccess.Context;
using DataAccess.Repository;
using Ninject.Modules;

namespace Services.DependencyInjection
{
    public class BlogModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IBlogRepository>().To<BlogRepository>();
            Bind<IContextFactory>().To<ContextFactory>();
            Bind<IBlogService>().To<BlogService>();
        }
    }
}
