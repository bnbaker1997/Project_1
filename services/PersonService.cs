public class PersonService //changed from REPO to SERVICE
{
    PersonRepo personRepo = new();

    public void AddPerson(Person person)
    {
        PersonRepo.AddPerson(person);
    }

    public bool UpdatePerson(Person updatePerson)
    {
        var existingPerson = personRepo.GetPerson(updatePerson.PersonId);
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

    public Person? GetPerson(int? personId)
    {
        var existingPerson = personRepo.GetPerson(personId);
        return existingPerson;
    }

    public Person? GetUser(string userId)
    {
        var existingPerson = personRepo.GetUser(userId);
        return existingPerson;
    }
}