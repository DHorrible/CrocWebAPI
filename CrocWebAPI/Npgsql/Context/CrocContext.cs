using CrocWebAPI.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CrocWebAPI.Npgsql.Context
{
    /*
    public class CrocContext : DbContext
    {
        public DbSet<Instance> Instances { get; set; }
        public DbSet<InstanceType> InstanceTypes { get; set; }

        public CrocContext(DbContextOptions<CrocContext> options)
            : base(options)
        { }
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.UseNpgsql("Host=http://127.0.0.1:52738;Database=croc;Username=postgres;Password=02042000");
    }
    */
}
