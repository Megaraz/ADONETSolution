using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MercuryAPI;
public class Person
{
    public Person(string personnr, string? firstName, string? lastName, int? yearOfBirth, int iD = -1)
    {
        ID = iD;
        Personnr = personnr ?? throw new ArgumentNullException(nameof(personnr));
        FirstName = firstName;
        LastName = lastName;
        YearOfBirth = yearOfBirth;
    }

    public int ID { get; set; }
    public string Personnr { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public int? YearOfBirth { get; set; }
}
