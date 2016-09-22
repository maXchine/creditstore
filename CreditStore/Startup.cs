using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(CreditStore.Startup))]
namespace CreditStore
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
