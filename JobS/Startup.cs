using Hangfire;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using Hangfire.MemoryStorage;
using JobS.Lib;
using Microsoft.AspNetCore.Builder;

namespace JobS
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<AutomaticRetryAttribute>();
            services.AddHangfire((provider, config) =>
            {

                config.SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
                .UseSimpleAssemblyNameTypeSerializer()
                .UseDefaultTypeSerializer()
                .UseMemoryStorage();
                config.UseFilter(provider.GetRequiredService<AutomaticRetryAttribute>());
            }


            );
            services.AddHangfireServer();

            services.AddHangfireServer();

            services.AddTransient<IJobChecker, JobChecker>();


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app,
            IBackgroundJobClient backgroundJobClient,
            IRecurringJobManager recurringJobManager,
            IServiceProvider serviceProvider)

        {

            app.UseHangfireDashboard();


            //backgroundJobClient.Enqueue(() => Console.WriteLine("Hello bro"));
            var jobs = ConfigHelper.GetConfigVal("Jobs");
            foreach (var job in jobs)
            {
                recurringJobManager.AddOrUpdate(
             job.Key,

              () => serviceProvider.GetService<IJobChecker>().Check(job.Key),

              job.Value
              );

            }


        }
    }

}
