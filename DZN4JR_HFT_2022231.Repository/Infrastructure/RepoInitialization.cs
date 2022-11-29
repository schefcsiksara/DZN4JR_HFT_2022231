using DZN4JR_HFT_2022231.Repository.DbContexts;
using DZN4JR_HFT_2022231.Repository.Interfaces;
using DZN4JR_HFT_2022231.Repository.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DZN4JR_HFT_2022231.Repository.Infrastructure
{
    public static class RepoInitialization
    {
        public static void InitRepoServices(IServiceCollection services)
        {
            services.AddScoped<DbContext, PaintStoreMemoryDbContext>();
            services.AddScoped<IPaintRepository, PaintRepository>();
            services.AddScoped<IBrandRepository, BrandRepository>();
            services.AddScoped<ICustomerRepository, CustomerRepository>();
        }
    }
}
