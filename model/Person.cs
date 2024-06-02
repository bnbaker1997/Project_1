
public class Person
{
    public int? PersonId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public long? PhoneNumber { get; set; } //add validation for phone number i.e. must contain 10 digits
    public string? Email { get; set; }  //add validation for email i.e. must contain @ and .
    public string Person_Type { get; set; }  //employee or Person
    public string? UserId { get; set; }
    public string? Password { get; set; }
    public string? Role { get; set; } //admin, user, read only
    public string Status { get; set; }  //should this be an enum?  used to indicate employee status (active, inactive) or Person status (not interested, interested, under contract, sold)
    public Lot? LotId { get; set; } //link to Lot
    public Person()
    {
        //Default constructor
    }

    public override string ToString()
    {
        return Newtonsoft.Json.JsonConvert.SerializeObject(this);
    }
}