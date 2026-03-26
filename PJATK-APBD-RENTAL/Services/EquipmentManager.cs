namespace PJATK_APBD_RENTAL.Services;

using PJATK_APBD_RENTAL.Models;

public class EquipmentManager
{
    private readonly List<Equipment> _inventory = new();

    public void AddEquipment(Equipment item)
    {
        _inventory.Add(item);
    }

    public List<Equipment> AllEquipment => _inventory.ToList();

    public Equipment? GetById(Guid id) => _inventory.FirstOrDefault(e => e.Id == id);
}