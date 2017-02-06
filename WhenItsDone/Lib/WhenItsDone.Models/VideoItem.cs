﻿using System.ComponentModel.DataAnnotations;

using WhenItsDone.Models.Constants;
using WhenItsDone.Models.Contracts;

namespace WhenItsDone.Models
{
    public class VideoItem : IDbModel
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MinLength(ValidationConstants.UrlLengthMinLength)]
        [MaxLength(ValidationConstants.UrlLengthMaxValue)]
        public string Url { get; set; }

        public int Rating { get; set; }

        public bool IsDeleted { get; set; }
    }
}
