namespace ODataBookStore.Models
{
    public class Address
    {
        public string City { get; set; }
        public string Street { get; set; }
    }
    public enum Category
    {
        Book,
        Magazine,
        Ebook
    }
}
