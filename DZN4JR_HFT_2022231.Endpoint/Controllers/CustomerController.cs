using DZN4JR_HFT_2022231.Logic.Interfaces;
using DZN4JR_HFT_2022231.Models.DTO;
using DZN4JR_HFT_2022231.Models.Entities;
using DZN4JR_HFT_2022231.Models.Model;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DZN4JR_HFT_2022231.Endpoint.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private ICustomerService service;

        public CustomerController(ICustomerService service)
        {
            this.service = service;
        }


        // GET: api/Paint
        [HttpGet]
        [ActionName("GetAll")]
        public IEnumerable<Customer> Get()
        {
            return service.ReadAll();
        }

        // GET api/Paint/5
        [HttpGet("{id}")]
        [ActionName("Get")]
        public Customer Get(int id)
        {
            return service.Read(id);
        }

        // POST api/Paint
        [HttpPost]
        [ActionName("Create")]
        public APIResult Post([FromBody] CustomerDTO model)
        {
            var result = new APIResult(true);

            try
            {
                service.Create(new Customer()
                {
                    CustomerName = model.CustomerName,
                    Adress = model.Adress,
                    Email = model.Email,
                    PhoneNumber = model.PhoneNumber,
                    RegularCustomer = model.RegularCustomer,
                });
            }
            catch (System.Exception ex)
            {
                result.IsSuccess = false;
                throw;
            }

            return result;
        }

        // PUT api/Paint/5
        [HttpPut("{id}")]
        [ActionName("Update")]
        public APIResult Put(int id, [FromBody] CustomerDTO model)
        {
            var result = new APIResult(true);

            try
            {
                var oldCustomer = Get(id);

                var newCustomer = new Customer()
                {
                    CustomerName = model.CustomerName,
                    Adress = model.Adress,
                    Email = model.Email,
                    PhoneNumber = model.PhoneNumber,
                    RegularCustomer = model.RegularCustomer,
                };

                oldCustomer = newCustomer;

                service.Update(oldCustomer);
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
    }
}
