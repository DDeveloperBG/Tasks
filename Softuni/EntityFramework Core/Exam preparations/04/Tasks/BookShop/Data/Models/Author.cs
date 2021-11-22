﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BookShop.Data.Models
{
    public class Author : BaseEntity
    {
        public Author()
        {
            AuthorsBooks = new HashSet<AuthorBook>();
        }

        [Required]
        [StringLength(30, MinimumLength = 3)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(30, MinimumLength = 3)]
        public string LastName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Phone { get; set; }

        public ICollection<AuthorBook> AuthorsBooks { get; set; }
    }
}
