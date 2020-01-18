using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RouteamTest.ViewModels
{
    public class CountAuthorBooksVM
    {
        public int Id { get; set; }
        public string Name { set; get; }
        public string SurName { set; get; }
        public string Patronymic { set; get; }
        public int BookCount { set; get; }

    }
}
