using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Formatter;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ODataBookStore.Models;

namespace ODataBookStore.Controllers //step 7
{
    public class BooksController : ODataController
    {
        private BookStoreContext db;

        public BooksController(BookStoreContext context)
        {
            db = context;
            db.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            if(context.Books.Count() == 0)
            {
                foreach(var b in DataSource.GetBooks())
                {
                    context.Books.Add(b);
                }
                context.SaveChanges();
            }
        }

        [EnableQuery]
        public IActionResult Get()
        {
            return Ok(db.Books.Include("Press").Include("Location"));
        }

        [EnableQuery]
        public IActionResult Get(int key, string version)
        {
            return Ok(db.Books.Include("Location").Include("Press").FirstOrDefault(c => c.Id == key));
        }

        [EnableQuery]
        public IActionResult Post([FromBody]Book book)
        {
            db.Books.Add(book);
            db.SaveChanges();
            return Created(book);
        }

        [EnableQuery]
        public IActionResult Patch([FromBody]Book book)
        {
            var _book = db.Books.Find(book.Id);
            if(_book == null)
            {
                return NotFound();
            }

            db.Update<Book>(book);
            db.SaveChanges();
            return Updated(book);
        }

        [EnableQuery]
        public IActionResult Delete([FromODataUri] int key)
        {
            Book b = db.Books.Include("Location").Include("Press").FirstOrDefault(c => c.Id == key);
            if (b == null)
            {
                return NotFound();
            }

            db.Books.Remove(b);
            db.SaveChanges();
            return Ok();
        }
    }
}
