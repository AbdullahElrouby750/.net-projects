﻿namespace hospital_classes;

public class Person{
   private string? Firstname { get; set; }
   private string? Lastname { get; set; }
   public string ? FullName 
   {
        get{
        return Firstname + " " + Lastname;
        } 
   }
   public string? PhoneNumber { get; set; }
   public int Age { get; set; }
   public DateOnly DateOfBirth { get; set; }
   public string? Gender { get; set; }
   public string? Statue { get; set; }
   public string? Address { get; set; }
   public string? BloodType { get; set; }
    
   //defult constractor 
   public Person()
   {
    Firstname = "First name";
    Lastname = "Last name";
    PhoneNumber = "xxxxxxxxxxx";
    Age = 0;
    DateOfBirth = DateOnly.FromDateTime(DateTime.Today);
    Gender = "Unknown";
    Statue = "Unknown";
    Address = "Unknown";
    BloodType = "Unknown";
   }

   public Person(string firstname, string lastname, string phonenumber, int age, DateOnly dateofbirth, string gender, string statue, string address, string bloodtype)
   {
    Firstname = firstname;
    Lastname = lastname;
    PhoneNumber = phonenumber;
    Age = age;
    DateOfBirth = dateofbirth;
    Gender = gender;
    Statue = statue;
    Address = address;
    BloodType = bloodtype;
   }
}