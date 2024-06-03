using System.Diagnostics;
using System.Net;
using System.Transactions;
using System.Linq;
using System.ComponentModel.DataAnnotations;

public class Program
{
    static LotService lotService = new();
    static PersonService personService = new();

    public static void Main(string[] args)
    {
        OpeningMenu();
        Console.ReadKey();
        LoginMenu();
        Console.WriteLine("Press any key to exit the program");
        Console.ReadKey();
        MainMenu();
        // SalesMenu();
        CustomerMenu();
        // AdminMenu();
    }

    public static void OpeningMenu()
    {
        Console.WriteLine("*********************************************");
        Console.WriteLine("");
        Console.WriteLine("Welcome to the Building Lot Management System");
        Console.WriteLine("");
        Console.WriteLine("*********************************************");
        Console.WriteLine("Enter an option number to begin:");
        Console.WriteLine("");
        Console.WriteLine("1 - View Available Lots");
        Console.WriteLine("2 - Login Menu");
        Console.WriteLine("");
        int input = int.Parse(Console.ReadLine());

        switch (input)
        {
            case 1:
                GetAvailableLots();
                break;
            case 2:
                LoginMenu();
                break;
            default:
                Console.WriteLine("Invalid input. Please try again.");
                break;
        }
    }
    public static void LoginMenu()
    {
        System.Console.WriteLine("********************************");
        System.Console.WriteLine("Login Menu");
        System.Console.WriteLine("_______________________________");
        Console.WriteLine("Please enter your User Id: ");
        string userId = Console.ReadLine();
        Person currentUser = personService.GetUser(userId);
        while (currentUser == null)
        {
            Console.WriteLine("User not found. Please try again.");
            Console.WriteLine("Please enter your User Id: ");
            userId = Console.ReadLine();
            currentUser = personService.GetUser(userId);
        }
        Console.WriteLine("Enter your password: ");
        string password = Console.ReadLine();
        //if input password matches the password in the database
        while (currentUser.Password != password)
        {
            Console.WriteLine("Incorrect password. Please try again.");
            Console.WriteLine("Enter your password: ");
            password = Console.ReadLine();
        }
        System.Console.WriteLine("********************************");
        System.Console.WriteLine(" ");
        Console.WriteLine("Welcome, " + currentUser.FirstName + " " + currentUser.LastName + "!");
        System.Console.WriteLine("  ");
        MainMenu();
    }
    public static void MainMenu()
    {
        System.Console.WriteLine("********************************");
        System.Console.WriteLine(" ");
        Console.WriteLine("Main Menu");
        System.Console.WriteLine("_______________________________");
        Console.WriteLine(" ");
        Console.WriteLine("1 - Get Lot");
        Console.WriteLine("2 - Get All Lots");
        Console.WriteLine("3 - Get Available Lots");
        Console.WriteLine("4 - Get Sold Lots");
        Console.WriteLine("5 - Add Lot");
        Console.WriteLine("6 - Update Lot");
        Console.WriteLine("7 - Customer Menu");
        //Console.WriteLine("8 - Admin Menu"); //not fully implemented
        Console.WriteLine("0 - Exit");
        System.Console.WriteLine("_______________________________");
        Console.WriteLine("Enter an option number: ");

        int input;
        while (!int.TryParse(Console.ReadLine(), out input) || input != 0)
        {
            ProcessInput(input);
            Console.WriteLine("Enter an option number: ");
        }
    }
    public static void ProcessInput(int input)  //Main Menu switch statement
    {
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
                CustomerMenu();
                break;
            // case 8:
            //     AdminMenu();
            // break;
            default:
                Console.WriteLine("Invalid input. Please try again.");
                break;
        }
    }

    public static void CustomerMenu()
    {
        System.Console.WriteLine(" ");
        Console.WriteLine("********************************");
        System.Console.WriteLine(" ");
        Console.WriteLine("Customer Menu");
        Console.WriteLine("_______________________________");
        System.Console.WriteLine(" ");
        Console.WriteLine("1 - Add New Customer"); //AddPerson w/out UserId, Password, Role
        Console.WriteLine("2 - Update Customer"); //UpdatePerson
        Console.WriteLine("3 - View Customer"); //GetPerson
        Console.WriteLine("4 - View All Interested Customers"); //GetInterestedCustomer
        Console.WriteLine("5 - View All Customers"); //GetAllCustomers
        Console.WriteLine("0 - Exit");
        System.Console.WriteLine("_______________________________");
        Console.WriteLine("Enter an option number: ");

        int input;
        while (!int.TryParse(Console.ReadLine(), out input) || input != 0)
        {
            ProcessCustomerMenuInput(input);
            Console.WriteLine("Enter an option number: ");
        }
    }
    private static void ProcessCustomerMenuInput(int input)  //Customer Menu switch statement
    {
        switch (input)
        {
            case 1:
                AddCustomer();
                break;
            case 2:
                UpdatePerson();
                break;
            case 3:
                GetPerson();
                break;
            case 4:
                GetInterestedCustomer();
                break;
            case 5:
                GetAllCustomers();
                break;
            case 0:
                break;
            default:
                Console.WriteLine("Invalid input. Please try again.");
                break;
        }
    }
    public static void AdminMenu() //not implemented
    {
        // TODO: AdminMenu
        //add admin specific methods for employee records, delete functionality, add admin, update admin, user name and password updates
        Console.WriteLine("******************************");
        Console.WriteLine("Administration Menu");
        Console.WriteLine("******************************");
        Console.WriteLine("1 - Add a New Employee");
        Console.WriteLine("2 - Update Employee");
        Console.WriteLine("3 - View All Active Employees");
        Console.WriteLine("4 - View All Inactive Employees");
        Console.WriteLine("5 - View All Admins");
        Console.WriteLine("6 - View All Users");
        Console.WriteLine("7 - Delete Employee");
        Console.WriteLine("0 - Exit");
        Console.WriteLine("_______________________________");
        Console.WriteLine("Enter an option number: ");

        int input;
        while (!int.TryParse(Console.ReadLine(), out input) || input != 0)
        {
            ProcessCustomerMenuInput(input);
            Console.WriteLine("Enter an option number: ");
        }
    }

    public static void GetLot()
    {
        Lot? retrievedLot = PromptForLot();
        if (retrievedLot != null)
        {
            System.Console.WriteLine("_______________________________");
            Console.WriteLine($"Lot ID: {retrievedLot.LotId}");
            Console.WriteLine($"Lot Nickname: {retrievedLot.Nickname}");
            Console.WriteLine($"Neighborhood: {retrievedLot.Neighborhood}");
            Console.WriteLine($"Lot Size (acres): {retrievedLot.LotSizeAcres}");
            Console.WriteLine($"Is Available: {retrievedLot.IsAvailable}");
            Console.WriteLine($"Interested Customer 1: {retrievedLot.InterestedCustomer_1}");
            Console.WriteLine($"Interested Customer 2: {retrievedLot.InterestedCustomer_2}");
            Console.WriteLine($"Under Contract To Customer: {retrievedLot.UnderContractToCustomerId}");
            Console.WriteLine($"Sold To Customer: {retrievedLot.SoldToCustomerId}");
            Console.WriteLine($"List Price: {retrievedLot.ListedPrice}");
            Console.WriteLine($"Sale Price: {retrievedLot.SalePrice}");
            System.Console.WriteLine("_______________________________");
            System.Console.WriteLine("");
            System.Console.WriteLine("Please make your next selection:");
            MainMenu();
        }
        else
        {
            Console.WriteLine("*********************************");
            Console.WriteLine("Lot not found. Please try again.");
            Console.WriteLine("*********************************");
            System.Console.WriteLine(" ");
            MainMenu();
        }
    }
    public static void RetrieveAllLots()
    {
        List<Lot> allLots = lotService.GetAllLots();
        if (allLots.Count == 0)
        {
            Console.WriteLine("No available lots were found.");
            return;
        }
        foreach (Lot lot in allLots)
        {
            System.Console.WriteLine("_______________________________");
            Console.WriteLine($"Lot ID: {lot.LotId}");
            Console.WriteLine($"Lot Nickname: {lot.Nickname}");
            Console.WriteLine($"Neighborhood: {lot.Neighborhood}");
            Console.WriteLine($"Lot Size (acres): {lot.LotSizeAcres}");
            Console.WriteLine($"Is Available: {lot.IsAvailable}");
            Console.WriteLine($"Interested Customer 1: {lot.InterestedCustomer_1}");
            Console.WriteLine($"Interested Customer 2: {lot.InterestedCustomer_2}");
            Console.WriteLine($"Under Contract To Customer: {lot.UnderContractToCustomerId}");
            Console.WriteLine($"Sold To Customer: {lot.SoldToCustomerId}");
            Console.WriteLine($"List Price: {lot.ListedPrice}");
            Console.WriteLine($"Sale Price: {lot.SalePrice}");
            System.Console.WriteLine("_______________________________");
        }
    }

    public static void GetAvailableLots()
    {
        List<Lot> availableLots = lotService.GetAvailableLots();
        if (availableLots.Count == 0)
        {
            Console.WriteLine("No available lots were found.");
            return;
        }
        foreach (Lot lot in availableLots)
        {
            System.Console.WriteLine("_______________________________");
            Console.WriteLine($"Lot ID: {lot.LotId}");
            Console.WriteLine($"Lot Nickname: {lot.Nickname}");
            Console.WriteLine($"Neighborhood: {lot.Neighborhood}");
            System.Console.WriteLine($"Address: {lot.Address}");
            Console.WriteLine($"Lot Size (acres): {lot.LotSizeAcres}");
            Console.WriteLine($"List Price: {lot.ListedPrice}");
            System.Console.WriteLine("_______________________________");
        }
    }

    public static void GetSoldLots()
    {
        List<Lot> soldLots = lotService.GetSoldLots();
        if (soldLots.Count == 0)
        {
            Console.WriteLine("No sold lots were found.");
            return;
        }
        foreach (Lot lot in soldLots)
        {
            System.Console.WriteLine("_______________________________");
            Console.WriteLine($"Lot ID: {lot.LotId}");
            Console.WriteLine($"Lot Nickname: {lot.Nickname}");
            Console.WriteLine($"Neighborhood: {lot.Neighborhood}");
            Console.WriteLine($"Lot Size (acres): {lot.LotSizeAcres}");
            Console.WriteLine($"Is Available: {lot.IsAvailable}");
            Console.WriteLine($"Interested Customer 1: {lot.InterestedCustomer_1}");
            Console.WriteLine($"Interested Customer 2: {lot.InterestedCustomer_2}");
            Console.WriteLine($"Under Contract To Customer: {lot.UnderContractToCustomerId}");
            Console.WriteLine($"Sold To Customer: {lot.SoldToCustomerId}");
            Console.WriteLine($"List Price: {lot.ListedPrice}");
            Console.WriteLine($"Sale Price: {lot.SalePrice}");
            System.Console.WriteLine("_______________________________");

        }
    }

    public static void AddLot()
    //TODO: Add in the Sales Record and related Person(s) to the input
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
        System.Console.WriteLine("Listing Price: ");
        decimal listedPrice = decimal.TryParse(Console.ReadLine(), out decimal listedPriceResult) ? listedPriceResult : 0.0m;
        System.Console.WriteLine("Sale Price: ");
        decimal salePrice = decimal.TryParse(Console.ReadLine(), out decimal salePriceResult) ? salePriceResult : 0.0m;

        lot.Nickname = nickname;
        lot.Address = address;
        lot.Neighborhood = neighborhood;
        lot.LotSizeAcres = lotSizeAcres;
        lot.IsAvailable = isAvailable;
        lot.ListedPrice = listedPrice;
        lot.SalePrice = salePrice;
        lot.InterestedCustomer_1 = 0;
        lot.InterestedCustomer_2 = 0;
        lot.UnderContractToCustomerId = 0;
        lot.SoldToCustomerId = 0;

        lotService.AddLot(lot);
    }
    public static void UpdateLot()
    {
        Lot? retrievedLot = PromptForLot();

        if (retrievedLot == null)
        {
            System.Console.WriteLine("Lot not found");
            return;
        }
        if (retrievedLot != null)
        {
            System.Console.WriteLine("_______________________________");
            System.Console.WriteLine("Lot found: ");
            System.Console.WriteLine(" ");
            Console.WriteLine($"Lot ID: {retrievedLot.LotId}");
            Console.WriteLine($"Lot Nickname: {retrievedLot.Nickname}");
            Console.WriteLine($"Neighborhood: {retrievedLot.Neighborhood}");
            System.Console.WriteLine($"Address: {retrievedLot.Address}");
            Console.WriteLine($"Lot Size (acres): {retrievedLot.LotSizeAcres}");
            Console.WriteLine($"Is Available: {retrievedLot.IsAvailable}");
            Console.WriteLine($"Interested Customer 1: {retrievedLot.InterestedCustomer_1}");
            Console.WriteLine($"Interested Customer 2: {retrievedLot.InterestedCustomer_2}");
            Console.WriteLine($"Under Contract To Customer: {retrievedLot.UnderContractToCustomerId}");
            Console.WriteLine($"Sold To Customer: {retrievedLot.SoldToCustomerId}");
            Console.WriteLine($"List Price: {retrievedLot.ListedPrice}");
            Console.WriteLine($"Sale Price: {retrievedLot.SalePrice}");
            System.Console.WriteLine("_______________________________");
            System.Console.WriteLine("");
            Console.WriteLine("Enter new information to update the lot");
            System.Console.WriteLine("OR Press Enter to keep the existing value");
            System.Console.WriteLine("_______________________________");
            System.Console.WriteLine("");
            Console.WriteLine("Enter a lot nickname: ");
            var inputNickname = Console.ReadLine();
            string nickname = inputNickname == string.Empty ? retrievedLot.Nickname : inputNickname;
            Console.WriteLine("Enter the neighborhood or area name: ");
            var inputNeighborhood = Console.ReadLine();
            string neighborhood = inputNeighborhood == string.Empty ? retrievedLot.Neighborhood : inputNeighborhood;
            Console.WriteLine("Enter the full property address: ");
            var inputAddress = Console.ReadLine();
            string address = inputAddress == string.Empty ? retrievedLot.Address : inputAddress;
            Console.WriteLine("Enter the lot size (acres): ");
            var inputLotSizeAcres = Console.ReadLine();
            decimal lotSizeAcres = inputLotSizeAcres == string.Empty ? retrievedLot.LotSizeAcres : decimal.Parse(inputLotSizeAcres);

            Console.WriteLine("Is the lot available to sell? (true/false): ");
            string inputIsAvailable = InputIsAvailable();  // Handling for t|true and f|false
            bool isAvailable = inputIsAvailable == string.Empty ? retrievedLot.IsAvailable : bool.Parse(inputIsAvailable);

            Console.WriteLine("Interested Customer 1 Id: ");
            var inputInterestedCustomer1 = Console.ReadLine();
            int? interestedCustomer_1 = inputInterestedCustomer1 == string.Empty ? retrievedLot.InterestedCustomer_1 : int.Parse(inputInterestedCustomer1);
            Console.WriteLine("Interested Customer 2 Id: ");
            var inputInterestedCustomer2 = Console.ReadLine();
            int? interestedCustomer_2 = inputInterestedCustomer2 == string.Empty ? retrievedLot.InterestedCustomer_2 : int.Parse(inputInterestedCustomer2);

            Console.WriteLine("Under Contract To Customer Id: ");
            var inputUnderContractToCustomerId = Console.ReadLine();
            int? UnderContractToCustomerId = inputUnderContractToCustomerId == string.Empty ? retrievedLot.UnderContractToCustomerId : int.Parse(inputUnderContractToCustomerId);

            Console.WriteLine("Sold To Customer Id: ");
            var inputSoldToCustomerId = Console.ReadLine();
            int? SoldToCustomerId = inputSoldToCustomerId == string.Empty ? retrievedLot.SoldToCustomerId : int.Parse(inputSoldToCustomerId);

            Console.WriteLine("List Price: ");
            var inputListedPrice = Console.ReadLine();
            decimal? ListedPrice = inputListedPrice == string.Empty ? retrievedLot.ListedPrice : decimal.TryParse(inputListedPrice, out decimal listedPrice) ? (decimal)listedPrice : 0.0m;

            Console.WriteLine("Sale Price: ");
            var inputSalePrice = Console.ReadLine();
            //decimal? SalePrice = inputSalePrice == string.Empty ? retrievedLot.SalePrice : decimal.TryParse(inputSalePrice, out decimal salePrice) ? (decimal)salePrice : 0.0m;
            decimal SalePrice = inputSalePrice == string.Empty ? retrievedLot.SalePrice.GetValueOrDefault() : decimal.Parse(inputSalePrice);

            retrievedLot.Nickname = nickname;
            retrievedLot.Neighborhood = neighborhood;
            retrievedLot.Address = address;
            retrievedLot.LotSizeAcres = lotSizeAcres;
            retrievedLot.IsAvailable = isAvailable;
            retrievedLot.InterestedCustomer_1 = interestedCustomer_1;
            retrievedLot.InterestedCustomer_2 = interestedCustomer_2;
            retrievedLot.UnderContractToCustomerId = UnderContractToCustomerId;
            retrievedLot.SoldToCustomerId = SoldToCustomerId;
            retrievedLot.ListedPrice = ListedPrice;
            retrievedLot.SalePrice = SalePrice;

        }
        lotService.UpdateLot(retrievedLot, retrievedLot.LotId ?? 0);
        System.Console.WriteLine("_______________________________ ");
        System.Console.WriteLine("Lot updated successfully.");
        System.Console.WriteLine("_______________________________ ");
        MainMenu();
    }

    public static void DeleteLot()
    //TODO: REMOVE this method
    {
        Lot? retrievedLot = PromptForLot();
        if (retrievedLot == null)
        {
            System.Console.WriteLine("Lot not found");
            return;
        }

        if (retrievedLot != null)
        {
            System.Console.WriteLine("Here's what I found: ");
            System.Console.WriteLine("_______________________________");
            Console.WriteLine($"Lot ID: {retrievedLot.LotId}");
            Console.WriteLine($"Lot Nickname: {retrievedLot.Nickname}");
            Console.WriteLine($"Neighborhood: {retrievedLot.Neighborhood}");
            Console.WriteLine($"Lot Size (acres): {retrievedLot.LotSizeAcres}");
            Console.WriteLine($"Is Available: {retrievedLot.IsAvailable}");
            Console.WriteLine($"Interested Customer 1: {retrievedLot.InterestedCustomer_1}");
            Console.WriteLine($"Interested Customer 2: {retrievedLot.InterestedCustomer_2}");
            Console.WriteLine($"Under Contract To Customer: {retrievedLot.UnderContractToCustomerId}");
            Console.WriteLine($"Sold To Customer: {retrievedLot.SoldToCustomerId}");
            Console.WriteLine($"List Price: {retrievedLot.ListedPrice}");
            Console.WriteLine($"Sale Price: {retrievedLot.SalePrice}");
            System.Console.WriteLine("_______________________________");
            Console.WriteLine("Are you sure you want to delete this lot? (yes/no): ");
            string input = Console.ReadLine() ?? string.Empty;
            if (input.ToLower() == "yes")
            {
                lotService.RemoveLot(retrievedLot);

                // Verify deletion
                Lot? verifyLot = lotService.GetLot(retrievedLot.LotId ?? 0);
                if (verifyLot == null)
                {
                    Console.WriteLine("Lot successfully deleted");
                }
                else
                {
                    Console.WriteLine("Failed to delete lot. Please try again.");
                    return;
                }
            }
            else
            {
                Console.WriteLine("Ok, this Lot will not be deleted");
                return;
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

    public static string InputIsAvailable()  // Handling for t|true and f|false
    {
        var inputIsAvailable = Console.ReadLine();

        if (inputIsAvailable == "t" || inputIsAvailable == "true" || inputIsAvailable == "T" || inputIsAvailable == "TRUE")
        {
            inputIsAvailable = "true";
        }
        else if (inputIsAvailable == "f" || inputIsAvailable == "false" || inputIsAvailable == "F" || inputIsAvailable == "FALSE")
        {
            inputIsAvailable = "false";
        }
        else if (inputIsAvailable == string.Empty)
        {
            inputIsAvailable = null;
        }
        return inputIsAvailable ?? string.Empty;
    }

    public static Person? BasePerson()
    {
        Person newPerson = new Person();

        Console.WriteLine("Enter the first name: ");
        string firstName = Console.ReadLine() ?? string.Empty;
        Console.WriteLine("Enter the last name: ");
        string lastName = Console.ReadLine() ?? string.Empty;
        long phoneNumber;
        while (true)
        {
            Console.Write("Enter 10-digit phone number: ");
            bool isParsed = long.TryParse(Console.ReadLine(), out phoneNumber);
            if (isParsed && phoneNumber.ToString().Length == 10)
            {
                break;
            }
            Console.WriteLine("Invalid phone number. Please try again.");
        }
        string email;
        while (true)
        {
            Console.Write("Enter Email: ");
            email = Console.ReadLine() ?? string.Empty;
            if (email.Contains("@") && email.Contains(".") && email.Length >= 5)
            {
                break;
            }
            Console.WriteLine("Invalid email. Please try again.");
        }
        newPerson.FirstName = firstName;
        newPerson.LastName = lastName;
        newPerson.PhoneNumber = phoneNumber;
        newPerson.Email = email;

        return newPerson;
    }
    public static void AddCustomer()
    {
        Person newPerson = BasePerson();
        if (newPerson == null)
        {
            Console.WriteLine("Failed to add customer");
            return;
        }
        if (newPerson != null)
        {
            Console.WriteLine("Enter Customer Status: ");
            Console.WriteLine("_______________________________");
            Console.WriteLine("[I] Interested");
            Console.WriteLine("[N] Not interested");
            Console.WriteLine("[U] Under contract");
            Console.WriteLine("[S] Sale Complete");
            string status = Console.ReadLine().ToUpper() ?? string.Empty;

            // while (status != "I" || status != "N" || status != "U" || status != "S")
            // {
            //     Console.WriteLine("Invalid status. Please try again.");
            // }
            // if (status == "I" || status == "U" || status == "S")
            // {
            //     System.Console.WriteLine("Enter the Lot ID: ");
            //     int lotId = int.Parse(Console.ReadLine());
            //     Lot lot = lotService.GetLot(lotId);

            //     if (lot == null)
            //     {
            //         Console.WriteLine("Lot not found. Please try again.");
            //         return;
            //     }
            //     newPerson.LotId = lot;
            // }
            newPerson.Person_Type = "Customer";
            newPerson.Status = status;
            newPerson.UserId = " ";
            newPerson.Password = " ";
            newPerson.Role = " ";
        }
        PersonService.AddPerson(newPerson);
        if (newPerson != null)
        {
            Console.WriteLine("Customer added successfully");
            Console.WriteLine(newPerson.ToString());
            System.Console.WriteLine("Press ENTER to return to the menu:");
            Console.ReadKey();
            CustomerMenu();
        }
        else
        {
            Console.WriteLine("Failed to add customer");
            return;
        }
    }
    public static void AddEmployee()
    {
        Person newPerson = BasePerson();
        if (newPerson == null)
        {
            Console.WriteLine("Failed to add customer");
            return;
        }
        if (newPerson != null)
        {
            Console.WriteLine("Enter Employee Role: (Admin, User)");
            string role = Console.ReadLine();
            Console.WriteLine("Enter Employee Status: (Active, Inactive)");
            string status = Console.ReadLine();
            System.Console.WriteLine("Enter User ID: ");
            string userId = Console.ReadLine();

            newPerson.UserId = userId;
            newPerson.Password = "password";
            newPerson.Role = role;
            newPerson.Status = status;
        }
        PersonService.AddPerson(newPerson);
        if (newPerson != null)
        {
            Console.WriteLine("Employee added successfully");
            Console.WriteLine(newPerson.ToString());
            System.Console.WriteLine("Press ENTER to return to the menu:");
            Console.ReadKey();
            CustomerMenu();
        }
        else
        {
            Console.WriteLine("Failed to add employee. Please try again.");
            return;
        }
    }
    public static void GetAllCustomers()
    {
        List<Person> customers = personService.GetAllCustomers();
        if (customers.Count == 0)
        {
            Console.WriteLine("No customers were found.");
            return;
        }
        foreach (Person customer in customers)
        {
            System.Console.WriteLine(" ");
            Console.WriteLine($"Customer ID: {customer.PersonId}");
            Console.WriteLine($"First Name: {customer.FirstName}");
            Console.WriteLine($"Last Name: {customer.LastName}");
            Console.WriteLine($"Phone Number: {customer.PhoneNumber}");
            Console.WriteLine($"Email: {customer.Email}");
            Console.WriteLine($"Person Type: {customer.Person_Type}");
            Console.WriteLine($"Status: {customer.Status}");
            System.Console.WriteLine("_______________________________");
        }
        System.Console.WriteLine(" ");
        System.Console.WriteLine("Press ENTER to return to the menu:");
        Console.ReadKey();
        CustomerMenu();
    }
    public static void GetInterestedCustomer()
    {
        List<Person> interestedCustomers = personService.GetInterestedCustomers();
        if (interestedCustomers.Count == 0)
        {
            Console.WriteLine("No interested customers were found.");
            return;
        }
        foreach (Person customer in interestedCustomers)
        {
            System.Console.WriteLine(" ");
            Console.WriteLine($"Customer ID: {customer.PersonId}");
            Console.WriteLine($"First Name: {customer.FirstName}");
            Console.WriteLine($"Last Name: {customer.LastName}");
            Console.WriteLine($"Phone Number: {customer.PhoneNumber}");
            Console.WriteLine($"Email: {customer.Email}");
            Console.WriteLine($"Person Type: {customer.Person_Type}");
            Console.WriteLine($"Status: {customer.Status}");
            System.Console.WriteLine("_______________________________");
        }
        System.Console.WriteLine(" ");
        System.Console.WriteLine("Press ENTER to return to the menu:");
        Console.ReadKey();
        CustomerMenu();
    }

    public static void UpdatePerson()
    {
        Console.WriteLine("Enter Customer ID or 0 to exit:");
        int input = int.Parse(Console.ReadLine() ?? "0");
        Person retrievedPerson = personService.GetPerson(input);
        if (retrievedPerson == null)
        {
            Console.WriteLine("Person not found");
            return;
        }
        if (retrievedPerson != null)
        {
            Console.WriteLine("Person found: " + retrievedPerson);
            Console.WriteLine("Enter new information for the person");
            Console.WriteLine("OR Press Enter to keep the existing value");
            Console.WriteLine("Enter the first name: ");
            string inputFirstName = Console.ReadLine();
            string firstName = inputFirstName == string.Empty ? retrievedPerson.FirstName : inputFirstName;
            Console.WriteLine("Enter the last name: ");
            string inputLastName = Console.ReadLine();
            string lastName = inputLastName == string.Empty ? retrievedPerson.LastName : inputLastName;
            Console.WriteLine("Enter the phone number: ");
            string inputPhoneNumber = Console.ReadLine();
            long phoneNumber = (long)(inputPhoneNumber == string.Empty ? retrievedPerson.PhoneNumber : long.Parse(inputPhoneNumber));
            Console.WriteLine("Enter the email: ");
            string inputEmail = Console.ReadLine();
            string email = inputEmail == string.Empty ? retrievedPerson.Email : inputEmail;
            Console.WriteLine("Enter the person type: ");
            string inputPersonType = Console.ReadLine();
            string personType = inputPersonType == string.Empty ? retrievedPerson.Person_Type : inputPersonType;
            Console.WriteLine("Enter the status: ");
            string inputStatus = Console.ReadLine();
            string status = inputStatus == string.Empty ? retrievedPerson.Status : inputStatus;
            Console.WriteLine("Enter the user ID: ");
            string inputUserId = Console.ReadLine();
            string userId = inputUserId == string.Empty ? retrievedPerson.UserId : inputUserId;
            Console.WriteLine("Enter the password: ");
            string inputPassword = Console.ReadLine();
            string password = inputPassword == string.Empty ? retrievedPerson.Password : inputPassword;
            Console.WriteLine("Enter the role: ");
            string inputRole = Console.ReadLine();
            string role = inputRole == string.Empty ? retrievedPerson.Role : inputRole;

            retrievedPerson.FirstName = firstName;
            retrievedPerson.LastName = lastName;
            retrievedPerson.PhoneNumber = phoneNumber;
            retrievedPerson.Email = email;
            retrievedPerson.Person_Type = personType;
            retrievedPerson.Status = status;
            retrievedPerson.UserId = userId;
            retrievedPerson.Password = password;
            retrievedPerson.Role = role;
        }
        personService.UpdatePerson(retrievedPerson, retrievedPerson.PersonId ?? 0);
    }
    public static Person? GetPerson()
    {
        Console.WriteLine("Enter Customer ID or 0 to exit:");
        int input = int.Parse(Console.ReadLine() ?? "0");
        Person retrievedPerson = personService.GetPerson(input);
        if (retrievedPerson == null)
        {
            Console.WriteLine("Person not found");
        }
        if (retrievedPerson != null)
        {
            Console.WriteLine("Person found: " + retrievedPerson);
        }
        return retrievedPerson;
    }
    // public static void DeletePerson() //Remove
    //                                   //TODO: REMOVE this method
    // {
    //     Person? retrievedPerson = PromptForPerson();
    //     if (retrievedPerson == null)
    //     {
    //         Console.WriteLine("Person not found");
    //         return;
    //     }
    //     if (retrievedPerson != null)
    //     {
    //         Console.WriteLine("Here's what I found: " + retrievedPerson);
    //         Console.WriteLine("Are you sure you want to delete this person? (yes/no): ");
    //         string input = Console.ReadLine() ?? string.Empty;

    //         if (input.ToLower() == "yes")
    //         {
    //             personService.RemovePerson(retrievedPerson);

    //             // Verify deletion
    //             Person? verifyPerson = personService.GetPerson(retrievedPerson.PersonId ?? 0);
    //             if (verifyPerson == null)
    //             {
    //                 Console.WriteLine("Person successfully deleted");
    //             }
    //             else
    //             {
    //                 Console.WriteLine("Failed to delete person");
    //                 return;
    //             }
    //         }
    //         else
    //         {
    //             Console.WriteLine("Ok, this Person will not be deleted");
    //             return;
    //         }
    //     }
    // }
    public static Person? PromptForPerson()
    {
        Person? retrievedPerson = null;
        while (retrievedPerson == null)
        {
            Console.WriteLine("Enter Person ID or 0 to exit:");
            int input = int.Parse(Console.ReadLine());
            if (input == 0)
            {
                break;
            }
            retrievedPerson = personService.GetPerson(input);
        }
        return retrievedPerson;
    }
    public static void AddSale()
    {
        //TODO: Implement AddSale
    }
    public static void UpdateSale()
    {
        //TODO: Implement UpdateSale
    }
    public static void GetSale()
    {
        //TODO: Implement GetSale
    }
    public static void DeleteSale()
    {
        //TODO: Implement DeleteSale
    }
    public static void GetAvailableSales()
    {
        //TODO: Implement GetAvailableSales
    }
    public static void GetSoldSales()
    {
        //TODO: Implement GetSoldSales
    }
    public static void GetAdmin() //TODO: Implement GetAdmin
    {
        //TODO: Implement GetAdmin - use for Admin login view access
    }
    public static void GetAvailableAdmins()
    {
        //TODO: not sure this is needed - Implement GetAvailableAdmins - bulk pull of all Admins 
    }
}


