﻿using System.ComponentModel.DataAnnotations;

namespace ProductService.Dtos
{
    public class ProductDto
    {        
        public int Id { get; set; }        
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Cost { get; set; }
        public bool InStock { get; set; }
    }
}
