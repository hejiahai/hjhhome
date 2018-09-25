using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcMovie.Models
{
    public class subText
    {
        public int ID { get; set; }
        public string ProductName { get; set; }

        public DateTime ProductDate { get; set; }

        public decimal Price { get; set; }
        public int TypeID { get; set; }
    }
}
