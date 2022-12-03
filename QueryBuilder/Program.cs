using System.IO;

namespace QueryBuilder
{
    public class Program
    {
        static void Main()
        {
            Console.WriteLine("Welcome to the Query Builder!");

            QueryBuilder qb = new QueryBuilder(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent + @"\Data\LibraryBooks.db");

            Console.WriteLine("Please enter the ID of an author you would like to see: ");
            var authorID = int.Parse(Console.ReadLine());
            var author = qb.Read<Author>(authorID);

            Console.WriteLine("");
            Console.WriteLine("Author First Name: " + author.FirstName);
            Console.WriteLine("Author Surname: " + author.Surname);
            Console.WriteLine("Author ID: " + author.Id.ToString());
            Console.WriteLine("-----------------------------------------");

            Console.WriteLine("");
            Console.WriteLine("Inserting the author 'Jim Bob' with ID 999 into the table Author.");
            Author authorNew = new Author();
            authorNew.FirstName = "Jim";
            authorNew.Surname = "Bob";
            authorNew.Id = 999;
            qb.Create<Author>(authorNew);
            Console.WriteLine("Author has been added to the table.");
            Console.WriteLine("-----------------------------------------");

            Console.WriteLine("");
            Console.WriteLine("Reading all authors from the Author table.");
            var authors = qb.ReadAll<Author>();
            Console.WriteLine("-----------------------------------------");
            foreach (Author auth in authors)
            {
                Console.WriteLine("Author First Name: " + auth.FirstName);
                Console.WriteLine("Author Surname: " + auth.Surname);
                Console.WriteLine("Author ID: " + auth.Id.ToString());
                Console.WriteLine("");
            }
            Console.WriteLine("-----------------------------------------");

            //Placeholder for Updating
            Console.WriteLine("");
            Console.WriteLine("Updating the author 'Jim Bob' with ID 999 in the table Author to 'Jimmy Bobby'.");
            authorNew.FirstName = "Jimmy";
            authorNew.Surname = "Bobby";
           
            qb.Update<Author>(authorNew);
            Console.WriteLine("Author has been updated in the table.");
            Console.WriteLine("-----------------------------------------");

            Console.ReadLine();
            
            //Placeholder for Deleting
            Console.WriteLine("");
            Console.WriteLine("Deleting the author 'Jimmy Bobby' with ID 999 from the table Author.");
          
            qb.Delete<Author>(authorNew);
            Console.WriteLine("Author has been deleted from the table.");
            Console.WriteLine("-----------------------------------------");
        }
    }
}