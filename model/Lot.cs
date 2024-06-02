
public class Lot
{
    public int? LotId { get; set; }  // freeform but need to verify it is unique; required
    public string? Nickname { get; set; } // optional
    public string? Address { get; set; }
    public string Neighborhood { get; set; } = ""; // required default empty string
    public decimal LotSizeAcres { get; set; } = 0.0m; // required default to 0.0m
    public bool IsAvailable { get; set; } = true; // required; default to true
    public int? InterestedCustomer_1 { get; set; } = 0;//link to Person
    public int? InterestedCustomer_2 { get; set; } = 0;//link to Person
    public int? UnderContractToCustomerId { get; set; } = 0;//link to Person
    public int? SoldToCustomerId { get; set; } = 0;//link to Person
    public decimal? ListedPrice { get; set; } // required
    public decimal? SalePrice { get; set; } // required

    public Lot()
    {
        //Default constructor
    }

    public Lot(int? lotId, string? nickname, string? address, string neighborhood, decimal lotSizeAcres, bool isAvailable, int? interestedCustomer_1, int? interestedCustomer_2, int? underContractToCustomerId, int? soldToCustomerId, decimal? listedPrice, decimal? salePrice)
    {
        LotId = lotId;
        Nickname = nickname;
        Address = address;
        Neighborhood = neighborhood;
        LotSizeAcres = lotSizeAcres;
        IsAvailable = isAvailable;
        InterestedCustomer_1 = interestedCustomer_1;
        InterestedCustomer_2 = interestedCustomer_2;
        UnderContractToCustomerId = underContractToCustomerId;
        SoldToCustomerId = soldToCustomerId;
        ListedPrice = listedPrice;
        SalePrice = salePrice;
    }

    public override string ToString()
    {
        return Newtonsoft.Json.JsonConvert.SerializeObject(this);
    }
}

/*
- Lot Number - Unique Key
- Location Information
    - Address   
    - Neighborhood  
- Lot Size 
    - acres     
    - feet *REMOVED
- Description of property
    - options like "wooded", "cleared", "corner", "hillside"
- Acquired Price 
- Acquired Date (YYYY/MM/DD)
- Listed Price
- Listed Date (YYYY/MM/DD)
- Sold Price
- Sold Date (YYYY/MM/DD)
- Status: Available, Hold, Pending Sale, Sold
- Status Date (YYYY/MM/DD)
*/