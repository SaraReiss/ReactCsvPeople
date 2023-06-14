
using Microsoft.EntityFrameworkCore;


namespace ReactCsvPeople.Data
{
    public class PeopleDataContext : DbContext
    {
        private string _connectionString;

        public PeopleDataContext(string connectionString)
        {
            _connectionString = connectionString;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_connectionString);
        }

        public DbSet<FakerPerson> People { get; set; }

    }

}


    
