

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
    public float Bill { get; set; }

    public Patient()
    {
        Weight = 0;
        Height = 0;
        CurrentProblem = null;
        MedicalHistory = new Dictionary<string, string>();
        Disabilities = null;
        EmergencyContact = new Dictionary<string, string>();
        Operation = false;
        NumberOfPatients = 0;
        Bill = 0;
    }

    public Patient(string firstName, string lastName, string phoneNumber, int age, DateOnly dateOfBirth, string gender, string statue, string address, string bloodType,
                    int weight, int height, string currentProblem, string disabilities, int patientid, bool operation, int numberOfPatients, float bill)
                    : base(firstName, lastName, phoneNumber, age, dateOfBirth, gender, statue, address, bloodType)
    {
        Weight = weight;
        Height = height;
        CurrentProblem = currentProblem;
        MedicalHistory = new Dictionary<string, string>();
        Disabilities = disabilities;
        EmergencyContact = new Dictionary<string, string>();
        PatientID = patientid;
        Operation = operation;
        NumberOfPatients = numberOfPatients;
        Bill = bill;
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

    public void PrintNurseReport()
    {
        Console.WriteLine("Nurse Report:");
    }

    public void PrintPharmaReport()
    {
        Console.WriteLine("Pharmacist Report:");
    }

    public void PrintRadiologistReport()
    {
        Console.WriteLine("Radiologist Report:");
    }

    public bool NeedOperation()
    {
        return Operation;
    }
}
