using System;
using System.Net.Http;
using AzureFromTheTrenches.Commanding;
using AzureFromTheTrenches.Commanding.Abstractions;
using AzureFromTheTrenches.Commanding.AspNetCore;
using AzureFromTheTrenches.Commanding.AspNetCore.Swashbuckle;
using Cart.Application;
using Cart.Commands;
using DecomposedApplication.Commanding;
using DecomposedApplication.Filters;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Notification.Client;
using Notification.Commands;
using Product.Client;
using Swashbuckle.AspNetCore.Swagger;

namespace DecomposedApplication
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

            commandRegistry
                .AddNotificationClient(Configuration["notifications:client:serviceBusConnectionString"])
                .AddProductClient(new Uri(Configuration["products:client:getProductQuery"]));

            // Configure our in process subsystems
            services
                .AddCartApplication(commandRegistry);

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
                c.SwaggerDoc("v1", new Info { Title = "Decomposed Application API v1", Version = "v1" });
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
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Decomposed Application API v1");
            });

            app.UseMvc();
        }
    }
}
