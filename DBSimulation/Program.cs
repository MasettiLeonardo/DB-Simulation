using Microsoft.Extensions.FileProviders;

namespace DBSimulation
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers();
            builder.Services.AddSwaggerGen();
            
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowWebApp", builder =>
                {
                    builder.AllowAnyOrigin()
                           .AllowAnyMethod()
                           .AllowAnyHeader();
                });
            });

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            
            app.UseHttpsRedirection();
            app.UseCors("AllowWebApp");
            app.UseAuthorization();
            app.UseStaticFiles();

            app.UseFileServer(new FileServerOptions
            {
                FileProvider = new PhysicalFileProvider(
                    Path.Combine(Directory.GetCurrentDirectory())),
                RequestPath = "",
                EnableDefaultFiles = true
            });

            app.MapControllers();

            app.Run();
        }
    }

}
