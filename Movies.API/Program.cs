
using Microsoft.EntityFrameworkCore;
using Movies.Framework.DAL.Contracts;
using Movies.Framework.DAL;
using Movies.Framework.Data;

namespace Movies.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddRazorPages();

            // Configuration setup
            var configuration = new ConfigurationBuilder()
                .SetBasePath(builder.Environment.ContentRootPath)
                .AddJsonFile("appsettings.json") // Assuming your connection string is in appsettings.json
                .Build();

            builder.Configuration.AddConfiguration(configuration); // Add configuration to the builder
            builder.Services.AddControllersWithViews();
            builder.Services.AddDbContext<MovieContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("MoviesDBConnection"));
            });

            builder.Services.AddControllers();
            builder.Services.AddScoped<IMovieService, MovieService>();
            
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Data seeding logic
            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                try
                {
                    var context = services.GetRequiredService<MovieContext>();

                    context.Database.Migrate();

                    // Seed data
                    DataSeeder.SeedData(context);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("An error occurred while seeding the database: " + ex.Message);
                }
            }

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