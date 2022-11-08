﻿namespace SkiShop.Data.Models.Product
{
    using System.ComponentModel.DataAnnotations;

    public class Type
    {
        public Type()
        {
            Products = new HashSet<Product>();
        }

        [Key]
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; }

        public bool IsDeleted { get; set; }

        public ICollection<Product> Products { get; set; }
    }
}