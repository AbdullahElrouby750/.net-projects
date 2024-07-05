namespace hospital_classes;

public class Employee : Person
{

    internal double Salary { get; set; }
    internal double Bouns { get; set; }
   //internal List<Dictionary<DateOnly, int>> DailyWorkHours { get; set; }
   internal TimeSpan? WorkHours { get; set; }
   internal DateTime DailyLoginTime { get; set; }
   internal DateTime DailyLogoutTime { get; set; }

    internal readonly DateOnly? StartingDate;
    internal string HRreport { get; set; }
    internal int Experience { get; set; }
    internal Dictionary<string, string>? PreviousExperience { get; set; }
    internal string HospitalID { get; set; }
    internal bool SalaryReceived { get; set; }
    protected string BankAccount { get; set; }
    protected string AccountNumber { get; set; }
    static protected int NumberOfEmployees { get; set; }
    internal int Warnings { get; set; } // internal so it can be accessable from hr.writeReports
    internal string Department { get; set; }
    static protected int NumberOfEmployee { get; set; }

    //default constructor
    public Employee()
    {
        Salary = 0.0;
        Bouns = 0.0;
        //DailyWorkHours = new List<Dictionary<DateOnly, int>>();
        Experience = 0;
        PreviousExperience = new Dictionary<string, string>();
        HospitalID = "xxxxxxxx";
        SalaryReceived = false;
        BankAccount = "Bank name";
        AccountNumber = "0000 0000 0000 0000";
        NumberOfEmployees++;
        Warnings = 0;
        HRreport = "";
        NumberOfEmployee = 0;
        DailyLoginTime = new DateTime();
        DailyLogoutTime = new DateTime();
        Department = "";
    }
    public Employee(Dictionary<string, dynamic> total, double raisesAndBouns = 0.0, bool salaryReceived = false)
     : base(fullname: total["FullName"], phonenumber: total["PhoneNumber"], age: (int)total["Age"], dateofbirth: total["DateOfBirth"], gender: total["Gender"], statue: total["Statue"], address: total["Address"], bloodtype: total["BloodType"])
    {
        SalaryReceived = salaryReceived;
        Bouns = raisesAndBouns;
        HRreport = "";
        Warnings = 0;
        Salary = total["Salary"];
        Experience = (int)total["Experience"];
        HospitalID = total["HospitalID"];
        BankAccount = total["BankAccount"];
        AccountNumber = total["AccountNumber"];
        StartingDate = total["StartingDate"];
        PreviousExperience = total["PreviousExperience"];
        WorkHours = total["WorkHours"];
        Department = total["Department"];
    }
}