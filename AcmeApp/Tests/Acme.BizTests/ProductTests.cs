using Microsoft.VisualStudio.TestTools.UnitTesting;
using Acme.Biz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acme.Biz.Tests
{
    [TestClass()]
    public class ProductTests
    {
        [TestMethod()]
        public void SayHelloTest()
        {
            //Arrange
            var currentProduct = new Product();
            currentProduct.ProductName = "Saw";
            currentProduct.ProductId = 1;
            currentProduct.Description = "15-inch steel blade hand saw";
            currentProduct.ProductVendor.CompanyName = "ABC Corp";
            
            var expected = "Hello Saw (1): 15-inch steel blade hand saw - Available on: ";
            //Act
            var actual = currentProduct.SayHello();

            //Assert
            Assert.AreEqual(expected, actual);
        }
        [TestMethod()]
        public void SayHello_ParameterizedConstructor()
        {
            //Arrange
            var currentProduct = new Product(1, "Saw", "15-inch steel blade hand saw");
            var expected = "Hello Saw (1): 15-inch steel blade hand saw - Available on: ";
            //Act
            var actual = currentProduct.SayHello();

            //Assert
            Assert.AreEqual(expected, actual);
        }
        [TestMethod()]
        public void SayHello_ObjectInitializer()
        {
            //Arrange
            var currentProduct = new Product
            {
                ProductId = 1,
                ProductName = "Saw",
                Description = "15-inch steel blade hand saw"
            };
            var expected = "Hello Saw (1): 15-inch steel blade hand saw - Available on: ";
            //Act
            var actual= currentProduct.SayHello();
            //Assert
            Assert.AreEqual(expected, actual);
        }
        [TestMethod()]
        public void Product_Null()
        {
            //Arrange
            Product currentProduct = null;
            var companyName = currentProduct?.ProductVendor?.CompanyName;

            string expected = null;
            //Act
            var actual = companyName;
            
            //Assert
            Assert.AreEqual(expected, actual);
        }
        [TestMethod()]
        public void ConvertMetersToInchesTest()
        {
            // Arrange
            var expected = 78.74;
            // Act
            var actual = Product.InchesPerMeter * 2;
            // Assert
            Assert.AreEqual(expected, actual);
        }
        [TestMethod()]
        public void MinimumPriceBulkTest()
        {
            // Arrange
            var bulkProduct = new Product(1, "Bulk Tools", "Lots of tools");       
            var expected = 1.99m;
            // Act
            var actual = bulkProduct.MinimumPrice;
            // Assert
            Assert.AreEqual(expected, actual);
        }
        [TestMethod()]
        public void MinimumPriceTest()
        {
            // Arrange
            var regularProduct = new Product();       
            var expected = 0.96m;
            // Act
            var actual = regularProduct.MinimumPrice;
            // Assert
            Assert.AreEqual(expected, actual);
        }
        [TestMethod()]
        public void ProductNameFormattingTest()
        {
            // Arrange
            var productNameSetWithSpaces = new Product();
            productNameSetWithSpaces.ProductName = "  Skull Smasher  ";
            
            var expected = "Skull Smasher";
            // Act
            var actual = productNameSetWithSpaces.ProductName;
            // Assert
            Assert.AreEqual(expected, actual);
        }
        [TestMethod()]
        public void ProductNameTooShortTest()
        {
            // Arrange
            var shortProductName = new Product();
            shortProductName.ProductName = "Ab";

            string expected = null;
            string expectedMessage = "Product Name must be longer than 2 characters and shorter than 20 characters.";
            // Act
            string actual = shortProductName.ProductName;
            string actualMessage = shortProductName.ValidationNessage;
            // Assert
            Assert.AreEqual(expected, actual);
            Assert.AreEqual(expectedMessage, actualMessage);
        }
        [TestMethod()]
        public void ProductNameTooLongTest()
        {
            // Arrange
            var longProductName = new Product();
            longProductName.ProductName = "Some Really Long Name That is Completely Ridiculous";

            string expected = null;
            string expectedMessage = "Product Name must be longer than 2 characters and shorter than 20 characters.";
            // Act
            string actual = longProductName.ProductName;
            string actualMessage = longProductName.ValidationNessage;
            // Assert
            Assert.AreEqual(expected, actual);
            Assert.AreEqual(expectedMessage, actualMessage);
        }
        [TestMethod()]
        public void ProductNameJustRight()
        {
            // Arrange
            var validProductName = new Product();
            validProductName.ProductName = "Just the right name";

            string expected = "Just the right name";
            string expectedMessage = null;
            // Act
            string actual = validProductName.ProductName;
            string actualMessage = validProductName.ValidationNessage;
            // Assert
            Assert.AreEqual(expected, actual);
            Assert.AreEqual(expectedMessage, actualMessage);
        }
        [TestMethod()]
        public void Category_DefaultValue()
        {
            // Arrange
            var currentProduct = new Product();

            string expected = "Tools";
 
            // Act
            string actual = currentProduct.Category;
 
            // Assert
            Assert.AreEqual(expected, actual);
        }
        [TestMethod()]
        public void Description_DefaultValue()
        {
            // Arrange
            var currentProduct = new Product();

            string expected = "No description";
 
            // Act
            string actual = currentProduct.Description;
 
            // Assert
            Assert.AreEqual(expected, actual);
        }
        [TestMethod()]
        public void Category_NewValue()
        {
            // Arrange
            var currentProduct = new Product();
            currentProduct.Category = "Garden";

            string expected = "Garden";
 
            // Act
            string actual = currentProduct.Category;
 
            // Assert
            Assert.AreEqual(expected, actual);
        }
        [TestMethod()]
        public void Description_NewValue()
        {
            // Arrange
            var currentProduct = new Product();
            currentProduct.Description = "Bathroom and kitchen cleaner";

            string expected = "Bathroom and kitchen cleaner";
 
            // Act
            string actual = currentProduct.Description;
 
            // Assert
            Assert.AreEqual(expected, actual);
        }
        [TestMethod()]
        public void ProductCodeLambdaConcatTest()
        {
            // Arrange
            var currentProduct = new Product();
            currentProduct.Category = "Garden";
            currentProduct.SequenceNumber = 21;

            string expected = "21-Garden";
 
            // Act
            string actual = currentProduct.ProductCode;
 
            // Assert
            Assert.AreEqual(expected, actual);
        }
    }
}