namespace PJATK_APBD_RENTAL.Services;

using PJATK_APBD_RENTAL.Reports;
using PJATK_APBD_RENTAL.Models;
using PJATK_APBD_RENTAL.Models.Actors;

public class RentalService
{
    private readonly Dictionary<Guid, Rental> _rentals = new();
    private readonly ILogger _logger;

    public RentalService(ILogger logger) {
        _logger = logger;
    }

    public void MakeRental(User user, Equipment equipment, int rentalDays) {
        _logger.LogInfo($"Próba wypożyczenia {equipment.Name} dla {user.LastName}...");

        if (equipment.Status != EquipmentStatus.Available)
        {
            _logger.LogError($"Sprzęt {equipment.Name} jest zajęty!");
            return;
        }

        int activeCount = _rentals.Values.Count(r => r.User.Id == user.Id && r.ActualReturnDate == null);
        if (activeCount >= user.MaxActiveRentals)
        {
            _logger.LogError($"Odmowa! {user.LastName} ma już {activeCount} aktywnych wypożyczeń (Limit: {user.MaxActiveRentals}).");
            return;
        }

        var rental = new Rental(user, equipment, DateTime.Now, rentalDays);
        equipment.Status = EquipmentStatus.Rented;
        
        _rentals.Add(rental.Id, rental);
        
        _logger.LogInfo($"Sukces! ID wypożyczenia: {rental.Id}");
    }

    public void ReturnItem(Guid rentalId) {
        if (!_rentals.TryGetValue(rentalId, out var rental))
        {
            _logger.LogError($"Nie znaleziono wypożyczenia o ID: {rentalId}");
            return;
        }

        if (rental.ActualReturnDate != null)
        {
            _logger.LogInfo("Ten sprzęt został już wcześniej zwrócony.");
            return;
        }

        rental.ActualReturnDate = DateTime.Now;
        rental.Item.Status = EquipmentStatus.Available;

        if (rental.ActualReturnDate > rental.DueDate)
        {
            int delayDays = (rental.ActualReturnDate.Value - rental.DueDate).Days;
            rental.Penalty = delayDays * 50.0m;
            _logger.LogError($"SPÓŹNIENIE! Naliczono karę: {rental.Penalty} PLN ({delayDays} dni).");
        }
        else
        {
            _logger.LogInfo($"Zwrot sprzętu {rental.Item.Name} przyjęty pomyślnie.");
        }
    }
    
    public List<Rental> GetAllRentals()
    {
        return _rentals.Values.ToList();
    }
    
    public Rental GetRental(Guid id)
    {
        return _rentals[id];
    }
    
    
}