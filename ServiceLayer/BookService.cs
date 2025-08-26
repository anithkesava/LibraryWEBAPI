using Azure.Core;
using LibraryServicesAPI.DBContext;
using LibraryServicesAPI.Models;
namespace LibraryServicesAPI.ServiceLayer
{
    public interface IBooks
    {
        public List<Book> GetAllBooks();
        public bool AddBook(Book book);
        public bool RemoveBook(int id);
        public bool RemoveAllBooks();
        public bool UpdateBooks(int id, Book book);
        public List<Book> DisplayBookByGenre(string genre);
        public bool IsBookExists(string bookname);
        public bool IsBookCanBeBorrow(string bookname);
        public void BookBorrowedFromLibrary(string bookname);
    }
    public class BookService : IBooks
    {
        private readonly AppDbContext _appDbContext;
        public BookService(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public List<Book> GetAllBooks()
        {
            return _appDbContext.Books.ToList();
        }
        public bool AddBook(Book book)
        {
            if (string.IsNullOrWhiteSpace(book.BookName) ||
                string.IsNullOrWhiteSpace(book.AuthorName) ||
                string.IsNullOrWhiteSpace(book.Genre) ||
                string.IsNullOrWhiteSpace(book.Language) ||
                book.Page < 1 ||
                book.Price < 0)
            {
                return false;
            }
            var books = _appDbContext.Books.Add(book);
            _appDbContext.SaveChanges();
            return true;
        }
        public bool RemoveBook(int id)
        {
            var isBookIdExists = _appDbContext.Books.Where(x => x.Id == id).FirstOrDefault();
            if (isBookIdExists != null)
            {
                _appDbContext.Books.Remove(isBookIdExists);
                _appDbContext.SaveChanges();
                return true;
            }
            return false;
        }
        public bool RemoveAllBooks()
        {
            var booklist = _appDbContext.Books.ToList();
            if (booklist.Count > 0)
            {
                foreach (var book in booklist)
                {
                    _appDbContext.Remove(book);
                    _appDbContext.SaveChanges();
                }
                return true;
            }
            return false;
        }
        public bool UpdateBooks(int id, Book book)
        {
            var isBookExists = _appDbContext.Books.FirstOrDefault(x => x.Id == id);
            if (isBookExists != null)
            {
                isBookExists.BookName = book.BookName;
                isBookExists.AuthorName = book.AuthorName;
                isBookExists.Page = book.Page;
                isBookExists.Genre = book.Genre;
                isBookExists.Language = book.Language;
                isBookExists.Price = book.Price;
                _appDbContext.SaveChanges();
                return true;
            }
            return false;
        }
        public List<Book> DisplayBookByGenre(string genre)
        {
            return _appDbContext.Books.Where(x => x.Genre == genre).ToList();
        }
        public bool IsBookExists(string bookname)
        {
            return _appDbContext.Books.Any(x => x.BookName == bookname);
        }
        public bool IsBookCanBeBorrow(string bookname)
        {
            var book = _appDbContext.Books.FirstOrDefault(x => x.BookName == bookname);
            if (book != null)
            {
                if (book.BookCount > 0)
                {
                    return true;
                }
                return false;
            }
            return false;
        }
        public void BookBorrowedFromLibrary(string bookname)
        {
            var book = _appDbContext.Books.FirstOrDefault(x => x.BookName == bookname);
            if (book != null && book.BookCount > 0)
            {
                book.BookCount--;
                _appDbContext.SaveChanges();
            }
        }
    }
}
