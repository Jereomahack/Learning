using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Lightway_Academy_school_fee_application.Startup))]
namespace Lightway_Academy_school_fee_application
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
