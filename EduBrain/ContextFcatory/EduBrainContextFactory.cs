using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;
using EduBrain.Data;

namespace EduBrain.ContextFcatory
{
    public class EduBrainContextFactory : IDesignTimeDbContextFactory<EduBrainContext>
    {
        public EduBrainContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<EduBrainContext>();
            optionsBuilder.UseSqlServer("Server=(LocalDb)\\MSSQLLocalDB;Database=EduBrain_Dev;Trusted_Connection=True;Encrypt=false;TrustServerCertificate=true");

            return new EduBrainContext(optionsBuilder.Options);
        }
    }
}
