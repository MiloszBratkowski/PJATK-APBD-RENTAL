namespace PJATK_APBD_RENTAL.Models.Actors;

public abstract class User
{
    public Guid Id { get; } = Guid.NewGuid();
    
    public string FirstName { get; set; }
    
    public string LastName { get; set; }

    public abstract int MaxActiveRentals { get; }

    protected User(string firstName, string lastName)
    {
        FirstName = firstName;
        LastName = lastName;
    }
}