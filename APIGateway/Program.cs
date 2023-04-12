using Ocelot.DependencyInjection;
using Ocelot.Middleware;

namespace APIGateway
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            //Ocelot dependenciy injection oalrak eklendi.
            builder.Services.AddOcelot();

            var app = builder.Build();

            //ocelot.json file'dan routing ayarlarýný almasý için aþaðýdaki tanýmlama yapýldý.
            builder.WebHost.ConfigureAppConfiguration((host, config) =>
            {
                config.AddJsonFile("ocelot.json");
            });
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.UseOcelot(); //awaitle bekledik, ocelotu eklemiþ olduk.

            app.Run();
        }
    }
}