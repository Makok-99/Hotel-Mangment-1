using System;
using System.IO;
using System.Collections.Generic;

namespace program
{
    internal class Makok
    {
        enum Room { SingleRoom = 1, DoubleRoom = 2, SuiteRoom = 3 }

        static string ReadName()
        {
            Console.WriteLine("");
            Console.WriteLine("\tWelcome to Al-Maasara Hotel!");
            Console.WriteLine("\t============================");

            Console.WriteLine("Enter your name:\n");
            string name = Console.ReadLine();

            Console.WriteLine($"Hello Mr. {name}, welcome to our hotel!\n");

            return name;
        }

        static int ReadNumber(string message)
        {
            Console.WriteLine(message);
            return int.Parse(Console.ReadLine());
        }

        static int NumberOfNight()
        {
            return ReadNumber("Please enter the number of nights you wish to stay:");
        }

        static void RoomInHotel(out string roomName, out int roomPrice)
        {
            Console.WriteLine("Please choose your room type:\n");
            Console.WriteLine("1. Single Room - $5000 per night");
            Console.WriteLine("2. Double Room - $7000 per night");
            Console.WriteLine("3. Suite Room  - $12000 per night");

            string roomType = Console.ReadLine();

            switch (roomType)
            {
                case "1":
                    roomName = "Single Room";
                    roomPrice = 5000;
                    break;
                case "2":
                    roomName = "Double Room";
                    roomPrice = 7000;
                    break;
                case "3":
                    roomName = "Suite Room";
                    roomPrice = 12000;
                    break;
                default:
                    roomName = "Unknown";
                    roomPrice = 0;
                    Console.WriteLine("Invalid room type selected.");
                    break;
            }
        }

        static void PrintInvoice(string name, string roomName, int roomPrice, int nights, int persons, string paymentMethod)
        {
            int personsPrice = persons * 300;
            int total = (roomPrice * nights) + personsPrice;
            int discount = nights > 5 ? total / 10 : 0;
            int finalPrice = total - discount;

            Console.Clear();
            Console.WriteLine("==============================================");
            Console.WriteLine("\t\tInvoice\t\t");
            Console.WriteLine("==============================================");
            Console.WriteLine(
                "Name".PadRight(20) +
                "Persons".PadRight(10) +
                "Room".PadRight(20) +
                "Total".PadRight(15) +
                "Paid By".PadRight(15) +
                "Date".PadRight(31)
            );
            Console.WriteLine("-----------------------------------------------------------------------------------------------------------------");

            Console.WriteLine(
                name.PadRight(20) +
                persons.ToString().PadRight(10) +
                roomName.PadRight(20) +
                finalPrice.ToString().PadRight(15) +
                paymentMethod.PadRight(15) +
                DateTime.Now.ToString().PadRight(31)
            );

            Console.WriteLine("==================================================================================================================");

            string record =
                $"{name},{persons},{roomName},{nights},{roomPrice},{personsPrice},{total},{discount},{finalPrice},{paymentMethod},{DateTime.Now}";

            File.AppendAllText("reservations.txt", record + Environment.NewLine);
        }

        static void ShowPreviousInvoice(string name)
        {
            if (!File.Exists("reservations.txt"))
                return;

            string[] lines = File.ReadAllLines("reservations.txt");

            foreach (string line in lines)
            {
                if (string.IsNullOrWhiteSpace(line)) continue;
                string[] p = line.Split(',');
                if (p.Length < 11) continue;

                if (p[0] == name)
                {
                    Console.WriteLine("==============================================");
                    Console.WriteLine("\t\tPrevious Invoice\t\t");
                    Console.WriteLine("==============================================");
                    Console.WriteLine(
                        "Name".PadRight(20) +
                        "Persons".PadRight(10) +
                        "Room".PadRight(20) +
                        "Total".PadRight(15) +
                        "Paid By".PadRight(15) +
                        "Date".PadRight(31)
                    );
                    Console.WriteLine("-----------------------------------------------------------------------------------------------------------------");

                    Console.WriteLine(
                        p[0].PadRight(20) +
                        p[1].PadRight(10) +
                        p[2].PadRight(20) +
                        p[8].PadRight(15) +
                        p[9].PadRight(15) +
                        p[10].PadRight(31)
                    );

                    Console.WriteLine("==================================================================================================================\n");
                    return;
                }
            }
        }

