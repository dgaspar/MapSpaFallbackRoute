using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace SpaRouterTest
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services
                .AddMvc()
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseMvc(routes =>
            {
                routes.MapRoute("default", "{controller=Home}/{action=Index}/{id?}");
            });

            app.MapWhen(x => !x.Request.Path.Value.StartsWith("/api"), builder =>
            {
               builder.UseMvc(routes => { routes.MapSpaFallbackRoute("spa-fallback", defaults: new {controller = "Home", action = "Index"}); });
            });
        }
    }

    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return new OkObjectResult("Home Index");
        }
    }

    [ApiController]
    [Route("api")]
    public class ApiController : Controller
    {
        [HttpGet("index")]
        public IActionResult Index()
        {
            return new OkObjectResult("Api Get Index");
        }
    }
}
