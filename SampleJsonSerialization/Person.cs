namespace SampleJsonSerialization;
public class Person
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
}

public class IEnumerablePerson
{
    IEnumerable<Person> Persons { get; set; }
}
