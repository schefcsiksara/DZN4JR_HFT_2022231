using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace DZN4JR_HFT_2022231.Models.Entities
{
    [Table("Paints")]
    public class Paint
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Type { get; set; }

        public int BasePrice { get; set; }
        public string Volume { get; set; }
        public string Color { get; set; }

        public int BrandId { get; set; }

       
        public virtual Brand Brand { get; set; }
        public virtual IEnumerable<Customer> Customers { get; set; }

        public Paint()
        {
        }

        public Paint(int id, string type, int basePrice, int brandId)
        {
            Id = id;
            Type = type;
            BasePrice = basePrice;
            BrandId = brandId;
        }

        public Paint(int id, string type, int basePrice, Brand brand)
        {
            Id = id;
            Type = type;
            BasePrice = basePrice;
            BrandId = brand.Id;
            Brand = brand;
        }

        public override string ToString()
        {
            return $"Id: {Id}; Name: {Type}; BasePrice: {BasePrice}; BrandId: {BrandId}";
        }
    }
}
