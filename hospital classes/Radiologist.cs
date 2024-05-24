namespace hospital_classes;

public class Radiologist : Employee, WritingReports
{
    public static int numberOfRadiologist = 0;
    public string specialization;



    public Radiologist(Dictionary<string, dynamic> data)
    : base(data)
    {

        specialization = specialization;
        numberOfRadiologist++;

    }



    public Radiologist()
    {
        specialization = "Unknown";
        numberOfRadiologist++;
    }

    public Patient SearchPatientRecord(int patientID)
    {

        if (Receptionist.patientData.ContainsKey(patientID))
        {
            return Receptionist.patientData[patientID];
        }
        else
        {
            Console.WriteLine($"Patient with ID {patientID} not found.");
            return null;
        }
    }


    public void WriteReport()
    {
        Console.Write($"Enter Patient ID : ");
        int patientID = int.Parse(Console.ReadLine());

        Patient patient = SearchPatientRecord(patientID);
        if (patient != null)
        {

            string report = Console.ReadLine();

            patient.RadiologistReport = $"Patient Name: {patient.FullName}\n";
            patient.RadiologistReport += $"PatientID: {patient.PatientID}\n";
            patient.RadiologistReport += $"Radiologist Report: {patient.RadiologistReport}\n";



            Console.WriteLine("Does the patient require needx-rays? (yes/no):");
            string x_raysInput = Console.ReadLine().ToLower();

            if (x_raysInput == "yes")
            {
                patient.MedicalXray = true;
            }
            else
            {
                patient.MedicalXray= false;
            }



            Console.WriteLine(report);
        }
    }


    public void PrintHRreport()
    {
        Console.WriteLine(HRreport);
    }
    public void Printsalary()
    {
        if (SalaryReceived == true)
        {
            int Salary = 0;
            int Bouns = 0;
            double salaryAfterBouns = Salary + Bouns;
            Console.WriteLine($"Salary received successfully: {salaryAfterBouns:c} ");
            Console.WriteLine($"Your main salary: {Salary}");
            Console.WriteLine($"Your bouns: {Bouns}");
            SalaryReceived = false;
        }
        else
        {
            Console.WriteLine($"salary not sent yet :(");
        }
    }
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
