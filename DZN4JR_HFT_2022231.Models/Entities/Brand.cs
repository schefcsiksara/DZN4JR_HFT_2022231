﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace DZN4JR_HFT_2022231.Models.Entities
{
    [Table("Brands")]
    public class Brand
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [MaxLength(30)]
        public string Name { get; set; }
        [MaxLength(30)]
        public string WholeSalerName { get; set; }
        [MaxLength(30)]
        public string Country { get; set; }
        [MaxLength(30)]
        public string Address { get; set; }
        public int Rating { get; set; }


        [JsonIgnore]
        public virtual IEnumerable<Paint> Paints { get; set; }

        public Brand()
        {
        }

        public Brand(int id, string name, string wholeSalerName, string country, string address, int rating)
        {
            Id = id;
            Name = name;
            WholeSalerName = wholeSalerName;
            Country = country;
            Address = address;
            Rating = rating;
        }

        public override string ToString()
        {
            return $"Id: {Id}; Name: {Name}";
        }
    }
}
