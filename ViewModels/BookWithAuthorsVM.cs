using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RouteamTest.ViewModels
{
    public class BookWithAuthorsVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Genre { get; set; }
        public List<AuthorVM> authors { get; set; }

    }
}
