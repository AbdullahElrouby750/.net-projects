namespace hospital_classes;

public class Patient : Person
{
    public int Weight { get; set; }
    public int Height { get; set; }
    public string CurrentProblem { get; set; }
    public Dictionary<string, string> MedicalHistory { get; set; }
    public string Disabilities { get; set; }
    public Dictionary<string, string> EmergencyContact { get; set; }
    public int PatientID { get; set; }
    public bool Operation { get; set; }
    public static int NumberOfPatients { get; set; }
    private float Bill { get; set; }
    public string DoctorReport { get; set; }
    public string PharmacistReport { get; set; }
    public string NurseReport { get; set; }
    public string RadiologistReport { get; set; }

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
        DoctorReport = "";
        PharmacistReport = "";
        NurseReport = "";
        RadiologistReport = "";
        PatientID = NumberOfPatients;
        NumberOfPatients++;



    }

    public Patient(Dictionary<string, dynamic> patientInfo)
        : base(patientInfo["firstName"], patientInfo["lastName"], patientInfo["phoneNumber"], patientInfo["age"], patientInfo["dateOfBirth"], patientInfo["gender"], patientInfo["status"], patientInfo["address"], patientInfo["bloodType"])
    {
        Weight = patientInfo["weight"];
        Height = patientInfo["height"];
        CurrentProblem = patientInfo["currentProblem"];
        Operation = false;
        Bill = 0;
        PatientID = NumberOfPatients;
        NumberOfPatients++;


    }

    public void SetMedicalHistory(string disease, string info)
    {
        if (MedicalHistory == null)
        {
            MedicalHistory = new Dictionary<string, string>();
        }
        MedicalHistory[disease] = info;
    }

    public void PrintMedicalHistory()
    {
        Console.WriteLine("Medical History:");
        if (MedicalHistory != null)
        {
            foreach (var entry in MedicalHistory)
            {
                Console.WriteLine($"{entry.Key}: {entry.Value}");
            }
        }
    }

    public void SetEmergencyContact(string fields, string info)
    {
        if (EmergencyContact == null)
        {
            EmergencyContact = new Dictionary<string, string>();
        }
        EmergencyContact[fields] = info;
    }

    public void PrintEmergencyContact()
    {
        Console.WriteLine("Emergency Contact:");
        if (EmergencyContact != null)
        {
            foreach (var entry in EmergencyContact)
            {
                Console.WriteLine($"{entry.Key}: {entry.Value}");
            }
        }
    }

    public void PrintReports()
    {
        Console.WriteLine($"Doctorreport: {DoctorReport}");
        Console.WriteLine($"Pharmacistreport: {PharmacistReport}");
        Console.WriteLine($"Nursereport: {NurseReport}");
        Console.WriteLine($"Radiologistreport: {RadiologistReport}");
    }

    public bool NeedOperation()
    {
        return Operation;
    }

    public void PrintBill()
    {
        Console.WriteLine($"Bill: {Bill}");
    }
}
