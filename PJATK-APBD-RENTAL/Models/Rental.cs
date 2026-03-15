namespace PJATK_APBD_RENTAL.Models;

public class Rental
{
    private int _rentalID;
    private Equipment _equipment;
    private Student _student;
    private Employee _employee;
    private DateTime startRentalDate;
    private DateTime endRentalDate;
    private DateTime realBackDate;

    public Rental(Student student, Employee employee, Equipment equipment, DateTime startRentalDate, DateTime endRentalDate)
    {
        this._student = student;
        this._employee = employee;
        this._equipment = equipment;
        this.startRentalDate = startRentalDate;
        this.endRentalDate = endRentalDate;
    }
}