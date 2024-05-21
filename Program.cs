using System.Net;
using System.Transactions;

public class Program
{
    static LotService lotService = new(); //changed from lotRepo to lotService
    static PersonRepo personRepo = new(); //changed from personStorage to personRepo

    public static void Main(string[] args)
    {//   **get this from Movie USER code to set up a shared connection
     // string path = @"C:\Users\U1H402\Documents\RevatureTraining\Project_1-1\project_1.txt";
     // string connectionString = File.ReadAllText(path);
     // System.Console.WriteLine(connectionString);  //remove later
     LoginMenu();
     MainMenu();

    }

    //TODO: Add a menu for the user to log in and access the main menu
    public static void LoginMenu()
    {
        System.Console.WriteLine("__Building Lot Management System__");
        Console.WriteLine("Please enter your User Id: ");
        string userId = Console.ReadLine();
        Person thisPerson = personRepo.GetUser(userId);
        if (thisPerson != null)
            Console.WriteLine("Enter your password: ");
            string password = Console.ReadLine();
            //if input password matches the password in the database

            if (thisPerson.Password == password)
            {
                Console.WriteLine("Welcome " + thisPerson.FirstName + " " + thisPerson.LastName);
                MainMenu();
            }
            else
            {
                Console.WriteLine("Incorrect password. Please try again.");
            }
    }
       public static void MainMenu()
    {
        Console.WriteLine("1 - Get Lot");
        Console.WriteLine("2 - Get All Lots");
        Console.WriteLine("3 - Get Available Lots");
        Console.WriteLine("4 - Get Sold Lots");
        Console.WriteLine("5 - Add Lot");
        Console.WriteLine("6 - Update Lot");
        Console.WriteLine("7 - Delete Lot");
        Console.WriteLine("0 - Exit");
        Console.WriteLine("Enter an option number: ");

        int input = int.Parse(Console.ReadLine() ?? "0");
        switch (input)
        {
            case 1:
                GetLot();
                break;
            case 2:
                RetrieveAllLots();
                break;
            case 3:
                GetAvailableLots();
                break;
            case 4:
                GetSoldLots();
                break;
            case 5:
                AddLot();
                break;
            case 6:
                UpdateLot();
                break;
            case 7:
                DeleteLot();
                break;
            case 0:
                break;
            default:
                Console.WriteLine("Invalid input");
                break;
        }
    }

    public static void GetLot()
    {
        Console.WriteLine("Enter Lot Number: ");
        int lotId = int.Parse(Console.ReadLine() ?? "0");
        Lot lot = lotService.GetLot(lotId);
        if (lot != null)
        {
            Console.WriteLine(lot);
        }
    }

    public static void RetrieveAllLots()
    {
        List<Lot> allLots = lotService.GetAllLots();
        foreach (Lot lot in allLots)
        {
            Console.WriteLine(lot);
        }
    }

    public static void GetAvailableLots()
    {
        List<Lot> availableLots = lotService.GetAvailableLots();
        foreach (Lot lot in availableLots)
        {
            Console.WriteLine(lot);
        }
    }

    public static void GetSoldLots()
    {
        List<Lot> soldLots = lotService.GetSoldLots();
        foreach (Lot lot in soldLots)
        {
            Console.WriteLine(lot);
        }
    }

    //TODO: FIX THIS METHOD   
    public static void AddLot()
    {
        Lot lot = new Lot();

        Console.WriteLine("Enter a lot nickname: ");
        string nickname = Console.ReadLine() ?? string.Empty;
        Console.WriteLine("Enter the full property address: ");
        string address = Console.ReadLine() ?? string.Empty;
        Console.WriteLine("Enter the neighborhood or area name: ");
        string neighborhood = Console.ReadLine() ?? string.Empty;
        Console.WriteLine("Enter the lot size (acres): ");
        decimal lotSizeAcres = decimal.TryParse(Console.ReadLine(), out decimal result) ? result : 0.0m;
        Console.WriteLine("Is the lot available to sell? (true/false): ");
        bool isAvailable = bool.TryParse(Console.ReadLine(), out bool isAvailableResult) && isAvailableResult;

        lot.Nickname = nickname;
        lot.Address = address;
        lot.Neighborhood = neighborhood;
        lot.LotSizeAcres = lotSizeAcres;
        lot.IsAvailable = isAvailable;

        lotService.AddLot(lot);


    }

    public static void UpdateLot()
    {
        Lot? retrievedLot = PromptForLot();

        if (retrievedLot == null) return;
        // var updatedLot = lotService.UpdateLot(retrievedLot);
        if (retrievedLot != null)
        {
            System.Console.WriteLine("Lot found: " + retrievedLot);
            Console.WriteLine("Enter new information for the lot");
            System.Console.WriteLine("OR Press Enter to keep the existing value");
            Console.WriteLine("Enter a lot nickname: ");
            string nickname = Console.ReadLine() ?? string.Empty;
            Console.WriteLine("Enter the full property address: ");
            string address = Console.ReadLine() ?? string.Empty;
            Console.WriteLine("Enter the neighborhood or area name: ");
            string neighborhood = Console.ReadLine() ?? string.Empty;
            Console.WriteLine("Enter the lot size (acres): ");
            decimal lotSizeAcres = decimal.TryParse(Console.ReadLine(), out decimal result) ? result : 0.0m;
            Console.WriteLine("Is the lot available to sell? (true/false): ");
            bool isAvailable = bool.TryParse(Console.ReadLine(), out bool isAvailableResult) && isAvailableResult;

            retrievedLot.Nickname = nickname;
            retrievedLot.Address = address;
            retrievedLot.Neighborhood = neighborhood;
            retrievedLot.LotSizeAcres = lotSizeAcres;
            retrievedLot.IsAvailable = isAvailable;
        }
        lotService.UpdateLot(retrievedLot);
    }

    public static void DeleteLot()
    {
        Lot? retrievedLot = PromptForLot();
        if (retrievedLot == null) return;


        if (retrievedLot != null)
        {
            System.Console.WriteLine("Lot found: " + retrievedLot);
            Console.WriteLine("Are you sure you want to delete this lot? (yes/no): ");
            string input = Console.ReadLine() ?? string.Empty;
            if (input.ToLower() == "yes")
            {
                lotService.RemoveLot(retrievedLot);
            }
        }
    }

    public static Lot? PromptForLot()
    {
        Lot? retrievedLot = null;
        while (retrievedLot == null)
        {
            Console.WriteLine("Enter Lot Number or 0 to exit:");
            int input = int.Parse(Console.ReadLine() ?? "0");
            if (input == 0)
            {
                break;
            }
            retrievedLot = lotService.GetLot(input);
        }
        return retrievedLot;
    }

    public static void AddPerson()
    {
        //TODO add this
    }
    public static void UpdatePerson()
    {
        //TODO add this
    }
    public static void GetPerson()
    {
        //TODO add this
    }
    // public static void DeletePerson()
    // {
    //     //TODO add this
    // }

}

