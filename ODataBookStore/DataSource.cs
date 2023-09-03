using ODataBookStore.Models;

namespace ODataBookStore
{
    public static class DataSource //step 6
    {
        private static IList<Book> listBooks { get; set; }
        public static IList<Book> GetBooks()
        {
            if(listBooks != null)
            {
                return listBooks;
            }
            listBooks = new List<Book>();
            Book book = new Book
            {
                Id = 1,
                ISBN = "123-4-56-7-89-0",
                Title = "Clean code",
                Author = "co chi",
                Price = 14.597m,
                Location = new Address
                {
                    City = "City 1",
                    Street = "Street 1",
                },
                Press = new Press
                {
                    Id = 1,
                    Name = "Press 1",
                    Category = Category.Book,
                }
            };
            listBooks.Add(book);
            book = new Book
            {
                Id = 2,
                ISBN = "123-4-56-7-89-1",
                Title = "I love c#",
                Author = "co chi nma xinh",
                Price = 14.597m,
                Location = new Address
                {
                    City = "City 2",
                    Street = "Street 2",
                },
                Press = new Press
                {
                    Id = 2,
                    Name = "Press 2",
                    Category = Category.Book,
                }
            };
            listBooks.Add(book);
            book = new Book
            {
                Id = 3,
                ISBN = "111-222-222-333",
                Title = "I love c# #h #i",
                Author = "co chi nma xinh part2",
                Price = 12.597m,
                Location = new Address
                {
                    City = "City test",
                    Street = "Street test",
                },
                Press = new Press
                {
                    Id = 2,
                    Name = "Press 2",
                    Category = Category.Book,
                }
            };
            listBooks.Add(book);
            book = new Book
            {
                Id = 4,
                ISBN = "111-222-222-333",
                Title = "I love c# #h #i",
                Author = "co chi nma xinh part 3 ",
                Price = 13.597m,
                Location = new Address
                {
                    City = "City2",
                    Street = "Street test 2",
                },
                Press = new Press
                {
                    Id = 2,
                    Name = "Press 2",
                    Category = Category.Book,
                }
            };
            listBooks.Add(book);
            return listBooks;
            book = new Book
            {
                Id = 5,
                ISBN = "newnewnew",
                Title = "test123",
                Author = "testtest",
                Price = 14.597m,
                Location = new Address
                {
                    City = "City2",
                    Street = "Street test 2",
                },
                Press = new Press
                {
                    Id = 2,
                    Name = "Press 2",
                    Category = Category.Book,
                }
            };
            listBooks.Add(book);
            return listBooks;
        }
    }
}
