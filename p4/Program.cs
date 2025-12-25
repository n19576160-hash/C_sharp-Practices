using System;

namespace BankingSystem
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("===== Bank Account System =====\n");

            // Step 1:Creating two accounts
            BankAccount imtiaz = new BankAccount("ACC001", "Imtiaz", 45000);
            BankAccount faria = new BankAccount("ACC002", "Faria", 67000);

            // Step 2:displaying accounts information
            imtiaz.DisplayAccountInfo();
            faria.DisplayAccountInfo();

            // Step 3: money deposited to Imtiaz's account 
            Console.WriteLine("--- 5000 taka added ---");
            imtiaz.Deposit(5000);

            // Step 4: money withdrawn from Faria's account
            Console.WriteLine("\n--- 10000 taka withdrawn ---");
            faria.Withdraw(10000);

            // Step 5: Imtiaz transferred money to Faria's account
            Console.WriteLine("\n--- Imtiaz sent 5000 taka ---");
            imtiaz.Transfer(15000, faria);

            // Step 6: Trying to withdraw money
            Console.WriteLine("\n--- Imtiaz tried to withdraw100000 taka ---");
            imtiaz.Withdraw(100000);

            // Step 7: Current state
            Console.WriteLine("\n===== Accounts Current State =====");
            imtiaz.DisplayAccountInfo();
            faria.DisplayAccountInfo();

           
            Console.WriteLine("\nPress any key to exit...");
            Console.ReadKey();
        }
    }
}