using System;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;
using DZN4JR_HFT_2022231.Logic.Services;
using DZN4JR_HFT_2022231.Models.Entities;
using DZN4JR_HFT_2022231.Models.Model;
using DZN4JR_HFT_2022231.Repository.Repositories;
using DZN4JR_HFT_2022231.Repository.Interfaces;


namespace DZN4JR_HFT_2022231.Test
{
    [TestFixture]
    public class PaintShopTest
    {
        #region CRUD TEST

        [Test]
        public void CreateWithSuccess()
        {
            var brandBefore = new Brand()
            {
                Name = "Brand",
                WholeSalerName = "Brand",
                Country = "Brand",
                Address = "Brand",
                Rating = 5,
            };

            var brandAfter = new Brand()
            {
                Id = 1,
                Name = "Brand",
                WholeSalerName = "Brand",
                Country = "Brand",
                Address = "Brand",
                Rating = 5,
            };

            List<Paint> paints = new List<Paint>();

            //Arrange
            var paintRepository = new Mock<IPaintRepository>();
            var brandRepository = new Mock<IBrandRepository>();

            brandRepository.Setup(x => x.Create(It.IsAny<Brand>())).Returns(brandAfter);

            // Act
            var service = new BrandService(brandRepository.Object, paintRepository.Object);

            var result = service.Create(brandBefore);

            Assert.That(result, Is.SameAs(brandAfter));
            Assert.That(result, Is.Not.SameAs(brandBefore));
        }

        [Test]
        public void CreateWithTooLongName()
        {
            var brandBefore = new Brand()
            {
                Name = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa",
                WholeSalerName = "Brand",
                Country = "Brand",
                Address = "Brand",
                Rating = 5,
            };

            var brandAfter = new Brand()
            {
                Id = 1,
                Name = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa",
                WholeSalerName = "Brand",
                Country = "Brand",
                Address = "Brand",
                Rating = 5,
            };

            List<Paint> paints = new List<Paint>();

            //Arrange
            var paintRepository = new Mock<IPaintRepository>();
            var brandRepository = new Mock<IBrandRepository>();

            brandRepository.Setup(x => x.Create(It.IsAny<Brand>())).Returns(brandAfter);

            // Act
            var service = new BrandService(brandRepository.Object, paintRepository.Object);

            var exception = Assert.Throws<ApplicationException>(() => service.Create(brandBefore));
            Assert.That(exception.Message, Is.EqualTo("Brand name too long. Max characters: 30"));
        }

        [Test]
        public void CreateWithEmptyName()
        {
            var brandBefore = new Brand()
            {
                Name = "",
                WholeSalerName = "Brand",
                Country = "Brand",
                Address = "Brand",
                Rating = 5,
            };

            var brandAfter = new Brand()
            {
                Id = 1,
                Name = "",
                WholeSalerName = "Brand",
                Country = "Brand",
                Address = "Brand",
                Rating = 5,
            };

            List<Paint> paints = new List<Paint>();

            //Arrange
            var paintRepository = new Mock<IPaintRepository>();
            var brandRepository = new Mock<IBrandRepository>();

            brandRepository.Setup(x => x.Create(It.IsAny<Brand>())).Returns(brandAfter);

            // Act
            var service = new BrandService(brandRepository.Object, paintRepository.Object);

            var exception = Assert.Throws<ApplicationException>(() => service.Create(brandBefore));
            Assert.That(exception.Message, Is.EqualTo("Brand name cannot be empty"));
        }

        #endregion

        #region NON CRUD TEST

        [Test]
        public void GetCustomerWithFavoritePaint_WithoutDataTest()
        {
            //Arrange
            var paints = new List<Paint>();
            var customers = new List<Customer>();

            var paintRepository = new Mock<IPaintRepository>();
            var customerRepository = new Mock<ICustomerRepository>();

            paintRepository.Setup(x => x.ReadAll()).Returns(paints.AsQueryable());
            customerRepository.Setup(x => x.ReadAll()).Returns(customers.AsQueryable());

            //Act
            var service = new CustomerService(paintRepository.Object, customerRepository.Object);
            var result = service.GetCustomerWithFavoritePaints();

            //Assert
            Assert.IsNotNull(result);
            Assert.That(result.Count, Is.EqualTo(0));
        }

