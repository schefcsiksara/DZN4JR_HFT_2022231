using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DZN4JR_HFT_2022231.Models.DTO
{
    public class PaintDTO
    {
        public string Type { get; set; }
        public int BasePrice { get; set; }
        public string Volume { get; set; }
        public string Color { get; set; }
        public int BrandId { get; set; }
    }
}
