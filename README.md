# Projekt2 – System ERP dla Cukrowni

## Opis

Projekt2 to aplikacja typu ERP stworzona z myślą o zarządzaniu procesami w cukrowni. Umożliwia zarządzanie pracownikami, klientami, produktami oraz zamówieniami, oferując przejrzysty interfejs użytkownika i funkcjonalności wspierające codzienną działalność przedsiębiorstwa.

## Funkcjonalności

- **Zarządzanie Pracownikami**: dodawanie, edycja, usuwanie, przeglądanie.
- **Zarządzanie Klientami**: pełna obsługa danych klientów.
- **Zarządzanie Produktami**: katalog produktów z możliwością edycji.
- **Zamówienia**: tworzenie i zarządzanie zamówieniami klientów.
- **Raportowanie**: generowanie raportów sprzedaży i zamówień.
- **System Uwierzytelniania**: logowanie, rejestracja, role użytkowników.

## Technologie

- ASP.NET Core MVC
- Entity Framework Core
- SQL Server
- Razor

- ## Instalacja

Wymagania:
- [.NET 6 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/6.0)
- SQL Server (lokalny lub zdalny)
- (Opcjonalnie) Entity Framework CLI: 'dotnet tool install --global dotnet-ef'


# Sklonuj repozytorium
git clone https://github.com/IlikeSzyszkas/Projekt2.git
cd Projekt2

# Przywróć zależności
dotnet restore

# Zastosuj migracje i utwórz bazę danych
dotnet ef database update

# Uruchom aplikację
dotnet run

# Aplikacja będzie dostępna pod adresem:
# https://localhost:5001


Upewnij się, że w pliku 'appsettings.json' znajdują się poprawne dane do połączenia z Twoim serwerem SQL.

## Konfiguracja
Plik appsettings.json zawiera konfigurację połączenia z bazą danych. Upewnij się, że dane są poprawne przed uruchomieniem aplikacji.
