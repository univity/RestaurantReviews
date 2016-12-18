using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CMMI.Models
{
    /// <summary>
    /// Used to bind and validate restaurant data submitted to the API controllers
    /// </summary>
    public class RestaurantDetailModel
    {
        public int? Id { get; set; }

        [Required]
        [MaxLength(256, ErrorMessage = "Restaurant exceeds the maximum length.")]
        public string Restaurant { get; set; }

        [Required]
        public int CuisineId { get; set; }

        [Required]
        [MaxLength(256, ErrorMessage = "Address exceeds the maximum length.")]
        public string Address { get; set; }

        [MaxLength(256, ErrorMessage = "Address2 exceeds the maximum length.")]
        public string Address2 { get; set; }

        [Required]
        [MaxLength(256, ErrorMessage = "City exceeds the maximum length.")]
        public string City { get; set; }

        [Required]
        public int StateId { get; set; }

        [Required]
        [RegularExpression(@"^(?!00000)[0-9]{5,5}$", ErrorMessage = "Zipcode is invalid")]
        public string Zipcode { get; set; }

        [Required]
        public int UserId { get; set; }

        
    }
}