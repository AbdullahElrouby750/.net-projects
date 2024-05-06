namespace hospital_classes;

public class Patient : Person{
    public int Weight { get; set; }
    public int Height { get; set; }
    public string CurrentProblem { get; set; }
    public Dictionary<string, string> MedicalHistory { get; set; }
    public string Disabilities { get; set; }
    public Dictionary<string, string> EmergencyContact { get; set; }
    public bool Operation { get; set; }
    public Patient()
    {
        // MedicalHistory = new Dictionary<string, string>();
        Dictionary<string, string> dictionarymedicalHisory = new Dictionary<string, string>();
        EmergencyContact = new Dictionary<string, string>();
    }
    public void SetMedicalHistory(string disease, string info)
    {
         if (MedicalHistory == null)
             MedicalHistory = new Dictionary<string, string>();
             MedicalHistory[disease] = info;
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

    public void PrintDoctorReport()
    {
        Console.WriteLine("DoctorReport:");
    }
    public void PrintNurseReport()
    {
        Console.WriteLine("NurseRepoet:");
    }
    public void PrintPharmaReport()
    {
        Console.WriteLine("PharmaReport:");
    }
    public void PrintRadiologistReport()
    {
        Console.WriteLine("RadiologisReport:");
    }
    public bool NeedOperation()
    {
        return Operation;
    }

}