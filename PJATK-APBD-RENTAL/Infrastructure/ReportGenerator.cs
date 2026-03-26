using PJATK_APBD_RENTAL.Models;
using PJATK_APBD_RENTAL.Models.Actors;

namespace PJATK_APBD_RENTAL.Reports;

public class ReportGenerator
{
    private readonly ILogger _logger;

    public ReportGenerator(ILogger logger)
    {
        _logger = logger;
    }

    public void PrintUserStats(List<Rental> rentals)
    {
        Console.WriteLine("\n>>> STATYSTYKI UŻYTKOWNIKÓW (Aktywne wypożyczenia) <<<");
        var stats = rentals
            .Where(r => r.ActualReturnDate == null)
            .GroupBy(r => r.User.LastName)
            .Select(g => new { Name = g.Key, Count = g.Count() });

        foreach (var stat in stats)
        {
            Console.WriteLine($"- {stat.Name}: {stat.Count} szt.");
        }
    }

    public void PrintEquipmentStatus(List<Equipment> inventory)
    {
        Console.WriteLine("\n>>> STAN MAGAZYNOWY SPRZĘTU <<<");
        Console.WriteLine($"{"Nazwa",-20} | {"Status",-15}");
        Console.WriteLine(new string('-', 40));
        
        foreach (var item in inventory)
        {
            Console.WriteLine($"{item.Name,-20} | {item.Status,-15}");
        }
    }
    
    public void PrintFilteredRentals(List<Rental> rentals, string filterType = "wszystkie", User? userFilter = null)
    {
        IEnumerable<Rental> query = rentals;

        switch (filterType.ToLower())
        {
            case "opoznione":
                query = query.Where(r => r.ActualReturnDate == null && DateTime.Now > r.DueDate);
                break;
            case "zwrocone":
                query = query.Where(r => r.ActualReturnDate != null);
                break;
            case "wterminie":
                query = query.Where(r => r.ActualReturnDate == null && DateTime.Now <= r.DueDate);
                break;
        }

        if (userFilter != null)
        {
            query = query.Where(r => r.User.Id == userFilter.Id);
            Console.WriteLine($"\n[ RAPORT DLA: {userFilter.FirstName} {userFilter.LastName} | FILTR: {filterType.ToUpper()} ]");
        }
        else
        {
            Console.WriteLine($"\n[ RAPORT ZBIORCZY | FILTR: {filterType.ToUpper()} ]");
        }

        // 3. Wyświetlanie wyników
        Console.WriteLine($"{"ID (fragment)",-15} | {"Użytkownik",-15} | {"Sprzęt",-15} | {"Status",-12}");
        Console.WriteLine(new string('-', 65));

        if (!query.Any())
        {
            Console.WriteLine(" > Brak rekordów spełniających kryteria.");
            return;
        }

        foreach (var r in query)
        {
            string status = r.ActualReturnDate.HasValue ? "Zwrócono" : (DateTime.Now > r.DueDate ? "OPÓŹNIONE" : "W trakcie");
            Console.WriteLine($"{r.Id.ToString().Substring(0, 8),-15} | {r.User.LastName,-15} | {r.Item.Name,-15} | {status,-12}");
        }
    }
}