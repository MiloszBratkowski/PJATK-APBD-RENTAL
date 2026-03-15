namespace PJATK_APBD_RENTAL.Models;

public class Equipment
{
    private int id;
    private string name;
    private string description;
    private Status status;
    private enum Status
    {
        DOSTĘPNY = 1,
        WYPOŻYCZONY = 0
    }
    
}