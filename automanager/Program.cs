using System;
using System.IO;

namespace AutoManager
{
    class Program
    {
        // TABLICA DWUWYMIAROWA
        // Każdy wiersz = jedno auto
        // Kolumny:
        // [0] Marka
        // [1] Model
        // [2] Rok
        // [3] Cena
        // [4] Status
        static string[,] auta = new string[100, 5];

        // TABLICE JEDNOWYMIAROWE
        // Nagłówki do wyświetlania tabeli
        static string[] naglowki =
        {
            "ID",
            "Marka",
            "Model",
            "Rok",
            "Cena",
            "Status"
        };

        // Możliwe statusy auta
        static string[] statusy =
        {
            "Dostepny",
            "Sprzedany",
            "Zarezerwowany"
        };

        // Aktualna liczba aut w bazie
        static int liczbaAut = 0;

        // Ścieżka do pliku CSV z autami
        static string folderProjektu = Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory).Parent.Parent.FullName;
        static string plikAuta = Path.Combine(folderProjektu, "Dane", "auta.csv");

        static void Main(string[] args)
        {
            // Umożliwia wyświetlanie polskich znaków
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            // Wczytanie aut z pliku przy uruchomieniu programu
            WczytajAutaZPliku();

            // Zmienna sterująca działaniem programu
            bool dziala = true;

            // Główna pętla programu
            while (dziala)
            {
                // Wyświetlenie logo
                PokazLogo();

                // Wyświetlenie menu
                PokazMenu();

                // Pobranie wyboru użytkownika
                Console.Write("Wybierz opcję: ");
                string wybor = Console.ReadLine();

                // Obsługa menu przy pomocy switch case
                switch (wybor)
                {
                    case "1":
                        DodajAuto();
                        break;

                    case "2":
                        WyswietlAuta();
                        break;

                    case "3":
                        SzukajPoMarce();
                        break;

                    case "4":
                        SzukajDoCeny();
                        break;

                    case "5":
                        ZmienStatus("Sprzedany");
                        break;

                    case "6":
                        ZmienStatus("Zarezerwowany");
                        break;
                    case "7":
                        EdytujCene();
                        break;

                    case "8":
                        PokazAutaPoStatusie("Dostepny");
                        break;

                    case "9":
                        PokazAutaPoStatusie("Sprzedany");
                        break;

                    case "10":
                        PokazAutaPoStatusie("Zarezerwowany");
                        break;

                    case "11":
                        PokazNajdrozsze();
                        break;

                    case "12":
                        PokazNajtansze();
                        break;

                    case "13":
                        PokazRaport();
                        break;

                    case "14":
                        ZapiszRaport();
                        break;

                    case "15":
                        DoradcaKlienta();
                        break;

                    case "0":
                        dziala = false;
                        break;

                    default:
                        Console.WriteLine("Nie ma takiej opcji.");
                        break;
                }
                // Jeśli program dalej działa,
                // użytkownik musi kliknąć ENTER
                if (dziala)
                {
                    Console.WriteLine("\nNaciśnij ENTER, aby kontynuować...");
                    Console.ReadLine();
                }
            }
        }

        // Funkcja wyświetlająca logo programu
        static void PokazLogo()
        {
            Console.Clear();

            Console.ForegroundColor = ConsoleColor.Yellow;

            Console.WriteLine("=======================================================================");
            Console.WriteLine("");
            Console.WriteLine("      █████╗ ██╗   ██╗████████╗ ██████╗ ");
            Console.WriteLine("     ██╔══██╗██║   ██║╚══██╔══╝██╔═══██╗");
            Console.WriteLine("     ███████║██║   ██║   ██║   ██║   ██║");
            Console.WriteLine("     ██╔══██║██║   ██║   ██║   ██║   ██║");
            Console.WriteLine("     ██║  ██║╚██████╔╝   ██║   ╚██████╔╝");
            Console.WriteLine("     ╚═╝  ╚═╝ ╚═════╝    ╚═╝    ╚═════╝ ");
            Console.WriteLine("");
            Console.WriteLine("    ███╗   ███╗ █████╗ ███╗   ██╗ █████╗  ██████╗ ███████╗██████╗ ");
            Console.WriteLine("    ████╗ ████║██╔══██╗████╗  ██║██╔══██╗██╔════╝ ██╔════╝██╔══██╗");
            Console.WriteLine("    ██╔████╔██║███████║██╔██╗ ██║███████║██║  ███╗█████╗  ██████╔╝");
            Console.WriteLine("    ██║╚██╔╝██║██╔══██║██║╚██╗██║██╔══██║██║   ██║██╔══╝  ██╔══██╗");
            Console.WriteLine("    ██║ ╚═╝ ██║██║  ██║██║ ╚████║██║  ██║╚██████╔╝███████╗██║  ██║");
            Console.WriteLine("    ╚═╝     ╚═╝╚═╝  ╚═╝╚═╝  ╚═══╝╚═╝  ╚═╝ ╚═════╝ ╚══════╝╚═╝  ╚═╝");
            Console.WriteLine("");
            Console.WriteLine("=======================================================================");
            Console.WriteLine("             SYSTEM ZARZĄDZANIA SALONEM SAMOCHODOWYM");
            Console.WriteLine("=======================================================================");
            Console.WriteLine("");
            Console.ResetColor();
        }


