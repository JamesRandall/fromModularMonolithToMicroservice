using System;
using System.Net.Http;
using AzureFromTheTrenches.Commanding;
using AzureFromTheTrenches.Commanding.Abstractions;
using AzureFromTheTrenches.Commanding.AspNetCore;
using AzureFromTheTrenches.Commanding.AspNetCore.Swashbuckle;
using Cart.Application;
using Cart.Commands;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using ModularMonolith.Commanding;
using ModularMonolith.Filters;
using Notification.Application;
using Notification.Commands;
using Product.Application;
using Product.Commands;
using Swashbuckle.AspNetCore.Swagger;

namespace ModularMonolith
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IServiceProvider ServiceProvider { get; set; }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Configure the commanding mediator
            CommandingDependencyResolverAdapter adapter = new CommandingDependencyResolverAdapter(
                (fromType, toInstance) => services.AddSingleton(fromType, toInstance),
                (fromType, toType) => services.AddTransient(fromType, toType),
                (resolveTo) => ServiceProvider.GetService(resolveTo));
            ICommandRegistry commandRegistry = adapter.AddCommanding();
            services.Replace(new ServiceDescriptor(typeof(ICommandDispatcher), typeof(TelemetryDispatcher), ServiceLifetime.Transient));
            
            // Configure our subsystems
            services
                .AddNotificationApplication(commandRegistry)
                .AddCartApplication(commandRegistry)
                .AddProductApplication(commandRegistry);

            // Configure a REST API
            services
                // For demo purposes we inject some claims without relying on an external provider
                .AddMvc(options => options.Filters.Add<SetClaimsForDemoFilter>())
                // Configure a REST API based around our commands
                .AddAspNetCoreCommanding(cfg => cfg
                    .Claims(claimsMapper => claimsMapper.MapClaimToPropertyName("UserId", "AuthenticatedUserId"))
                    .Controller("Email", controller => controller
                        .Action<SendEmailCommand>(HttpMethod.Post))
                    .Controller("Cart", controller => controller
                        .Action<AddToCartCommand>(HttpMethod.Put))
                )
                // Add the FluentValidation system
                .AddFluentValidation();

            // Configure Swagger
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "Modular Monolith API v1", Version = "v1" });
                c.AddAspNetCoreCommanding();
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            ServiceProvider = app.ApplicationServices;
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Modular Monolith API v1");
            });

            app.UseMvc();
        }
    }
}
