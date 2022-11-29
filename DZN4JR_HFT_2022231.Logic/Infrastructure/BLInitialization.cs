using DZN4JR_HFT_2022231.Logic.Interfaces;
using DZN4JR_HFT_2022231.Logic.Services;
using DZN4JR_HFT_2022231.Repository.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DZN4JR_HFT_2022231.Logic.Infrastructure
{
    public static class BLInitialization
    {
        public static void InitBLServices(IServiceCollection services)
        {
            RepoInitialization.InitRepoServices(services);
            services.AddScoped<IBrandService, BrandService>();
            services.AddScoped<ICustomerService, CustomerService>();
            services.AddScoped<IPaintService, PaintService>();
        }
    }
}