        //Menu programu
        static void PokazMenu()
        {
            Console.WriteLine("1. Dodaj auto");
            Console.WriteLine("2. Wyświetl wszystkie auta");
            Console.WriteLine("3. Wyszukaj auto po marce");
            Console.WriteLine("4. Wyszukaj auta do podanej ceny");
            Console.WriteLine("5. Sprzedaj auto");
            Console.WriteLine("6. Zarezerwuj auto");
            Console.WriteLine("7. Edytuj cenę auta"); 
            Console.WriteLine("8. Pokaż auta dostępne");
            Console.WriteLine("9. Pokaż auta sprzedane");
            Console.WriteLine("10. Pokaż auta zarezerwowane");
            Console.WriteLine("11. Pokaż najdroższe auto"); 
            Console.WriteLine("12. Pokaż najtańsze auto"); 
            Console.WriteLine("13. Pokaż raport salonu"); 
            Console.WriteLine("14. Zapisz raport do pliku"); 
            Console.WriteLine("15. Doradca klienta"); 
            Console.WriteLine("0. Wyjście");

            Console.WriteLine();
        }

        // Funkcja wczytująca auta z pliku CSV
        static void WczytajAutaZPliku()
        {
            // Sprawdzenie czy plik istnieje
            if (!File.Exists(plikAuta))
            {
                Console.WriteLine("Nie znaleziono pliku auta.csv");
                Console.WriteLine(Path.GetFullPath(plikAuta));

                Console.ReadLine();
                return;
            }

            // Wczytanie wszystkich linii z pliku
            string[] linie = File.ReadAllLines(plikAuta);

            // Zerowanie liczby aut
            liczbaAut = 0;

            // Pętla zaczyna się od 1,
            // ponieważ linia 0 to nagłówki
            for (int i = 1; i < linie.Length; i++)
            {
                // Pominięcie pustych linii
                if (linie[i].Trim() == "")
                {
                    continue;
                }

                // Rozdzielenie danych po średniku
                string[] dane = linie[i].Split(';');

                // Sprawdzenie poprawności danych
                if (dane.Length == 5 && liczbaAut < 100)
                {
                    // Wpisanie danych do tablicy 2D
                    auta[liczbaAut, 0] = dane[0];
                    auta[liczbaAut, 1] = dane[1];
                    auta[liczbaAut, 2] = dane[2];
                    auta[liczbaAut, 3] = dane[3];
                    auta[liczbaAut, 4] = dane[4];

                    // Zwiększenie liczby aut
                    liczbaAut++;
                }
            }
        }

        // Funkcja zapisująca auta do pliku CSV
        static void ZapiszAutaDoPliku()
        {
            // Tablica linii do zapisania
            string[] linie = new string[liczbaAut + 1];

            // Nagłówek pliku CSV
            linie[0] = "Marka;Model;Rok;Cena;Status";

            // Tworzenie kolejnych linii
            for (int i = 0; i < liczbaAut; i++)
            {
                linie[i + 1] =
                    auta[i, 0] + ";" +
                    auta[i, 1] + ";" +
                    auta[i, 2] + ";" +
                    auta[i, 3] + ";" +
                    auta[i, 4];
            }

                // Zapis do pliku
                try
                {
                    File.WriteAllLines(plikAuta, linie);
                }
                catch
                {
                    Console.WriteLine("Wystąpił błąd podczas zapisu pliku.");
                }
            }

