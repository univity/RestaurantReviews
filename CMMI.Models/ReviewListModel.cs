using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMMI.Models
{
    /// <summary>
    /// Used to return a list of reviews requested through the API
    /// </summary>
    public class ReviewListModel
    {
        public int Id { get; set; }

        public string Restaurant { get; set; }

        public string Username { get; set; }

        public string Review { get; set; }

        public DateTime CreatedDate { get; set; }
    }
}
