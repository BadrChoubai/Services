namespace Shifts.Model
{
    public class DataSeeder(ShiftsDbContext context)
    {
        private readonly ShiftsDbContext _context = context;

        public void Seed()
        {
            if (_context.Shifts.Any())
            {
                return;
            }

            var shifts = new List<Shift>
            {
                new()
                {
                    Id = new Guid("2e1f8d0e-0e3e-44c0-8ea7-2afb6ff9e2f2"),
                    Duration = TimeSpan.FromHours(4),
                    Location = new Location
                    {
                        Id = new Guid("2d68b461-2517-4386-871f-ca6b8340cb5b"),
                        Address = "8350 Quintero St",
                        City = "Commerce City",
                        StateCode = "CO",
                        PostalCode = "80022",
                        CountryCode = "US",
                        Facility = new Facility
                        {
                            Id = new Guid("d56d6118-2a64-4295-940f-dfe3a9cae3c7"),
                            Alias = "BDU5",
                            Type = FacilityType.SortCenter
                        }
                    }
                }
            };

            _context.Shifts.AddRange(shifts);
            _context.SaveChanges();
        }
    }
}