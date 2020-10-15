using System;
using EldaCPRFirstAidTraining.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

[assembly: HostingStartup(typeof(EldaCPRFirstAidTraining.Areas.Identity.IdentityHostingStartup))]
namespace EldaCPRFirstAidTraining.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            //builder.ConfigureServices((context, services) => {
            //    services.AddDbContext<EldaCPRFirstAidTrainingContext>(options =>
            //        options.UseSqlServer(
            //            context.Configuration.GetConnectionString("EldaCPRFirstAidTrainingContextConnection")));

            //    services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
            //        .AddEntityFrameworkStores<EldaCPRFirstAidTrainingContext>();
            //});
        }
    }
}