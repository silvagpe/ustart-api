using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UStart.Domain.Entities;
using UStart.Infrastructure.Helpers;

namespace UStart.Infrastructure.Context
{

    public class UStartContext : DbContext
    {

        public UStartContext(DbContextOptions<UStartContext> options)
         : base(options)
        { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }
        
        public DbSet<Usuario> Usuarios { get; set; }

        public override int SaveChanges()
        {           
            return base.SaveChanges();
        }

        // exemplo para EF
        // protected override void OnModelCreating(ModelBuilder modelBuilder)
        // {
        //     modelBuilder.Entity<BuyerOrder>()
        //         .ToTable("buyerorders")
        //         .HasKey(x => new { x.OrganizationId, x.DistributionChannelId, x.Identity, x.OrderItemId });


        //     EntityFrameworkPostgresHelper.MapPropertiesLowerCase<BuyerOrder>(modelBuilder);


        //     base.OnModelCreating(modelBuilder);
        // }


    }
}
