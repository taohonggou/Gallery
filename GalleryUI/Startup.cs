using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(GalleryUI.Startup))]
namespace GalleryUI
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