            // Funkcja dodająca nowe auto
            static void DodajAuto()
            {
                if (liczbaAut >= 100)
                {
                    Console.WriteLine("Baza aut jest pełna.");
                    return;
                }
                Console.Write("Podaj markę: ");
                string marka = Console.ReadLine();

            Console.Write("Podaj model: ");
            string model = Console.ReadLine();

            Console.Write("Podaj rok produkcji: ");
            string rok = Console.ReadLine();

            Console.Write("Podaj cenę: ");
            string cena = Console.ReadLine();

            // Sprawdzenie pustych pól
            if (marka == "" || model == "" || rok == "" || cena == "")
            {
                Console.WriteLine("Wszystkie pola muszą być uzupełnione.");
                return;
            }

            // Sprawdzenie roku
            if (!int.TryParse(rok, out int rokLiczba))
            {
                Console.WriteLine("Rok musi być liczbą.");
                return;
            }

            // Zakres roku
            if (rokLiczba < 1950 || rokLiczba > 2026)
            {
                Console.WriteLine("Rok musi być z zakresu 1950-2026.");
                return;
            }

            // Sprawdzenie ceny
            if (!int.TryParse(cena, out int cenaLiczba))
            {
                Console.WriteLine("Cena musi być liczbą.");
                return;
            }

            // Cena większa od 0
            if (cenaLiczba <= 0)
            {
                Console.WriteLine("Cena musi być większa od 0.");
                return;
            }

            // Dodanie auta do tablicy
            auta[liczbaAut, 0] = marka;
            auta[liczbaAut, 1] = model;
            auta[liczbaAut, 2] = rok;
            auta[liczbaAut, 3] = cena;
            auta[liczbaAut, 4] = statusy[0];

            // Zwiększenie liczby aut
            liczbaAut++;

            // Zapis do pliku
            ZapiszAutaDoPliku();

            Console.WriteLine("Auto zostało dodane.");
        }

        // Funkcja wyświetlająca wszystkie auta
        static void WyswietlAuta()
        {
            // Sprawdzenie czy baza jest pusta
            if (liczbaAut == 0)
            {
                Console.WriteLine("Brak aut w bazie.");
                return;
            }

            // Wyświetlenie nagłówków
            Console.WriteLine(
                $"{naglowki[0],-5}" +
                $"{naglowki[1],-15}" +
                $"{naglowki[2],-20}" +
                $"{naglowki[3],-10}" +
                $"{naglowki[4],-12}" +
                $"{naglowki[5],-15}"
            );

            Console.WriteLine("---------------------------------------------------------------------");

            // Wyświetlenie wszystkich aut
            for (int i = 0; i < liczbaAut; i++)
            {
                Console.WriteLine(
                    $"{i + 1,-5}" +
                    $"{auta[i, 0],-15}" +
                    $"{auta[i, 1],-20}" +
                    $"{auta[i, 2],-10}" +
                    $"{auta[i, 3],-12}" +
                    $"{auta[i, 4],-15}"
                );
            }
        }

        // Funkcja wyszukująca auta po marce
        static void SzukajPoMarce()
        {
            Console.Write("Podaj markę: ");
            string marka = Console.ReadLine();

            // Sprawdzenie pustej marki
            if (marka == "")
            {
                Console.WriteLine("Marka nie może być pusta.");
                return;
            }

            // Sprawdzenie czy marka nie jest liczbą
            if (int.TryParse(marka, out int liczba))
            {
                Console.WriteLine("Marka nie może być liczbą.");
                return;
            }

            bool znaleziono = false;

            // Szukanie aut
            for (int i = 0; i < liczbaAut; i++)
            {
                if (auta[i, 0].ToLower() == marka.ToLower())
                {
                    Console.WriteLine(
                        $"{i + 1}. " +
                        $"{auta[i, 0]} " +
                        $"{auta[i, 1]} | " +
                        $"Rok: {auta[i, 2]} | " +
                        $"Cena: {auta[i, 3]} zł | " +
                        $"Status: {auta[i, 4]}"
                    );

                    znaleziono = true;
                }
            }

            // Komunikat jeśli nic nie znaleziono
            if (!znaleziono)
            {
                Console.WriteLine("Nie znaleziono aut o podanej marce.");
            }
        }

