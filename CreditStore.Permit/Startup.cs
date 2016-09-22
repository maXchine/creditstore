using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(CreditStore.Permit.Startup))]
namespace CreditStore.Permit
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
