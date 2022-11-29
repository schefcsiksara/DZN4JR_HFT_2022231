using DZN4JR_HFT_2022231.Logic.Interfaces;
using DZN4JR_HFT_2022231.Models.Entities;
using DZN4JR_HFT_2022231.Repository.Interfaces;
using DZN4JR_HFT_2022231.Repository.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DZN4JR_HFT_2022231.Logic.Services
{
    public class CustomerService : ICustomerService
    {
        private ICustomerRepository repository;

        public CustomerService(ICustomerRepository repository)
        {
            this.repository = repository;
        }

        public Customer Create(Customer entity)
        {
            return repository.Create(entity);
        }

        public void Delete(int id)
        {
            var entity = Read(id);

            repository.Delete(entity);
        }

        public Customer Read(int id)
        {
            return repository.Read(id);
        }

        public List<Customer> ReadAll()
        {
            return repository.ReadAll().ToList();
        }

        public Customer Update(Customer entity)
        {
            var oldEntity = Read(entity.Id);

            oldEntity = entity;

            return repository.Update(entity);
        }
    }
}
