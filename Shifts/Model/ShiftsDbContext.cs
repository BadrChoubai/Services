using Microsoft.EntityFrameworkCore;

namespace Shifts.Model
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