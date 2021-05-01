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
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

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

            return services;
        }
        
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped<IUserService, UserService>();
            services.AddTransient<IUnitOfWork, UnitOfWork>();
            services.AddScoped(typeof(IRepository<>), typeof(BaseRepository<>));
            services.AddSingleton<IPasswordService, PasswordService>();
            services.AddScoped<IBodyTypeService, BodyTypeService>();
            services.AddScoped<IDocumentTypeService, DocumentTypeService>();

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
