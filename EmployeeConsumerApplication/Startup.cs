using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(EmployeeConsumerApplication.Startup))]
namespace EmployeeConsumerApplication
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
