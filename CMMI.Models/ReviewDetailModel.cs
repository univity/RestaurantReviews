using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CMMI.Models
{
    /// <summary>
    /// Used to bind and validate review data submitted to the API controllers
    /// </summary>
    public class ReviewDetailModel
    {
        public int? Id { get; set; }

        [Required]
        public int RestaurantId { get; set; }

        [Required]
        public int UserId { get; set; }

        [Required]
        public string Review { get; set; }

    }
}