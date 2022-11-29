using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DZN4JR_HFT_2022231.Models.Model
{
    public class PaintWithBrandName
    {
        public string BrandName { get; set; }
        public string PaintColor { get; set; }

        public PaintWithBrandName()
        {

        }

        public PaintWithBrandName(string brandName, string paintColor)
        {
            BrandName = brandName;
            PaintColor = paintColor;
        }

        public override string ToString()
        {
            return $"{BrandName} {PaintColor}";
        }
    }
}
