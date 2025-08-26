using LibraryServicesAPI.DBContext;
using LibraryServicesAPI.Models;
using LibraryServicesAPI.ServiceLayer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.FileProviders;

namespace LibraryServicesAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BooksController : Controller
    {
        private readonly IBooks _books;
        public BooksController(IBooks books)
        {
            _books = books;
        }
        [HttpGet("GetAllBooks")]
        public ActionResult<List<Book>> GetAllBooksDetails()
        {
            var booklist = _books.GetAllBooks();
            if (booklist.Count > 0)
            {
                return Ok(booklist);
            }
            return NotFound(new { Error = " No Books Found " });
        }

        [HttpPost("AddBooks")]
        public IActionResult AddBooks([FromBody] Book book)
        {
            var isBookAdded = _books.AddBook(book);
            if (isBookAdded)
            {
                return Ok(new { msg = "Books Added in the Database" });
            }
            else
            {
                return Conflict(new { msg = "Missing some field inputs" });
            }
        }

        [HttpDelete("RemoveBook")]
        public IActionResult RemoveBook(int id)
        {
            var isbookremovd = _books.RemoveBook(id);
            if (isbookremovd)
            {
                return Ok(new { msg = "Book removed Successfully" });
            }
            else
            {
                return NotFound(new { msg = $"Book with Id: {id} not exists" });
            }
        }

        [HttpDelete("RemoveAllBooks")]
        public IActionResult RemoveAllBooks()
        {
            var isallBooksRemoved = _books.RemoveAllBooks();
            if (isallBooksRemoved)
            {
                return Ok(new { msg = "All Books are Removed From Database" });
            }
            else
            {
                return NotFound(new { msg = "No Books are available to Remove" });
            }
        }

        [HttpPut("UpdateBook")]
        public IActionResult UpdateBook(int id, [FromBody] Book book)
        {
            var isbookupdated = _books.UpdateBooks(id, book);
            if (isbookupdated)
            {
                return Ok(new { msg = "Updates are saved to database" });
            }
            return NotFound(new { msg = $"Update are failed Id: {id} not exists" });
        }

        [HttpGet("ShowBooksByGenre")]
        public ActionResult<List<Book>> ShowBookByGenre(string genre)
        {
            var booklist = _books.DisplayBookByGenre(genre);
            if (booklist.Count > 0)
            {
                return Ok(booklist);
            }
            return NotFound(new { msg = $"No Books Found in this Genre : {genre}" });
        }

        [HttpPost("BorrowBook")]
        public IActionResult BorrowBook(string bookname)
        {
            var isbookexists = _books.IsBookExists(bookname);
            if (isbookexists)
            {
                bool isbookcanborrow = _books.IsBookCanBeBorrow(bookname);
                if(isbookcanborrow)
                {
                    _books.BookBorrowedFromLibrary(bookname);
                    return Ok(new { message = "Book Borrowed Successfully" });
                }
                return Conflict(new { message = "Book Count Reaches its Limit" });
            }
            return NotFound(new { message = $"Book : {bookname} not exists " });
        }
    }
}
