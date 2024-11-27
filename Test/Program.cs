using MercuryAPI;
using Microsoft.Identity.Client;
namespace Test;

internal class Program
{
    static void Main(string[] args)
    {
        string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Mercury;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False";
        Mercury mercury = new Mercury(connectionString);

        mercury.DeletePersonByID(16);



        //Person? person = mercury.GetPersonByID(10);

        //if (person != null)
        //{
        //    if (person.FirstName == null)
        //    {
        //        Console.WriteLine("FirstName null");
        //    }
        //    else
        //    {
        //        Console.WriteLine($"{person.ID}, {person.Personnr}, {person.FirstName}, {person.LastName}, {person.YearOfBirth}");

        //    }

        //}

        //Person? person = new Person("19920410-5235", "Karin", "Kanin", 1992, 11);

        //List<Person>? persons = mercury.GetPersons();

        //if (persons != null)
        //{
        //    foreach (var item in persons)
        //    {
        //        Console.WriteLine(item.ID + item.FirstName);
        //    }
        //}

        //mercury.UpdatePerson(person);
        //Console.WriteLine(person.LastName);

        //mercury.AddPerson(person);
        //Console.WriteLine(person.ID);
    }
}
