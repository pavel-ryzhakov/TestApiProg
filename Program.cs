using Microsoft.EntityFrameworkCore;
using TestApiProg.Data;
using TestApiProg.Services;


namespace TestApiProg
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddDbContext<TestAPIContext>(options =>
                options.UseNpgsql(builder.Configuration.GetConnectionString("TestApiDB") ?? throw new InvalidOperationException("Connection string 'RegistrationAPIContext' not found.")));
            builder.Services.AddScoped<IProductService, ProductService>();
            builder.Services.AddSwaggerGen();
            builder.Services.AddControllers();
            
            builder.Services.AddAutoMapper(typeof(AutoMapperProfile));

            var app = builder.Build();
            app.UseHttpsRedirection();
            app.UseRouting();
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseEndpoints(endpoints =>
                endpoints.MapControllers());
            app.Run();
        }
    }
}