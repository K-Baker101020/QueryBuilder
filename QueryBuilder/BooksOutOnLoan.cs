namespace QueryBuilder
{
    public class BooksOutOnLoan : IClassModel
    {
        public BooksOutOnLoan()
        { }

        public int Id { get; set; }
        public int BookId { get; set; }
        public int DateIssued { get; set; }
        public int DueDate { get; set; }
        public int DateReturned { get; set; }
    }
}