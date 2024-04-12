using Microsoft.Extensions.DependencyInjection;
using Serilog;
using Microsoft.EntityFrameworkCore;
using BookStoreApp.API.Data;
using BookStoreApp.API.Repositories;
using Microsoft.AspNetCore.Hosting;
using BookStoreApp.API.Mappings;
namespace BookStoreApp.API
{ 
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            //var conString = builder.Configuration.GetConnectionString("BookStoreAppDBConnection");
            //builder.Services.AddDbContext<BookStoreDBContext>(options => options.UseSqlServer(conString));

            builder.Services.AddDbContext<BookStoreDBContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("BookStoreAppDBConnection")));

            builder.Services.AddScoped<IBookStoreAuthorRepository, SQLAuthorRepository>();
            builder.Services.AddScoped<IBookStoreBookRepository, SQLBookRepository>();

            //injects Automapper when the application starts
            builder.Services.AddAutoMapper(typeof(AutoMapperProfiles));

            builder.Services.AddControllers();
            builder.Services.AddHttpContextAccessor();

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAll", b =>
                {
                    b.AllowAnyMethod().AllowAnyHeader().AllowAnyOrigin();
                });
            });

            //builder.Services.AddScoped<IBookStoreAuthorRepository, SQLAuthorRepository>();

            //builder.Services.AddAutoMapper(typeof(Startup));


            builder.Host.UseSerilog((ctx, cl)=>
                cl.WriteTo.Console()
                .ReadFrom.Configuration(ctx.Configuration));

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseCors("AllowAll");

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");


            app.Run();
        }
    }
}
