using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Talabat.APIs.Errors;
using Talabat.APIs.Helpers;
using Talabat.APIs.Middelwares;
using Talabat.Core.Repositories.Contract;
using Talabat.Repository;
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
				options.UseLazyLoadingProxies()
						.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
			});

			builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

			builder.Services.AddAutoMapper(m=> m.AddProfile(new MappingProfiles()));

			builder.Services.Configure<ApiBehaviorOptions>(option =>
			{
				option.InvalidModelStateResponseFactory = (actioncontext) =>
				{
					var errors = actioncontext.ModelState.Where(p => p.Value.Errors.Count() > 0)
										.SelectMany(p => p.Value.Errors)
										.Select(e => e.ErrorMessage)
										.ToList();

					var response = new ApiValidationErrorResponse()
					{
						Errors = errors
					};

					return new BadRequestObjectResult(response);
				};
				
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

			app.UseMiddleware<ExceptionsMiddelware>();

			if (app.Environment.IsDevelopment())
			{
				app.UseSwagger();
				app.UseSwaggerUI();
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
