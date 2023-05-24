using DZN4JR_HFT_2022231.Models.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace DZN4JR_HFT_2022231.WPFClient
{
    /// <summary>
    /// Interaction logic for NonCrudWindow.xaml
    /// </summary>
    public partial class NonCrudWindow : Window
    {
        string baseURL = "http://localhost:49044/";

        public List<PaintWithBrandName> PaintColorWithBrands;
        public List<PaintWithBrandNameAndPrice> PaintColorWithBrandsOrderedByPrice;
        public List<PaintWithBrandName> PaintColorWithBrandsRateOver3;
        public List<PaintWithBrandName> PaintColorWithBrandsFromHungary;
        public List<BrandWithPaintPrice> AllPaintsPrice;
        public List<CustomerWithFavoritePaint> CustomerWithFavoritePaints;

        public NonCrudWindow()
        {
            InitializeComponent();
            PaintColorWithBrands = WebApi.GetCall(getURL("brand/getpaintcolorwithbrands")).Result.Content.ReadAsAsync<List<PaintWithBrandName>>().Result;
            PaintColorWithBrandsOrderedByPrice = WebApi.GetCall(getURL("brand/getpaintcolorwithbrandorderedbyprice")).Result.Content.ReadAsAsync<List<PaintWithBrandNameAndPrice>>().Result;
            PaintColorWithBrandsRateOver3 = WebApi.GetCall(getURL("brand/getpaintcolorwithbrandsrateover3")).Result.Content.ReadAsAsync<List<PaintWithBrandName>>().Result;
            PaintColorWithBrandsFromHungary = WebApi.GetCall(getURL("brand/getpaintcolorwithbrandsfromhungary")).Result.Content.ReadAsAsync<List<PaintWithBrandName>>().Result;
            AllPaintsPrice = WebApi.GetCall(getURL("brand/getallpaintsprice")).Result.Content.ReadAsAsync<List<BrandWithPaintPrice>>().Result;
            CustomerWithFavoritePaints = WebApi.GetCall(getURL("customer/getcustomerwithfavoritepaints")).Result.Content.ReadAsAsync<List<CustomerWithFavoritePaint>>().Result;

            paintColorWithBrands.ItemsSource = PaintColorWithBrands;
            paintColorWithBrandsOrderedByPrice.ItemsSource = PaintColorWithBrandsOrderedByPrice;
            paintColorWithBrandsRateOver3.ItemsSource = PaintColorWithBrandsRateOver3;
            paintColorWithBrandsFromHungary.ItemsSource = PaintColorWithBrandsFromHungary;
            allPaintsPrice.ItemsSource = AllPaintsPrice;
            customerWithFavoritePaints.ItemsSource = CustomerWithFavoritePaints;
        }


        public string getURL(string url)
        {
            return baseURL + url;
        }
    }
}
