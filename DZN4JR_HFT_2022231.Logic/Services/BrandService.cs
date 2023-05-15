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

            if (entity.WholeSalerName.Length > 30)
            {
                throw new ApplicationException("WholeSalerName too long. Max characters: 30");
            }

            if (entity.Country.Length > 30)
            {
                throw new ApplicationException("Country too long. Max characters: 30");
            }

            if (entity.Address.Length > 30)
            {
                throw new ApplicationException("Address too long. Max characters: 30");
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
            if (string.IsNullOrEmpty(entity.Name))
            {
                throw new ApplicationException("Brand name cannot be empty");
            }

            if (entity.Name.Length > 30)
            {
                throw new ApplicationException("Brand name too long. Max characters: 30");
            }

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
                         orderby paint.BasePrice ascending
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
                         orderby paint.Color ascending, paint.BasePrice descending
                         select new PaintWithBrandName()
                         {
                             BrandName = brand.Name,
                             PaintColor = paint.Color
                         };

            return result.ToList();
        }

        public List<BrandWithPaintPrice> GetAllPaintsPrice()
        {
            var result = from paint in paintRepository.ReadAll()
                         join brand in brandRepository.ReadAll()
                            on paint.BrandId equals brand.Id
                         group paint.BasePrice by brand.Id into grouped
                         select new BrandWithPaintPrice
                         {
                             BrandId = grouped.Key,
                             PaintPrice = grouped.Sum()
                         };

            return result.ToList();
        }
    }
}
