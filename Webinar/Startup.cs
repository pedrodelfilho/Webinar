using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(Webinar.StartupOwin))]

namespace Webinar
{
    public partial class StartupOwin
    {
        public void Configuration(IAppBuilder app)
        {
            //AuthStartup.ConfigureAuth(app);
        }
    }
}
