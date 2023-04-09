using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? Price { get; set; }
        public DateTime DueDate { get; set; }
        public DateTime PurchaseDate { get; set; }
        public string? PhotoReference { get; set; }
    }
}
