using DZN4JR_HFT_2022231.Logic.Interfaces;
using DZN4JR_HFT_2022231.Models.DTO;
using DZN4JR_HFT_2022231.Models.Entities;
using DZN4JR_HFT_2022231.Models.Model;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DZN4JR_HFT_2022231.Endpoint.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class BrandController : ControllerBase
    {
        private IBrandService service;

        public BrandController(IBrandService service)
        {
            this.service = service;
        }


        // GET: api/Brand
        [HttpGet]
        [ActionName("GetAll")]
        public IEnumerable<Brand> Get()
        {
            return service.ReadAll();
        }

        // GET api/Brand/5
        [HttpGet("{id}")]
        [ActionName("Get")]
        public Brand Get(int id)
        {
            return service.Read(id);
        }

        // POST api/Brand
        [HttpPost]
        [ActionName("Create")]
        public APIResult Post([FromBody] BrandDTO model)
        {
            var result = new APIResult(true);

            try
            {
                service.Create(new Brand()
                {
                    Name = model.Name,
                    WholeSalerName = model.WholeSalerName,
                    Country = model.Country,
                    Address = model.Address,
                    Rating = model.Rating,
                });
            }
            catch (System.Exception ex)
            {
                result.IsSuccess = false;
                throw;
            }

            return result;
        }

        // PUT api/Brand/5
        [HttpPut("{id}")]
        [ActionName("Update")]
        public APIResult Put(int id, [FromBody] BrandDTO model)
        {
            var result = new APIResult(true);

            try
            {
                var oldBrand = Get(id);

                var newBrand = new Brand()
                {
                    Name = model.Name,
                    WholeSalerName = model.WholeSalerName,
                    Country = model.Country,
                    Address = model.Address,
                    Rating = model.Rating,
                };

                oldBrand = newBrand;

                service.Update(oldBrand);
            }
            catch (System.Exception ex)
            {
                result.IsSuccess = false;
                throw;
            }

            return result;
        }

        // DELETE api/Brand/5
        [HttpDelete("{id}")]
        [ActionName("Delete")]
        public APIResult Delete(int id)
        {
            var result = new APIResult(true);

            try
            {
                service.Delete(id);
            }
            catch (System.Exception ex)
            {
                result.IsSuccess = false;
                throw;
            }

            return result;
        }

        [HttpGet]
        [ActionName("GetPaintColorWithBrands")]
        public List<PaintWithBrandName> GetPaintColorWithBrands()
        {
            return service.GetPaintColorWithBrands();
        }

        [HttpGet]
        [ActionName("GetPaintColorWithBrandsOrderedByPrice")]
        public List<PaintWithBrandNameAndPrice> GetPaintColorWithBrandsOrderedByPrice()
        {
            return service.GetPaintColorWithBrandsOrderedByPrice();
        }

        [HttpGet]
        [ActionName("GetPaintColorWithBrandsRateOver3")]
        public List<PaintWithBrandName> GetPaintColorWithBrandsRateOver3()
        {
            return service.GetPaintColorWithBrandsRateOver3();
        }

        [HttpGet]
        [ActionName("GetPaintColorWithBrandsFromHungary")]
        public List<PaintWithBrandName> GetPaintColorWithBrandsFromHungary()
        {
            return service.GetPaintColorWithBrandsFromHungary();
        }

        [HttpGet]
        [ActionName("GetAllPaintsPrice")]
        public List<BrandWithPaintPrice> GetAllPaintsPrice()
        {
            return service.GetAllPaintsPrice();
        }
    }
}
