using System.Data.SqlClient;
using Geico.Moat.App.Fnd.Data.ADO;
public class PersonStorage //change to REPO
{
    //   **get this from Movie USER code to set up a shared connection
    //   private readonly string _connectionString;  
    //     public PersonStorage(string connString)
    //     {
    //         _connectionString = connString;
    //     }

    // public Dictionary<int, Person> person;
    // public int nextPersonId = 1;
    public static void AddPerson(Person person)
    {
        // *this line can be removed with the shared connection
        var connectionString = "data source=GEIPW0785V4;initial catalog=Project_1;user id=sa;password=password"; // connection string to SQL Server

        using SqlConnection sqlConnection = new SqlConnection(connectionString);  // using statement to ensure resources are released after query is done
        sqlConnection.Open();

        string query = "INSERT INTO dbo.Person (FirstName, LastName, PhoneNumber, Email, Person_Type, Status, UserId, Password, Role) VALUES (@FirstName, @LastName, @PhoneNumber, @Email, @Person_Type, @Status, @UserId, @Password, @Role);"; // SQL query to insert a new person
                                                                                                                                                                                                                                                //string sql = "INSERT INTO [Person] OUTPUT inserted.* VALUES (@FirstName, @LastName, @PhoneNumber, @Email, @Person_Type, @Status, @UserId, @Password, @Role)

        SqlCommand command = new(query, sqlConnection);
        command.Parameters.AddWithValue("@FirstName", person.FirstName);
        command.Parameters.AddWithValue("@LastName", person.LastName);
        command.Parameters.AddWithValue("@PhoneNumber", person.PhoneNumber);
        command.Parameters.AddWithValue("@Email", person.Email);
        command.Parameters.AddWithValue("@Person_Type", person.Person_Type);
        command.Parameters.AddWithValue("@Status", person.Status);
        command.Parameters.AddWithValue("@UserId", person.UserId);
        command.Parameters.AddWithValue("@Password", person.Password);
        command.Parameters.AddWithValue("@Role", person.Role);

        command.ExecuteNonQuery(); // execute the query, only returns # of rows effected
        //to get the person back, user the sql line with OUTPUT.* + SQLDataReader in GetPerson
    }

    public Person? GetPerson(int? personId)
    {
        string connectionString = "data source=GEIPW0785V4;initial catalog=Project_1;user id=sa;password=password"; // connection string to SQL Server
        Person persons = new Person();  // empty list to add results to// empty list to add results to
        int Id = personId ?? 0;

        // help from: https://learn.microsoft.com/en-us/dotnet/framework/data/adonet/retrieving-data-using-a-datareader
        using SqlConnection sqlConnection = new(connectionString); // using statement to ensure resources are released after query is done
        sqlConnection.Open();
        string query = $"SELECT * FROM dbo.Person WHERE PersonId = {Id};"; // SQL query to get a person
        SqlCommand command = new(query, sqlConnection);
        SqlDataReader reader = command.ExecuteReader(); // execute the query; returns a data reader object
        // Remove the declaration of the 'person' variable since it is already declared in the code above
        // var person = new List<Person>(); // Initialize person as a List<Person>
        if (reader.HasRows)
        {
            while (reader.Read())
            // check out Ryan's Add User reader code to return1 person instead of a list
            {
                var newPerson = new Person()
                {
                    PersonId = SqlExtensions.GetInt32(reader, "PersonId") ?? 0,
                    LastName = SqlExtensions.GetString(reader, "LastName"),
                    FirstName = SqlExtensions.GetString(reader, "FirstName") ?? string.Empty,
                    PhoneNumber = SqlExtensions.GetInt32(reader, "PhoneNumber") ?? 0,
                    Email = SqlExtensions.GetString(reader, "Email") ?? string.Empty,
                    Person_Type = SqlExtensions.GetString(reader, "Person_Type") ?? string.Empty,
                    Status = SqlExtensions.GetString(reader, "Status") ?? string.Empty,
                    UserId = SqlExtensions.GetString(reader, "UserId") ?? string.Empty,
                    Password = SqlExtensions.GetString(reader, "Password") ?? string.Empty,
                    Role = SqlExtensions.GetString(reader, "Role") ?? string.Empty
                };
                persons.Add(newPerson); // Add getPerson to the person list
            }
        }
        reader.Close();
        return persons;
    }

