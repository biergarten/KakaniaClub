using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mas.Infrastructure.Data
{
    public class ApplicationContextFactory : IDesignTimeDbContextFactory<ApplicationContext>
    {
        public ApplicationContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationContext>();
            optionsBuilder.UseSqlServer(
            //"Data Source=(localdb)\\MSSQLLocalDB; Initial Catalog=MembershipApplicationService;Integrated Security=SSPI;");
            "Server=tcp:kakaniaclubserver.database.windows.net,1433;Initial Catalog=mas_db;Persist Security Info=False;User ID=francisco;Password=_Password99;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
            //"Data Source= (localdb)\\MSSQLLocalDB; Initial Catalog=MasDataMigrationsTest");
            return new ApplicationContext(optionsBuilder.Options);
        }
    }
}
