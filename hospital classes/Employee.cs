namespace hospital_classes;

public class Employee : Person
{
    protected double Salary {get; set; }
    protected double RaisesAndBouns {get; set; }
    public int WorkHours { get; set; }
    public string[] WorkDayes { get; set; }
    public int Experience { get; set; }
    public Dictionary<string , string> PreviousExperience { get; set; }
    protected string HospitalID { get; set; }
    protected bool SalaryReceived { get; set; }
    protected string BankAccount { get; set; }
    protected string AccountNumber { get; set; }
    static protected int NumberOfEmployees { get; set; }

    //default constructor
    public Employee()
    {
        Salary = 0.0;
        RaisesAndBouns = 0.0;
        WorkHours = 0;
        WorkDayes = new string[7];
        Experience = 0;
        PreviousExperience = new Dictionary<string, string>();
        HospitalID = "xxxxxxxx";
        SalaryReceived = false;
        BankAccount = "Bank name";
        AccountNumber = "0000 0000 0000 0000";
        NumberOfEmployees++;
    }


}
