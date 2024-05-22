namespace hospital_classes;

public class Employee : Person
{
    
    protected double Salary { get; set; }
    protected double Bouns { get; set; }
    public List<Dictionary<DateOnly, int>> DailyWorkHours { get; set; }
    public TimeSpan WorkHours { get; set; }
    protected DateTime ?DailyLoginTime { get; set; }
    protected DateTime ?DailyLogoutTime { get; set; }
    protected readonly DateOnly StartingDate; 
    protected string HRreport { get; set; }
    public int Experience { get; set; }
    public Dictionary<string, string> PreviousExperience { get; set; }
    protected string HospitalID { get; set; }
    protected string hospitalAccount { get; set; }
    protected bool SalaryReceived { get; set; }
    protected string BankAccount { get; set; }
    protected string AccountNumber { get; set; }
    static protected int NumberOfEmployees { get; set; }
    protected int Warnings { get; set; }
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
    }
    public Employee(Dictionary<string, dynamic> total, double raisesAndBouns = 0.0, bool salaryReceived = false)
     : base (firstname: total["firstname"], lastname: total["lastname"], phonenumber: total["phonenumber"], age: total["age"], dateofbirth: total["dateofbirth"], gender: total["gender"], statue: total["statue"], address: total["address"], bloodtype: total["bloodtype"])
    {
        Bouns = total["Bouns"];
        SalaryReceived=false;
        Salary = total["salary"];
        Experience = total["Experience"];
        HospitalID = total["HospitalID"];
        BankAccount = total["BankAccount"];
        AccountNumber = total["AccountNumber"];
        StartingDate = total["StartingDate"];
        PreviousExperience = total["PreviousExperience "];
        DailyWorkHours = total["DailyWorkHours"];
    }
}