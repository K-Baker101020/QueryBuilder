namespace QueryBuilder
{
    public class BookAuthor : IClassModel
    {
        public BookAuthor()
        { }

        public int Id { get; set; }
        public string AuthorId { get; set; }
        public string BookId { get; set; }
    }
}
