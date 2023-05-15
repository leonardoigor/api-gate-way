using ApiGateWay;

internal class Program
{
    private static void Main(string[] args)
    {
        CreateDefaultBuilder(args).Build().Run();


    }
    public static IHostBuilder CreateDefaultBuilder(string[] args)

    {

        var builder = Host.CreateDefaultBuilder(args);
        builder.ConfigureServices(s => s.AddSingleton(builder));
        builder.ConfigureAppConfiguration((hostingContext, config) =>
          {
              config
                  .SetBasePath(hostingContext.HostingEnvironment.ContentRootPath)
                  .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                  .AddJsonFile($"appsettings.{hostingContext.HostingEnvironment.EnvironmentName}.json", optional: true, reloadOnChange: true)
                  //   .AddJsonFile($"configuration.json")
                  //   .AddJsonFile($"configuration.{hostingContext.HostingEnvironment.EnvironmentName}.json")
                  .AddJsonFile($"ocelot.json")
                 //   .AddJsonFile($"ocelot.{hostingContext.HostingEnvironment.EnvironmentName}.json")
                 .AddJsonFile(Path.Combine(hostingContext.HostingEnvironment.ContentRootPath, "configuration.json"))
                 .AddEnvironmentVariables();
          });
        builder.ConfigureWebHostDefaults(webBuilder =>
           {
               webBuilder.UseStartup<Startup>();
           });

        return builder;
    }

}
// app.Run();
