using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DZN4JR_HFT_2022231.Models.Model
{
    public class BrandWithPaintPrice
    {
        public int BrandId { get; set; }
        public int PaintPrice { get; set; }

        public BrandWithPaintPrice()
        {

        }

        public BrandWithPaintPrice(int brandId, int paintPrice)
        {
            BrandId = brandId;
            PaintPrice = paintPrice;
        }

        public override string ToString()
        {
            return $"{BrandId} sum price is {PaintPrice}";
        }
    }
}
