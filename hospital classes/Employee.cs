namespace hospital_classes;

public class Employee : Person
{
    
    public double Salary { get; set; }
    protected double Bouns { get; set; }
    public List<Dictionary<DateOnly, int>> DailyWorkHours { get; set; }
    public TimeSpan WorkHours { get; set; }
    public DateTime ?DailyLoginTime { get; set; }
    public DateTime ?DailyLogoutTime { get; set; }
    public readonly DateOnly StartingDate; 
    public string HRreport { get; set; }
    public int Experience { get; set; }
    public Dictionary<string, string> PreviousExperience { get; set; }
    public string HospitalID { get; set; }
    protected string hospitalAccount { get; set; }
    protected bool SalaryReceived { get; set; }
    protected string BankAccount { get; set; }
    protected string AccountNumber { get; set; }
    static protected int NumberOfEmployees { get; set; }
    protected int Warnings { get; set; }
    public string Department { get; set; }
    static protected int NumberOfEmployee { get; set; }

    //default constructor
    public Employee() 
    {
        Salary = 0.0;
        Bouns=0.0; 
        DailyWorkHours = new List< Dictionary<DateOnly,int>>();
        Experience = 0;
        PreviousExperience = new Dictionary<string, string>();
        HospitalID = "xxxxxxxx";
        SalaryReceived = false;
        BankAccount = "Bank name";
        AccountNumber = "0000 0000 0000 0000";
        NumberOfEmployees++;
        Warnings = 0;
        HRreport = "xxxxxxx";
        hospitalAccount = "  ";
        NumberOfEmployee = 0;
        DailyLoginTime =null;
        DailyLogoutTime = null;
        Department = "";
    }
    public Employee(Dictionary<string, dynamic> total, double raisesAndBouns = 0.0, bool salaryReceived = false)
     : base (firstname: total["FirstName"], lastname: total["LastName"], phonenumber: total["PhoneNumber"], age: total["Age"], dateofbirth: total["DateOfBirth"], gender: total["Gender"], statue: total["Statue"], address: total["Address"], bloodtype: total["BloodType"])
    {
        SalaryReceived=false;
        Salary = total["Salary"];
        Experience = total["Experience"];
        HospitalID = total["HospitalID"];
        BankAccount = total["BankAccount"];
        AccountNumber = total["AccountNumber"];
        StartingDate = total["StartDate"];
        PreviousExperience = total["PreviousExperience"];
        WorkHours = total["WorkHours"];
        Department = total["Department"];
    }
}