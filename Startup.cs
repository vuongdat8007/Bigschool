using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Bigschool_TH_11.Startup))]
namespace Bigschool_TH_11
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
