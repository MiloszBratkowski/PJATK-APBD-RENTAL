namespace PJATK_APBD_RENTAL.Reports;

using PJATK_APBD_RENTAL.Models;
using PJATK_APBD_RENTAL.Models.Actors;
using PJATK_APBD_RENTAL.Services;


public class ConsoleUI
{
    private readonly UserManager _userMgr;
    private readonly EquipmentManager _equipMgr;
    private readonly RentalService _rentalSvc;
    private readonly ReportGenerator _reports;
    private readonly ILogger _logger;

    public ConsoleUI(UserManager userMgr, EquipmentManager equipMgr, RentalService rentalSvc, ReportGenerator reports, ILogger logger)
    {
        _userMgr = userMgr;
        _equipMgr = equipMgr;
        _rentalSvc = rentalSvc;
        _reports = reports;
        _logger = logger;
    }

    public void Run()
    {
        bool exit = false;
        while (!exit)
        {
            PrintMenu();
            string choice = Console.ReadLine() ?? "";
            exit = HandleChoice(choice);
            
            if (!exit)
            {
                Console.WriteLine("\nNaciśnij dowolny klawisz...");
                Console.ReadKey();
            }
        }
    }

    private void PrintMenu()
    {
        Console.Clear();
        Console.WriteLine("======= SYSTEM WYPOŻYCZALNI PJATK =======");
        Console.WriteLine("1. Dodaj Użytkownika");
        Console.WriteLine("2. Dodaj Sprzęt");
        Console.WriteLine("3. Wypożycz Sprzęt (Make Rental)");
        Console.WriteLine("4. Zwróć Sprzęt (Return)");
        Console.WriteLine("5. Raport: Stan Magazynu");
        Console.WriteLine("6. Raport: Wszystkie Wypożyczenia");
        Console.WriteLine("7. Raport: Spóźnienia i Kary");
        Console.WriteLine("0. Wyjście");
        Console.Write("\nWybierz opcję: ");
    }

    private bool HandleChoice(string choice)
    {
        switch (choice)
        {
            case "1": AddUser(); return false;
            case "2": AddEquipment(); return false;
            case "3": ProcessRental(); return false;
            case "4": ProcessReturn(); return false;
            case "5": _reports.PrintEquipmentStatus(_equipMgr.AllEquipment); return false;
            case "6": _reports.PrintFilteredRentals(_rentalSvc.GetAllRentals()); return false;
            case "7": _reports.PrintFilteredRentals(_rentalSvc.GetAllRentals(), "opoznione"); return false;
            case "0": return true;
            default: _logger.LogError("Nieprawidłowa opcja."); return false;
        }
    }

    private void AddUser()
    {
        Console.Write("Imię: "); string fn = Console.ReadLine() ?? "";
        Console.Write("Nazwisko: "); string ln = Console.ReadLine() ?? "";
        Console.Write("Typ (S - Student, P - Pracownik): ");
        string type = (Console.ReadLine() ?? "").ToUpper();

        if (type == "S") _userMgr.AddUser(new Student(fn, ln));
        else _userMgr.AddUser(new Employee(fn, ln));
        _logger.LogInfo("Użytkownik dodany pomyślnie.");
    }

    private void AddEquipment()
    {
        Console.Write("Nazwa sprzętu: "); string name = Console.ReadLine() ?? "";
        // TUTAJ TYLKO LAPTOP DLA ULATWIENIA - OCZYWISCIE MOZNA ZROBIC WYBOR SPRZETU Z MENU DOSTOSOWANYM DO DANEGO TYPU
        _equipMgr.AddEquipment(new Laptop(name, "Dodany z menu", "i5", 8));
        _logger.LogInfo("Sprzęt zarejestrowany.");
    }

    private void ProcessRental()
    {
        Console.WriteLine("\n--- WYBÓR UŻYTKOWNIKA ---");
        _userMgr.AllUsers.ForEach(u => Console.WriteLine($"{u.LastName} ({u.GetType().Name})"));
        Console.Write("Nazwisko: ");
        var user = _userMgr.GetByLastName(Console.ReadLine() ?? "");

        Console.WriteLine("\n--- WYBÓR SPRZĘTU ---");
        _equipMgr.AllEquipment.Where(e => e.Status == EquipmentStatus.Available)
            .ToList().ForEach(e => Console.WriteLine($"- {e.Name} [ID: {e.Id}]"));
        
        Console.Write("Podaj ID sprzętu: ");
        if (Guid.TryParse(Console.ReadLine(), out Guid eId))
        {
            var equip = _equipMgr.GetById(eId);
            if (user != null && equip != null) _rentalSvc.MakeRental(user, equip, 7);
            else _logger.LogError("Błąd danych.");
        }
    }

    private void ProcessReturn()
    {
        Console.WriteLine("\n--- AKTYWNE WYPOŻYCZENIA ---");
        _rentalSvc.GetAllRentals().Where(r => r.ActualReturnDate == null)
            .ToList().ForEach(r => Console.WriteLine($"ID: {r.Id} | {r.User.LastName} -> {r.Item.Name}"));
        
        Console.Write("ID wypożyczenia: ");
        if (Guid.TryParse(Console.ReadLine(), out Guid rId)) _rentalSvc.ReturnItem(rId);
    }
}