        [Test]
        [TestCaseSource(nameof(GetPaintColorWithBrandsTestData))]
        public void GetPaintColorWithBrandsWithExpectedDataTest(List<Paint> paints, List<Brand> brands, List<PaintWithBrandName> expectedData)
        {
            //Arrange
            var paintRepository = new Mock<IPaintRepository>();
            var brandRepository = new Mock<IBrandRepository>();

            paintRepository.Setup(x => x.ReadAll()).Returns(paints.AsQueryable());
            brandRepository.Setup(x => x.ReadAll()).Returns(brands.AsQueryable());

            //Act
            var service = new BrandService(brandRepository.Object, paintRepository.Object);
            var result = service.GetPaintColorWithBrands();
            var szin = result.Where(x => x.PaintColor.Equals("101E fehér")).FirstOrDefault();

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.NotNull(szin);
        }

        [Test]
        [TestCaseSource(nameof(GetPaintColorWithBrandNameAndPriceTestData))]
        public void GetPaintColorWithBrandsOrderedByPriceWithExpectedDataTest(List<Paint> paints, List<Brand> brands, List<PaintWithBrandNameAndPrice> expectedData)
        {
            //Arrange
            var paintRepository = new Mock<IPaintRepository>();
            var brandRepository = new Mock<IBrandRepository>();

            paintRepository.Setup(x => x.ReadAll()).Returns(paints.AsQueryable());
            brandRepository.Setup(x => x.ReadAll()).Returns(brands.AsQueryable());

            // Act
            var service = new BrandService(brandRepository.Object, paintRepository.Object);
            var result = service.GetPaintColorWithBrandsOrderedByPrice();

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result[0].PaintColor.Equals("118D fehér"), $"First is {result[0].PaintColor}");
        }

