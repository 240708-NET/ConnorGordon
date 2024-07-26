using Microsoft.EntityFrameworkCore;
using Project1.Data;

namespace Project1.API {
    public class Program {
        public static void Main(string[] args) {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddScoped<IData>(pData => new DataSerial(File.ReadAllText("../Project1.Data/ConnectionString")));

            var app = builder.Build();

            if (app.Environment.IsDevelopment()) {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            //app.UseHttpsRedirection();
            app.MapControllers();

            app.Run();
        }
    }
}