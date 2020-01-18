using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RouteamTest.Models
{
    public class BookAuthor : BaseLink
    {
        [Required]
        public int AuthorId { get; set; }
        public virtual Author Author { get; set; }
        [Required]
        public int BookId { get; set; }
        public virtual Book Book { get; set; }
    }
}
