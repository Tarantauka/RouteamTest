using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RouteamTest.Models
{
    public class Author : BaseModel
    {
       
        public string SurName { set; get; }
       
        public string Patronymic { set; get; }

        public virtual List<BookAuthor> BookAuthor { get; set; }
        public Author()
        {
            BookAuthor = new List<BookAuthor>();
        }
    }
}
