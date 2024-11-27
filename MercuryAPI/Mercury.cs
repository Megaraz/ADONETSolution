/*
 * C reate
 * R ead
 * U pdate
 * D elete
 */
using System.Data;
using Microsoft.Data.SqlClient;

namespace MercuryAPI
{
    public class Mercury
    {
        private readonly string _connectionString;
        public Mercury(string connectionString)
        {
            _connectionString = connectionString;
        }

        public void AddPerson(Person person)
        {
            using SqlConnection connection = new SqlConnection(_connectionString);
            using SqlCommand command = connection.CreateCommand();
            
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "AddPerson";

            // In-parameters
            command.Parameters.Add("@Personnr", SqlDbType.VarChar, 13).Value = person.Personnr;
            command.Parameters.Add("@FirstName", SqlDbType.VarChar, 32).Value = person.FirstName == null ? DBNull.Value : person.FirstName;
            command.Parameters.Add("@LastName", SqlDbType.VarChar, 32).Value = person.LastName == null ? DBNull.Value : person.LastName;
            command.Parameters.Add("@YearOfBirth", SqlDbType.Int).Value = person.YearOfBirth == null ? DBNull.Value : person.YearOfBirth;
            
            // Out-parameter
            command.Parameters.Add("@ID", SqlDbType.Int).Direction = ParameterDirection.Output;
            connection.Open();
            command.ExecuteNonQuery();

            person.ID = (int)command.Parameters["@ID"].Value;

            
        }

        public void UpdatePerson(Person person)
        {
            using SqlConnection connection = new SqlConnection(_connectionString);
            using SqlCommand command = connection.CreateCommand();

            command.CommandType= CommandType.StoredProcedure;
            command.CommandText = "UpdatePerson";

            command.Parameters.Add("@ID", SqlDbType.Int).Value = person.ID;
            command.Parameters.Add("@Personnr", SqlDbType.VarChar, 13).Value = person.Personnr;
            command.Parameters.Add("@FirstName", SqlDbType.VarChar, 32).Value = person.FirstName == null ? DBNull.Value : person.FirstName;
            command.Parameters.Add("@LastName", SqlDbType.VarChar, 32).Value = person.LastName == null ? DBNull.Value : person.LastName;
            command.Parameters.Add("@YearOfBirth", SqlDbType.Int).Value = person.YearOfBirth == null ? DBNull.Value : person.YearOfBirth;

            connection.Open();
            command.ExecuteNonQuery();
        }

        public void DeletePersonByID(int id)
        {
            // En person kan inte deletas om den är kopplad i foreign keys, men det räcker här att bara försöka från Persons.

            Person? person = null;

            using SqlConnection connection1 = new SqlConnection(_connectionString);
            using SqlCommand command1 = connection1.CreateCommand();

            command1.CommandText = "Select * from Persons where ID = @ID";
            command1.Parameters.Add("@ID", SqlDbType.Int).Value = id;
            connection1.Open();

            using SqlDataReader reader = command1.ExecuteReader();

            
            if (reader.Read())
            {
                person = new Person
                    (
                        (string)reader["Personnr"],
                        reader.IsDBNull("FirstName") ? null : (string)reader["FirstName"],
                        reader.IsDBNull("LastName") ? null : (string)reader["LastName"],
                        reader.IsDBNull("YearOfBirth") ? null : (int?)reader["YearOfBirth"],
                        id
                    );

                Console.WriteLine($"{person.ID}  {person.FirstName} has been deleted from DB");
            }
            connection1.Close();
            

            using SqlConnection connection2 = new SqlConnection(_connectionString);
            using SqlCommand command2 = connection2.CreateCommand();

            command2.CommandText = @"Delete from Persons where ID = @ID";
            command2.Parameters.Add("@ID", SqlDbType.Int).Value = id;

            connection2.Open();

            command2.ExecuteNonQuery();

        }

        public List<Person>? GetPersons()
        {

            List<Person>? persons = new List<Person>();

            using SqlConnection connection = new SqlConnection(_connectionString);
            using SqlCommand command = connection.CreateCommand();

            command.CommandText = @"select * from Persons";
            connection.Open();

            using SqlDataReader reader = command.ExecuteReader();

                
            while (reader.Read())
            {
                persons.Add
                (
                    new Person
                        (
                            (string)reader["Personnr"],
                            reader.IsDBNull("FirstName") ? null : (string)reader["FirstName"],
                            reader.IsDBNull("LastName") ? null : (string)reader["LastName"],
                            reader.IsDBNull("YearOfBirth") ? null : (int?)reader["YearOfBirth"],
                            (int)reader["ID"]
                        )

                );

            }

            
            return persons;
            

            //throw new NotImplementedException();
        }
        // EXTRA
        public void CascadingDeletePersonByID(int id)
        {

        }

        public Person? GetPersonByID(int id)
        {
            // Declare a return type variable
            Person? person = null;

            using SqlConnection connection = new SqlConnection(_connectionString);
            using SqlCommand command = connection.CreateCommand();

            command.CommandText = @"select * from Persons where ID = @ID";
            command.CommandType = CommandType.Text; // Default blir detta (text) ändå så behövs ej.
            command.Parameters.Add("@ID", SqlDbType.Int).Value = id;

            connection.Open();
            using SqlDataReader reader = command.ExecuteReader();

            if (reader.Read())
            {

                person = new Person(
                    (string)reader["Personnr"],
                    reader.IsDBNull("FirstName") ? null : (string)reader["FirstName"],
                    reader.IsDBNull("LastName") ? null : (string)reader["LastName"],
                    reader.IsDBNull("YearOfBirth") ? null : (int?)reader["YearOfBirth"],
                    id
                    );
            }

            return person;
        }

        //void DemoTernaryOperator()
        //{
        //    int i = 0, j = 1, k = 2;

        //    if (j < k)
        //    {
        //        i = 10;
        //    }
        //    else
        //    {
        //        i = 20;
        //    }

        //    i = j < k ? 10 : 20;


        //}

    }
}
