namespace WebApi.Controllers;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("[controller]")]
public class BooksController : ControllerBase
{
    private static List<Book> Books = new List<Book>
    {
        new Book { Id = 1, Title = "Lean Startup", GenreId = 1, Page = 200, PublishDate = new DateTime(2001, 06, 12) },
        // GenreId 1 -> Personal Growth
        new Book { Id = 2, Title = "Herland", GenreId = 2, Page = 250, PublishDate = new DateTime(2010, 05, 23) },
        // GenreId 2 -> Science Fiction
        new Book { Id = 3, Title = "Dune", GenreId = 2, Page = 540, PublishDate = new DateTime(2001, 12, 21) },
        new Book { Id = 4, Title = "1984", GenreId = 2, Page = 500, PublishDate = new DateTime(2009, 02, 15) },
        new Book { Id = 5, Title = "The Alchemist", GenreId = 1, Page = 300, PublishDate = new DateTime(2015, 11, 11) }
    };

    [HttpGet]
    public ActionResult<List<Book>> GetBooks()
    {
        var bookList = Books.OrderBy(x => x.Id).ToList();
        return bookList;
    }

    [HttpGet("{id}")]
    public ActionResult<Book> GetBookByID(int id)
    {
        var book = Books.FirstOrDefault(book => book.Id == id);
        return book;
    }

    [HttpGet("search")]
    public ActionResult<Book> GetFromQuery([FromQuery] int id)
    {
        var book = Books.FirstOrDefault(book => book.Id == id);
        return book;
    }

    [HttpPost]
    public IActionResult AddBook([FromBody] Book newBook)
    {
        var book = Books.FirstOrDefault(book => book.Id == newBook.Id);
        if (book != null) return BadRequest();
        else
        {
            Books.Add(newBook);
            return Ok(newBook);
        }
    }

    [HttpPut("{id}")]
    public IActionResult UpdateBook(int id, [FromBody] Book updatedBook)
    {
        var book = Books.FirstOrDefault(book => book.Id == id);
        if (book == null) return NotFound();
        else
        {
            book.Id = updatedBook.Id;
            book.Title = updatedBook.Title;
            book.GenreId = updatedBook.GenreId;
            book.Page = updatedBook.Page;
            book.PublishDate = updatedBook.PublishDate;
            return Ok(book);
        }
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteBook(int id)
    {
        var book = Books.FirstOrDefault(book => book.Id == id);
        if (book == null) return NotFound();
        else
        {
            Books.Remove(book);
            return Ok();
        }
    }
}