public class Sale
{
    public int LotId  { get; set; }  //links to  in Lots.cs; required
    public int SaleId { get; set; }  //add ID generator; required
    public double AcquiredPrice { get; set; } // required
    public long AcquiredDate { get; set; } // required
    public double? ListedPrice { get; set; } // optional
    public long? ListedDate { get; set; } // required if ListedPrice is provided
    public double? SoldPrice { get; set; } // optional
    public long? SoldDate { get; set; } // required if SoldPrice is provided
    public string Status { get; set; } // list of options; required - Listed, Sold, Pending Sale, Hold
    public long StatusDate { get; set; } // creation time stamp added with each update; required
    public Person? SoldBy { get; set; } //link the employee to the sale
    public Person? SoldTo { get; set; } //link the Person to the sale

    public Sale()
    {
        Status = "";
    }

//todo: add SoldBy and SoldTo
    public Sale(int lotId , double acquiredPrice, long acquiredDate, double listedPrice, long listedDate, double soldPrice, long soldDate, string status, long statusDate, Person? soldBy, Person? soldTo)
    {
        LotId = lotId;  
        AcquiredPrice = acquiredPrice;
        AcquiredDate = acquiredDate;
        ListedPrice = listedPrice;
        ListedDate = listedDate;
        SoldPrice = soldPrice;
        SoldDate = soldDate;
        Status = status;
        StatusDate = statusDate;
        SoldBy = soldBy;
        SoldTo = soldTo;
    }
//todo: add SoldBy and SoldTo
    public override string ToString()
    {
        return $"Lot Number: {LotId}\nAcquired Price: {AcquiredPrice}\nAcquired Date: {AcquiredDate}\nListed Price: {ListedPrice}\nListed Date: {ListedDate}\nSold Price: {SoldPrice}\nSold Date: {SoldDate}\nStatus: {Status}\nStatus Date: {StatusDate}\nSold by: {SoldBy}\nSold to: {SoldTo}";
    }
}