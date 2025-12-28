using System;

namespace BankingSystem
{
    public class BankAccount
    {
        // Properties - Account info
        public string AccountNumber{get;set;}
        public string AccountName{get;set;}
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
            if (amount <= 0)
            {
                throw new ArgumentException("Deposit amount must be positive");
            }

            Balance += amount;
        }

        // Method 2: Withdraw 
        public void Withdraw(double amount)
        {
            if (amount <= 0)
            {
                throw new ArgumentException("Withdrawal amount must be positive");
            }

            if (Balance < amount)
            {
                throw new InvalidOperationException($"Insufficient balance. Current balance: {Balance:N2} BDT");
            }

            Balance -= amount;
        }

        // Method 3: Transfer to another account
        public void Transfer(double amount, BankAccount targetAccount)
        {
            if (amount <= 0)
            {
                throw new ArgumentException("Transfer amount must be positive");
            }

            if (targetAccount == null)
            {
                throw new ArgumentNullException(nameof(targetAccount), "Target account cannot be null");
            }

            this.Withdraw(amount);
            targetAccount.Balance += amount;
        }
        
    }
}
