using System;

namespace Pathe_hr.obj
{
    class PaymentSystem
    {
        public static void SelectPaymentMethodAndConfirm()
        {
            Console.WriteLine("Kies een betaalmethode:");
            string[] paymentOptions = { "iDEAL", "PayPal", "Credit/Debit", "Cash (op locatie)" };
            int selectedIndex = DisplayMenu(paymentOptions);

            // Betaal bevestiging afdrukken
            switch (paymentOptions[selectedIndex])
            {
                case "iDEAL":
                    Console.WriteLine("U heeft betaald met iDEAL");
                    break;
                case "PayPal":
                    Console.WriteLine("U heeft betaald met PayPal");
                    break;
                case "Credit/Debit":
                    Console.WriteLine("U heeft betaald met Credit/Debit");
                    break;
                case "Cash (op locatie)":
                    Console.WriteLine("U heeft betaald met Cash");
                    break;
                default:
                    Console.WriteLine("Onbekende betaalmethode");
                    break;
            }
        }

        static int DisplayMenu(string[] options)
        {
            int selectedIndex = 0;

            while (true)
            {
                Console.Clear();
                Console.WriteLine("Gebruik de pijltjestoetsen om te navigeren en druk op Enter om te selecteren:");
                for (int i = 0; i < options.Length; i++)
                {
                    if (i == selectedIndex)
                    {
                        Console.Write("=> ");
                    }
                    else
                    {
                        Console.Write("   ");
                    }
                    Console.WriteLine(options[i]);
                }

                ConsoleKeyInfo key = Console.ReadKey();

                switch (key.Key)
                {
                    case ConsoleKey.UpArrow:
                        selectedIndex = (selectedIndex == 0) ? options.Length - 1 : selectedIndex - 1;
                        break;
                    case ConsoleKey.DownArrow:
                        selectedIndex = (selectedIndex == options.Length - 1) ? 0 : selectedIndex + 1;
                        break;
                    case ConsoleKey.Enter:
                        return selectedIndex;
                }
            }
        }
    }
}