    public bool UpdatePerson(Person personToUpdate)
    {

        var connectionString = "data source=GEIPW0785V4;initial catalog=Project_1;user id=sa;password=password"; // connection string to SQL Server
        using SqlConnection sqlConnection = new(connectionString); // using statement to ensure resources are released after query is done
        sqlConnection.Open();
        string query = "UPDATE dbo.Person SET FirstName = @FirstName, LastName = @LastName, PhoneNumber = @PhoneNumber, Email = @Email, Person_Type = @Person_Type, Status = @Status, UserId = @UserId, Password = @Password, Role = @Role WHERE PersonId = @PersonId;"; // SQL query to update a person
        SqlCommand command = new(query, sqlConnection);
        command.Parameters.AddWithValue("@PersonId", personToUpdate.PersonId);
        command.Parameters.AddWithValue("@FirstName", personToUpdate.FirstName);
        command.Parameters.AddWithValue("@LastName", personToUpdate.LastName);
        command.Parameters.AddWithValue("@PhoneNumber", personToUpdate.PhoneNumber);
        command.Parameters.AddWithValue("@Email", personToUpdate.Email);
        command.Parameters.AddWithValue("@Person_Type", personToUpdate.Person_Type);
        command.Parameters.AddWithValue("@Status", personToUpdate.Status);
        command.Parameters.AddWithValue("@UserId", personToUpdate.UserId);
        command.Parameters.AddWithValue("@Password", personToUpdate.Password);
        command.Parameters.AddWithValue("@Role", personToUpdate.Role);

        int rowsAffected = command.ExecuteNonQuery(); // execute the query
        return rowsAffected > 0; // return true if the update was successful, false otherwise
    }

    public Person? GetUser(string? userId)
    {
        string connectionString = "data source=GEIPW0785V4;initial catalog=Project_1;user id=sa;password=password"; // connection string to SQL Server
        Person user = new();  // empty list to add results to// empty list to add results to
        string Id = userId ?? "0";

        // help from: https://learn.microsoft.com/en-us/dotnet/framework/data/adonet/retrieving-data-using-a-datareader
        using SqlConnection sqlConnection = new(connectionString); // using statement to ensure resources are released after query is done
        sqlConnection.Open();
        string query = $"SELECT * FROM dbo.Person WHERE UserId = {Id};"; // SQL query to get a person
        SqlCommand command = new(query, sqlConnection);
        SqlDataReader reader = command.ExecuteReader(); // execute the query; returns a data reader object
        // Remove the declaration of the 'person' variable since it is already declared in the code above
        // var person = new List<Person>(); // Initialize person as a List<Person>
        if (reader.HasRows)
        {

            command.Parameters.AddWithValue("@PersonId", user.PersonId);
            command.Parameters.AddWithValue("@FirstName", user.FirstName);
            command.Parameters.AddWithValue("@LastName", user.LastName);
            command.Parameters.AddWithValue("@PhoneNumber", user.PhoneNumber);
            command.Parameters.AddWithValue("@Email", user.Email);
            command.Parameters.AddWithValue("@Person_Type", user.Person_Type);
            command.Parameters.AddWithValue("@Status", user.Status);
            command.Parameters.AddWithValue("@UserId", user.UserId);
            command.Parameters.AddWithValue("@Password", user.Password);
            command.Parameters.AddWithValue("@Role", user.Role);
        }
        reader.Close();
        return user;
    }
}