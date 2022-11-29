using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DZN4JR_HFT_2022231.Models.Model
{
    public class PaintWithBrandNameAndPrice
    {
        public string BrandName { get; set; }
        public string PaintColor { get; set; }
        public int PaintPrice { get; set; }

        public override string ToString()
        {
            return $"{BrandName}, {PaintColor}, {PaintPrice}";
        }
    }
}
