using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoryTaler.Models
{
    public class Product
    {
        public Guid ProductId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public ProductType ProductType { get; set; }
        public double Rating { get; set; }

        public Product() {
            ProductId= Guid.NewGuid();
            Name = "Some Product";
            Description = "Some Description";
            Rating = 1.5;
        }
	}
}
