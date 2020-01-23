using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MySchool.Startup))]
namespace MySchool
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
