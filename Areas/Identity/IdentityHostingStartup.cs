using Microsoft.AspNetCore.Hosting;

[assembly: HostingStartup(typeof(AoTTG2.IDS.Areas.Identity.IdentityHostingStartup))]
namespace AoTTG2.IDS.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
            });
        }
    }
}