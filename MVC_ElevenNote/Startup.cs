using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MVC_ElevenNote.Startup))]
namespace MVC_ElevenNote
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
