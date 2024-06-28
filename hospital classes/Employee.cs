namespace hospital_classes;

public class Employee : Person
{

    public double Salary { get; set; }
    public double Bouns { get; set; }
   //public List<Dictionary<DateOnly, int>> DailyWorkHours { get; set; }
   public TimeSpan? WorkHours { get; set; }
   public DateTime DailyLoginTime { get; set; }
   public DateTime DailyLogoutTime { get; set; }

    public readonly DateOnly? StartingDate;
    public string HRreport { get; set; }
    public int Experience { get; set; }
    public Dictionary<string, string>? PreviousExperience { get; set; }
    public string HospitalID { get; set; }
    public bool SalaryReceived { get; set; }
    protected string BankAccount { get; set; }
    protected string AccountNumber { get; set; }
    static protected int NumberOfEmployees { get; set; }
    public int Warnings { get; set; } // public so it can be accessable from hr.writeReports
    public string Department { get; set; }
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