namespace ECommerceAPI.Data.Models
{
	using Microsoft.EntityFrameworkCore;
	using Microsoft.EntityFrameworkCore.Design;
	using Microsoft.Extensions.Configuration;
	using System.IO;

	public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
	{
		public AppDbContext CreateDbContext(string[] args)
		{
			// Get the configuration (from appsettings.json or environment variables)
			var configuration = new ConfigurationBuilder()
				.SetBasePath(Directory.GetCurrentDirectory())
				.AddJsonFile("appsettings.json")
				.Build();

			// Create the DbContextOptionsBuilder and configure it to use SQL Server
			var builder = new DbContextOptionsBuilder<AppDbContext>();
			var connectionString = configuration.GetConnectionString("DefaultConnection");

			builder.UseSqlServer(connectionString);

			return new AppDbContext(builder.Options);
		}
	}
}
