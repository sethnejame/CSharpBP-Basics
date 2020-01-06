using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Acme.Common;

namespace Acme.Biz
{   
    
    /// <summary>
    /// Manages products carried in inventory
    /// </summary>
    public class Product
    {
        public const double InchesPerMeter = 39.37;

        public readonly decimal MinimumPrice;
        public Product()
        {
            this.MinimumPrice = 0.96M;
            this.Category = "Tools";
            Console.WriteLine("Product instance created. . .");
            // this.ProductVendor = new Vendor();
        }
        public Product(int productId, string productName, string description) : this()
        {
            this.ProductId = productId;
            this.ProductName = productName;
            this.Description = description;

            if (ProductName.StartsWith("Bulk"))
            {
                this.MinimumPrice = 1.99M;
            }

            Console.WriteLine("Product instance has a name: " + ProductName);
        }
        private string productName;

        public string ProductName
        {
            get { return productName?.Trim(); }
            set
            {
                if (value.Length <= 2 || value.Length > 20)
                {
                    ValidationNessage = "Product Name must be longer than 2 characters and shorter than 20 characters.";
                }
                else
                {
                    productName = value;
                }
            }
        }

        public string ValidationNessage { get; set; }
        
        public string Description { get; set; } = "No description";
        private int productId;

        internal string Category { get; set; }
        public int SequenceNumber { get; set; } = 1;
        public int ProductId { get; set; } 

        private Vendor productVendor;
        public Vendor ProductVendor
        {
            get
            {
                productVendor ??= new Vendor();
                return productVendor;
            }
            set { productVendor = value;  }
        }

        private DateTime? availabilityDate;
        public DateTime? AvailabilityDate
        {
            get => availabilityDate;
            set => availabilityDate = value;
        }

        public string ProductCode => this.SequenceNumber + "-" + this.Category;

        public string SayHello()
        {
            //var vendor = new Vendor();
            //vendor.SendWelcomeEmail("Message from Hell");
            
            var emailService = new EmailService();
            emailService.SendMessage("You're fired!", this.ProductName, "sethnejame@gmail.com");
            var result = LoggingService.LogAction("saying hello");
            
            return "Hello " + ProductName +
                   " (" + ProductId + "): " +
                   Description + " - " +
                "Available on: " +
                AvailabilityDate?.ToShortDateString();
        }

    }
}
