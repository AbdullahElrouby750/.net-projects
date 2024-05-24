using hospital_classes;

public class Doctor : Employee , WritingReports
{
    public static int numberOfDoctors = 0;
    public string specialization;


    public Doctor(Dictionary<string, dynamic> data)
        : base(data)
    {
        specialization = data["specialization"];
        numberOfDoctors++;
    }

    public Doctor()
    {
        this.specialization = "Unknown";
        numberOfDoctors++;
    }



    public Patient SearchpatientData(int patientID)
    {
        if (Receptionist.patientData.ContainsKey(patientID))
        {
            return Receptionist.patientData[patientID];
        }
        else
        {
            Console.WriteLine($"Patient with ID {patientID} not found in the data.");
            return null;
        }
    }





    public void WriteReport()
    {

        Console.Write($"Enter Patient ID : ");
        int patientID = Convert.ToInt32(Console.ReadLine());

        Patient patient = SearchpatientData(patientID);
        if (patient != null)
        {

            string report = Console.ReadLine();

            patient.DoctorReport = $"Patient Name: {patient.FullName}\n";
            patient.DoctorReport += $"PatientID: {patient.PatientID}\n";
            patient.DoctorReport += $"Doctor Report: {patient.DoctorReport}\n";
            patient.DoctorReport += $"Medical History: {patient.PrintMedicalHistory()}\n";

            Console.WriteLine("Does the patient require Operation? (yes/no):");
            string OperationInput = Console.ReadLine().ToLower();

            if (OperationInput == "yes")
            {
                patient.Operation = true;
            }
            else
            {
                patient.Operation = false;
            }
            patient.DoctorReport += $"Doctor: {FullName}\n";
            patient.DoctorReport += $"Date: {DateTime.Now.ToShortDateString()} {DateTime.Now.ToShortTimeString()}\n";

        }
    }



    public void PrintHRreport()
    {
        Console.WriteLine(HRreport);

    }
    public void Printsalary() { }

    public void login()
    {
        DailyLoginTime = DateTime.Now;

    }


    public void logout()
    {

        TimeSpan? hoursWorkedToday = DateTime.Now - DailyLoginTime;
        if (hoursWorkedToday < WorkHours)
        {
            Console.WriteLine($"Warning! your logging out {WorkHours - hoursWorkedToday} hours earlier");
            Console.WriteLine("Are you sure you want to log out? y/n");
            while (true)
            {
                string answer = Console.ReadLine();
                if (answer == "y")
                {
                    DailyLogoutTime = DateTime.Now;
                    break;
                }
                else if (answer == "n")
                {
                    break;
                }
                else
                {
                    Console.WriteLine("please enter y or n");
                }
            }
        }
    }
    
}