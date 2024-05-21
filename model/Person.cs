
public class Person
{
    public int? PersonId { get; set; }  //add ID generator
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public int? PhoneNumber { get; set; } //add validation for phone number i.e. must contain 10 digits
    public string? Email { get; set; }  //add validation for email i.e. must contain @ and .
    public string Person_Type { get; set; }  //employee or Person
    public string? UserId { get; set; }
    public string? Password { get; set; }
    public string? Role { get; set; } //admin, user, read only
    public string Status { get; set; }  //should this be an enum?  used to indicate employee status (active, inactive) or Person status (not interested, interested, under contract, sold)


    public Person()
    {
        FirstName = "";
        LastName = "";
        Person_Type = "";
        Status = "";

    }
    public Person(int personId, string lastName, string firstName, int? phoneNumber, string email, string person_Type, string status)
    {
        PersonId = personId;
        LastName = lastName;
        FirstName = firstName;
        PhoneNumber = phoneNumber;
        Email = email;
        Person_Type = person_Type;
        Status = status;
    }

    public Person(int personId, string lastName, string firstName, int phoneNumber, string email, string person_Type, string status, string userId, string password, string role)
    {
        PersonId = personId;
        LastName = lastName;
        FirstName = firstName;
        PhoneNumber = phoneNumber;
        Email = email;
        Person_Type = person_Type;
        Status = status;
        UserId = userId;
        Password = password;
        Role = role;
    }

    public override string ToString()
    {
        return Newtonsoft.Json.JsonConvert.SerializeObject(this);
    }

    internal void Add(Person newPerson)
    {
        throw new NotImplementedException();
    }
}

/*
- Person ID - Unique Key
- Name - First, Last
- Phone Number  
- Email
- Related Lot Number 
    - allow for multiples
- Related Person ID
    - allow for multiples
- Status: Interest, No Interest, Holding Lot(s), Purchased Lot(s)
- Status Date (YYYY/MM/DD)
*/