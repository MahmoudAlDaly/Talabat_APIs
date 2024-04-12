using Microsoft.EntityFrameworkCore;
using Talabat.Repository.Data;

namespace Talabat_APIs
{
	public class Program
	{
		public static async Task Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			// Add services to the container.

			builder.Services.AddControllers();
			// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
			builder.Services.AddEndpointsApiExplorer();
			builder.Services.AddSwaggerGen();

			builder.Services.AddDbContext<StoreContext>(options =>
			{
				options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
			});

			var app = builder.Build();

			#region Migration

			var scope = app.Services.CreateScope();

			try
			{
				var services = scope.ServiceProvider;
				var dbcontext = services.GetRequiredService<StoreContext>();

				var loggerfactory = services.GetRequiredService<ILoggerFactory>();
				try
				{
					await dbcontext.Database.MigrateAsync(); // update database

					await StoreContextSeed.SeedAsync(dbcontext); // seeding
				}
				catch (Exception ex)
				{

					var logger = loggerfactory.CreateLogger<Program>();
					logger.LogError(ex, "Error in apply Migration");
				}
			}
			finally
			{
				scope.Dispose();
			} 

			#endregion

			// Configure the HTTP request pipeline.
			if (app.Environment.IsDevelopment())
			{
				app.UseSwagger();
				app.UseSwaggerUI();
			}

			app.UseHttpsRedirection();

			app.UseAuthorization();


			app.MapControllers();

			app.Run();
		}
	}
}
