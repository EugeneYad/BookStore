using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookStore.Models
{
    public class SearchResult
    {
        public List<BookEx> Books { get; set; }

        public List<StudentEx> Students { get; set; }
        
        public List<PlayerEx> Players { get; set; }
    }
}