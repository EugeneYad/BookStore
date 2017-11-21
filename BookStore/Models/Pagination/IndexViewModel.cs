using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookStore.Models.Pagination
{
    /// <summary>
    /// A model for the phone page
    /// </summary>
    public class IndexViewModel
    {
        /// <summary>
        /// A list of the phones
        /// </summary>
        public IEnumerable<Phone> Phones { get; set; }

        /// <summary>
        /// Page properties
        /// </summary>
        public PageInfo PageInfo { get; set; }
    }
}