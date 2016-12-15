using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(PCA.Startup))]
namespace PCA
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