        static void ShowAllCustomers()
        {
            if (!File.Exists("reservations.txt"))
            {
                Console.WriteLine("No reservations file found.\n");
                return;
            }

            string[] lines = File.ReadAllLines("reservations.txt");

            if (lines.Length == 0)
            {
                Console.WriteLine("No reservations found.\n");
                return;
            }

            Console.WriteLine("==============================================");
            Console.WriteLine("\t\t All Customers\t\t");
            Console.WriteLine("==============================================");

            Console.WriteLine(
                "Name".PadRight(20) +
                "Persons".PadRight(10) +
                "Room".PadRight(20) +
                "Total".PadRight(15) +
                "Paid By".PadRight(15) +
                "Date".PadRight(31)
            );
            Console.WriteLine("-----------------------------------------------------------------------------------------------------------------");

            foreach (string line in lines)
            {
                if (string.IsNullOrWhiteSpace(line)) continue;
                string[] p = line.Split(',');
                if (p.Length < 11) continue;

                Console.WriteLine(
                    p[0].PadRight(20) +
                    p[1].PadRight(10) +
                    p[2].PadRight(20) +
                    p[8].PadRight(15) +
                    p[9].PadRight(15) +
                    p[10].PadRight(31)
                );
            }

            Console.WriteLine("==================================================================================================================");
        }

        static void ReadVisaInfo()
        {
            string visaNumber; int visaYear; string cvc;

            while (true)
            {
                Console.WriteLine("Enter your Visa Number (16 digits):");
                visaNumber = Console.ReadLine();

                if (visaNumber.Length == 16 && long.TryParse(visaNumber, out _))
                    break;

                Console.WriteLine("Invalid Visa Number! It must be exactly 16 digits.\n");
            }

            while (true)
            {
                Console.WriteLine("Enter Visa Expiration Year (>= 2025):");
                string input = Console.ReadLine();

                if (int.TryParse(input, out visaYear) && visaYear >= 2025)
                    break;

                Console.WriteLine("Invalid Year! Must be 2025 or higher.\n");
            }

            while (true)
            {
                Console.WriteLine("Enter CVC Code (3 digits):");
                cvc = Console.ReadLine();

                if (cvc.Length == 3 && int.TryParse(cvc, out _))
                    break;

                Console.WriteLine("Invalid CVC! Must be exactly 3 digits.\n");
            }

            Console.WriteLine("\nVisa Accepted Successfully!");
        }

        static void VodafoneCash()
        {
            string number;
            while (true)
            {
                Console.WriteLine("Enter your VodafoneCash Number:");
                number = Console.ReadLine();

                if (number.Length == 11 && long.TryParse(number, out _))
                    break;

                Console.WriteLine("Invalid number! It must be 11 digits.\n");
            }

            Console.WriteLine("\nAccepted Successfully!");
        }

        static string Paymentmethods()
        {
            Console.Clear();
            Console.WriteLine("========================================");
            Console.WriteLine("{0,30}", "Payment methods");
            Console.WriteLine("========================================");
            Console.WriteLine("[1] Visa.");
            Console.WriteLine("[2] Vodafone Cash.");
            Console.WriteLine("========================================");
            Console.Write("Enter your choice: ");
            string choice = Console.ReadLine();
            Console.Clear();

            switch (choice)
            {
                case "1":
                    ReadVisaInfo();
                    return "Visa";

                case "2":
                    VodafoneCash();
                    return "Vodafone Cash";

                default:
                    Console.WriteLine("Invalid choice.");
                    return "";
            }
        }