        // Funkcja wyszukująca auta do podanej ceny
        static void SzukajDoCeny()
        {
            Console.Write("Podaj maksymalną cenę: ");
            string tekst = Console.ReadLine();

            // Walidacja ceny
            if (!int.TryParse(tekst, out int maxCena))
            {
                Console.WriteLine("Cena musi być liczbą.");
                return;
            }

            if (maxCena <= 0)
            {
                Console.WriteLine("Cena musi być większa od 0.");
                return;
            }

            bool znaleziono = false;

            // Szukanie aut
            for (int i = 0; i < liczbaAut; i++)
            {
                int cena = int.Parse(auta[i, 3]);

                if (cena <= maxCena &&
                    auta[i, 4] == "Dostepny")
                {
                    Console.WriteLine(
                        $"{i + 1}. " +
                        $"{auta[i, 0]} " +
                        $"{auta[i, 1]} | " +
                        $"Cena: {auta[i, 3]} zł"
                    );

                    znaleziono = true;
                }
            }

            // Jeśli brak wyników
            if (!znaleziono)
            {
                Console.WriteLine("Brak dostępnych aut w podanym budżecie.");
            }
        }

        // Funkcja zmieniająca status auta
        static void ZmienStatus(string nowyStatus)
        {
            // Wyświetlenie wszystkich aut
            WyswietlAuta();

            if (liczbaAut == 0)
            {
                return;
            }

            Console.Write("Podaj ID auta: ");
            string tekst = Console.ReadLine();

            // Sprawdzenie ID
            if (!int.TryParse(tekst, out int id))
            {
                Console.WriteLine("ID musi być liczbą.");
                return;
            }

            int indeks = id - 1;

            // Sprawdzenie zakresu ID
            if (indeks < 0 || indeks >= liczbaAut)
            {
                Console.WriteLine("Auto o takim ID nie istnieje.");
                return;
            }

            // Sprawdzenie czy auto jest już sprzedane
            if (auta[indeks, 4] == "Sprzedany")
            {
                Console.WriteLine("To auto jest już sprzedane.");
                return;
            }

            // Zmiana statusu
            auta[indeks, 4] = nowyStatus;

            // Zapis zmian do pliku
            ZapiszAutaDoPliku();

            Console.WriteLine("Status auta został zmieniony.");
        }
        // wyswietalanie auta według statusu
        static void PokazAutaPoStatusie(string statusDoFiltrowania)
        {
            if (liczbaAut == 0)
            {
                Console.WriteLine("Brak aut w bazie.");
                return;
            }

            bool znaleziono = false;

            // nagłówki tabeli 
            Console.WriteLine(
                $"{naglowki[0],-5}" +
                $"{naglowki[1],-15}" +
                $"{naglowki[2],-20}" +
                $"{naglowki[3],-10}" +
                $"{naglowki[4],-12}" +
                $"{naglowki[5],-15}"
            );
            Console.WriteLine("---------------------------------------------------------------------");

            for (int i = 0; i < liczbaAut; i++)
            {
                // Sprawdzamy czy status auta w bazie zgadza się z tym, czego szukamy
                if (auta[i, 4] == statusDoFiltrowania)
                {
                    Console.WriteLine(
                        $"{i + 1,-5}" +
                        $"{auta[i, 0],-15}" +
                        $"{auta[i, 1],-20}" +
                        $"{auta[i, 2],-10}" +
                        $"{auta[i, 3],-12}" +
                        $"{auta[i, 4],-15}"
                    );
                    znaleziono = true;
                }
            }

            if (!znaleziono)
            {
                Console.WriteLine($"Brak aut o statusie: {statusDoFiltrowania}");
            }
        }

