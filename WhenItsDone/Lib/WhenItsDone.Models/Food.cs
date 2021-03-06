﻿using System.ComponentModel.DataAnnotations;
using WhenItsDone.Common.Enums;
using WhenItsDone.Models.Constants;
using WhenItsDone.Models.Contracts;

namespace WhenItsDone.Models
{
    public class Food : IDbModel
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MinLength(ValidationConstants.NameMinLength)]
        [MaxLength(ValidationConstants.NameMaxLength)]
        [RegularExpression(RegexConstants.EnBgSpaceMinus)]
        public string Name { get; set; }

        public IngredientType IngredientType { get; set; }

        public MeasurementUnitType MeasurementUnitType { get; set; }

        [Range(ValidationConstants.DishPriceMinValue, ValidationConstants.DishPriceMaxValue)]
        public decimal PricePerUnit { get; set; }

        public int NutritionFactsId { get; set; }

        [Required]
        public virtual NutritionFacts NutritionFacts { get; set; }

        public bool IsDeleted { get; set; }
    }
}
