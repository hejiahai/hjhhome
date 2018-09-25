using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MvcMovie.Models
{

    public class Product
    {
        public int ID { get; set; }
        public string ProductName { get; set; }

        public DateTime ProductDate { get; set; }

        public decimal Price { get; set; }
        public int TypeID { get; set; }
        public ProductType ProductType { get; set; }
    }
    public class ProductType
    {
        public int ID { get; set; }
        public string TypeName { get; set; }

        public ICollection<Product> product { get; set; }//一种类型可能有多个商品，所以是集合
    }
    public class ProductViewModel
    {
        public int ID { get; set; }
        public string ProductName { get; set; }

        public DateTime ProductDate { get; set; }

        public decimal Price { get; set; }

        public string TypeName { get; set; }
    }
}
