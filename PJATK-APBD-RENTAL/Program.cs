using PJATK_APBD_RENTAL.Models;
using PJATK_APBD_RENTAL.Models.Actors;
using PJATK_APBD_RENTAL.Reports;
using PJATK_APBD_RENTAL.Services;

// --- INICJALIZACJA SYSTEMU ---
var logger = new ConsoleLogger();
var userMgr = new UserManager();
var equipMgr = new EquipmentManager();
var rentalSvc = new RentalService(logger);
var reports = new ReportGenerator(logger);

/*
 * TUTAJ ZROBIŁEM ELEMENT DEMO KTÓREGO NIE BĘDZIE W NORMALNYM PROGRAMIE
 */
Console.WriteLine("======= PJATK RENTAL SYSTEM - DEMO SCENARIUSZ =======\n");

// 11. Dodanie kilku egzemplarzy sprzętu różnych typów
var laptop = new Laptop("Dell XPS", "Laptop 15 cali", "i7", 16);
var kamera = new Camera("Sony A7S", "Kamera 4K", "4K", "35mm");
var projektor = new Projector("Epson EB", "Projektor biurowy", 3000, 150);

equipMgr.AddEquipment(laptop);
equipMgr.AddEquipment(kamera);
equipMgr.AddEquipment(projektor);
logger.LogInfo("Dodano 3 różne typy sprzętu do magazynu.");

// 12. Dodanie kilku użytkowników różnych typów
var student = new Student("Jan", "Kowalski");      // Limit 2
var pracownik = new Employee("Anna", "Nowak");    // Limit 5

userMgr.AddUser(student);
userMgr.AddUser(pracownik);
logger.LogInfo("Zarejestrowano Studenta (limit 2) i Pracownika (limit 5).");

// 13. Poprawne wypożyczenie sprzętu
logger.LogInfo("\n--- [13] Poprawne wypożyczenie ---");
rentalSvc.MakeRental(student, laptop, 7);

// 14. Próba wykonania niepoprawnej operacji
logger.LogInfo("\n--- [14] Próba: Wypożyczenie niedostępnego sprzętu ---");
rentalSvc.MakeRental(pracownik, laptop, 5);

logger.LogInfo("\n--- [14] Próba: Przekroczenie limitu przez studenta ---");
rentalSvc.MakeRental(student, kamera, 7);
rentalSvc.MakeRental(student, projektor, 7);

// 15. Zwrot sprzętu w terminie
logger.LogInfo("\n--- [15] Zwrot w terminie ---");
var rentalToReturn = rentalSvc.GetAllRentals().First(r => r.Item == laptop);
rentalSvc.ReturnItem(rentalToReturn.Id);

// 16. Zwrot opóźniony skutkujący naliczeniem kary
logger.LogInfo("\n--- [16] Zwrot opóźniony (Symulacja) ---");
rentalSvc.MakeRental(pracownik, projektor, 3);
var lateRental = rentalSvc.GetAllRentals().First(r => r.Item == projektor);

lateRental.RentalDate = DateTime.Now.AddDays(-30);
lateRental.DueDate = DateTime.Now.AddDays(-20);

rentalSvc.ReturnItem(lateRental.Id);

// 17. Wyświetlenie raportu końcowego o stanie systemu
Console.WriteLine("\n" + new string('=', 60));
Console.WriteLine("--- [17] RAPORT KOŃCOWY STANU SYSTEMU ---");
reports.PrintEquipmentStatus(equipMgr.AllEquipment);
reports.PrintUserStats(rentalSvc.GetAllRentals());
reports.PrintFilteredRentals(rentalSvc.GetAllRentals(), "wszystkie");

Console.WriteLine("\nScenariusz zakończony. Naciśnij dowolny klawisz...");
/*
 * KONIEC ELEMENTU DEMO, PONIŻEJ URUCHAMIANIE KLASY KONSOLOWE
 */

var ui = new ConsoleUI(userMgr, equipMgr, rentalSvc, reports, logger);
ui.Run();