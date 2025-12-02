using DAL.Entities;
using Microsoft.EntityFrameworkCore;


namespace DAL.Data
{
    public class GarenaContext : DbContext
    {
        public GarenaContext()
        {
        }
        public GarenaContext(DbContextOptions<GarenaContext> options)
            : base(options)
        {
        }

        public DbSet<User> User { get; set; }

       

    }
}
