namespace PJATK_APBD_RENTAL.Models.Actors;

public class Student : User
{
    public override int MaxActiveRentals => 2;

    public Student(string firstName, string lastName) : base(firstName, lastName)
    {
    }
}