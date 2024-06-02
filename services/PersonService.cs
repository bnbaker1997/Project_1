public class PersonService
{
    PersonRepo personRepo = new();

    public static bool AddPerson(Person newPerson)
    {
        return PersonRepo.AddPerson(newPerson);
    }
    public Person? GetPerson(int personId)
    {
        var retrievedPerson = personRepo.GetPerson(personId);
        return retrievedPerson;
    }
    public List<Person> GetInterestedCustomers()
    {
        return personRepo.GetInterestedCustomers();
    }
    public List<Person> GetAllCustomers()
    {
        return personRepo.GetAllCustomers();
    }
    public Person? GetUser(string userId)
    {
        var currentUser = personRepo.GetUser(userId);
        return currentUser;
    }
    public bool UpdatePerson(Person updatePerson, int PersonId)
    {
        var existingPerson = personRepo.GetPerson(PersonId);
        if (existingPerson != null)
        {
            bool updateSuccessful = personRepo.UpdatePerson(updatePerson); // Call the method on the instance of PersonStorage
            return updateSuccessful;
        }
        else
        {
            Console.WriteLine("Person not found - enter a valid Person Number");
            return false;
        }
    }
    // public bool RemovePerson(Person retrievedPerson)
    // {
    //     bool didRemove = personRepo.RemovePerson(retrievedPerson.PersonId);

    //     if (didRemove)
    //     {
    //         return true;
    //     }
    //     else
    //     {
    //         return false;
    //     }
    // }
}