using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.IO;

public class Program
{
    public static void Main()
    {

        // Step 1: Create an Employee List
        var employees = GenerateEmployees(100);

        // Step 2: Operations

        // Find Max and Min Salary for Male Employees
        var maleEmployees = employees.Where(e => e.Gender == Gender.Male);
        var maxSalaryEmployee = maleEmployees.OrderByDescending(e => e.Salary).FirstOrDefault();
        var minSalaryEmployee = maleEmployees.OrderBy(e => e.Salary).FirstOrDefault();

        Console.WriteLine($"Max Salary Male Employee: Id = {maxSalaryEmployee?.Id}, Name = {maxSalaryEmployee?.FirstName} {maxSalaryEmployee?.LastName}, DOB = {maxSalaryEmployee?.DOB}");
        Console.WriteLine($"Min Salary Male Employee: Id = {minSalaryEmployee?.Id}, Name = {minSalaryEmployee?.FirstName} {minSalaryEmployee?.LastName}, DOB = {minSalaryEmployee?.DOB}");

        // Create Simplified List
        var simplifiedList = employees.Select(e => new
        {
            ID = e.Id,
            FullName = $"{e.FirstName} {e.LastName}",
            e.Gender
        }).ToList();

        // Filter Female Employees
        var femaleEmployees = employees.Where(e => e.Gender == Gender.Female).ToList();

        // Sort Employees by Gender and Salary
        var sortedEmployees = employees.OrderBy(e => e.Gender).ThenBy(e => e.Salary).ToList();

        // Export Employee Data to JSON
        ExportToJson(employees, "employees.json");

        Console.WriteLine("Employee data exported to employees.json");

        // Prevent console window from closing
        Console.WriteLine("Press any key to exit...");
        Console.ReadLine();
    }

    // Method to Generate Employee Data
    private static List<Emp> GenerateEmployees(int count)
    {
        var random = new Random();
        var employees = new List<Emp>();

        for (int i = 1; i <= count; i++)
        {
            employees.Add(new Emp
            {
                Id = i,
                FirstName = $"FirstName{i}",
                LastName = $"LastName{i}",
                Department = (Dept)random.Next(0, 6),
                Gender = (Gender)random.Next(0, 2),
                Country = (Country)random.Next(0, 6),
                Salary = random.Next(30000, 150000),
                DOB = new DateTime(random.Next(1970, 2000), random.Next(1, 13), random.Next(1, 28))
            });
        }

        return employees;
    }

    // Method to Export Data to JSON
    private static void ExportToJson(List<Emp> employees, string fileName)
    {
        var json = JsonSerializer.Serialize(employees, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText(fileName, json);
    }
}

// Employee Class and Enums
public class Emp
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public Dept Department { get; set; }
    public Gender Gender { get; set; }
    public Country Country { get; set; }
    public int Salary { get; set; }
    public DateTime DOB { get; set; }
}

public enum Dept { IT, HR, Payroll, Engineering, Admin, Sales }
public enum Gender { Male, Female }
public enum Country { USA, India, Japan, UK, Germany, Australia }
