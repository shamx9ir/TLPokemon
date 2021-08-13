using Autofac;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TLPokemon.Api.Models;
using TLPokemon.Api.Services.Network;
using TLPokemon.Api.Services.Pokemon;
using TLPokemon.Api.Services.Translation;

namespace TLPokemon.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddRouting(options => options.LowercaseUrls = true);
            services.AddHttpClient();
            services.Configure<TLPokemonConfiguration>(Configuration.GetSection("TLPokemonConfiguration"));
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "TLPokemon.Api", Version = "v1" });
            });
        }

        public void ConfigureContainer(ContainerBuilder builder)
        {
            builder.RegisterType<PokemonService>().As<IPokemonService>();
            builder.RegisterType<NetworkService>().As<INetworkService>();
            builder.RegisterType<Pokemon>();
            builder.RegisterType<TranslatedPokemon>();
            builder.RegisterType<YodaTranslationService>();
            builder.RegisterType<ShakespearTranslationService>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "TLPokemon.Api v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
