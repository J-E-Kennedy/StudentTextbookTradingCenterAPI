using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using SttcBookTrade.Entities;
using SttcBookTrade.Services;
using Swashbuckle.AspNetCore.Swagger;

namespace SttcBookTrade
{
#pragma warning disable CS1591
    public class Startup
    {

        public static IConfiguration Configuration { get; private set; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v0.1", new Info { Title = "Student Textbook Trading Center API", Version = "v0.1" });

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });

            services.AddMvc()
                .AddMvcOptions(o => o.OutputFormatters.Add(
                    new XmlDataContractSerializerOutputFormatter()));

            services.AddCors();

            var connectionString = Configuration["connectionStrings:bookTradeDBConnectionString_Azure"];
            services.AddDbContext<BookTradeContext>(o => o.UseSqlServer(connectionString));
            

            services.AddScoped<IBookTradeRepository, BookTradeRepository>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory, BookTradeContext bookTradeContext)
        {

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v0.1/swagger.json", "Student Textbook Tradting Center API V0.1");
                c.RoutePrefix = string.Empty;
            });

            app.UseCors(options => options.AllowAnyOrigin().AllowAnyMethod()
            .AllowAnyHeader().AllowCredentials());

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            bookTradeContext.EnsureSeedDataForContext();

            app.UseStatusCodePages();

            AutoMapper.Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<Entities.User, Models.UserDto>();
                cfg.CreateMap<Entities.User, Models.UserWithoutBooksDto>();
                cfg.CreateMap<Entities.Book, Models.BookDto>();
                cfg.CreateMap<Entities.Book, Models.BookWithSellerDto>();
            }
            );

            app.UseMvc();

        }
    }
#pragma warning restore CS1591
}
