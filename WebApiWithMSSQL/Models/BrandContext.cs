using Microsoft.EntityFrameworkCore;

namespace WebApiWithMSSQL.Models
{
    public class DBContext : DbContext
    {
        public DBContext(DbContextOptions<DBContext> options) : base(options)
        {

        }
        public DbSet<Student> Student { get; set; }
        public DbSet<StudentDetails> StudentDetails { get; set; }
    }
}
