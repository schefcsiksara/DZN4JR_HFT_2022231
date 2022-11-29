using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DZN4JR_HFT_2022231.Models.DTO
{
    public class CustomerDTO
    {
        public string CustomerName { get; set; }
        public string Adress { get; set; }
        public string Email { get; set; }
        public int PhoneNumber { get; set; }
        public bool RegularCustomer { get; set; }
        public int FavoritePaintColorId { get; set; }
    }
}
