namespace PJATK_APBD_RENTAL.Models;

public abstract class Equipment
{
    public Guid Id { get; private set; } = Guid.NewGuid();
    public string Name { get; set; }
    public string Description { get; set; }
    public EquipmentStatus Status { get; set; } = EquipmentStatus.Available;
    
    protected Equipment(string name, string description)
    {
        Name = name;
        Description = description;
    }
    
}

public enum EquipmentStatus
{
    Available,    
    Rented,       
    Unavailable   
}