        // Funkcja dla opcji 15 - Doradca klienta
        static void DoradcaKlienta()
        {
            Console.WriteLine("=== DORADCA KLIENTA ===");

            // 1. Pobranie i walidacja ceny minimalnej
            Console.Write("Podaj minimalną cenę (zł): ");
            string minCenaTekst = Console.ReadLine();
            if (!int.TryParse(minCenaTekst, out int minCena) || minCena < 0)
            {
                Console.WriteLine("Cena minimalna musi być liczbą większą lub równą 0.");
                return;
            }

            // 2. Pobranie i walidacja ceny maksymalnej
            Console.Write("Podaj maksymalną cenę (zł): ");
            string maxCenaTekst = Console.ReadLine();
            if (!int.TryParse(maxCenaTekst, out int maxCena) || maxCena <= 0)
            {
                Console.WriteLine("Cena maksymalna musi być liczbą większą od 0.");
                return;
            }

            // Sprawdzenie logiczne zakresu cen
            if (minCena > maxCena)
            {
                Console.WriteLine("Cena minimalna nie może być większa od ceny maksymalnej.");
                return;
            }

            // 3. Pobranie preferowanej marki
            Console.Write("Podaj preferowaną markę (lub wciśnij ENTER / wpisz 'dowolna' dla wszystkich): ");
            string preferowanaMarka = Console.ReadLine().Trim();

            bool czyDowolnaMarka = string.IsNullOrEmpty(preferowanaMarka) || preferowanaMarka.ToLower() == "dowolna";

            Console.WriteLine("\n=== DOPASOWANE DOSTĘPNE AUTA ===");

            // Wyświetlenie nagłówków tabeli
            Console.WriteLine(
                $"{naglowki[0],-5}" +
                $"{naglowki[1],-15}" +
                $"{naglowki[2],-20}" +
                $"{naglowki[3],-10}" +
                $"{naglowki[4],-12}" +
                $"{naglowki[5],-15}"
            );
            Console.WriteLine("---------------------------------------------------------------------");

            bool znalezionoBrak = true;

            for (int i = 0; i < liczbaAut; i++)
            {
                // tylko dostępne auta
                if (auta[i, 4] != "Dostepny")
                {
                    continue;
                }

                // Pobieramy cenę auta z bazy
                int cenaAuta = int.Parse(auta[i, 3]);

                // Filtrowanie po cenie min i max
                if (cenaAuta >= minCena && cenaAuta <= maxCena)
                {
                    // Filtrowanie po marce 
                    if (czyDowolnaMarka || auta[i, 0].ToLower() == preferowanaMarka.ToLower())
                    {
                        Console.WriteLine(
                            $"{i + 1,-5}" +
                            $"{auta[i, 0],-15}" +
                            $"{auta[i, 1],-20}" +
                            $"{auta[i, 2],-10}" +
                            $"{auta[i, 3],-12}" +
                            $"{auta[i, 4],-15}"
                        );
                        znalezionoBrak = false;
                    }
                }
            }

            if (znalezionoBrak)
            {
                Console.WriteLine("Niestety, nie znaleźliśmy dostępnych aut spełniających Twoje kryteria.");
            }
        }
        // Opcja 7: Edytuj cenę auta
        static void EdytujCene()
        {
            WyswietlAuta();

            if (liczbaAut == 0)
            {
                return;
            }

            Console.Write("Podaj ID auta do zmiany ceny: ");
            string tekstId = Console.ReadLine();

            if (!int.TryParse(tekstId, out int id))
            {
                Console.WriteLine("ID musi być liczbą.");
                return;
            }

            int indeks = id - 1;

            if (indeks < 0 || indeks >= liczbaAut)
            {
                Console.WriteLine("Auto o takim ID nie istnieje.");
                return;
            }

            Console.Write("Podaj nową cenę (zł): ");
            string tekstCena = Console.ReadLine();

            if (!int.TryParse(tekstCena, out int nowaCena) || nowaCena <= 0)
            {
                Console.WriteLine("Cena musi być poprawną liczbą większą od 0.");
                return;
            }

            // Zmiana ceny w tablicy
            auta[indeks, 3] = nowaCena.ToString();

            // Zapis do pliku
            ZapiszAutaDoPliku();

            Console.WriteLine("Cena auta została pomyślnie zaktualizowana.");
        }

