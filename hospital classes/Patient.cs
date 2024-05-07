namespace hospital_classes;

public class Patient : Person {
    public int Weight { get; set; }
    public int Height { get; set; }
    public string CurrentProblem { get; set; }
    public Dictionary<string, string> MedicalHistory { get; set; }
    public string Disabilities { get; set; }
    public Dictionary<string, string> EmergencyContact { get; set; }
    public int PatientID { get; set; }
    public bool Operation { get; set; }
    public Patient()
     {
         Weight = 0;
         Height = 0;
         CurrentProblem = null; 
         MedicalHistory = null;
         Disabilities = null;  
         EmergencyContact = null; 
         Operation = false; 
     }

    public Patient(string firstName, string lastName, string phoneNumber, int age, DateOnly dateOfBirth, string gender, string statue, string address, string bloodType
        ,int weight, int height, string currentProblem, string disabilities, int patientid, bool operation )
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
    }
    public void SetMedicalHistory(string disease, string info)
    {
        if (MedicalHistory == null)
        {
            MedicalHistory = new Dictionary<string, string>();
            MedicalHistory[disease] = info;
        }
    }
public void PrintMedicalHistory()
{
    Console.WriteLine("MedicalHistory:");
    if (MedicalHistory != null) 
    {
        foreach (var entry in MedicalHistory)
        {
            Console.WriteLine($"{entry.Key}: {entry.Value}");
        }
    }
}
public void SetEmergencyContact(string fields, string info){
    if (EmergencyContact == null){
    EmergencyContact = new Dictionary<string, string>();
    EmergencyContact[fields] = info;
    }
}
public void PrintEmergencyContact()
{
    Console.WriteLine("EmergencyContact:");
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
        Console.WriteLine("NurseReport:");
    }
    public void PrintPharmaReport()
    {
        Console.WriteLine("PharmacistReport:");
    }
    public void PrintRadiologistReport()
    {
        Console.WriteLine("RadiologistReport:");
    }
    public bool NeedOperation()
    {
        return Operation;
    }


}