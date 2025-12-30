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
            Console.WriteLine(" Creating bank accounts...\n");

            // Creating Multiple accounts
            try
            {
                bankAccounts.Add(new BankAccount("ACC-001", "Imtiaz Rahman", 45000, "Savings"));
                bankAccounts.Add(new BankAccount("ACC-002", "Faria Islam", 67000, "Current"));
                bankAccounts.Add(new BankAccount("ACC-003", "Sakib Ahmed", 30000, "Savings"));
                bankAccounts.Add(new BankAccount("ACC-004", "Nusrat Jahan", 52000, "Savings"));
                bankAccounts.Add(new BankAccount("ACC-005", "Rakib Hasan", 18000, "Current"));

                Console.WriteLine($"{bankAccounts.Count} bank accounts created successfully!\n");
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"✗ Error creating account: {ex.Message}");
                return;
            }

            
        

            // Step 2:displaying account informations
            DisplayAllAccounts(bankAccounts);
            // Step 3:Calculating the amount of total balance of the bank
            DisplayTotalBalance(bankAccounts);

            
            // Step 4: Deposit 
            Console.WriteLine("Transaction 1: Imtiaz deposites 10000 BDT ---");
            HandleDeposit(bankAccounts[0], 10000);

            Console.WriteLine("\n--- Transaction 2: Faria deposits 15000 BDT ---");
            HandleDeposit(bankAccounts[1], 15000);

            Console.WriteLine("\n--- Invalid Transaction: Negative deposit ---");
            HandleDeposit(bankAccounts[2], -5000);

            // Step 5: money withdrawn from Faria's account
            Console.WriteLine("--- Transaction 3: Sakib withdraws 8000 BDT ---");
            HandleWithdraw(bankAccounts[2], 8000);

            Console.WriteLine("\n--- Transaction 4: Nusrat withdraws 12000 BDT ---");
            HandleWithdraw(bankAccounts[3], 12000);

            Console.WriteLine("\n--- Failed Transaction: Insufficient balance ---");
            HandleWithdraw(bankAccounts[4], 50000);


            // Step 6:  Transfer 
            Console.WriteLine("--- Transaction 5: Imtiaz transfers 20000 BDT to Faria ---");
            HandleTransfer(bankAccounts[0], bankAccounts[1], 20000);

            Console.WriteLine("\n--- Transaction 6: Sakib transfers 5000 BDT to Rakib ---");
            HandleTransfer(bankAccounts[2], bankAccounts[4], 5000);

            Console.WriteLine("\n--- Failed Transaction: Transfer to same account ---");
            HandleTransfer(bankAccounts[0], bankAccounts[0], 5000);

            Console.WriteLine("\n--- Failed Transaction: Insufficient balance for transfer ---");
            HandleTransfer(bankAccounts[4], bankAccounts[0], 100000);



            // Step 7: Current state:Updated information
            Console.WriteLine("\n===== Accounts Current State =====");
            DisplayAllAccounts(bankAccounts);
            DisplayTotalBalance(bankAccounts);


            // Step 8:Extra Analysis
            FindRichestAccount(bankAccounts);
            FindPoorestAccount(bankAccounts);
            CalculateAverageBalance(bankAccounts);

            // ধাপ ৯: Account type breakdown
            
            Console.WriteLine("Account Type Breakdown");
            

            DisplayAccountsByType(bankAccounts, "Savings");
            DisplayAccountsByType(bankAccounts, "Current");

            // Step 10: searching for an Specific Account 
            SearchAndDisplayAccount(bankAccounts, "ACC-003");
            SearchAndDisplayAccount(bankAccounts, "ACC-999");



           
            Console.WriteLine("\nPress any key to exit...");
            Console.ReadKey();
        }



        // Helper Method 1: Show all Accounts 
        static void DisplayAllAccounts(List<BankAccount> accounts)
        {
            Console.WriteLine("All Bank Accounts:");

            for (int i = 0; i < accounts.Count; i++)
            {
                Console.Write($"{i + 1}. {accounts[i].GetSummary()}");
                
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

           
            Console.WriteLine($" Bank has a total Balance: {totalBalance:N2} BDT");
           
        }

        // Handle deposit with try-catch
        static void HandleDeposit(BankAccount account, double amount)
        {
            try
            {
                account.Deposit(amount);
                Console.WriteLine($"Deposit successful!");
                Console.WriteLine($"  Account: {account.AccountName}");
                Console.WriteLine($"  Amount: {amount:N2} BDT");
                Console.WriteLine($"  New Balance: {account.Balance:N2} BDT");
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"Deposit failed: {ex.Message}");
            }
        }

        // Handle withdraw with try-catch
        static void HandleWithdraw(BankAccount account, double amount)
        {
            try
            {
                account.Withdraw(amount);
                Console.WriteLine($" Withdrawal successful!");
                Console.WriteLine($"  Account: {account.AccountName}");
                Console.WriteLine($"  Amount: {amount:N2} BDT");
                Console.WriteLine($"  New Balance: {account.Balance:N2} BDT");
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"✗ Withdrawal failed: {ex.Message}");
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine($" Withdrawal failed!");
                Console.WriteLine($"   {ex.Message}");
            }
        }

        // Handle transfer with try-catch
        static void HandleTransfer(BankAccount fromAccount, BankAccount toAccount, double amount)
        {
            try
            {
                fromAccount.Transfer(amount, toAccount);
                Console.WriteLine($" Transfer successful!");
                Console.WriteLine($"  From: {fromAccount.AccountName} ({fromAccount.AccountNumber})");
                Console.WriteLine($"  To: {toAccount.AccountName} ({toAccount.AccountNumber})");
                Console.WriteLine($"  Amount: {amount:N2} BDT");
                Console.WriteLine($"  {fromAccount.AccountName}'s new balance: {fromAccount.Balance:N2} BDT");
                Console.WriteLine($"  {toAccount.AccountName}'s new balance: {toAccount.Balance:N2} BDT");
            }
            
            catch (ArgumentNullException ex)
            {
                Console.WriteLine($" Transfer failed: {ex.Message}");
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine($" Transfer failed: {ex.Message}");
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($" Transfer failed: {ex.Message}");
            }
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
            Console.WriteLine($"   {richest.GetSummary()}");
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
            Console.WriteLine($"   {poorest.GetSummary()}");
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

        // Display accounts by type
        static void DisplayAccountsByType(List<BankAccount> accounts, string accountType)
        {
            Console.WriteLine($" {accountType} Accounts:");
    

            int count = 0;
            double totalBalance = 0;

            foreach (BankAccount account in accounts)
            {
                if (account.AccountType == accountType)
                {
                    count++;
                    totalBalance += account.Balance;
                    Console.WriteLine($"   {account.GetSummary()}");
                }
            }

            if (count == 0)
            {
                Console.WriteLine($"   No {accountType} accounts found.");
            }
            else
            {
                Console.WriteLine($"\n   Total {accountType} Accounts: {count}");
                Console.WriteLine($"   Total Balance: {totalBalance:N2} BDT");
            }
            Console.WriteLine();
        }


        // Helper Method 6: Finding an account usingAccount Number 
        static void SearchAndDisplayAccount(List<BankAccount> accounts, string accountNumber)
        {
            Console.WriteLine($"Searching for account: {accountNumber}");
            BankAccount foundAccount = null;


            foreach (BankAccount account in accounts)
            {
                if (account.AccountNumber == accountNumber)
                {
                     foundAccount = account;
                    break;
                }
            }
            if (foundAccount != null)
            {
                Console.WriteLine("Account found!\n");
                Console.WriteLine(foundAccount.GetAccountInfo());
            }
            else
            {
                Console.WriteLine($"Account {accountNumber} not found in the system.");
            }
            Console.WriteLine();
        }
    }
}
