using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HelpDeskCore.Models
{
    public class HelpDeskCoreContext : DbContext
    {
        public HelpDeskCoreContext(DbContextOptions<HelpDeskCoreContext> options)
            : base(options)
        {
        }

        public DbSet<TicketsModel> TicketsModel { get; set; }
        public DbSet<AssetsModel> AssetsModel { get; set; }
    }
}
