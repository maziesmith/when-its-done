﻿using System.ComponentModel.DataAnnotations;

using WhenItsDone.Models.Constants;
using WhenItsDone.Models.Contracts;

namespace WhenItsDone.Models
{
    public class Payment : IDbModel
    {
        public int Id { get; set; }

        public bool IsDeleted { get; set; }

        public int WorkerId { get; set; }

        public virtual Worker Worker { get; set; }

        [Range(ValidationConstants.AmountPaidMinValue, ValidationConstants.AmountPaidMaxValue)]
        public decimal AmountPaid { get; set; }
    }
}
