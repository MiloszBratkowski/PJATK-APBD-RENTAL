namespace PJATK_APBD_RENTAL.Models.Actors;

public class Employee : User
{
    public override int MaxActiveRentals => 5;

    public Employee(string firstName, string lastName) : base(firstName, lastName)
    {
    }
}