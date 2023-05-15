using DZN4JR_HFT_2022231.Logic.Interfaces;
using DZN4JR_HFT_2022231.Models.DTO;
using DZN4JR_HFT_2022231.Models.Entities;
using DZN4JR_HFT_2022231.Models.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System.Collections.Generic;
using TRZJ1J_HFT_2022231.Endpoint.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DZN4JR_HFT_2022231.Endpoint.Controllers
{
    //[Route("api/[controller]/[action]")]
    [Route("[controller]")]
    [ApiController]
    public class PaintController : ControllerBase
    {
        private IPaintService service;
        IHubContext<SignalRHub> hub;

        public PaintController(IPaintService service, IHubContext<SignalRHub> hub)
        {
            this.service = service;
            this.hub = hub;
        }


        // GET: api/Paint
        [HttpGet]
        //[ActionName("GetAll")]
        public IEnumerable<Paint> ReadAll()
        {
            return service.ReadAll();
        }

        // GET api/Paint/5
        [HttpGet("{id}")]
        //[ActionName("Get")]
        public Paint Read(int id)
        {
            return service.Read(id);
        }

        // POST api/Paint
        [HttpPost]
        //[ActionName("Create")]
        public APIResult Post([FromBody] Paint model)
        {
            var result = new APIResult(true);

            try
            {
                service.Create(new Paint()
                {
                    Type = model.Type,
                    BasePrice = model.BasePrice,
                    Volume = model.Volume,
                    Color = model.Color,
                    BrandId = model.BrandId,
                });

                this.hub.Clients.All.SendAsync("PaintCreated", model);
            }
            catch (System.Exception ex)
            {
                result.IsSuccess = false;
                throw;
            }

            return result;
        }

        // PUT api/Paint/5
        [HttpPut()]
        //[ActionName("Update")]
        public APIResult Put([FromBody] Paint model)
        {
            var result = new APIResult(true);

            try
            {
                service.Update(model);

                this.hub.Clients.All.SendAsync("PaintUpdated", model);
            }
            catch (System.Exception ex)
            {
                result.IsSuccess = false;
                throw;
            }

            return result;
        }

        // DELETE api/Paint/5
        [HttpDelete("{id}")]
        //[ActionName("Delete")]
        public APIResult Delete(int id)
        {
            var result = new APIResult(true);

            try
            {
                var delete = service.Read(id);
                service.Delete(id);
                this.hub.Clients.All.SendAsync("PaintDeleted", delete);
            }
            catch (System.Exception ex)
            {
                result.IsSuccess = false;
                throw;
            }

            return result;
        }
    }
}
