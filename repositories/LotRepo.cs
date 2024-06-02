using System.Data.SqlClient;
using Geico.Moat.App.Fnd.Data.ADO;
public class LotRepo  //changed from LotStorage to LotRepo
{
    //public Dictionary<int, Lot> Lot;
    // public int nextId = 1;
    public LotRepo()
    { }

    public List<Lot> RetrieveAllLots()
    {
        string connectionString = "data source=GEIPW0785V4;initial catalog=Project_1;user id=sa;password=password"; // connection string to SQL Server
        var lots = new List<Lot>(); // empty list to add results to

        using (SqlConnection sqlConnection = new SqlConnection(connectionString))  // using statement to ensure resources are released after query is done
        {
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
                        Neighborhood = SqlExtensions.GetString(reader, "Neighborhood") ?? string.Empty,
                        LotSizeAcres = SqlExtensions.GetDecimal(reader, "LotSize_Acres") ?? 0.0m,
                        InterestedCustomer_1 = SqlExtensions.GetInt32(reader, "InterestedCustomer_1") ?? 0,
                        InterestedCustomer_2 = SqlExtensions.GetInt32(reader, "InterestedCustomer_2") ?? 0,
                        IsAvailable = SqlExtensions.GetBoolean(reader, "IsAvailable") ?? true,
                        SoldToCustomerId = SqlExtensions.GetInt32(reader, "SoldToCustomerId") ?? 0,
                        UnderContractToCustomerId = SqlExtensions.GetInt32(reader, "UnderContractToCustomerId") ?? 0,
                        ListedPrice = SqlExtensions.GetDecimal(reader, "ListedPrice") ?? 0.0m,
                        SalePrice = SqlExtensions.GetDecimal(reader, "SalePrice") ?? 0.0m,

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
                        Neighborhood = SqlExtensions.GetString(reader, "Neighborhood") ?? string.Empty,
                        LotSizeAcres = SqlExtensions.GetDecimal(reader, "LotSize_Acres") ?? 0.0m,
                        ListedPrice = SqlExtensions.GetDecimal(reader, "ListedPrice") ?? 0.0m,
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
                        Neighborhood = SqlExtensions.GetString(reader, "Neighborhood") ?? string.Empty,
                        LotSizeAcres = SqlExtensions.GetDecimal(reader, "LotSize_Acres") ?? 0.0m,
                        SoldToCustomerId = SqlExtensions.GetInt32(reader, "SoldToCustomerId") ?? 0,
                        UnderContractToCustomerId = SqlExtensions.GetInt32(reader, "UnderContractToCustomerId") ?? 0,
                        ListedPrice = SqlExtensions.GetDecimal(reader, "ListedPrice") ?? 0.0m,
                        SalePrice = SqlExtensions.GetDecimal(reader, "SalePrice") ?? 0.0m,
                    };
                    lots.Add(lot);
                }
            }
            reader.Close();
        }
        return lots;
    }

    public Lot AddLot(Lot lot)
    {
        string connectionString = "data source=GEIPW0785V4;initial catalog=Project_1;user id=sa;password=password"; // connection string to SQL Server
        var lots = new List<Lot>();
        using SqlConnection sqlConnection = new(connectionString); // using statement to ensure resources are released after query is done
        sqlConnection.Open();
        string query = $"INSERT INTO dbo.Lot OUTPUT inserted.* VALUES (@Nickname, @Address, @Neighborhood, @LotSize_Acres, @IsAvailable, @ListedPrice, @SalePrice, @InterestedCustomer_1, @InterestedCustomer_2, @UnderContractToCustomerId, @SoldToCustomerId)"; // SQL query to insert a new lot
        using SqlCommand command = new(query, sqlConnection);

        command.Parameters.AddWithValue("@Nickname", lot.Nickname);
        command.Parameters.AddWithValue("@Address", lot.Address);
        command.Parameters.AddWithValue("@Neighborhood", lot.Neighborhood);
        command.Parameters.AddWithValue("@LotSize_Acres", lot.LotSizeAcres);
        command.Parameters.AddWithValue("@IsAvailable", lot.IsAvailable);
        command.Parameters.AddWithValue("@InterestedCustomer_1", lot.InterestedCustomer_1);
        command.Parameters.AddWithValue("@InterestedCustomer_2", lot.InterestedCustomer_2);
        command.Parameters.AddWithValue("@UnderContractToCustomerId", lot.UnderContractToCustomerId);
        command.Parameters.AddWithValue("@SoldToCustomerId", lot.SoldToCustomerId);
        command.Parameters.AddWithValue("@ListedPrice", lot.ListedPrice);
        command.Parameters.AddWithValue("@SalePrice", lot.SalePrice);

        SqlDataReader reader = command.ExecuteReader(); // execute the query; returns a data reader object
        if (reader.HasRows)
        {
            while (reader.Read())
            {
                var _lot = new Lot()
                {
                    LotId = SqlExtensions.GetInt32(reader, "LotId"),
                    Nickname = SqlExtensions.GetString(reader, "Nickname"),
                    Address = SqlExtensions.GetString(reader, "Address"),
                    Neighborhood = SqlExtensions.GetString(reader, "Neighborhood") ?? string.Empty,
                    LotSizeAcres = SqlExtensions.GetDecimal(reader, "LotSize_Acres") ?? 0.0m,
                    IsAvailable = SqlExtensions.GetBoolean(reader, "IsAvailable") ?? true,
                    InterestedCustomer_1 = SqlExtensions.GetInt32(reader, "InterestedCustomer_1") ?? 0,
                    InterestedCustomer_2 = SqlExtensions.GetInt32(reader, "InterestedCustomer_2") ?? 0,
                    UnderContractToCustomerId = SqlExtensions.GetInt32(reader, "UnderContractToCustomerId") ?? 0,
                    SoldToCustomerId = SqlExtensions.GetInt32(reader, "SoldToCustomerId") ?? 0,
                    ListedPrice = SqlExtensions.GetDecimal(reader, "ListedPrice") ?? 0.0m,
                    SalePrice = SqlExtensions.GetDecimal(reader, "SalePrice") ?? 0.0m
                };
                lots.Add(_lot);
            }
        }
        reader.Close();
        return lots.FirstOrDefault();
    }

    public bool UpdateLot(Lot lot, int lotId)
    {
        string connectionString = "data source=GEIPW0785V4;initial catalog=Project_1;user id=sa;password=password"; // connection string to SQL Server
        int Id = lotId;
        using SqlConnection sqlConnection = new(connectionString); // using statement to ensure resources are released after query is done
        sqlConnection.Open();
        // string query = $"UPDATE dbo.Lot SET (Nickname, Address, Neighborhood, LotSize_Acres, IsAvailable) VALUES (@Nickname, @Address, @Neighborhood, @LotSize_Acres, @IsAvailable) WHERE LotId = {Id};"; // SQL query to update a lot
        string query = $"UPDATE dbo.Lot SET Nickname = @Nickname, Address = @Address, Neighborhood = @Neighborhood, LotSize_Acres = @LotSize_Acres, IsAvailable = @IsAvailable, ListedPrice = @ListedPrice, SalePrice = @SalePrice, InterestedCustomer_1 = @InterestedCustomer_1, InterestedCustomer_2 = @InterestedCustomer_2, UnderContractToCustomerId = @UnderContractToCustomerId, SoldToCustomerId = @SoldToCustomerId  WHERE LotId = @LotId;";
        SqlCommand command = new(query, sqlConnection);

        command.Parameters.AddWithValue("@LotId", lot.LotId);
        command.Parameters.AddWithValue("@Nickname", lot.Nickname);
        command.Parameters.AddWithValue("@Address", lot.Address);
        command.Parameters.AddWithValue("@Neighborhood", lot.Neighborhood);
        command.Parameters.AddWithValue("@LotSize_Acres", lot.LotSizeAcres);
        command.Parameters.AddWithValue("@IsAvailable", lot.IsAvailable);
        command.Parameters.AddWithValue("@ListedPrice", lot.ListedPrice);
        command.Parameters.AddWithValue("@SalePrice", lot.SalePrice);
        command.Parameters.AddWithValue("@InterestedCustomer_1", lot.InterestedCustomer_1);
        command.Parameters.AddWithValue("@InterestedCustomer_2", lot.InterestedCustomer_2);
        command.Parameters.AddWithValue("@UnderContractToCustomerId", lot.UnderContractToCustomerId);
        command.Parameters.AddWithValue("@SoldToCustomerId", lot.SoldToCustomerId);
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
                    Neighborhood = SqlExtensions.GetString(reader, "Neighborhood") ?? string.Empty,
                    LotSizeAcres = SqlExtensions.GetDecimal(reader, "LotSize_Acres") ?? 0.0m,
                    IsAvailable = SqlExtensions.GetBoolean(reader, "IsAvailable") ?? true,
                    InterestedCustomer_1 = SqlExtensions.GetInt32(reader, "InterestedCustomer_1") ?? 0,
                    InterestedCustomer_2 = SqlExtensions.GetInt32(reader, "InterestedCustomer_2") ?? 0,
                    SoldToCustomerId = SqlExtensions.GetInt32(reader, "SoldToCustomerId") ?? 0,
                    UnderContractToCustomerId = SqlExtensions.GetInt32(reader, "UnderContractToCustomerId") ?? 0,
                    ListedPrice = SqlExtensions.GetDecimal(reader, "ListedPrice") ?? 0.0m,
                    SalePrice = SqlExtensions.GetDecimal(reader, "SalePrice") ?? 0.0m,
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

