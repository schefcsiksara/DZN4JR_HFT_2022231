using DZN4JR_HFT_2022231.Logic.Interfaces;
using DZN4JR_HFT_2022231.Models.Entities;
using DZN4JR_HFT_2022231.Models.Model;
using DZN4JR_HFT_2022231.Repository.Interfaces;
using DZN4JR_HFT_2022231.Repository.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DZN4JR_HFT_2022231.Logic.Services
{
    public class BrandService : IBrandService
    {
        private IBrandRepository brandRepository;
        private IPaintRepository paintRepository;

        public BrandService(IBrandRepository brandRepository, IPaintRepository paintRepository)
        {
            this.brandRepository = brandRepository;
            this.paintRepository = paintRepository;
        }

        public Brand Create(Brand entity)
        {
            if (string.IsNullOrEmpty(entity.Name))
            {
                throw new ApplicationException("Brand name cannot be empty");
            }

            if (entity.Name.Length > 30)
            {
                throw new ApplicationException("Brand name too long. Max characters: 30");
            }

            return brandRepository.Create(entity);
        }

        public void Delete(int id)
        {
            var entity = Read(id);

            brandRepository.Delete(entity);
        }

        public Brand Read(int id)
        {
            return brandRepository.Read(id);
        }

        public List<Brand> ReadAll()
        {
            return brandRepository.ReadAll().ToList();
        }

        public Brand Update(Brand entity)
        {
            var oldEntity = Read(entity.Id);

            oldEntity = entity;

            return brandRepository.Update(entity);
        }

        public List<PaintWithBrandName> GetPaintColorWithBrands()
        {
            var result = from paint in paintRepository.ReadAll()
                         join brand in brandRepository.ReadAll()
                            on paint.BrandId equals brand.Id
                         select new PaintWithBrandName()
                         {
                             BrandName = brand.Name,
                             PaintColor = paint.Color
                         };

            return result.ToList();
        }

      
        public List<PaintWithBrandNameAndPrice> GetPaintColorWithBrandsOrderedByPrice()
        {
            var result = from paint in paintRepository.ReadAll()
                         join brand in brandRepository.ReadAll()
                            on paint.BrandId equals brand.Id
                         orderby paint.BasePrice
                         select new PaintWithBrandNameAndPrice()
                         {
                             BrandName = brand.Name,
                             PaintColor = paint.Color,
                             PaintPrice = paint.BasePrice
                         };

            return result.ToList();
        }

        public List<PaintWithBrandName> GetPaintColorWithBrandsRateOver3()
        {
            var result = from paint in paintRepository.ReadAll()
                         join brand in brandRepository.ReadAll()
                            on paint.BrandId equals brand.Id
                         where brand.Rating > 3
                         select new PaintWithBrandName()
                         {
                             BrandName = brand.Name,
                             PaintColor = paint.Color
                         };

            return result.ToList();
        }

        public List<PaintWithBrandName> GetPaintColorWithBrandsFromHungary()
        {
            var result = from paint in paintRepository.ReadAll()
                         join brand in brandRepository.ReadAll()
                            on paint.BrandId equals brand.Id
                         where brand.Country == "Hungary" || brand.Country == "hungary"
                         select new PaintWithBrandName()
                         {
                             BrandName = brand.Name,
                             PaintColor = paint.Color
                         };

            return result.ToList();
        }
    }
}
