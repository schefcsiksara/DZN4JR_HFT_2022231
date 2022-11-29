using DZN4JR_HFT_2022231.Models.Entities;
using DZN4JR_HFT_2022231.Models.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DZN4JR_HFT_2022231.Logic.Interfaces
{
    public interface IBrandService
    {
        Brand Create(Brand entity);
        Brand Read(int id);
        List<Brand> ReadAll();
        Brand Update(Brand entity);
        void Delete(int id);
        public List<PaintWithBrandName> GetPaintColorWithBrands();
        public List<PaintWithBrandNameAndPrice> GetPaintColorWithBrandsOrderedByPrice();
        public List<PaintWithBrandName> GetPaintColorWithBrandsRateOver3();
        public List<PaintWithBrandName> GetPaintColorWithBrandsFromHungary();
    }
}
