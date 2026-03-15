using PJATK_APBD_RENTAL.Models;

namespace PJATK_APBD_RENTAL.Services;

public class RentalService
{
    private static Dictionary<int, Rental> _rentals = new Dictionary<int, Rental>();
    
    public void makeRental(Student student, Employee employee, Equipment equipment, int rentalDays)
    {
        Rental rental = new Rental(student, employee, equipment, DateTime.Now, DateTime.Now.AddDays(rentalDays));
        _rentals.Add(_rentals.Keys.ToList().Max()+1, rental);
        
    }
}