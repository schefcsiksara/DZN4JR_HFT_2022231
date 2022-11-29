using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DZN4JR_HFT_2022231.Models.Entities
{
    public class Customer
    {
        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
        public string Adresse { get; set; }
        public string Email { get; set; }
        public bool RegularCustomer { get; set; }

        public Customer(int customerId, string customerName, string adresse, string email, bool regularCustomer)
        {
            CustomerId = customerId;
            CustomerName = customerName;
            Adresse = adresse;
            Email = email;
            RegularCustomer = regularCustomer;
        }
       
    }
}
