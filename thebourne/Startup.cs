using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(thebourne.Startup))]
namespace thebourne
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
