using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace demo_app
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddHealthChecks();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapHealthChecks("/healthz");

                endpoints.MapGet("/", async context =>
                {
                    await context.Response.WriteAsync("By the way, this is the second version." + Environment.NewLine);

                    var connection = context.Features.Get<IHttpConnectionFeature>();
                    await context.Response.WriteAsync($"Listening IP: " + connection.LocalIpAddress + Environment.NewLine);
                    await context.Response.WriteAsync($"Hostname: " + Dns.GetHostName() + Environment.NewLine);
                });
            });
        }
    }
}
