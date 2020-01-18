using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RouteamTest.Models
{
    public class Book : BaseModel
    {
       
        [Required]
        public string Genre { get; set; }

        public virtual List<BookAuthor> BookAuthor { get; set; }
        public Book()
        {
            BookAuthor = new List<BookAuthor>();
        }
    }
}
