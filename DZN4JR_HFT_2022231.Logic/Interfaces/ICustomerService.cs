using DZN4JR_HFT_2022231.Models.Entities;
using DZN4JR_HFT_2022231.Models.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DZN4JR_HFT_2022231.Logic.Interfaces
{
    public interface ICustomerService
    {
        Customer Create(Customer entity);
        Customer Read(int id);
        List<Customer> ReadAll();
        Customer Update(Customer entity);
        void Delete(int id);
        List<CustomerWithFavoritePaint> GetCustomerWithFavoritePaints();
    }
}
