using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PRO.Core;
using PRO.Core.Models;
using PRO.Core.Models.Auth;
using PRO.Data.Configurations;
using System;
using System.Collections.Generic;
using System.Text;

namespace PRO.Data
{
    public class ProDbContext : IdentityDbContext<User,Role,Guid>
    {

        public DbSet<Music> Musics { get; set; }
        public DbSet<Artist> Artists { get; set; }
        //public DbSet<Notification> Notifications { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public ProDbContext(DbContextOptions<ProDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfiguration(new MusicConfiguration());
            builder.ApplyConfiguration(new ArtistConfiguration());
        }

    }
}
