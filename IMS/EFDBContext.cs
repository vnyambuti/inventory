using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMS
{
    internal class Efcontext:DbContext
    {
        private readonly IConfiguration configuration;
        public DbSet<User> users { get; set; }
        public Efcontext(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        public Efcontext()
        {
            
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(ConfigurationManager.ConnectionStrings["IMS"].ConnectionString);
        }
    }
}
