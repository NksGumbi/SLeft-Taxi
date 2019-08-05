using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SLeft_Taxi.Startup))]
namespace SLeft_Taxi
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