        // Opcja 11: Pokaż najdroższe auto
        static void PokazNajdrozsze()
        {
            if (liczbaAut == 0)
            {
                Console.WriteLine("Brak aut w bazie.");
                return;
            }

            int maxCena = -1;
            int maxIndeks = -1;

            for (int i = 0; i < liczbaAut; i++)
            {
                int cena = int.Parse(auta[i, 3]);
                if (cena > maxCena)
                {
                    maxCena = cena;
                    maxIndeks = i;
                }
            }

            Console.WriteLine("=== NAJDROŻSZE AUTO W SALONIE ===");
            Console.WriteLine($"Marka: {auta[maxIndeks, 0]}");
            Console.WriteLine($"Model: {auta[maxIndeks, 1]}");
            Console.WriteLine($"Rok: {auta[maxIndeks, 2]}");
            Console.WriteLine($"Cena: {auta[maxIndeks, 3]} zł");
            Console.WriteLine($"Status: {auta[maxIndeks, 4]}");
        }

        // Opcja 12: Pokaż najtańsze auto
        static void PokazNajtansze()
        {
            if (liczbaAut == 0)
            {
                Console.WriteLine("Brak aut w bazie.");
                return;
            }

            int minCena = int.MaxValue;
            int minIndeks = -1;

            for (int i = 0; i < liczbaAut; i++)
            {
                int cena = int.Parse(auta[i, 3]);
                if (cena < minCena)
                {
                    minCena = cena;
                    minIndeks = i;
                }
            }

            Console.WriteLine("=== NAJTAŃSZE AUTO W SALONIE ===");
            Console.WriteLine($"Marka: {auta[minIndeks, 0]}");
            Console.WriteLine($"Model: {auta[minIndeks, 1]}");
            Console.WriteLine($"Rok: {auta[minIndeks, 2]}");
            Console.WriteLine($"Cena: {auta[minIndeks, 3]} zł");
            Console.WriteLine($"Status: {auta[minIndeks, 4]}");
        }

        // Metoda pomocnicza generująca linie raportu 
        // Wykorzystywana przez opcję 13 (wyświetlanie) i 14 (zapis)
        static string[] GenerujDaneRaportu()
        {
            int dostepne = 0;
            int sprzedane = 0;
            int zarezerwowane = 0;
            long wartoscDostepnych = 0; // long zabezpiecza przed zbyt dużą kwotą
            long wartoscSprzedanych = 0;

            for (int i = 0; i < liczbaAut; i++)
            {
                int cena = int.Parse(auta[i, 3]);

                if (auta[i, 4] == "Dostepny")
                {
                    dostepne++;
                    wartoscDostepnych += cena;
                }
                else if (auta[i, 4] == "Sprzedany")
                {
                    sprzedane++;
                    wartoscSprzedanych += cena;
                }
                else if (auta[i, 4] == "Zarezerwowany")
                {
                    zarezerwowane++;
                }
            }

            string[] linieRaportu = new string[]
            {
                "==================================",
                "         RAPORT SALONU",
                "==================================",
                $"Całkowita liczba aut w bazie: {liczbaAut}",
                "----------------------------------",
                $"Auta dostępne: {dostepne}",
                $"Łączna wartość aut dostępnych: {wartoscDostepnych} zł",
                "----------------------------------",
                $"Auta sprzedane: {sprzedane}",
                $"Łączny przychód ze sprzedaży: {wartoscSprzedanych} zł",
                "----------------------------------",
                $"Auta zarezerwowane: {zarezerwowane}",
                "=================================="
            };

            return linieRaportu;
        }

        // Opcja 13: Pokaż raport salonu
        static void PokazRaport()
        {
            if (liczbaAut == 0)
            {
                Console.WriteLine("Brak aut w bazie do wygenerowania raportu.");
                return;
            }

            string[] raport = GenerujDaneRaportu();

            foreach (string linia in raport)
            {
                Console.WriteLine(linia);
            }
        }

        // Opcja 14: Zapisz raport do pliku Dane/raport.txt
        static void ZapiszRaport()
        {
            if (liczbaAut == 0)
            {
                Console.WriteLine("Brak aut w bazie do zapisania raportu.");
                return;
            }

            string plikRaportu = Path.Combine(folderProjektu, "Dane", "raport.txt");

            try
            {
                string[] raport = GenerujDaneRaportu();
                File.WriteAllLines(plikRaportu, raport);
                Console.WriteLine($"Raport został pomyślnie zapisany w pliku:\n{plikRaportu}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Wystąpił błąd podczas zapisu raportu: {ex.Message}");
            }
        }
    }
}