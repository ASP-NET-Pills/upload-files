using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace UploadFiles
{
    internal class Startup
    {
        private readonly IConfiguration configuration;
        private readonly IHostingEnvironment hostingEnvironment;

        public Startup(IConfiguration configuration, IHostingEnvironment hostingEnvironment)
        {
            this.configuration = configuration;
            this.hostingEnvironment = hostingEnvironment;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvcCore(options =>
                {
                    options.InputFormatters.Insert(0, new StreamInputFormatter());
                })
                .AddApiExplorer()
                .AddAuthorization()
                .AddJsonFormatters()
                .AddDataAnnotations();

            services.AddHttpContextAccessor();

            services.AddHealthChecks();

            if (hostingEnvironment.IsDevelopment())
            {
                services.AddSwaggerGen();
            }

            services.Configure<KestrelServerOptions>(configuration.GetSection("Kestrel"));
            services.Configure<FormOptions>(configuration.GetSection("FormRequests"));
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IApplicationLifetime lifetime)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            else
            {
                app.UseHsts();
            }

            app.UseAuthentication();
            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}