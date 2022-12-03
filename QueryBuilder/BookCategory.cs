namespace QueryBuilder
{
   
   public class BookCategory : IClassModel
    {
        public BookCategory()
        { }

        public int Id { get; set; }
        public string CategoryId { get; set; }
        public string BookId { get; set; }
    }
}
