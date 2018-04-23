using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Programming_Challenge.Startup))]
namespace Programming_Challenge
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
