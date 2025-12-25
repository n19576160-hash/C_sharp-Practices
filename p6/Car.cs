using System;

namespace CarRentalSystem
{
    public class Car
    {
        // Car Properties 
        public string CarId{get;private set;}
        public string Model{get;private set;}
        public string Brand{get;private set;}
        public double DailyRate{get;private set;}
        public bool IsAvailable{get;private set;}
        public bool MaintenanceDue{get;private set;}
        public int TotalRentalDays{get;private set;}
        public double TotalRevenue{get;private set;}

        // Constructor - Create new car
        public Car(string carId, string brand, string model, double dailyRate)
        {
            this.CarId = carId;
            this.Brand = brand;
            this.Model = model;
            this.DailyRate = dailyRate;
            this.IsAvailable = true;  // All cars are available initially
            this.MaintenanceDue = false;
            this.TotalRentalDays = 0;
            this.TotalRevenue = 0;
        }

        // Method 1: Renting Portion
        public bool RentCar(int days, string customerName)
        {
            if (!IsAvailable)
            {
                Console.WriteLine($"Sorry! {Brand} {Model} already rented out.");
                return false;
            }

            if (MaintenanceDue)
            {
                Console.WriteLine($"Sorry! {Brand} {Model} in maintenance ");
                return false;
            }

            if (days <= 0)
            {
                Console.WriteLine(" duration to rent a car must be at least 1 day or more.");
                return false;
            }

            // Renting the car
            IsAvailable = false;
            TotalRentalDays += days;
            double rentalCost = days * DailyRate;
            TotalRevenue += rentalCost;

            Console.WriteLine($"Successful! {Brand} {Model} is rented out.");
            Console.WriteLine($"  Customer Name: {customerName}");
            Console.WriteLine($"  Total Rental Days : {days} day/s");
            Console.WriteLine($"  Daily Rate: {DailyRate:N2} taka");
            Console.WriteLine($"  Total Cost: {rentalCost:N2} taka");

            // maintenance needed after half a month of rental for each car
            if (TotalRentalDays >= 15)
            {
                MaintenanceDue = true;
                Console.WriteLine("  Caution: maintenance needed !");
            }

            return true;
        }

        // Method 2: Returning Back
        public bool ReturnCar()
        {
            if (IsAvailable)
            {
                Console.WriteLine($"({Brand} {Model} ):This car is available now!");
                return false;
            }

            IsAvailable = true;
            Console.WriteLine($"{Brand} {Model} is returned sucessfully.");

            if (MaintenanceDue)
            {
                Console.WriteLine("Please ,complete the maintenance");
            }

            return true;
        }

        // Method 3: Maintenance 
        public void PerformMaintenance()
        {
            if (!IsAvailable)
            {
                Console.WriteLine($"If car is with the customer, maintenance paused until returned");
                return;
            }

            Console.WriteLine($"{Brand} {Model} is in maintainence...");
            MaintenanceDue = false;
            TotalRentalDays = 0;  // rental counter reset
            Console.WriteLine($"Maintenance completed! car is ready for rent again।");
        }

        // Method 4: Displaying Car information
        public void DisplayCarInfo()
        {
            string status = IsAvailable ? "Available" : "Rented";
            if (MaintenanceDue)
            {
                status = "Maintenance Due";
            }

            Console.WriteLine($"  [{CarId}] {Brand} {Model} ");
            Console.WriteLine($"       Daily Rate: {DailyRate:N2} taka");
            Console.WriteLine($"       Status: {status}");
            Console.WriteLine($"       Total rental Days: {TotalRentalDays} day");
            Console.WriteLine($"       total Revenue: {TotalRevenue:N2} taka");
        }

        // Method 5:Short Description( for List)
        public void DisplayShortInfo()
        {
            string status = IsAvailable ? "Yes" : "No";
            if (MaintenanceDue) status = "MT";
            
            Console.WriteLine($"  {status} [{CarId}] {Brand} {Model} - {DailyRate:N2} Taka per day");
        }
    }
}