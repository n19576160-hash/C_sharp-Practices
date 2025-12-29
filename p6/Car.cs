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
        public void RentCar(int days, string customerName)
        {
            if (days <= 0)
            {
                throw new ArgumentException("Rental days must be positive");
            }
             if (!IsAvailable)
            {
                throw new InvalidOperationException($"{Brand} {Model} is currently rented out");
            }

            if (MaintenanceDue)
            {
                throw new InvalidOperationException($"{Brand} {Model} requires maintenance before rental");
            }

            // Perform rental
            IsAvailable = false;
            TotalRentalDays += days;
            TotalRevenue += days * DailyRate;

            // Check if maintenance needed
            if (TotalRentalDays >= 15)
            {
                MaintenanceDue = true;
            }
        }

        // Method 2: Returning Back
        public void ReturnCar()
        {
            if (IsAvailable)
            {
                throw new InvalidOperationException($"{Brand} {Model} is not currently rented");
            }

            IsAvailable = true;
        }

        // Method 3: Maintenance 
        public void PerformMaintenance()
        {
            if (!IsAvailable)
            {
                throw new InvalidOperationException("Cannot perform maintenance while car is rented");
            }

            MaintenanceDue = false;
            TotalRentalDays = 0;
        }

        // Method 4: Get rental cost
         public double CalculateRentalCost(int days)
        {
            if (days <= 0)
                throw new ArgumentException("Days must be positive");

            return days * DailyRate;
        }


        // Method 5:Get Car info
        public string GetCarInfo()
        {
            string status = IsAvailable ? "Available" : "Rented";
            if (MaintenanceDue) status = "Maintenance Due";

            return $"[{CarId}] {Brand} {Model}\n" +
                   $"Daily Rate: {DailyRate:N2} BDT\n" +
                   $"Status: {status}\n" +
                   $"Total Days Rented: {TotalRentalDays}\n" +
                   $"Total Revenue: {TotalRevenue:N2} BDT";
        }

        public string GetShortInfo()
        {
            string status = IsAvailable ? "Available" : "Not Available";
            if (MaintenanceDue) status = "In Maintainance";
            return $"|{status}| [{CarId}] {Brand} {Model} - {DailyRate:N2} BDT/day";
        }

    }
}
