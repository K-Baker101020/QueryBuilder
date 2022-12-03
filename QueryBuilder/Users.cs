namespace QueryBuilder
{
    public class Users : IClassModel
    {
        public Users()
        { }

        public int Id { get; set; }
        public string UserName { get; set; }
        public string UserAddress { get; set; }
        public string OtherUserDetails { get; set; }
        public int AmountOfFine { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
    }
}