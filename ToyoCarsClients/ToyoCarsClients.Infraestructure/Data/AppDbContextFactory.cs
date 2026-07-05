using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace ToyoCarsClients.Infraestructure.Data
{
    public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
    {
        public AppDbContext CreateDbContext(string[] args)
        {
            var optionBuilder = new DbContextOptionsBuilder<AppDbContext>();
            optionBuilder.UseMySql("Server=toyooeste.com.ve:2082;Database=mialeggi_TClient;User=mialeggi_sitio1;Password=xgigovfolw3Orwl;",
                new MySqlServerVersion(new Version(8, 0, 21)));

            return new AppDbContext(optionBuilder.Options);
        }
    }
}
