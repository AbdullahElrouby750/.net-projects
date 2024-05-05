namespace hospital_classes;

public class Person{
    public string? Firstname { get; set; }
   public string? Lastname { get; set; }
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

}

