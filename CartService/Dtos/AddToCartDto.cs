﻿using System.ComponentModel.DataAnnotations;

namespace CartService.Dtos
{
    public class AddToCartDto
    {
        [Required]
        public int UserId { get; set; }

        [Required]
        public int ProductId { get; set; }

        [Required]
        [Range(1, int.MaxValue)]
        public int Quantity { get; set; }
    }
}
