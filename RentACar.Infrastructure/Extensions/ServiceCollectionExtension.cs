using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using RentACar.Core.Interfaces;
using RentACar.Core.Services;
using RentACar.Infrastructure.Data;
using RentACar.Infrastructure.Interfaces;
using RentACar.Infrastructure.Options;
using RentACar.Infrastructure.Repositories;
using RentACar.Infrastructure.Services;
using System;
using System.IO;

namespace RentACar.Infrastructure.Extensions
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddDbContexts(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<RentACarContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("RentACar"))
            );

            return services;
        }

        public static IServiceCollection AddOptions(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<PasswordOptions>(options => configuration.GetSection("PasswordOptions").Bind(options));
            services.Configure<ImageOptions>(options => configuration.GetSection("ImageOptions").Bind(options));

            return services;
        }
        
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddTransient<IUnitOfWork, UnitOfWork>();

            // Services - Singleton
            services.AddSingleton<IPasswordService, PasswordService>();
            services.AddSingleton<IFileService, FileService>();


            // Services - Scoped
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IBodyTypeService, BodyTypeService>();
            services.AddScoped<IDocumentTypeService, DocumentTypeService>();
            services.AddScoped<ICarService, CarService>();
            services.AddScoped<IBrandService, BrandService>();
            services.AddScoped<IPaymentTypeService, PaymentTypeService>();

            return services;
        }

        public static IServiceCollection AddSwagger(this IServiceCollection services,string xmlFileName)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "RentACar.Api", Version = "v1" });

                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFileName);
                c.IncludeXmlComments(xmlPath);
            });

            return services;
        }


    }
}
