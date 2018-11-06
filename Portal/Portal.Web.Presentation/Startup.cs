using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Hosting.Internal;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json.Serialization;
using Portal.core.Infrastructure;
using Portal.Infrastructure;
using Portal.Service.Media;
using Portal.Service.News;
using Portal.Standard.Service.Media;
using Portal.Web.Presentation.Area.Extension;


namespace Portal.Web.Presentation
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IMapper mapper)
        {
            Configuration = configuration;
            Mapper = mapper;
        }

        public IConfiguration Configuration { get; }

        public IMapper Mapper { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            //services.AddDbContext<PortalDbContext>(c =>
            //{
            //    c.UseSqlServer(Configuration.GetConnectionString("MarketingContext"));
            //});
            //services.AddDbContext<PortaIIdentityContext>(c =>
            //{
            //    c.UseSqlServer(Configuration.GetConnectionString("IdentityContext"));
            //});

            var mapper = CreateMapperConfiguration();
            services.AddSingleton(mapper);
            services.AddMvc().AddJsonOptions(options => options.SerializerSettings.ContractResolver=new CamelCasePropertyNamesContractResolver()).AddNToastNotifyToastr();
            services.AddKendo();
            services.AddTransient(typeof(IRepository<>), typeof(EfRepository<>));
            services.AddTransient<INewsService, NewsService>();
            //services.AddTransient<IPictureService, PictureService>();
            services.AddTransient<IHostingEnvironment, HostingEnvironment>();

            //   services.AddIdentity<AppUser, IdentityRole>();



        }

        private static IMapper CreateMapperConfiguration()
        {
            var config = new MapperConfiguration(cfg => cfg.AddProfile(new MappingProfile()));

            var mapper = config.CreateMapper();
            return mapper;
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {


            if (env.IsDevelopment())
            {
              
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
            }

            app.UseStatusCodePages();
            app.UseStaticFiles();
            app.UseAuthentication();
            app.UseNToastNotify();
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "areas",
                    template: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
                );
                routes.MapRoute(
                    name: "default",
                    template: "{controller=home}/{action=Index}/{id?}"
                    );
            });
        }
    }
}
