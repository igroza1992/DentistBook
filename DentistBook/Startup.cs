using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(DentistBook.Startup))]
namespace DentistBook
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
