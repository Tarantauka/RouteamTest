using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RouteamTest.Models;
using RouteamTest.ViewModels;

namespace RouteamTest.Controllers
{
    public class AuthorController : GenericController<Author>
    {
        public AuthorController(AppDbContext context) : base(context)
        {
            
        }
        public IActionResult ListAuthorBooksCount()
        {
            List<CountAuthorBooksVM> model = new List<CountAuthorBooksVM>();
            foreach (var author in db.Authors)
            {
                model.Add(new CountAuthorBooksVM 
                {
                    Id= author.Id,
                    Name= author.Name,
                    SurName= author.SurName,
                    Patronymic= author.Patronymic,
                    BookCount = db.Entry(author)
                    .Collection(aut => aut.BookAuthor)
                    .Query()
                    .Count()
            });  
            }
            return View(model);
        }
    }
}