using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMMI.Models
{
    /// <summary>
    /// Used to return a list of restaurants requested through the API
    /// </summary>
    public class RestaurantListModel
    {
        public int Id { get; set; }

        public string Restaurant { get; set; }

        public string Cuisine { get; set; }

        public string City { get; set; }

        public int UserId { get; set; }
    }
}
