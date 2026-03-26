using PJATK_APBD_RENTAL.Models.Actors;

namespace PJATK_APBD_RENTAL.Models;

public class Rental
{
    public Guid Id { get; } = Guid.NewGuid();
    public User User { get; set; }
    public Equipment Item { get; set; }
    public DateTime RentalDate { get; set; }
    public DateTime DueDate { get; set; }
    public DateTime? ActualReturnDate { get; set; }
    public decimal Penalty { get; set; } = 0;

    public Rental(User user, Equipment item, DateTime rentalDate, int daysToReturn)
    {
        User = user;
        Item = item;
        RentalDate = rentalDate;
        DueDate = rentalDate.AddDays(daysToReturn);
    }
    
}