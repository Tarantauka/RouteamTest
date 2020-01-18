using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RouteamTest.Models;
using RouteamTest.ViewModels;

namespace RouteamTest.Controllers
{
    public class BookController : GenericController<Book>
    {
        public BookController(AppDbContext context) : base(context)
        {
        }
        [HttpGet]
        public override async Task<IActionResult> List()
        {
            var model = await db.Books
                .Include(bo => bo.BookAuthor)
                .ThenInclude(ba => ba.Author)
                .ToListAsync();
            return View(model);
        }
        [HttpGet]
        public async Task<IActionResult> EditAuthorsOfBook(int id)
        {
            try
            {
                ValidateModel();

                var bookAuthors = db.BookAuthor.AsNoTracking()
                    .Where(ba => ba.BookId == id);//получаем все связи книги с авторами

                var authors = await db.Authors.ToListAsync();//получаем всех авторов
                var model = new List<AuthorBookVM>();
                foreach (var item in authors)
                {
                    var authorBook = new AuthorBookVM//создаем новый VM длякаждогоавтора
                    {
                        AuthorId = item.Id,
                        AuthorFullName = item.Name + item.SurName + item.Patronymic
                    };

                    if (bookAuthors.FirstOrDefault(ba=>ba.AuthorId==item.Id)!=null)//если у книги существет связь с данным автором то IsSelected true
                    {
                        authorBook.IsSelected = true;
                    }
                    else
                    {
                        authorBook.IsSelected = false;
                    }

                    model.Add(authorBook);
                }
                return View(model);
            }
            catch (DataException)
            {
                ModelState.AddModelError("", "Ошибка.");
            }
            return RedirectToAction(nameof(List));

        }
        [HttpPost]
        public virtual async Task<IActionResult> EditAuthorsOfBook(List<AuthorBookVM> model, int id)
        {
            {
                var bookAuthors = await db.BookAuthor.AsNoTracking()
                    .Where(ba => ba.BookId == id)
                    .ToListAsync();


                foreach (var item in model)
                {
                    
                        if (!item.IsSelected && bookAuthors.FirstOrDefault(x => x.AuthorId == item.AuthorId) != null)
                        {
                            db.BookAuthor.Remove(bookAuthors.FirstOrDefault(x => x.AuthorId == item.AuthorId));
                        }
                        else if (item.IsSelected && bookAuthors.FirstOrDefault(x => x.AuthorId == item.AuthorId) == null)
                        {
                            db.BookAuthor.Add(new BookAuthor
                            {
                                AuthorId = item.AuthorId,
                                BookId = id
                            });
                        }
                    
                }
                db.SaveChanges();
                return RedirectToAction(nameof(List));
            }
        }
    }

}
