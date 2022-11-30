using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DZN4JR_HFT_2022231.Models.Model
{
    public class CustomerWithFavoritePaint
    {
        public string CustomerName { get; set; }
        public string FavoritePaint { get; set; }

        public CustomerWithFavoritePaint()
        {

        }

        public CustomerWithFavoritePaint(string customerName, string favoritePaint)
        {
            CustomerName = customerName;
            FavoritePaint = favoritePaint;
        }

        public override string ToString()
        {
            return $"{CustomerName} {FavoritePaint}";
        }
    }
}
