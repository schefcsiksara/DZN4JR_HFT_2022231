using ConsoleTools;
using DZN4JR_HFT_2022231.client.Infrastructure;
using DZN4JR_HFT_2022231.Models.DTO;
using DZN4JR_HFT_2022231.Models.Entities;
using DZN4JR_HFT_2022231.Models.Model;
using System;
using System.Collections.Generic;

namespace DZN4JR_HFT_2022231.client
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Waiting for server stand up. Please press enter when it's done.");
            Console.ReadLine();

            var paintHttpService = new HttpService("Paint", new Uri("http://localhost:49044/api/"));
            var customerHttpService = new HttpService("Customer", new Uri("http://localhost:49044/api/"));
            var brandHttpService = new HttpService("Brand", new Uri("http://localhost:49044/api/"));

            var menu = new ConsoleMenu();

            menu
                .Add("Get paint colors with brand name", () => GetPaintColorWithBrands(brandHttpService))
                .Add("Get paint colors ordered by price", () => GetPaintColorWithBrandsOrderedByPrice(brandHttpService))
                .Add("Get paint colors from Hungary", () => GetPaintColorWithBrandsFromHungary(brandHttpService))
                .Add("Get paint colors where rate is over 3", () => GetPaintColorWithBrandsRateOver3(brandHttpService))
                .Add("Get customer with favorite paint", () => GetCustomerWithFavoritePaints(customerHttpService))

                .Add("Create Paint", () => AddPaint(paintHttpService))
                .Add("Read Paint", () => GetPaint(paintHttpService))
                .Add("Delete Paint", () => DeletePaint(paintHttpService))
                .Add("GetAll Paint", () => GetAllPaint(paintHttpService))

                .Add("Create Customer", () => AddCustomer(customerHttpService))
                .Add("Read Customer", () => GetCustomer(customerHttpService))
                .Add("Delete Customer", () => DeleteCustomer(customerHttpService))
                .Add("GetAll Customer", () => GetAllCustomer(customerHttpService))

                .Add("Create Brand", () => AddBrand(brandHttpService))
                .Add("Read Brand", () => GetBrand(brandHttpService))
                .Add("Delete Brand", () => DeleteBrand(brandHttpService))
                .Add("GetAll Brand", () => GetAllBrand(brandHttpService))

                .Add("close", x => x.CloseMenu())
                .Configure(cfg =>
                {
                    cfg.ClearConsole = true;
                });

            menu.Show();
        }

        #region UTILS

        private static void ShowPaintWithBrandName(List<PaintWithBrandName> entities)
        {
            foreach (var entity in entities)
            {
                Console.WriteLine(entity);
            }
        }

        private static void ShowPaintWithBrandNameAndPrice(List<PaintWithBrandNameAndPrice> entities)
        {
            foreach (var entity in entities)
            {
                Console.WriteLine(entity);
            }
        }
        private static void ShowCustomerWithFavoritePaint(List<CustomerWithFavoritePaint> entities)
        {
            foreach (var entity in entities)
            {
                Console.WriteLine(entity);
            }
        }

        #endregion

        #region NON-CRUD METHODS

        private static void GetPaintColorWithBrands(HttpService httpService)
        {
            ShowPaintWithBrandName(httpService.GetAll<PaintWithBrandName>("GetPaintColorWithBrands"));
            Console.WriteLine("Press any key to continue");
            Console.ReadKey();
        }

        private static void GetPaintColorWithBrandsOrderedByPrice(HttpService httpService)
        {
            ShowPaintWithBrandNameAndPrice(httpService.GetAll<PaintWithBrandNameAndPrice>("GetPaintColorWithBrandsOrderedByPrice"));
            Console.WriteLine("Press any key to continue");
            Console.ReadKey();
        } 
        
        private static void GetPaintColorWithBrandsRateOver3(HttpService httpService)
        {
            ShowPaintWithBrandName(httpService.GetAll<PaintWithBrandName>("GetPaintColorWithBrandsRateOver3"));
            Console.WriteLine("Press any key to continue");
            Console.ReadKey();
        }        

        private static void GetPaintColorWithBrandsFromHungary(HttpService httpService)
        {
            ShowPaintWithBrandName(httpService.GetAll<PaintWithBrandName>("GetPaintColorWithBrandsFromHungary"));
            Console.WriteLine("Press any key to continue");
            Console.ReadKey();
        }

        private static void GetCustomerWithFavoritePaints(HttpService httpService)
        {
            ShowCustomerWithFavoritePaint(httpService.GetAll<CustomerWithFavoritePaint>("GetCustomerWithFavoritePaints"));
            Console.WriteLine("Press any key to continue");
            Console.ReadKey();
        }

        #endregion

        #region Paint

        private static void ShowPaint(List<Paint> entities)
        {
            foreach (var entity in entities)
            {
                Console.WriteLine(entity);
            }
        }

        private static void GetAllPaint(HttpService httpService)
        {
            ShowPaint(httpService.GetAll<Paint>());
            Console.WriteLine("Press any key to continue");
            Console.ReadKey();
        }

        private static void GetPaint(HttpService httpService)
        {
            Console.WriteLine("Choose a number: ");
            int id = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine(httpService.Get<Paint, int>(id));
            Console.WriteLine("Press any key to continue");
            Console.ReadKey();
        }

        private static void DeletePaint(HttpService httpService)
        {
            Console.WriteLine("Choose a number: ");
            int id = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine(httpService.Delete<int>(id));
            Console.WriteLine("Press any key to continue");
            Console.ReadKey();
        }

        private static void AddPaint(HttpService httpService)
        {
            PaintDTO customer = AddPaintHandler();
            httpService.Create(customer);
            Console.WriteLine("Press any key to continue");
            Console.ReadKey();
        }

        private static PaintDTO AddPaintHandler()
        {
            Console.WriteLine("Type");
            string Type = Console.ReadLine();
            Console.WriteLine("BasePrice");
            int BasePrice = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Volume");
            string Volume = Console.ReadLine();
            Console.WriteLine("Color");
            string Color = Console.ReadLine();
            Console.WriteLine("BrandId");
            int BrandId = Convert.ToInt32(Console.ReadLine());

            return new PaintDTO()
            {
                Type = Type,
                BasePrice = BasePrice,
                Volume = Volume,
                Color = Color,
                BrandId = BrandId,
            };
        }

        #endregion

        #region Customer

        private static void ShowCustomer(List<Customer> entities)
        {
            foreach (var entity in entities)
            {
                Console.WriteLine(entity);
            }
        }

        private static void GetAllCustomer(HttpService httpService)
        {
            ShowCustomer(httpService.GetAll<Customer>());
            Console.WriteLine("Press any key to continue");
            Console.ReadKey();
        }

        private static void GetCustomer(HttpService httpService)
        {
            Console.WriteLine("Choose a number: ");
            int id = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine(httpService.Get<Customer, int>(id));
            Console.WriteLine("Press any key to continue");
            Console.ReadKey();
        }

        private static void DeleteCustomer(HttpService httpService)
        {
            Console.WriteLine("Choose a number: ");
            int id = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine(httpService.Delete<int>(id));
            Console.WriteLine("Press any key to continue");
            Console.ReadKey();
        }

        private static void AddCustomer(HttpService httpService)
        {
            CustomerDTO customer = AddCustomerHandler();
            httpService.Create(customer);
            Console.WriteLine("Press any key to continue");
            Console.ReadKey();
        }

        private static CustomerDTO AddCustomerHandler()
        {
            Console.WriteLine("CustomerName");
            string CustomerName = Console.ReadLine();
            Console.WriteLine("Adress");
            string Adress = Console.ReadLine();
            Console.WriteLine("Email");
            string Email = Console.ReadLine();
            Console.WriteLine("FavoritePaintId");
            int FavoritePaintId = int.Parse(Console.ReadLine());

            return new CustomerDTO()
            {
                CustomerName = CustomerName,
                Adress = Adress,
                Email = Email,
                FavoritePaintId = FavoritePaintId
            };
        }

        #endregion

        #region Brand

        private static void ShowBrand(List<Brand> entities)
        {
            foreach (var entity in entities)
            {
                Console.WriteLine(entity);
            }
        }

        private static void GetAllBrand(HttpService httpService)
        {
            ShowBrand(httpService.GetAll<Brand>());
            Console.WriteLine("Press any key to continue");
            Console.ReadKey();
        }

        private static void GetBrand(HttpService httpService)
        {
            Console.WriteLine("Choose a number: ");
            int id = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine(httpService.Get<Brand, int>(id));
            Console.WriteLine("Press any key to continue");
            Console.ReadKey();
        }

        private static void DeleteBrand(HttpService httpService)
        {
            Console.WriteLine("Choose a number: ");
            int id = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine(httpService.Delete<int>(id));
            Console.WriteLine("Press any key to continue");
            Console.ReadKey();
        }

        private static void AddBrand(HttpService httpService)
        {
            BrandDTO brand = AddBrandHandler();
            httpService.Create(brand);
            Console.WriteLine("Press any key to continue");
            Console.ReadKey();
        }

        private static BrandDTO AddBrandHandler()
        {
            Console.WriteLine("Name");
            string Name = Console.ReadLine();
            Console.WriteLine("WholeSalerName");
            string WholeSalerName = Console.ReadLine();
            Console.WriteLine("Country");
            string Country = Console.ReadLine();
            Console.WriteLine("Address");
            string Address = Console.ReadLine();
            Console.WriteLine("Rating");
            int Rating = int.Parse(Console.ReadLine());

            return new BrandDTO()
            {
                Name = Name,
                WholeSalerName = WholeSalerName,
                Country = Country,
                Address = Address,
                Rating = Rating,
            };
        }

        #endregion
    }
}
