using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Oazachaosu.Startup))]
namespace Oazachaosu
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
