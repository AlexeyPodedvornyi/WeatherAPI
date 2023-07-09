using System.Reflection;
using WeatherAPI.Parsers;
using WeatherAPI.Parsers.Interfaces;
using WeatherAPI.Services;
using WeatherAPI.Services.Interfaces;
using Swashbuckle.AspNetCore.Filters;
using WeatherAPI.Serializer.Interfaces;
using WeatherAPI.Models.Responses.Interfaces;
using WeatherAPI.Serializer;
using WeatherAPI.Factories.Interfaces;
using WeatherAPI.Factories;

namespace WeatherAPI
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(options =>
            {
                options.ExampleFilters();
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                options.IncludeXmlComments(xmlPath);
                
            });
            services.AddSwaggerExamplesFromAssemblyOf<Startup>();
            services.AddCors();

            services.AddScoped<IWeatherDataParser, WeatherDataParser>();
            services.AddScoped<IWeatherForecastService, WeatherForecastService>();
            services.AddScoped<IResponseModelSerializer<IResponseModel>, ResponseModelSerializer>();
            services.AddScoped<IErrorResponseModelFactory, ErrorResponseModelFactory>();
            services.AddScoped<HttpClient>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthorization();
            app.UseCors();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