        static void StartReservation()
        {
            string name = ReadName();

            ShowPreviousInvoice(name);

            int age = ReadNumber("Please Enter your age:");
            Console.Clear();

            if (age < 18)
            {
                Console.WriteLine("Sorry sir, we do not accept clients under 18.");
                return;
            }

            int persons = ReadNumber("Enter number of persons (each = $300):");

            RoomInHotel(out string roomName, out int roomPrice);

            int nights = NumberOfNight();

            int personsTotal = persons * 300;
            int total = (nights * roomPrice) + personsTotal;

            Console.WriteLine($"Total Price (Room + Persons): ${total}");
            Console.WriteLine("Type 'ok' to continue:");
            if (Console.ReadLine().ToLower() != "ok")
            {
                Console.WriteLine("Reservation cancelled.");
                return;
            }

            string paymentMethod = Paymentmethods();

            if (paymentMethod == "")
            {
                Console.WriteLine("Payment Failed!");
                return;
            }

            Console.WriteLine("\nPayment Successful! Check your invoice…");

            PrintInvoice(name, roomName, roomPrice, nights, persons, paymentMethod);
        }

        static void DeleteClient()
        {
            if (!File.Exists("reservations.txt"))
            {
                Console.WriteLine("No reservations file found.\n");
                return;
            }

            string[] lines = File.ReadAllLines("reservations.txt");
            if (lines.Length == 0)
            {
                Console.WriteLine("No clients found.\n");
                return;
            }

            Console.Write("Enter the name of the client to delete: ");
            string nameToDelete = Console.ReadLine();

            bool found = false;
            for (int i = 0; i < lines.Length; i++)
            {
                if (lines[i].StartsWith(nameToDelete + ","))
                {
                    found = true;
                    string[] p = lines[i].Split(',');

                    // Show client details in table format like ShowAllCustomers
                    Console.WriteLine("==============================================");
                    Console.WriteLine("\t\tClient Details\t\t");
                    Console.WriteLine("==============================================");
                    Console.WriteLine(
                        "Name".PadRight(20) +
                        "Persons".PadRight(10) +
                        "Room".PadRight(20) +
                        "Total".PadRight(15) +
                        "Paid By".PadRight(15) +
                        "Date".PadRight(31)
                    );
                    Console.WriteLine("-----------------------------------------------------------------------------------------------------------------");

                    Console.WriteLine(
                        p[0].PadRight(20) +
                        p[1].PadRight(10) +
                        p[2].PadRight(20) +
                        p[8].PadRight(15) +
                        p[9].PadRight(15) +
                        p[10].PadRight(31)
                    );

                    Console.WriteLine("==================================================================================================================\n");

                    Console.Write("Do you want to delete this client? (Y/N): ");
                    string confirm = Console.ReadLine();
                    if (confirm.ToLower() == "y")
                    {
                        var list = new List<string>(lines);
                        list.RemoveAt(i);
                        File.WriteAllLines("reservations.txt", list);
                        Console.WriteLine("Client deleted successfully.");
                    }
                    else
                    {
                        Console.WriteLine("Deletion cancelled.");
                    }
                    break;
                }
            }

            if (!found)
            {
                Console.WriteLine("Client not found.");
            }
        }

        static void RunHotelApp()
        {
            bool repeat = true;

            while (repeat)
            {
                Console.Clear();
                Console.WriteLine("========================================");
                Console.WriteLine("{0,30}", "Main Menu Screen in Hotel");
                Console.WriteLine("========================================");
                Console.WriteLine("[1] Show Client List.");
                Console.WriteLine("[2] Add New Client.");
                Console.WriteLine("[3] Delete Client.");
                Console.WriteLine("========================================");
                Console.Write("Enter your choice: ");
                string choice = Console.ReadLine();
                Console.Clear();

                switch (choice)
                {
                    case "1":
                        ShowAllCustomers();
                        break;

                    case "2":
                        StartReservation();
                        break;

                    case "3":
                        DeleteClient();
                        break;

                    default:
                        Console.WriteLine("Invalid choice.\n");
                        break;
                }

                Console.WriteLine("\nPress 'r' to return or any key to exit:");
                if (Console.ReadLine().ToLower() != "r")
                    repeat = false;
            }
        }

        static void Main()
        {
            RunHotelApp();
        }
    }
}
