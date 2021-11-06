using System;
using System.ComponentModel.DataAnnotations;

namespace VaporStore.DataProcessor.DTOs.Import
{
    public class GameImportModel
    {
        [Required]
        public string Name { get; set; }

        [Required]
        [Range(0, double.MaxValue)]
        public decimal Price { get; set; }

        [Required]
        public DateTime ReleaseDate { get; set; }

        [Required]
        public string Developer { get; set; }

        [Required]
        public string Genre { get; set; }

        [Required]
        [MinLength(1)]
        public string[] Tags { get; set; }
    }
}
