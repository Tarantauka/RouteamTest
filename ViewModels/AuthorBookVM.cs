using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RouteamTest.ViewModels
{
    public class AuthorBookVM
    {
        public int AuthorId { get; set; }
        public string AuthorFullName { get; set; }
        public bool IsSelected { get; set; }
    }
}
