using System;
using System.Collections.Generic;

namespace CarRentalSystem
{
    class Program
    {
        static void Main(string[] args)
        {
            
            Console.WriteLine("Car Rental Management System ");
            

            // Step 1: Car List
            List<Car> carFleet = new List<Car>();

            Console.WriteLine("Listing Cars...\n");

            // adding cars to the list
            carFleet.Add(new Car("CAR-001", "Toyota", "Corolla", 3500));
            carFleet.Add(new Car("CAR-002", "Honda", "Civic", 4000));
            carFleet.Add(new Car("CAR-003", "Toyota", "Prius", 5000));
            carFleet.Add(new Car("CAR-004", "BMW", "X5", 8000));
            carFleet.Add(new Car("CAR-005", "Mercedes", "C-Class", 7500));
            carFleet.Add(new Car("CAR-006", "Nissan", "Altima", 3200));

            Console.WriteLine($"Total {carFleet.Count} cars added!\n");

            // Step 2: Display all car information
            DisplayAllCars(carFleet);

            // Step 3:  Show Available cars
            DisplayAvailableCars(carFleet);


            // Step 4: Renting - Transaction 1
            Console.WriteLine("--- Transaction 1: Imtiaz rented Toyota Corolla ---");
            carFleet[0].RentCar(5, "Imtiaz Rahman");
            Console.WriteLine();

        
            // Step 5: trying to rent a car that is already rented (Unsuccessful attempt)
            Console.WriteLine("--- Transaction 2: Faria wanted to rent Toyota Corolla ---");

            try
            {
                carFleet[0].RentCar(3, "Faria Islam");
                Console.WriteLine(" Rental successful!");
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine($"Rental failed: {ex.Message}");
            }
            Console.WriteLine();

            // Step 6: Invalid rental days
            Console.WriteLine("--- Transaction 3: Mistake in rental days entering---");
            try
            {
                carFleet[1].RentCar(-5, "Sakib Ahmed");
                Console.WriteLine(" Rental successful!");
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"Rental failed: {ex.Message}");
            }
            Console.WriteLine();

            // Step 8: showing currently Available cars (Updated)
           
            DisplayAvailableCars(carFleet);

            

            // Step 9: Returning
            Console.WriteLine("--- Imtiaz has returned Toyota Corolla---");
            carFleet[0].ReturnCar();
            Console.WriteLine();


            // Step 10: re-renting
            Console.WriteLine("\n--- Transaction 5: Nusrat rented Toyota Corolla now--");
            carFleet[0].RentCar(12, "Nusrat Jahan");
            Console.WriteLine();

            // Step 11: Long term rent (Maintenance trigger)
            Console.WriteLine("--- Transaction 6: Rakib rented Mercedes (Long-term) ---");
            try{
                carFleet[4].RentCar(16, "Rakib Hasan");
                carFleet[4].PerformMaintenance();
                Console.WriteLine("Maintenance completed!");
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine($"Maintenance failed: {ex.Message}");
            }
            Console.WriteLine();
            
         

            // Step 12 Mercedes returned AND its Maintenance
            Console.WriteLine("--- Rakib returned Mercedes ---");
            carFleet[4].ReturnCar();
            Console.WriteLine();

            Console.WriteLine("--- Mercedes is in Maintenance---");
            carFleet[4].PerformMaintenance();
            Console.WriteLine();

            // Step 13: Status (Latest)
            
            DisplayAllCars(carFleet);

            // Step 14: Revenue Analysis
            
            CalculateTotalRevenue(carFleet);
            FindMostProfitableCar(carFleet);
            FindMostRentedCar(carFleet);
            Console.WriteLine();

            // Step 15: Search by ID
            
            Console.WriteLine("Find out : CAR-003");
            Car foundCar = FindCarById(carFleet, "CAR-003");
            if (foundCar != null)
            {
                Console.WriteLine("Car found -->");
                Console.WriteLine(foundCar.GetCarInfo());
            }

            
        }

        // Helper Method 1: Showing all cars
        static void DisplayAllCars(List<Car> cars)
        {
            Console.WriteLine("All");

            for (int i = 0; i < cars.Count; i++)
            {
                Console.WriteLine($"{i + 1}.");
                Console.WriteLine(cars[i].GetCarInfo());
                Console.WriteLine();
            }
        }

        // Helper Method 2: Showing only Available cars
        static void DisplayAvailableCars(List<Car> cars)
        {
            Console.WriteLine("Cars available for rent");

            int count = 0;
            foreach (Car car in cars)
            {
                if (car.IsAvailable && !car.MaintenanceDue)
                {
                    count++;
                    Console.WriteLine(car.GetShortInfo());
                }
            }

            if (count == 0)
            {
                Console.WriteLine("No car available now");
            }
            else
            {
                Console.WriteLine($"\n  Total cars Available: {count}");
            }
            Console.WriteLine();
        }

        // Helper Method 3: Calculate totalRevenue 
        static void CalculateTotalRevenue(List<Car> cars)
        {
            double TotalRevenue = 0;

            foreach (Car car in cars)
            {
                TotalRevenue += car.TotalRevenue;
            }

            Console.WriteLine($"Total revenue: {TotalRevenue:N2} taka");
        }

        // Helper Method 4: Find Most Profitable Car
        static void FindMostProfitableCar(List<Car> cars)
        {
            Car mostProfitable = cars[0];

            foreach (Car car in cars)
            {
                if (car.TotalRevenue > mostProfitable.TotalRevenue)
                {
                    mostProfitable = car;
                }
            }

            Console.WriteLine("\nMost Profitable car:");
            Console.WriteLine(mostProfitable.GetShortInfo());
            Console.WriteLine($"Total revenue: {mostProfitable.TotalRevenue:N2} taka");
        }

        // Helper Method 5: Find Most Rented Car
        static void FindMostRentedCar(List<Car> cars)
        {
            if (cars == null || cars.Count == 0) return;
            Car mostRented = cars[0];
            

            foreach (Car car in cars)
            {
                
               if (car.TotalRentalDays > mostRented.TotalRentalDays)
                {
                    mostRented = car;
                }
            }

            Console.WriteLine("\nMost Popular Car:");
            Console.WriteLine(mostRented.GetShortInfo());
        }

        // Helper Method 6:Finding car with its ID 
        static Car FindCarById(List<Car> cars, string carId)
        {
            foreach (Car car in cars)
            {
                if (car.CarId == carId)
                {
                    return car;
                }
            }

            Console.WriteLine($" {carId} ID not found");
            return null;
        }
    }
}
