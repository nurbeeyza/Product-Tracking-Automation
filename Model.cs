using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Product_Tracking_Automation
{
    public class User
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public bool IsAdmin { get; set; }
        public bool IsActive { get; set; }
        public bool IsWaitingApprove { get; set; }
    }

    public class Brand
    {
        public string Name { get; set; }
        public Guid Id { get; set; }

    }

    public class Product
    {
        public Guid Id { get; set; }
        public string Serial { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Lenght { get; set; }
        public decimal Width { get; set; }
        public decimal Depth { get; set; }
        public decimal Weight { get; set; }
        public decimal Volume { get; set; }
        public Guid BrandId { get; set; }
        public string BrandName { get; set; }
    }
    public class ProductFilter
    {
        public string Serial { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal MinLenght { get; set; }
        public decimal MaxLenght { get; set; }
        public decimal MinWidth { get; set; }
        public decimal MaxWidth { get; set; }
        public decimal MinDepth { get; set; }
        public decimal? MaxDepth { get; set; }
        public decimal MinWeight { get; set; }
        public decimal MaxWeight { get; set; }
        public decimal MinVolume { get; set; }
        public decimal MaxVolume { get; set; }
        public string BrandName { get; set; }


    }
    public class Stock
    {
        public Guid Id { get; set; }
        public Guid ProductId { get; set; }
        public string StockCode { get; set; }

        public string ProductName { get; set; }

        public float Count { get; set; }
        public string Unit { get; set; }

    }

    public class Company
    {
        public Guid Id { get; set; }
        public string TaxNo { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }
        public string Adress { get; set; }

    }
    public class Supplier
    {
        public Guid Id { get; set; }
        public Guid ProductId { get; set; }
        public Guid CompanyId { get; set; }
        public float Price { get; set; }
        public string Currency { get; set; }
        public string ProductName { get; set; }
        public string CompanyName { get; set; }

    }
    public class SupplierFilter
    {
        public Guid? ProductId { get; set; } // Nullable Guid
        public Guid? CompanyId { get; set; } // Nullable Guid
        public string ProductName { get; set; }
        public string CompanyName { get; set; }
        public float? MinPrice { get; set; } // Nullable float
        public float? MaxPrice { get; set; } // Nullable float
        public string Currency { get; set; }
    }

}