using Microsoft.VisualStudio.TestTools.UnitTesting;
using Acme.Biz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Acme.Common;

namespace Acme.Biz.Tests
{
    [TestClass()]
    public class VendorTests
    {
        [TestMethod()]
        public void SendWelcomeEmail_ValidCompany_Success()
        {
            // Arrange
            var vendor = new Vendor();
            vendor.CompanyName = "ABC Corp";
            var expected = "Message sent: Hello ABC Corp";

            // Act
            var actual = vendor.SendWelcomeEmail("Test Message");

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void SendWelcomeEmail_EmptyCompany_Success()
        {
            // Arrange
            var vendor = new Vendor();
            vendor.CompanyName = "";
            var expected = "Message sent: Hello";

            // Act
            var actual = vendor.SendWelcomeEmail("Test Message");

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void SendWelcomeEmail_NullCompany_Success()
        {
            // Arrange
            var vendor = new Vendor();
            vendor.CompanyName = null;
            var expected = "Message sent: Hello";

            // Act
            var actual = vendor.SendWelcomeEmail("Test Message");

            // Assert
            Assert.AreEqual(expected, actual);
        }
        [TestMethod()]
        public void PlaceOrder_Success()
        {
            // Arrange
            var currentVendor = new Vendor();
            var currentProduct = new Product(1, "Saw", "");
            var expected = new OperationResult(true, 
                "Order from Acme, Inc\r\nProduct: 1-Tools\r\nQuantity: 2\r\nOrder Instructions: standard delivery" );

            // Act
            var actual = currentVendor.PlaceOrder(currentProduct, 2);

            // Assert
            Assert.AreEqual(expected.Success, actual.Success);
            Assert.AreEqual(expected.Message, actual.Message);

        }
        [TestMethod()]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void PlaceOrder_Failure_Zero_Quantity()
        {
            // Arrange
            var currentVendor = new Vendor();
            var currentProduct = new Product();
            currentProduct.ProductName = "Skull Smasher";

            // Act
            var actual = currentVendor.PlaceOrder(currentProduct, 0);

            // Assert
            // Expected Exception
        }
        
        [TestMethod()]
        [ExpectedException(typeof(ArgumentNullException))]
        public void PlaceOrder_Failure_No_Product()
        {
            // Arrange
            var currentVendor = new Vendor();

            // Act
            var actual = currentVendor.PlaceOrder(null, 42);

            // Assert
            // Expected Exception
        }
        [TestMethod()]
        public void PlaceOrder_With_Date_Overload()
        {
            // Arrange
            var currentVendor = new Vendor();
            var currentProduct = new Product(1, "Saw", "");
            var expected = new OperationResult(true, 
                "Order from Acme, Inc\r\nProduct: 1-Tools\r\nQuantity: 2\r\nDeliver By: 2020-01-23\r\nOrder Instructions: standard delivery");

            // Act
            var actual = currentVendor.PlaceOrder(currentProduct, 2, new DateTimeOffset(2020, 01, 23, 0, 0, 0, new TimeSpan(-7, 0, 0)));

            // Assert
            Assert.AreEqual(expected.Success, actual.Success);
            Assert.AreEqual(expected.Message, actual.Message);

        }
        [TestMethod()]
        public void PlaceOrder_With_IncAddress_SendCopy()
        {
            // Arrange
            var currentVendor = new Vendor();
            var currentProduct = new Product(1, "Saw", "");
            var expected = new OperationResult(true, 
                "Test With Address With Copy");

            // Act
            var actual = currentVendor.PlaceOrder(currentProduct, quantity: 2, Vendor.IncludeAddress.Yes, Vendor.SendCopy.Yes);

            // Assert
            Assert.AreEqual(expected.Success, actual.Success);
            Assert.AreEqual(expected.Message, actual.Message);

        }
        [TestMethod()]
        public void PlaceOrder_NoDeliveryDate()
        {
            // Arrange
            var currentVendor = new Vendor();
            var currentProduct = new Product(1, "Saw", "");
            var expected = new OperationResult(true, 
                "Order from Acme, Inc\r\nProduct: 1-Tools\r\nQuantity: 2\r\nOrder Instructions: leave out front");

            // Act
            var actual = currentVendor.PlaceOrder(currentProduct, quantity: 2, instructions: "leave out front");

            // Assert
            Assert.AreEqual(expected.Success, actual.Success);
            Assert.AreEqual(expected.Message, actual.Message);

        }
    }
}