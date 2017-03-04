using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MangaGods.Startup))]
namespace MangaGods
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
