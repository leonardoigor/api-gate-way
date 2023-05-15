using ApiGateWay;

internal class Program
{
    private static void Main(string[] args)
    {
        CreateDefaultBuilder(args).Build().Run();


    }
    public static IHostBuilder CreateDefaultBuilder(string[] args) =>
    Host.CreateDefaultBuilder(args)
        .ConfigureAppConfiguration((hostingContext, config) =>
        {
            config.SetBasePath(hostingContext.HostingEnvironment.ContentRootPath)
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .AddJsonFile($"appsettings.{hostingContext.HostingEnvironment.EnvironmentName}.json", optional: true, reloadOnChange: true)
            .AddJsonFile($"configuration.{hostingContext.HostingEnvironment.EnvironmentName}.json")
            .AddEnvironmentVariables();
        })
        .ConfigureWebHostDefaults(webBuilder =>
        {
            webBuilder.UseStartup<Startup>();

        });
}
// app.Run();
