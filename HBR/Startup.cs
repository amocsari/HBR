using BLL.Mappings;
using BLL.Services.Implementation;
using BLL.Services.Interface;
using DAL;
using HBR.Filters;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.AzureADB2C.UI;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;

namespace HBR
{
    public class Startup
    {
        //TODO: connection string better handling
        private readonly string localConnectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=Hbr;Integrated Security=True";
        private readonly string azureConnectionString = "Data Source=tcp:hbr.database.windows.net,1433;Initial Catalog=HbrDatabase;User Id=amocsari@hbr.database.windows.net;Password=19Witchking65;";

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAuthentication(AzureADB2CDefaults.BearerAuthenticationScheme)
                .AddAzureADB2CBearer(options => Configuration.Bind("AzureAdB2C", options));
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.AddScoped<IHbrDbContext, HbrDbContext>();
            services.AddMvc(c => c.Filters.Add(typeof(ExceptionFilter))).SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.AddDbContext<HbrDbContext>(options => options.UseSqlServer(localConnectionString));

            services.AddSingleton(service => Mappings.Configure());
            services.AddScoped<IBookService, BookService>();
            services.AddScoped<IBookmarkService, BookmarkService>();
            services.AddScoped<IGenreService, GenreService>();
            services.AddScoped<IGoodReadsApiService, GoodReadsApiService>();
            services.AddScoped<IBlobStorageService, BlobStorageService>();
            services.AddScoped<ITimeService, TimeService>();

            // Register the Swagger generator, defining 1 or more Swagger documents
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "HBR", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();

                // Enable middleware to serve generated Swagger as a JSON endpoint.
                app.UseSwagger();

                // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.), 
                // specifying the Swagger JSON endpoint.
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "HBR");
                });
            }

            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseMvc();
        }
    }
}
