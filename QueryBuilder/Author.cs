namespace QueryBuilder
{
    public class Author : IClassModel
    {
        public Author()
        { }

        public int Id { get; set; }
        public string FirstName { get; set; }
        public string Surname { get; set; }
    }
}