using System;

namespace BankingSystem
{
    public class BankAccount
    {
        // Properties - Account info
        public string AccountNumber{get;private set;}
        public string AccountName{get;private set;}
        public double Balance{get;private set;}
        public DateTime OpenDate{get;private set;}
        public string AccountType{get;private set;}

        // Constructor - creating new account
        public BankAccount(string accountNumber, string accountName, double initialBalance,string accountType="Savings")
        {

            if (initialBalance < 0)  throw new ArgumentException("Initial balance cannot be negative");

            if (string.IsNullOrEmpty(accountNumber)) throw new ArgumentException("Account number cannot be empty");

            if (string.IsNullOrEmpty(accountName))  throw new ArgumentException("Account name cannot be empty");


            this.AccountNumber = accountNumber;
            this.AccountName = accountName;
            this.Balance = initialBalance;
            this.OpenDate = DateTime.Now;
            this.AccountType = accountType;
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
                throw new InvalidOperationException(
                    $"Insufficient balance. Available: {Balance:N2} BDT, Required: {amount:N2} BDT"
                );
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
            if (this.AccountNumber == targetAccount.AccountNumber)
            {
                throw new InvalidOperationException("Cannot transfer to the same account");
            }
             if (Balance < amount)
            {
                throw new InvalidOperationException(
                    $"Insufficient balance for transfer. Available: {Balance:N2} BDT"
                );
            }

            
            this.Withdraw(amount);
            targetAccount.Deposit(amount);
        
            
        }
        //Method 4: Check if given Account has sufficient balance
        public bool HasSufficientBalance(double amount)
        {
            return Balance >= amount;
        }

        // Method 5: Get account info as string
        public string GetAccountInfo()
        {
            return $"Account Number: {AccountNumber}\n" +
                   $"Account Name: {AccountName}\n" +
                   $"Account Type: {AccountType}\n" +
                   $"Balance: {Balance:N2} BDT\n" +
                   $"Opened: {OpenDate:dd MMM yyyy}";
        }

        
        
        //Method 6: Get short summary
        public string GetSummary()
        {
            return $"[{AccountNumber}] {AccountName} - {Balance:N2} BDT ({AccountType})";
        }

        //Method 7: Get account holder name
        public string GetAccountHolder()
        {
            return AccountName;
        }
    }
}
