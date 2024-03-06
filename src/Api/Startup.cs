using DogShelterService.Api.Domain;
using DogShelterService.Api.Features.AddDog;
using DogShelterService.Api.Repositories;
using DogShelterService.Api.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace DogShelterService.Api
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IDogRepository, DogRepository>();
            services.AddScoped<IBreedService, BreedService>();
            services.AddScoped<AddDogHandler>();
            services.AddScoped<SearchDogsHandler>();

            services.AddSwaggerGen();
            services.AddControllers();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();

                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseRouting();
            app.UseEndpoints(_ => _.MapControllers());
        }
    }
}