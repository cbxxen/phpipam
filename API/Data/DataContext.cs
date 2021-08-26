using API.Entities;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class DataContext : DbContext
    {
        //constructor
        public DataContext(DbContextOptions options) : base(options)
        {
            
        }

        //This is the Databse. AppUser=used Controller, Users=Table Name
        public DbSet<AppUser> Users { get; set; }
    }
}