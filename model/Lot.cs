public class Lot  
{
    public int? LotId { get; set; }  // freeform but need to verify it is unique; required
    public string? Nickname { get; set; } // optional
    public string? Address { get; set; } 
    public string Neighborhood { get; set; }  // required
    public decimal LotSizeAcres { get; set; } // required 
    public Person? InterestedPerson_1 { get; set; } //link to Person
    public Person? InterestedPerson_2 { get; set; } //link to Person
    public bool IsAvailable { get; set; } // required

    public Lot()
    {
        Neighborhood = "";
        LotSizeAcres = 0.0m;
        IsAvailable = true;
    }


    public Lot(int lotId, string? nickname, string? address, string neighborhood, decimal lotSizeAcres, Person? interestedPerson_1, Person? interestedPerson_2, bool isAvailable)
    {
        LotId = lotId;
        Nickname = nickname;
        Address = address;
        Neighborhood = neighborhood;
        LotSizeAcres = lotSizeAcres;
        InterestedPerson_1 = interestedPerson_1;
        InterestedPerson_2 = interestedPerson_2;
        IsAvailable = isAvailable;
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