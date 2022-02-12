using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Activities;
using Application.Core;
using MediatR;
using Persistence;
using Microsoft.EntityFrameworkCore;

namespace API.Extensions
{
    public static class ApplicationServiceExtensions
    {
        public static WebApplicationBuilder AddApplicationServices(this WebApplicationBuilder builder)
        {
            builder.Services.AddSwaggerGen();

            var ConnectionString = builder.Configuration.GetConnectionString("Default");
            builder.Services.AddDbContext<DataContext>(options =>
            {
                options.UseSqlite(ConnectionString);
            });

            builder.Services.AddMediatR(typeof(List.Handler).Assembly);
            builder.Services.AddAutoMapper(typeof(MappingProfiles).Assembly);
            return builder;
        }
    }
}