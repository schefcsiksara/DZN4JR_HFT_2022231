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
    public class CustomerService : ICustomerService
    {
        private IPaintRepository paintRepository;
        private ICustomerRepository customerRepository;

        public CustomerService(IPaintRepository paintRepository, ICustomerRepository customerRepository)
        {
            this.paintRepository = paintRepository;
            this.customerRepository = customerRepository;
        }

        public CustomerService(ICustomerRepository repository)
        {
            this.customerRepository = repository;
        }

        public Customer Create(Customer entity)
        {
            if (string.IsNullOrEmpty(entity.CustomerName))
            {
                throw new ApplicationException("Name cannot be empty");
            }

            if (string.IsNullOrEmpty(entity.Adress))
            {
                throw new ApplicationException("Address cannot be empty");
            }

            if (string.IsNullOrEmpty(entity.Email))
            {
                throw new ApplicationException("Email cannot be empty");
            }

            if (entity.CustomerName.Length > 30)
            {
                throw new ApplicationException("Name cannot be more than 30 characters");
            }

            if (entity.Adress.Length > 30)
            {
                throw new ApplicationException("Adress cannot be more than 30 characters");
            }

            if (entity.Email.Length > 30)
            {
                throw new ApplicationException("Email cannot be more than 30 characters");
            }


            return customerRepository.Create(entity);
        }

        public void Delete(int id)
        {
            var entity = Read(id);

            if (entity == null)
            {
                throw new ApplicationException("There is no brand with the given id");
            }

            customerRepository.Delete(entity);
        }

        public Customer Read(int id)
        {
            return customerRepository.Read(id);
        }

        public List<Customer> ReadAll()
        {
            return customerRepository.ReadAll().ToList();
        }

        public Customer Update(Customer entity)
        {
            if (string.IsNullOrEmpty(entity.CustomerName))
            {
                throw new ApplicationException("Name cannot be empty");
            }

            if (string.IsNullOrEmpty(entity.Adress))
            {
                throw new ApplicationException("Address cannot be empty");
            }

            if (string.IsNullOrEmpty(entity.Email))
            {
                throw new ApplicationException("Email cannot be empty");
            }

            if (entity.CustomerName.Length > 30)
            {
                throw new ApplicationException("Name cannot be more than 30 characters");
            }

            if (entity.Adress.Length > 30)
            {
                throw new ApplicationException("Adress cannot be more than 30 characters");
            }

            if (entity.Email.Length > 30)
            {
                throw new ApplicationException("Email cannot be more than 30 characters");
            }

            var oldEntity = Read(entity.Id);

            oldEntity = entity;

            return customerRepository.Update(entity);
        }

        public List<CustomerWithFavoritePaint> GetCustomerWithFavoritePaints()
        {
            var result = from customer in customerRepository.ReadAll()
                         join paint in paintRepository.ReadAll()
                            on customer.Id equals paint.Id
                         select new CustomerWithFavoritePaint()
                         {
                             CustomerName = customer.CustomerName,
                             FavoritePaint = paint.Color
                         };

            return result.ToList();
        }

    }
}
