using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RouteamTest.Models;
using RouteamTest.ViewModels;

namespace RouteamTest.Controllers
{
    [Route("api/v1")]
    [ApiController]
    public class BookApiController : ControllerBase
    {
        private readonly AppDbContext _context;

        public BookApiController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet("book")]
        public async Task<ActionResult<List<BookWithAuthorsVM>>> GetBooks()
        {
            var books = await _context.Books
                .Include(bo => bo.BookAuthor).ThenInclude(ba => ba.Author).ToListAsync();

            List<BookWithAuthorsVM> model = new List<BookWithAuthorsVM>();
            foreach (var item in books)
            {
                model.Add(new BookWithAuthorsVM
                {
                    Id=item.Id,
                    Name=item.Name,
                    Genre=item.Genre,
                    authors = item.BookAuthor.Select(b=>new AuthorVM {
                        Id=b.Author.Id,
                        Name=b.Author.Name,
                        SurName=b.Author.SurName,
                        Patronymic=b.Author.Patronymic
                    }).ToList()
            });
            }
            return model;
        }
        [HttpGet("book/{id}")]
        public async Task<ActionResult<BookVM>> GetBook(int id)
        {
            var model = await _context.Books.AsNoTracking().FirstOrDefaultAsync(b => b.Id == id);
            if(model == null)
            {
                return NotFound();
            }
            return new BookVM { Id = model.Id, Name = model.Name, Genre = model.Genre };
        }

        [HttpPost("book")]
        public async Task<ActionResult<BookVM>> CreateBook(BookVM model)
        {
            if (ModelState.IsValid)
            {
                Book book = _context.Books.FirstOrDefault(b => b.Name == model.Name && 
                b.Genre == b.Genre);
                if (book == null)
                {

                    _context.Books.Add(new Book 
                    { 
                    Name=model.Name,
                    Genre=model.Genre
                    });
                    await _context.SaveChangesAsync();
                    return Ok(model);
                }
               
            }
            return NotFound();
        }

        [HttpPost("book/{id}")]
        public async Task<ActionResult> EditBook(BookVM book)
        {
            if (_context.Books.AsNoTracking().Where(b => b.Id == book.Id) != null)
            {
                _context.Books.Update(new Book
                {
                    Id = book.Id,
                    Name = book.Name,
                    Genre = book.Genre
                });
                await _context.SaveChangesAsync();
                return Ok(book);

            }
            else return NotFound();

        }




        [HttpDelete("book/{id}")]
        public async Task<ActionResult> DeleteBook(int id)
        {
            var book = await _context.Books.FindAsync(id);
            if (book == null)
            {
                return NotFound();
            }

            _context.Books.Remove(book);
            await _context.SaveChangesAsync();

            return Ok();
        }
      
    }
}
