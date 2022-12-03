namespace QueryBuilder
{
    public class Books : IClassModel
    {
        public Books()
        { }

        public int Id { get; set; }
        public string Title { get; set; }
        public string Isbn { get; set; }
        public int DateOfPublication { get; set; }
    }
}