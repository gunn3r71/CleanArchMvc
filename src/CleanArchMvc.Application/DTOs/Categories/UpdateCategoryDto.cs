﻿using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CleanArchMvc.Application.DTOs.Categories
{
    public record UpdateCategoryDto
    {
        [Required(ErrorMessage = "{0} cannot be null.")]
        [Display(Name = "Id")]
        public int Id { get; set; }

        [Required(ErrorMessage = "{0} cannot be null.", AllowEmptyStrings = false)]
        [StringLength(254, ErrorMessage = "{0} length must be between {2} and {1}.", MinimumLength = 3)]
        [DisplayName("CategoryName")]
        public string Name { get; set; }
    }
}