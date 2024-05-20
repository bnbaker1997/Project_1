public class PersonRepo
{
    PersonStorage personStorage = new();

    public void AddPerson(Person person)
    {
        PersonStorage.AddPerson(person);
    }

    public bool UpdatePerson(Person updatePerson)
    {
        var existingPerson = personStorage.GetPerson(updatePerson.PersonId);
        if (existingPerson != null)
        {
            bool updateSuccessful = personStorage.UpdatePerson(updatePerson); // Call the method on the instance of PersonStorage
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
        var existingPerson = personStorage.GetPerson(personId);
        return existingPerson;
    }
}