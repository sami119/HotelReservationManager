using System;
using System.Collections.Generic;
using System.Text;
using HotelReservationManager.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace HotelReservationManager.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Client> Clients
        {
            get; set;
        }
        
        public DbSet<Room> Rooms { get; set; }
    }
}
