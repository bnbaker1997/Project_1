using System.Data.SqlClient;
using Geico.Moat.App.Fnd.Data.ADO;
public class LotStorage
{
    //public Dictionary<int, Lot> Lot;
    // public int nextId = 1;
    public LotStorage()
    { }

    public List<Lot> RetrieveAllLots()
    {
        string connectionString = "data source=GEIPW0785V4;initial catalog=Project_1;user id=sa;password=password"; // connection string to SQL Server
        var lots = new List<Lot>(); // empty list to add results to

        using (SqlConnection sqlConnection = new SqlConnection(connectionString))  // using statement to ensure resources are released after query is done
        {
            // help from: https://learn.microsoft.com/en-us/dotnet/framework/data/adonet/retrieving-data-using-a-datareader
            sqlConnection.Open();
            string query = "SELECT * FROM dbo.Lot;"; // SQL query to get all lots; can edit for other methods like GetAvailableLots, GetSoldLots, GetLot etc.
            SqlCommand command = new(query, sqlConnection);

            SqlDataReader reader = command.ExecuteReader(); // execute the query; returns a data reader object
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    var lot = new Lot()
                    {
                        LotId = SqlExtensions.GetInt32(reader, "LotId"),
                        Nickname = SqlExtensions.GetString(reader, "Nickname"),
                        Address = SqlExtensions.GetString(reader, "Address"),
                        Neighborhood = SqlExtensions.GetString(reader, "Neighborhood"),
                        LotSizeAcres = SqlExtensions.GetDecimal(reader, "LotSizeAcres") ?? 0.0m,
                        InterestedPerson_1 = null, // TODO: finish me
                        InterestedPerson_2 = null, // TODO: finish me
                        IsAvailable = SqlExtensions.GetBoolean(reader, "IsAvailable") ?? true,
                    };
                    lots.Add(lot);
                }
            }
            reader.Close();
        }
        return lots;
    }
    public List<Lot> GetAvailableLots()
    {
        string connectionString = "data source=GEIPW0785V4;initial catalog=Project_1;user id=sa;password=password"; // connection string to SQL Server
        var lots = new List<Lot>(); // empty list to add results to

        using (SqlConnection sqlConnection = new SqlConnection(connectionString))  // using statement to ensure resources are released after query is done
        {
            // help from: https://learn.microsoft.com/en-us/dotnet/framework/data/adonet/retrieving-data-using-a-datareader
            sqlConnection.Open();
            string query = "SELECT * FROM dbo.Lot WHERE isAvailable = 1;"; // SQL query to get all lots; can edit for other methods like GetAvailableLots, GetSoldLots, GetLot etc.
            SqlCommand command = new(query, sqlConnection);

            SqlDataReader reader = command.ExecuteReader(); // execute the query; returns a data reader object
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    var lot = new Lot()
                    {
                        LotId = SqlExtensions.GetInt32(reader, "LotId"),
                        Nickname = SqlExtensions.GetString(reader, "Nickname"),
                        Address = SqlExtensions.GetString(reader, "Address"),
                        Neighborhood = SqlExtensions.GetString(reader, "Neighborhood"),
                        LotSizeAcres = SqlExtensions.GetDecimal(reader, "LotSize_Acres") ?? 0.0m,
                        InterestedPerson_1 = null, // TODO: finish me
                        InterestedPerson_2 = null, // TODO: finish me
                        IsAvailable = SqlExtensions.GetBoolean(reader, "IsAvailable") ?? true,
                    };
                    lots.Add(lot);
                }
            }
            reader.Close();
        }
        return lots;
    }

    public List<Lot> GetSoldLots()
    {
        string connectionString = "data source=GEIPW0785V4;initial catalog=Project_1;user id=sa;password=password"; // connection string to SQL Server
        var lots = new List<Lot>(); // empty list to add results to

        using (SqlConnection sqlConnection = new SqlConnection(connectionString))  // using statement to ensure resources are released after query is done
        {
            // help from: https://learn.microsoft.com/en-us/dotnet/framework/data/adonet/retrieving-data-using-a-datareader
            sqlConnection.Open();
            string query = "SELECT * FROM dbo.Lot WHERE isAvailable = 0;"; // SQL query to get all lots; can edit for other methods like GetAvailableLots, GetSoldLots, GetLot etc.
            SqlCommand command = new(query, sqlConnection);

            SqlDataReader reader = command.ExecuteReader(); // execute the query; returns a data reader object
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    var lot = new Lot()
                    {
                        LotId = SqlExtensions.GetInt32(reader, "LotId"),
                        Nickname = SqlExtensions.GetString(reader, "Nickname"),
                        Address = SqlExtensions.GetString(reader, "Address"),
                        Neighborhood = SqlExtensions.GetString(reader, "Neighborhood"),
                        LotSizeAcres = SqlExtensions.GetDecimal(reader, "LotSize_Acres") ?? 0.0m,
                        InterestedPerson_1 = null, // TODO: finish me
                        InterestedPerson_2 = null, // TODO: finish me
                        IsAvailable = SqlExtensions.GetBoolean(reader, "IsAvailable") ?? true,
                    };
                    lots.Add(lot);
                }
            }
            reader.Close();
        }
        return lots;
    }

    public bool AddLot(Lot lot)
    {
        string connectionString = "data source=GEIPW0785V4;initial catalog=Project_1;user id=sa;password=password"; // connection string to SQL Server
        using SqlConnection sqlConnection = new(connectionString); // using statement to ensure resources are released after query is done
        sqlConnection.Open();
        string query = "INSERT INTO dbo.Lot (Nickname, Address, Neighborhood, LotSize_Acres, IsAvailable) VALUES (@Nickname, @Address, @Neighborhood, @LotSize_Acres, @IsAvailable);"; // SQL query to insert a new lot
        SqlCommand command = new(query, sqlConnection);

        command.Parameters.AddWithValue("@Nickname", lot.Nickname);
        command.Parameters.AddWithValue("@Address", lot.Address);
        command.Parameters.AddWithValue("@Neighborhood", lot.Neighborhood);
        command.Parameters.AddWithValue("@LotSize_Acres", lot.LotSizeAcres);
        command.Parameters.AddWithValue("@IsAvailable", lot.IsAvailable);
        // command.ExecuteNonQuery(); // execute the query
        int rowsReturned = command.ExecuteNonQuery();

        if (rowsReturned > 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void UpdateLot(Lot lot)
    {
        // TODO: finish me
    }

    public Lot? GetLot(int? lotId)
    {
        string connectionString = "data source=GEIPW0785V4;initial catalog=Project_1;user id=sa;password=password"; // connection string to SQL Server
        List<Lot> lots = new List<Lot>(); // empty list to add results to
        int Id = lotId ?? 0;

        // help from: https://learn.microsoft.com/en-us/dotnet/framework/data/adonet/retrieving-data-using-a-datareader
        using SqlConnection sqlConnection = new(connectionString); // using statement to ensure resources are released after query is done
        sqlConnection.Open();
        string query = $"SELECT * FROM dbo.Lot WHERE LotId = {Id};"; // SQL query to get all lots; can edit for other methods like GetAvailableLots, GetSoldLots, GetLot etc.
        SqlCommand command = new(query, sqlConnection);
        SqlDataReader reader = command.ExecuteReader(); // execute the query; returns a data reader object
        if (reader.HasRows)
        {
            while (reader.Read())
            {
                var lot = new Lot()
                {
                    LotId = SqlExtensions.GetInt32(reader, "LotId"),
                    Nickname = SqlExtensions.GetString(reader, "Nickname"),
                    Address = SqlExtensions.GetString(reader, "Address"),
                    Neighborhood = SqlExtensions.GetString(reader, "Neighborhood"),
                    LotSizeAcres = SqlExtensions.GetDecimal(reader, "LotSize_Acres") ?? 0.0m,
                    InterestedPerson_1 = null, // TODO: finish me
                    InterestedPerson_2 = null, // TODO: finish me
                    IsAvailable = SqlExtensions.GetBoolean(reader, "IsAvailable") ?? true,
                };
                lots.Add(lot);
            }
        }
        reader.Close();
        return lots.FirstOrDefault();
    }

    public bool RemoveLot(int? lotId)
    {
        string connectionString = "data source=GEIPW0785V4;initial catalog=Project_1;user id=sa;password=password"; // connection string to SQL Server
        var lots = new List<Lot>();
        //int rowsAffectedByQuery = 0;

        using (SqlConnection sqlConnection = new SqlConnection(connectionString))
        {
            // note: in a true production application we'd want to do parameterized SQL, in order to prevent SQL Injection, e.g.: https://xkcd.com/327/
            string query = "DELETE FROM dbo.Lot WHERE LotId = " + lotId + ";";
            SqlCommand command = new(query, sqlConnection);
            sqlConnection.Open();

            // ExecuteNonQuery is for when you want to execute a query that doesn't return a table set, something like a DELETE
            // where you just want to fire it off and all you care about is the number of rows affected
            // so if rowsAffectedByQuery is > 0 then that means that it deleted some rows
            // https://learn.microsoft.com/en-us/dotnet/api/system.data.sqlclient.sqlcommand.executenonquery?view=netframework-4.8.1&viewFallbackFrom=net-8.0
            int rowsAffectedByQuery = command.ExecuteNonQuery();

            if (rowsAffectedByQuery > 0)
            {
                return true;
            }
            return false;
        }
    }
}


// LotId = lotId;
// Nickname = nickname;
// Address = address;
// Neighborhood = neighborhood;
// LotSizeAcres = lotSizeAcres;
// InterestedPerson_1 = interestedPerson_1;
// InterestedPerson_2 = interestedPerson_2;
// IsAvailable = isAvailable;