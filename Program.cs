using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

//todo: podlaczenie do api sprawdzanie warto�ci walut (bc)
//todo: button lokalizacji lub spr przez api po IP
namespace PAM2Zaliczenie
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }

}
