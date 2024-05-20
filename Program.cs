﻿using System.Net;
using System.Transactions;

public class Program
{
    static LotRepo lotRepo = new();

    public static void Main(string[] args)
    {
        MainMenu();
    }

    //TODO: Add a menu for the user to log in and access the main menu
    public static void LoginMenu()
    {
        Console.WriteLine("Enter your username: ");
        string username = Console.ReadLine();
        Console.WriteLine("Enter your password: ");
        string password = Console.ReadLine();
    }
    //TODO: Add a menu for the user to access the People data
    //TODO: Add a menu for the user to access the Sales data
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
        Lot lot = lotRepo.GetLot(lotId);
        if (lot != null)
        {
            Console.WriteLine(lot);
        }
    }

    public static void RetrieveAllLots()
    {
        List<Lot> allLots = lotRepo.GetAllLots();
        foreach (Lot lot in allLots)
        {
            Console.WriteLine(lot);
        }
    }

    public static void GetAvailableLots()
    {
        List<Lot> availableLots = lotRepo.GetAvailableLots();
        foreach (Lot lot in availableLots)
        {
            Console.WriteLine(lot);
        }
    }

    public static void GetSoldLots()
    {
        List<Lot> soldLots = lotRepo.GetSoldLots();
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

        lotRepo.AddLot(lot);


    }

    public static void UpdateLot()
    {
        Lot? retrievedLot = PromptForLot();
      
        if (retrievedLot == null) return;
        // var updatedLot = lotRepo.UpdateLot(retrievedLot);
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
        lotRepo.UpdateLot(retrievedLot);
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
                lotRepo.RemoveLot(retrievedLot);
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
            retrievedLot = lotRepo.GetLot(input);
        }
        return retrievedLot;
    }
}

