using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ODataBookStoreWebClient.Models;
using System.Diagnostics;
using System.Net.Http.Headers;
using System.Text;

namespace ODataBookStoreWebClient.Controllers
{
    public class BookController : Controller
    {
        private readonly HttpClient client = null;
        private string BooksApiUrl = "";
        public BookController() //hàm khởi tạo
        {
            client = new HttpClient();
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            client.DefaultRequestHeaders.Accept.Add(contentType);
            BooksApiUrl = "http://localhost:5251/odata/Books";
        }

        public async Task<IActionResult> Index()
        {
            HttpResponseMessage response = await client.GetAsync(BooksApiUrl); //get list book qua api ham khoi tao
            string strData = await response.Content.ReadAsStringAsync(); //to string
            dynamic temp = JObject.Parse(strData); //parse sang json
            List<Book> items = ((JArray)temp.value).Select(x => new Book // => list book
            { 
                Id = (int)x["Id"],
                Author = (string)x["Author"],
                ISBN = (string)x["ISBN"],
                Title = (string)x["Title"],
                Price = (decimal)x["Price"]
            }).ToList();
            return View(items);
        }

        public async Task<IActionResult> Details(int id)
        {
            HttpResponseMessage response = await client.GetAsync(BooksApiUrl + "(" + id + ")"); //get book by id
            string strData = await response.Content.ReadAsStringAsync(); //get respone value
            dynamic temp = JObject.Parse(strData); //parse sang json
            Book item = new Book // => gan vao book
            {
                Id = (int)temp["Id"],
                Author = (string)temp["Author"],
                ISBN = (string)temp["ISBN"],
                Title = (string)temp["Title"],
                Price = (decimal)temp["Price"],
                Location = new Address
                {
                    City = (string)temp["Location"]["City"],
                    Street = (string)temp["Location"]["Street"]
                }
                //,
                //Press = new Press
                //{
                //    Name = (string)temp["Press"]["Name"],
                //    Category = Enum.GetName(Category.Book),
                //}
            };
            return View(item);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(IFormCollection collection)
        {
            Book item = new Book // lay thong tin book nhap tu collection form
            {
                Author = collection["Author"],
                ISBN = collection["ISBN"],
                Title = collection["Title"],
                Price = decimal.Parse(collection["Price"]),
                Location = new Address //fix cung
                { 
                    City = "Ha Noi", 
                    Street = "A" 
                },
                Press = new Press
                {
                    Name = "Press Name",
                    Category = Enum.GetName(Category.Book),
                }
            };
            string strData = JsonConvert.SerializeObject(item); // convert đối tượng sang json
            HttpContent content = new StringContent(strData, Encoding.UTF8, "application/json"); //http entity header body
            HttpResponseMessage response = await client.PostAsync(BooksApiUrl, content); //post
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View();
        }

        public async Task<ActionResult> Edit(int id) //goi ra view book theo id tuong tu detail
        {
            HttpResponseMessage response = await client.GetAsync(BooksApiUrl + "(" + id + ")");
            string strData = await response.Content.ReadAsStringAsync();

            dynamic temp = JObject.Parse(strData);
            var lst = temp.value;
            Book item = new Book
            {
                Id = (int)temp["Id"],
                Author = (string)temp["Author"],
                ISBN = (string)temp["ISBN"],
                Title = (string)temp["Title"],
                Price = (decimal)temp["Price"],
                Location = new Address
                {
                    City = (string)temp["Location"]["City"],
                    Street = (string)temp["Location"]["Street"]
                }
            };
            return View(item);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(IFormCollection collection)
        {
            Book item = new Book
            {
                Id = Int32.Parse(collection["Id"]),
                Author = collection["Author"],
                ISBN = collection["ISBN"],
                Title = collection["Title"],
                Price = decimal.Parse(collection["Price"]),
                Location = new Address
                {
                    City = collection["Location.City"],
                    Street = collection["Location.Street"]
                },
                Press = new Press
                {
                    Name = "PressName",
                    Category = Enum.GetName(Category.Book)
                }
            };
            string strData = JsonConvert.SerializeObject(item);
            HttpContent content = new StringContent(strData, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await client.PatchAsync(BooksApiUrl, content);
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View();
        }

        public async Task<ActionResult> Delete(int id)
        {
            HttpResponseMessage response = await client.DeleteAsync(BooksApiUrl + "(" + id + ")"); //delete theo id
            return RedirectToAction("Index");
        }
    }
}
