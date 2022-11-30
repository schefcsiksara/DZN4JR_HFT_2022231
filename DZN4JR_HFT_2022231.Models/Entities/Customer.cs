using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DZN4JR_HFT_2022231.Models.Entities
{
    [Table("Customers")]
    public class Customer
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        [MaxLength(30)]
        public string CustomerName { get; set; }
        public string Adress { get; set; }
        public string Email { get; set; }
        public bool RegularCustomer { get; set; }
        public int FavoritePaintId { get; set; }

        [JsonIgnore]
        public virtual Paint FavoritePaint { get; set; }
        public Customer()
        {

        }
        public Customer(int customerId, string customerName, string adress, string email, bool regularCustomer, int favoritePaintId)
        {
            Id = customerId;
            CustomerName = customerName;
            Adress = adress;
            Email = email;
            RegularCustomer = regularCustomer;
            FavoritePaintId = favoritePaintId;
        }

        public override string ToString()
        {
            return $"{Id} - {CustomerName}, {Adress}";
        }
    }
}
