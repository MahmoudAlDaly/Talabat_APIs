using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StackExchange.Redis;
using Talabat.APIs.Errors;
using Talabat.APIs.Extensions;
using Talabat.APIs.Helpers;
using Talabat.APIs.Middelwares;
using Talabat.Core.Repositories.Contract;
using Talabat.Repository;
using Talabat.Repository._Identity;
using Talabat.Repository.Data;

namespace Talabat_APIs
{
	public  class Program
	{
		public static async Task Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			// Add services to the container.

			builder.Services.AddControllers();
			// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
			
			builder.Services.AddSwaggerServices();

			builder.Services.AddDbContext<StoreContext>(options =>
			{
				options.UseLazyLoadingProxies()
						.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
			});

			builder.Services.AddDbContext<ApplicationIdentityDbContext>(options =>
			{
				options.UseSqlServer(builder.Configuration.GetConnectionString("IdentityConnection"));
			});

			//ApplicationServicesExtension.AddApplicationServices(builder.Services);

			builder.Services.AddSingleton<IConnectionMultiplexer>((servicProvider)=>
			{
				var connetion = builder.Configuration.GetConnectionString("Rides");
				return ConnectionMultiplexer.Connect(connetion);
			});

			builder.Services.AddApplicationServices();

			builder.Services.AddApplicationServices();

			var app = builder.Build();

			#region Migration

			var scope = app.Services.CreateScope();

			try
			{
				var services = scope.ServiceProvider;
				var dbcontext = services.GetRequiredService<StoreContext>();
				var identityDbContext = services.GetRequiredService<ApplicationIdentityDbContext>();

				var loggerfactory = services.GetRequiredService<ILoggerFactory>();
				try
				{
					await dbcontext.Database.MigrateAsync(); // update database

					await StoreContextSeed.SeedAsync(dbcontext); // seeding

					await identityDbContext.Database.MigrateAsync();	


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

			app.UseMiddleware<ExceptionsMiddelware>();

			if (app.Environment.IsDevelopment())
			{
				app.UseSwaggerMiddlewares();
			}

			app.UseStatusCodePagesWithRedirects("/errors/{0}");

			app.UseStaticFiles();

			app.UseHttpsRedirection();

			app.UseAuthorization();


			app.MapControllers();

			app.Run();
		}
	}
}
