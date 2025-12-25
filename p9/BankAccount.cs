using System;

namespace BankingSystem
{
    public class BankAccount
    {
        // Properties - Account info
        public string AccountNumber{get;private set;}
        public string AccountName{get;private set;}
        public double Balance{get;private set;}

        // Constructor - creating new account
        public BankAccount(string accountNumber, string accountName, double initialBalance)
        {
            this.AccountNumber = accountNumber;
            this.AccountName = accountName;
            this.Balance = initialBalance;
        }

        // Method 1: Deposit
        public void Deposit(double amount)
        {
            if (amount > 0)
            {
                Balance += amount;
                Console.WriteLine($"Deposit successful! {amount} taka has been added।");
                Console.WriteLine($"Current Balance: {Balance} taka");
            }
            else
            {
                Console.WriteLine("Error! Please deposit a valid amount");
            }
        }

        // Method 2: Withdraw 
        public void Withdraw(double amount)
        {
            if (amount <= 0)
            {
                Console.WriteLine("Error! Please write a valid amount");
                return;
            }

            if (Balance >= amount)
            {
                Balance -= amount;
                Console.WriteLine($"Successfully credited ! {amount} taka has been withdrawn।");
                Console.WriteLine($"Current Balance: {Balance} taka");
            }
            else
            {
                Console.WriteLine("Failed to Withdraw! insufficient Balance");
                Console.WriteLine($"Current Balance: {Balance} taka");
            }
        }

        // Method 3: Transfer to another account
        public void Transfer(double amount, BankAccount targetAccount)
        {
            if (amount <= 0)
            {
                Console.WriteLine("Error! Please write a valid amount");
                return;
            }

            if (Balance >= amount)
            {
                
                //this line will withdraw amount from sender account 
                this.Withdraw(amount);
                //this line will deposit amount to receiver account 
                targetAccount.Deposit(amount);
                //success message will be shown to the sender
                Console.WriteLine($"Successfully transferred! {amount} taka has been sent to target account: {targetAccount.AccountName}");
                Console.WriteLine($"Your current Balance: {Balance} taka");

            }
            else
            {
                Console.WriteLine("Failed to withdraw! Insufficient Balance।");
                Console.WriteLine($"Current Balance: {Balance} taka");
            }
        }

        
        
        // Method 4: Display Account Info 
        public void DisplayAccountInfo()
        {
            Console.WriteLine($"  [{AccountNumber}] {AccountName} - Balance: {Balance:N2} taka");
        }
        
        
    }
}

