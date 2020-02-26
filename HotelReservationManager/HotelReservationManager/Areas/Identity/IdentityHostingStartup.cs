using System;
using HotelReservationManager.Areas.Identity.Data;
using HotelReservationManager.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

[assembly: HostingStartup(typeof(HotelReservationManager.Areas.Identity.IdentityHostingStartup))]
namespace HotelReservationManager.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
                services.AddDbContext<HotelReservationManagerDBContext>(options =>
                    options.UseSqlServer(
                        context.Configuration.GetConnectionString("HotelReservationManagerDBContextConnection")));

                services.AddDefaultIdentity<HotelManagerUser>()
                    .AddRoles<IdentityRole>()
                    .AddEntityFrameworkStores<HotelReservationManagerDBContext>()
                    .AddDefaultTokenProviders();
            });
        }
    }
}