        [Test]
        public void GetPaintColorWithBrandsOrderedByPriceWithoutDataTest()
        {
            List<Paint> paints = new List<Paint>();
            List<Brand> brands = new List<Brand>();

            //Arrange
            var paintRepository = new Mock<IPaintRepository>();
            var brandRepository = new Mock<IBrandRepository>();

            paintRepository.Setup(x => x.ReadAll()).Returns(paints.AsQueryable());
            brandRepository.Setup(x => x.ReadAll()).Returns(brands.AsQueryable());

            // Act
            var service = new BrandService(brandRepository.Object, paintRepository.Object);
            var result = service.GetPaintColorWithBrandsOrderedByPrice();

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Count, Is.EqualTo(0));
        }

        [Test]
        [TestCaseSource(nameof(GetPaintColorWithBrandsTestData))]
        public void GetPaintColorWithBrandsRateOver3WithExpectedDataTest(List<Paint> paints, List<Brand> brands, List<PaintWithBrandName> expectedData)
        {
            //Arrange
            var paintRepository = new Mock<IPaintRepository>();
            var brandRepository = new Mock<IBrandRepository>();

            paintRepository.Setup(x => x.ReadAll()).Returns(paints.AsQueryable());
            brandRepository.Setup(x => x.ReadAll()).Returns(brands.AsQueryable());

            //Act
            var service = new BrandService(brandRepository.Object, paintRepository.Object);
            var result = service.GetPaintColorWithBrandsRateOver3();

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Count.Equals(3));
        }

        [Test]
        public void GetPaintColorWithBrandsRateOver3WithoutDataTest()
        {
            List<Paint> paints = new List<Paint>();
            List<Brand> brands = new List<Brand>();

            //Arrange
            var paintRepository = new Mock<IPaintRepository>();
            var brandRepository = new Mock<IBrandRepository>();

            paintRepository.Setup(x => x.ReadAll()).Returns(paints.AsQueryable());
            brandRepository.Setup(x => x.ReadAll()).Returns(brands.AsQueryable());

            //Act
            var service = new BrandService(brandRepository.Object, paintRepository.Object);
            var result = service.GetPaintColorWithBrandsRateOver3();

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Count, Is.EqualTo(0));
        }

        [Test]
        [TestCaseSource(nameof(GetPaintColorWithBrandsTestData))]
        public void GetPaintColorWithBrandsFromHungaryWithExpectedDataTest(List<Paint> paints, List<Brand> brands, List<PaintWithBrandName> expectedData)
        {
            //Arrange
            var paintRepository = new Mock<IPaintRepository>();
            var brandRepository = new Mock<IBrandRepository>();

            paintRepository.Setup(x => x.ReadAll()).Returns(paints.AsQueryable());
            brandRepository.Setup(x => x.ReadAll()).Returns(brands.AsQueryable());

            //Act
            var service = new BrandService(brandRepository.Object, paintRepository.Object);
            var result = service.GetPaintColorWithBrandsFromHungary();

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.IsNotNull(result.Where(x => x.PaintColor.Equals("101E fehér")).FirstOrDefault());
        }

        #endregion

        #region DATA FOR TEST

        static IEnumerable<TestCaseData> GetPaintColorWithBrandsTestData()
        {
            var result = new List<TestCaseData>();

            #region InitValues

            var tricolor = new Brand(11111, "Tricolor", "Tricolor Kft.", "Hungary", "3531 Miskolc, Damjanich János u. 8/1", 5);
            var dulux = new Brand(22222, "Dulux", "Dulux Bt.", "Hungary", "4531 Nyírpazony, Béke u. 22/A.", 5);
            var trikkuria = new Brand(33333, "Trikkuria", "Tikkurila Oyj", "Finland", "FI-01301 Vantaa, Finland", 4);
            var hera = new Brand(44444, "Héra", "Héra Kft.", "Hungary", "2536 Nyergesújfalu, Nagy L. u. 3.", 2);

            var paint1 = new Paint(1111, "falfesték", 4560, "5 liter", "101E fehér", 11111);
            var paint2 = new Paint(2222, "falfesték", 6250, "6 liter", "204SG zöld", 22222);
            var paint3 = new Paint(3333, "fafesték", 7590, "2.4 liter", "421KL fekete", 33333);
            var paint4 = new Paint(4444, "fémfesték", 4160, "1 liter", "118D fehér", 44444);

            #endregion

            result.Add(new TestCaseData(
                new List<Paint>() { paint1, paint2, paint3, paint4 },
                new List<Brand>() { tricolor, dulux, trikkuria, hera },
                new List<PaintWithBrandName>()
                {
                    new PaintWithBrandName(tricolor.Name, paint1.Color),
                    new PaintWithBrandName(dulux.Name, paint2.Color),
                    new PaintWithBrandName(trikkuria.Name, paint3.Color),
                    new PaintWithBrandName(hera.Name, paint4.Color),
                }
            ));

            return result;
        }

        static IEnumerable<TestCaseData> GetPaintColorWithBrandNameAndPriceTestData()
        {
            var result = new List<TestCaseData>();

            #region InitValues

            var tricolor = new Brand(11111, "Tricolor", "Tricolor Kft.", "Hungary", "3531 Miskolc, Damjanich János u. 8/1", 5);
            var dulux = new Brand(22222, "Dulux", "Dulux Bt.", "Hungary", "4531 Nyírpazony, Béke u. 22/A.", 5);
            var trikkuria = new Brand(33333, "Trikkuria", "Tikkurila Oyj", "Finland", "FI-01301 Vantaa, Finland", 4);
            var hera = new Brand(44444, "Héra", "Héra Kft.", "Hungary", "2536 Nyergesújfalu, Nagy L. u. 3.", 2);

            var paint1 = new Paint(1111, "falfesték", 4560, "5 liter", "101E fehér", 11111);
            var paint2 = new Paint(2222, "falfesték", 6250, "6 liter", "204SG zöld", 22222);
            var paint3 = new Paint(3333, "fafesték", 7590, "2.4 liter", "421KL fekete", 33333);
            var paint4 = new Paint(4444, "fémfesték", 4160, "1 liter", "118D fehér", 44444);

            #endregion

            result.Add(new TestCaseData(
                new List<Paint>() { paint1, paint2, paint3, paint4 },
                new List<Brand>() { tricolor, dulux, trikkuria, hera },
                new List<PaintWithBrandNameAndPrice>()
                {
                    new PaintWithBrandNameAndPrice(tricolor.Name, paint1.Color, paint1.BasePrice),
                    new PaintWithBrandNameAndPrice(dulux.Name, paint2.Color, paint2.BasePrice),
                    new PaintWithBrandNameAndPrice(trikkuria.Name, paint3.Color, paint3.BasePrice),
                    new PaintWithBrandNameAndPrice(hera.Name, paint4.Color, paint4.BasePrice),
                }
            ));

            return result;
        }
        #endregion
    }
}
