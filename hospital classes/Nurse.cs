namespace hospital_classes;
public class Nurse : Employee ,WritingReports
{
    private Dictionary<int, Dictionary<int, bool>> Visits;
    private bool AllVisitsDone;
    public static int NumberOfNurses;
    private int numberOfVisits;

    public Nurse()
    {

        Visits = new Dictionary<int, Dictionary<int, bool>>();
        AllVisitsDone = false;
        NumberOfNurses++;

    }

    public Nurse(Dictionary<string, dynamic> data) : base(data) 
    {

        Visits = new Dictionary<int, Dictionary<int, bool>>();
        AllVisitsDone = false;
        NumberOfNurses++;

    }


    public void NumberOfVisits(int ID)
    {
        Console.WriteLine("Enter number of visits for patient ID " + ID + ":");
        numberOfVisits = int.Parse(Console.ReadLine());

        for (int i = 0; i < numberOfVisits; i++)
        {
            Visits[ID][i + 1] = false;
        }
    }



    public void AddVisit(int patientID, int VisitNumber, bool VisitDone)
    {
        if(!Receptionist.patientData.ContainsKey(patientID))
        {
            Console.WriteLine($"Patient with ID {patientID} not found in receptionist's data.");
            return;
        }
        if (!Visits.ContainsKey(patientID))
        {
            NumberOfVisits(patientID);
        }

        Visits[patientID][VisitNumber] = VisitDone;

    }



    public bool NeedVisits()
    {
        foreach (var patientVisits in Visits)
        {
            foreach (var visit in patientVisits.Value)
            {
                if (!visit.Value)
                {
                    AllVisitsDone = false;
                    return true;
                }
            }
        }
        AllVisitsDone = true;
        return false;
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
        DateTime DailyLoginTime = DateTime.Now;
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
        int patientID = int.Parse(Console.ReadLine());

        Patient patient = SearchpatientData(patientID);
        if (patient != null)
        {

            string report = Console.ReadLine();

            patient.NurseReport = $"Patient Name: {patient.FullName}\n";
            patient.NurseReport += $"PatientID: {patient.PatientID}\n";
            patient.NurseReport += $"Doctor Report: {patient.DoctorReport}\n";

            patient.NurseReport += $"Nurse: {FullName}\n";
            patient.NurseReport += $"Date: {DateTime.Now.ToShortDateString()} {DateTime.Now.ToShortTimeString()}\n";

        }
    }
}