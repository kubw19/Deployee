using Deployer.DatabaseModel;
using Deployer.Jobs;
using Deployer.Logic.Artifacts;
using Deployer.Logic.Steps;
using Deployer.Logic.Targets;
using Deployer.Repositories.Artifacts;
using Deployer.Repositories.DeploySteps;
using Deployer.Repositories.Projects;
using Deployer.Repositories.Targets;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Deployer.API
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
            var folder = Environment.SpecialFolder.LocalApplicationData;
            var path = Environment.GetFolderPath(folder);
            var dbPath = $"{path}{System.IO.Path.DirectorySeparatorChar}deployer.db";


            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            services.AddDbContext<DeployerContext>(x => x.UseSqlite($"Data Source={dbPath}"));
            services.AddControllersWithViews();


            services.AddScoped<IDeployStepsRepository, DeployStepsRepository>();
            services.AddScoped<IStepsLogic, StepsLogic>();
            services.AddScoped<ITargetsRepository, TargetsRepository>();
            services.AddScoped<ITargetsLogic, TargetsLogic>();

            services.AddScoped<IArtifactsRepository, ArtifactsRepository>();
            services.AddScoped<IArtifactsLogic, ArtifactsLogic>();

            services.AddScoped<IProjectsRepository, ProjectsRepository>();


            services.AddSingleton<JobBinder>();
            services.AddScoped<IJobManager, JobManager>();

            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "Deployer.SPA/dist";
            });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Deployer.API", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, DeployerContext deployerContext)
        {
            deployerContext.Database.Migrate();
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Deployer.API v1"));
            }

            //app.UseHttpsRedirection();
            app.UseStaticFiles();
            if (!env.IsDevelopment())
            {
                app.UseSpaStaticFiles();
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller}/{action=Index}/{id?}");
            });

            app.UseSpa(spa =>
            {
                // To learn more about options for serving an Angular SPA from ASP.NET Core,
                // see https://go.microsoft.com/fwlink/?linkid=864501

                spa.Options.SourcePath = "Deployer.SPA";

                if (env.IsDevelopment())
                {
                    //spa.UseAngularCliServer(npmScript: "start");
                    spa.UseProxyToSpaDevelopmentServer("http://localhost:4200");
                }
            });
        }
    }
}
