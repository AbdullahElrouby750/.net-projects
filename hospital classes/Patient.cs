namespace hospital_classes;

public class Patient : Person
{
    public double Weight { get; set; }
    public double Height { get; set; }
    public string CurrentProblem { get; set; }
    public Dictionary<string, string> MedicalHistory { get; set; }
    public string Disabilities { get; set; }
    public Dictionary<string, string> EmergencyContact { get; set; }
    public int PatientID { get; set; }
    public bool Operation { get; set; }
    public bool MedicalXray { get; set; }

    public static int NumberOfPatients { get; set; }
    private double Bill { get; set; }
    public string DoctorReport { get; set; }
    public string PharmacistReport { get; set; }
    public string NurseReport { get; set; }
    public string RadiologistReport { get; set; }

    public bool AllVisitsDone;


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
        NumberOfPatients++;
        PatientID = NumberOfPatients;
        AllVisitsDone = true;

    }

    public Patient(Dictionary<string, dynamic> patientInfo)
        : base(patientInfo["FullName"], patientInfo["PhoneNumber"], patientInfo["Age"], patientInfo["DateOfBirth"], patientInfo["Gender"], patientInfo["Statue"], patientInfo["Address"], patientInfo["BloodType"])
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

    }
    public string PrintMedicalHistory()
    {
        Console.WriteLine("Medical History:");
        if (MedicalHistory != null)
        {
            foreach (var entry in MedicalHistory)
            {
                Console.WriteLine($"{entry.Key}: {entry.Value}");
            }
        }
        return MedicalHistory!.ToString()!;
    }
    public string PrintEmergencyContact()
    {
        Console.WriteLine("Emergency Contact:");
        if (EmergencyContact != null)
        {
            foreach (var entry in EmergencyContact)
            {
                Console.WriteLine($"{entry.Key}: {entry.Value}");
            }
        }
        return EmergencyContact!.ToString()!;
    }

    public void PrintReports()
    {
        Console.WriteLine($"Doctor report: {DoctorReport}");
        Console.WriteLine($"Pharmacist report: {PharmacistReport}");
        Console.WriteLine($"Nurse report: {NurseReport}");
        Console.WriteLine($"Radiologist report: {RadiologistReport}");
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
        Console.WriteLine($"Bill: {Bill}");
    }
}
