# System Wypożyczalni PJATK-APBD

Konsolowy system obsługi wypożyczalni sprzętu uczelnianego. Projekt kładzie nacisk na **strukturę kodu**, **odpowiedzialność klas** oraz **dobre praktyki programistyczne**.

---

## Architektura i Decyzje Projektowe

W rozwiązaniu rozdzielono model domenowy od logiki wykonywania operacji i interfejsu konsolowego, dbając o to, by klasy miały jedną wyraźną odpowiedzialność.

### 1. High Cohesion
* **Podział Models na podfoldery (Actors i Equipment):** Zamiast jednej zbiorczej klasy, rozdzielono encje na logiczne grupy. Klasy reprezentują konkretne obiekty domenowe (użytkownicy vs sprzęt), co ułatwia rozwój i nawigację w projekcie.
* **Struktura Actorów:** Użytkownicy są podzieleni na konkretne typy (`Student`, `Employee`), z których każdy posiada unikalny zestaw reguł biznesowych i uprawnień.

### 2. Low Coupling
* **Abstrakcja Sprzętu:** `RentalService` operuje na bazowej klasie `Equipment`, nie znając szczegółów implementacyjnych `Laptopa` czy `Kamery`. Dzięki temu dodanie nowego urządzenia nie wymaga modyfikacji istniejącej logiki biznesowej.
* **Polimorficzne Limity:** Limity wypożyczeń (2 dla studenta, 5 dla pracownika) są zdefiniowane bezpośrednio w modelach (`MaxActiveRentals`).

### 3. Hermetyzacja
* **Właściwości { get; set; }:** Zastosowanie auto-properties umożliwia łatwą implementację walidacji w przyszłości bez zmiany interfejsu publicznego klas.
* **Unikalne ID:** Identyfikatory `Guid` są generowane automatycznie przy tworzeniu obiektu.

---

## Instrukcja uruchomienia

Wystarczy uruchomić program, w pierwszej kolejności wykona się kod demonstracyjny, następnie zostanie uruchomiona konsola do obsługi systemu.
