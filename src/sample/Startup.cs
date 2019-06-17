using System;
using AnyEntity;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Sample.Models;

namespace sample
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
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            var entitiesToRegister = new[]{
                typeof(TestClass1),
                typeof(TestClass2),
            };

            Action<ModelBuilder> onModelCreating = builder =>
            {
                foreach (var e in entitiesToRegister)
                {
                    builder.Entity(e)
                    .ToTable(e.Name)
                    .HasIndex(nameof(IAnyEntityModelBase<object>.Id));
                }
            };

            var connection = "Data Source=sample_app.db";
            services.ConfigureAnyEntity(entitiesToRegister, options => options.UseSqlite(connection), onModelCreating);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseAnyEntity();
            app.UseMvc();
        }
    }
}
