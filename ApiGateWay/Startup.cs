
using Ocelot.DependencyInjection;
using Ocelot.Middleware;


namespace ApiGateWay
{

    public class Startup
    {
        private IConfiguration Configuration;

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            // services.AddControllers();
            services.AddOcelot(Configuration);

            // services.AddEndpointsApiExplorer();

            services.AddSwaggerForOcelot(Configuration);
            services.AddMvc()
            .SetCompatibilityVersion(Microsoft.AspNetCore.Mvc.CompatibilityVersion.Version_3_0);
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // app.UsePathBase("/gateway");
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseStaticFiles();

            // app.UseSwagger();
            app.UseSwaggerForOcelotUI(opt =>
            {

                opt.PathToSwaggerGenerator = "/swagger/docs";
            });


            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                // endpoints.MapControllers();
                endpoints.MapGet("/", async context =>
                {

                    await context.Response.WriteAsync("API GATEWAY FUNCIONANDO");
                });
            });
            app.UseOcelot().Wait();
        }
    }
}