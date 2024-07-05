namespace hospital_classes;

public class Patient : Person
{
    public double Weight { get; set; }
    public double Height { get; set; }
    public string CurrentProblem { get; set; }
    public Dictionary<string, string> MedicalHistory { get; set; }
    public string Disabilities { get; set; }
    public Dictionary<string, string> EmergencyContact { get; set; }
    public string PatientID { get; set; }
    public bool Operation { get; set; }
    public bool MedicalXray { get; set; }
    private double Bill { get; set; }
    public string DoctorReport { get; set; }
    public string PharmacistReport { get; set; }
    public string NurseReport { get; set; }
    public string RadiologistReport { get; set; }

    public bool AllVisitsDone;

    public string Recet;

    public Dictionary<int, bool> Visits;


    public Patient()
    {
        Weight = 0;
        Height = 0;
        CurrentProblem = null;
        MedicalHistory = new Dictionary<string, string>();
        Disabilities = null;
        EmergencyContact = new Dictionary<string, string>();
        Operation = false;
        Bill = 0;
        MedicalXray = false;
        DoctorReport = "";
        PharmacistReport = "";
        NurseReport = "";
        RadiologistReport = "";
        PatientID = "";
        AllVisitsDone = true;
        Recet = string.Empty;
        Visits = new Dictionary<int, bool>();

    }

    public Patient(Dictionary<string, dynamic> patientInfo)
        : base(patientInfo["FullName"], patientInfo["PhoneNumber"], (int)patientInfo["Age"], patientInfo["DateOfBirth"], patientInfo["Gender"], patientInfo["Statue"], patientInfo["Address"], patientInfo["BloodType"])
    {
        //add the needed members
        Weight = patientInfo["Weight"];
        Height = patientInfo["Height"];
        CurrentProblem = patientInfo["CurrentProblem"];
        Disabilities = patientInfo["Disabilities"];
        Operation = patientInfo["Operation"];
        Bill = patientInfo["Bill"];
        MedicalXray = patientInfo["MedicalXray"];
        PatientID = patientInfo["HospitalID"];
        AllVisitsDone = patientInfo["AllVisitsDone"];
        Recet = patientInfo["Recet"];
        Visits = patientInfo["Visits"];
    }
    public void PrintMedicalHistory()
    {
        Console.WriteLine("Medical History:");
        if (MedicalHistory != null)
        {
            foreach (var entry in MedicalHistory)
            {
                Console.WriteLine($"{entry.Key}\t\t:\t\t{entry.Value}");
            }
        }
        else Console.WriteLine("None");
    }
    public void PrintEmergencyContact()
    {
        Console.WriteLine("Emergency Contact:");
        if (EmergencyContact != null)
        {
            foreach (var entry in EmergencyContact)
            {
                Console.WriteLine($"{entry.Key}\t\t:\t\t{entry.Value}");
            }
        }
    }

    public void PrintReports()
    {
        Console.WriteLine("Medical hestory :-");
        PrintMedicalHistory();
        Console.WriteLine($"\nDoctor report: {DoctorReport}");
        Console.WriteLine($"\nPharmacist report: {PharmacistReport}");
        Console.WriteLine($"\nNurse report: {NurseReport}");
        Console.WriteLine($"\nRadiologist report: {RadiologistReport}");
        Console.WriteLine($"\nBill: {Recet}");
    }

    public bool NeedOperation()
    {
        return Operation;
    }

    public bool NeedsMedicalXray()
    {
        return MedicalXray;
    }

    public void PrintBill()
    {
        Console.WriteLine($"Bill: {Recet}");
    }
    public double getBill()
    {
       return Bill;
    }

    public void setBill(double billValue)
    {
        Bill = billValue; 
    }
}
