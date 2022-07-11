﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Notes.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notes.Persistence
{
    public static class DepenedncyInjection
    {
        public static IServiceCollection AddPersistence (this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration["DbConnection"];
            services.AddDbContext<NotesDbContext>(options =>
            {
                options.UseNpgsql(connectionString);
            });
            services.AddScoped<INotesDbContext>(provider =>
            provider.GetService<NotesDbContext>());

            return services;
        }
    }
}
