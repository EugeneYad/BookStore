using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BookStore.Models.Pagination;

namespace BookStore.Controllers
{
    /// <summary>
    /// A controller to return a set of phones for pagination
    /// </summary>
    public class PhonesController : ApiController
    {
        private List<Phone> _phones = new List<Phone>
	        {
		        new Phone {Id = 1, Model = "Samsung Galaxy III", Producer = "Samsung"},
		        new Phone {Id = 2, Model = "Samsung Ace II", Producer = "Samsung"},
		        new Phone {Id = 3, Model = "HTC Hero", Producer = "HTC"},
		        new Phone {Id = 4, Model = "HTC One S", Producer = "HTC"},
		        new Phone {Id = 5, Model = "HTC One X", Producer = "HTC"},
		        new Phone {Id = 6, Model = "LG Optimus 3D", Producer = "LG"},
		        new Phone {Id = 7, Model = "Nokia N9", Producer = "Nokia"},
		        new Phone {Id = 8, Model = "Samsung Galaxy Nexus", Producer = "Samsung"},
		        new Phone {Id = 9, Model = "Sony Xperia X10", Producer = "SONY"},
		        new Phone {Id = 10, Model = "Samsung Galaxy II", Producer = "Samsung"}
	        };

        /// <summary>
        /// Get a set of phones for the requested page
        /// </summary>
        /// <param name="page">A page</param>
        /// <returns>Returns a IndexViewModel</returns>
        public IndexViewModel Get(int page)
        {
            int pageSize = 3; // количество объектов на страницу
            IEnumerable<Phone> phonesPerPages = _phones.Skip((page - 1) * pageSize).Take(pageSize);
            PageInfo pageInfo = new PageInfo { PageNumber = page, PageSize = pageSize, TotalItems = _phones.Count };
            IndexViewModel ivm = new IndexViewModel { PageInfo = pageInfo, Phones = phonesPerPages };
            return ivm;
        }
    }
}