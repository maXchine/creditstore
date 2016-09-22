using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(CreditStore.Background.Startup))]
namespace CreditStore.Background
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
