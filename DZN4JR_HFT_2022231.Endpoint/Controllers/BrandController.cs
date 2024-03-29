﻿using DZN4JR_HFT_2022231.Logic.Interfaces;
using DZN4JR_HFT_2022231.Models.DTO;
using DZN4JR_HFT_2022231.Models.Entities;
using DZN4JR_HFT_2022231.Models.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using TRZJ1J_HFT_2022231.Endpoint.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DZN4JR_HFT_2022231.Endpoint.Controllers
{
    //[Route("api/[controller]/[action]")]
    [Route("[controller]")]
    [ApiController]
    public class BrandController : ControllerBase
    {
        private IBrandService service;
        IHubContext<SignalRHub> hub;

        public BrandController(IBrandService service, IHubContext<SignalRHub> hub)
        {
            this.service = service;
            this.hub = hub;
        }


        // GET: api/Brand
        [HttpGet]
        //[ActionName("GetAll")]
        public IEnumerable<Brand> ReadAll()
        {
            return service.ReadAll();
        }

        // GET api/Brand/5
        [HttpGet("{id}")]
        //[ActionName("Get")]
        public Brand Read(int id)
        {
            return service.Read(id);
        }

        // POST api/Brand
        [HttpPost]
        //[ActionName("Create")]
        public APIResult Post([FromBody] Brand model)
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
                    Rating = Convert.ToInt32(model.Rating),
                });
                this.hub.Clients.All.SendAsync("BrandCreated", model);
            }
            catch (System.Exception ex)
            {
                result.IsSuccess = false;
                throw;
            }

            return result;
        }

        // PUT api/Brand/5
        [HttpPut()]
        //[ActionName("Update")]
        public APIResult Put([FromBody] Brand model)
        {
            var result = new APIResult(true);

            try
            {
                service.Update(model);

                this.hub.Clients.All.SendAsync("BrandUpdated", model);
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
        //[ActionName("Delete")]
        public APIResult Delete(int id)
        {
            var result = new APIResult(true);

            try
            {
                var delete = service.Read(id);
                service.Delete(id);
                this.hub.Clients.All.SendAsync("BrandDeleted", delete);
            }
            catch (System.Exception ex)
            {
                result.IsSuccess = false;
                throw;
            }

            return result;
        }

        [HttpGet("getpaintcolorwithbrands")]
        public List<PaintWithBrandName> GetPaintColorWithBrands()
        {
            return service.GetPaintColorWithBrands();
        }

        [HttpGet("getpaintcolorwithbrandorderedbyprice")]
        public List<PaintWithBrandNameAndPrice> GetPaintColorWithBrandsOrderedByPrice()
        {
            return service.GetPaintColorWithBrandsOrderedByPrice();
        }

        [HttpGet("getpaintcolorwithbrandsrateover3")]
        public List<PaintWithBrandName> GetPaintColorWithBrandsRateOver3()
        {
            return service.GetPaintColorWithBrandsRateOver3();
        }

        [HttpGet("getpaintcolorwithbrandsfromhungary")]
        public List<PaintWithBrandName> GetPaintColorWithBrandsFromHungary()
        {
            return service.GetPaintColorWithBrandsFromHungary();
        }

        [HttpGet("getallpaintsprice")]
        public List<BrandWithPaintPrice> GetAllPaintsPrice()
        {
            return service.GetAllPaintsPrice();
        }
    }
}
