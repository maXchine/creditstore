using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(CreditStore.Docs.Startup))]
namespace CreditStore.Docs
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
