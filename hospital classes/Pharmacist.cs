namespace hospital_classes;

public class Pharmacist : Employee , WritingReports
{

    public static int NumberOfPharmacists { get; set; }

    public Pharmacist()
    {
        NumberOfPharmacists++;
    }

    public Pharmacist(Dictionary<string,dynamic> data) : base(data)
    {
        NumberOfPharmacists++;
    }
    public void WriteReport(){}
    public void PrintHRreport(){}
    public void Printsalary(){}
    public void login(){}
    public void logout(){}
}
