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
    [Route("[controller]")]
    //[Route("api/[controller]/[action]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private ICustomerService service;
        IHubContext<SignalRHub> hub;

        public CustomerController(ICustomerService service, IHubContext<SignalRHub> hub)
        {
            this.service = service;
            this.hub = hub;
        }


        // GET: api/Paint
        [HttpGet]
        //[ActionName("GetAll")]
        public IEnumerable<Customer> ReadAll()
        {
            return service.ReadAll();
        }

        // GET api/Paint/5
        [HttpGet("{id}")]
        //[ActionName("Get")]
        public Customer Read(int id)
        {
            return service.Read(id);
        }

        // POST api/Paint
        [HttpPost]
        //[ActionName("Create")]
        public APIResult Post([FromBody] Customer model)
        {
            var result = new APIResult(true);

            try
            {
                service.Create(new Customer()
                {
                    CustomerName = model.CustomerName,
                    Adress = model.Adress,
                    Email = model.Email,
                    RegularCustomer = true,
                    FavoritePaintId = model.FavoritePaintId,
                });
                this.hub.Clients.All.SendAsync("CustomerCreated", model);
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
        public APIResult Put([FromBody] Customer model)
        {
            var result = new APIResult(true);

            try
            {
                service.Update(model);

                this.hub.Clients.All.SendAsync("CustomerUpdated", model);
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
                this.hub.Clients.All.SendAsync("CustomerDeleted", delete);
            }
            catch (System.Exception ex)
            {
                result.IsSuccess = false;
                throw;
            }

            return result;
        }
        /*
        [HttpGet]
        [ActionName("GetCustomerWithFavoritePaints")]
        public List<CustomerWithFavoritePaint> GetCustomerWithFavoritePaints()
        {
            return service.GetCustomerWithFavoritePaints();
        }*/
    }
}
