using System;
using System.Collections.Generic;
using System.Linq;

namespace BankingSystem
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("===== Bank Account Collection Management =====\n");

            // Step 1:Creating Bank accounts collection
            List<BankAccount> bankAccounts = new List<BankAccount>();


            //creating multiple accounts and adding them to list
            
            
            BankAccount acc1 = new BankAccount("ACC-001", "Imtiaz Rahman", 45000);
            bankAccounts.Add(acc1);
            BankAccount acc2 = new BankAccount("ACC-002", "Faria Islam", 67000);
            bankAccounts.Add(acc2);
            BankAccount acc3 = new BankAccount("ACC-003", "Sakib Ahmed", 30000);
            bankAccounts.Add(acc3);
            BankAccount acc4 = new BankAccount("ACC-004", "Nusrat Jahan", 52000);
            bankAccounts.Add(acc4);
            BankAccount acc5 = new BankAccount("ACC-005", "Rakib Hasan", 18000);
            bankAccounts.Add(acc5);

            
        

            // Step 2:displaying account informations
            DisplayAllAccounts(bankAccounts);
            // Step 3:Calculating the amount of total balance of the bank
            DisplayTotalBalance(bankAccounts);

            
            // Step 4: Deposit 
            Console.WriteLine("Transaction 1: Imtiaz deposited 10000 taka");
            acc1.Deposit(10000);
            Console.WriteLine();

            // Step 5: money withdrawn from Faria's account
            Console.WriteLine("Transaction 2:  15000 taka withdrawn from Faria's account");
            acc2.Withdraw(15000);
            Console.WriteLine();

            // Step 6:  Transfer 
            Console.WriteLine("Transaction 3: Sakib transferreed 8000 taka to Nusrat's account");
            acc3.Transfer(8000, acc4);
            Console.WriteLine();

            // Step 7:Multiple Transactions
            Console.WriteLine("Transaction 4: Rakib deposited 5000 taka");
            acc5.Deposit(5000);
            Console.WriteLine();

            Console.WriteLine("🔄 Transaction 5: Rakib tried to withdraw30000 taka");
            acc5.Withdraw(30000);
            Console.WriteLine();


            // Step 8: Current state:Updated information
            Console.WriteLine("\n===== Accounts Current State =====");
            DisplayAllAccounts(bankAccounts);
            DisplayTotalBalance(bankAccounts);


            // Step 9:Extra Analysis
            FindRichestAccount(bankAccounts);
            FindPoorestAccount(bankAccounts);
            CalculateAverageBalance(bankAccounts);

            // Step 10: searching for an Specific Account 
            Console.WriteLine("\nFind out: ACC-003");
            BankAccount foundAccount = FindAccountByNumber(bankAccounts, "ACC-003");
            if (foundAccount != null)
            {
                Console.WriteLine("Account found:");
                foundAccount.DisplayAccountInfo();
            }



           
            Console.WriteLine("\nPress any key to exit...");
            Console.ReadKey();
        }



        // Helper Method 1: Show all Accounts 
        static void DisplayAllAccounts(List<BankAccount> accounts)
        {
           
            for (int i = 0; i < accounts.Count; i++)
            {
                Console.Write($"{i + 1}. ");
                accounts[i].DisplayAccountInfo();
            }
            Console.WriteLine();
        }


        // Helper Method 2: count total Balance 
        static void DisplayTotalBalance(List<BankAccount> accounts)
        {
            double totalBalance = 0;
            
            foreach (BankAccount account in accounts)
            {
                totalBalance += account.Balance;
            }

           
            Console.WriteLine($" Bank has a total Balance: {totalBalance:N2} taka");
           
        }


        // Helper Method 3: The Account with the highest balance
        static void FindRichestAccount(List<BankAccount> accounts)
        {
            BankAccount richest = accounts[0];
            
            foreach (BankAccount account in accounts)
            {
                if (account.Balance > richest.Balance)
                {
                    richest = account;
                }
            }

            Console.WriteLine("Account with highest balance:");
            richest.DisplayAccountInfo();
        }


        // Helper Method 4: The Account with the lowest balance
        static void FindPoorestAccount(List<BankAccount> accounts)
        {
            BankAccount poorest = accounts[0];
            
            foreach (BankAccount account in accounts)
            {
                if (account.Balance < poorest.Balance)
                {
                    poorest = account;
                }
            }

            Console.WriteLine("\n Account with lowest balance :");
            poorest.DisplayAccountInfo();
        }



        // Helper Method 5: Calculating Average Balance 
        static void CalculateAverageBalance(List<BankAccount> accounts)
        {
            double totalBalance = 0;
            
            foreach (BankAccount account in accounts)
            {
                totalBalance += account.Balance;
            }

            double average = totalBalance / accounts.Count;
            Console.WriteLine($"\n Average Balance: {average:N2} taka");
        }


        // Helper Method 6: Finding an account usingAccount Number 
        static BankAccount FindAccountByNumber(List<BankAccount> accounts, string accountNumber)
        {
            foreach (BankAccount account in accounts)
            {
                if (account.AccountNumber == accountNumber)
                {
                    return account;
                }
            }
            return null;
        }
    }
}