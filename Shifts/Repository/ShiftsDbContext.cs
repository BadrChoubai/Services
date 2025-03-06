using Microsoft.EntityFrameworkCore;

using Shifts.Model;

namespace Shifts.Repository
{
    public class ShiftsDbContext : DbContext
    {
        public ShiftsDbContext()
        { }

        public ShiftsDbContext(DbContextOptions<ShiftsDbContext> options) : base(options)
        { }

        public DbSet<Shift> Shifts { get; set; }
    }
}