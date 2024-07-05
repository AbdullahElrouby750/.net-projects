namespace hospital_classes
{
    public class Person{
       //private string? Firstname { get; set; }
       //private string? Lastname { get; set; }
       internal string ? FullName { get; set; }

       internal string? PhoneNumber { get; set; }
       internal int Age { get; set; }
       internal DateOnly DateOfBirth { get; set; }
       internal string? Gender { get; set; }
       internal string? Statue { get; set; }
       internal string? Address { get; set; }
       internal string? BloodType { get; set; }
    
       //defult constractor 
       public Person()
       {
            //Firstname = "First name";
            //Lastname = "Last name";
        FullName = "";
        PhoneNumber = "xxxxxxxxxxx";
        Age = 0;
        DateOfBirth = DateOnly.FromDateTime(DateTime.Today);
        Gender = "Unknown";
        Statue = "Unknown";
        Address = "Unknown";
        BloodType = "Unknown";
       }

       public Person(string fullname, string phonenumber, int age, DateOnly dateofbirth, string gender, string statue, string address, string bloodtype)
       {
        //Firstname = firstname;
        //Lastname = lastname;
        FullName = fullname;
        PhoneNumber = phonenumber;
        Age = age;
        DateOfBirth = dateofbirth;
        Gender = gender;
        Statue = statue;
        Address = address;
        BloodType = bloodtype;
       }
    }
}