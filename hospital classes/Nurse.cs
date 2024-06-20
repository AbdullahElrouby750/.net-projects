namespace hospital_classes;
public class Nurse : Employee, WritingReports
{
    private Dictionary<int, Dictionary<int, bool>> Visits;
    public static int NumberOfNurses;
    private int numberOfVisits;

    public Nurse()
    {

        Visits = new Dictionary<int, Dictionary<int, bool>>();
        NumberOfNurses++;

    }

    public Nurse(Dictionary<string, dynamic> data) : base(data)
    {

        Visits = new Dictionary<int, Dictionary<int, bool>>();
        NumberOfNurses++;

    }


    private void NumberOfVisits(int ID)
    {
        Console.WriteLine("Enter number of visits for patient ID " + ID + ":");
        numberOfVisits = int.Parse(Console.ReadLine()!);

        for (int i = 0; i < numberOfVisits; i++)
        {
            Visits[ID][i + 1] = false;
        }
    }



    public void AddVisit()
    {
        string name = string.Empty;
        int patientID;
        while (true)
        {
            Console.Write("Enter patient ID : ");
            patientID = int.Parse(Console.ReadLine()!);

            if (patientID == 0)
            {
                return;
            }

            if (Receptionist.SearchpatientData(patientID) is Patient p)
            {
                name = p.FullName!;
                break;
            }
            Console.WriteLine("Please enter a valid patient ID or enter 0 to Exit");
        }

        int visitTargrted = 1;

        if (!Visits.ContainsKey(patientID))
        {
            NumberOfVisits(patientID);
        }
        Dictionary<int, bool> visitnumber = Visits[patientID];

        Console.WriteLine("Visits\t\tstatue");
        foreach (var visit in visitnumber)
        {
            Console.WriteLine($"{visit.Key}\t\t{visit.Value}");
            if (visit.Value == true)
            {
                visitTargrted = visit.Key + 1;
            }
        }
        Console.WriteLine($"mark visit number{visitTargrted} as done? yes / no ");

        string answer = Console.ReadLine()!;
        if (answer == "yes")
        {
            Visits[patientID][visitTargrted] = true; // try catch
            Console.WriteLine($"Visit for {name} with ID {patientID} done successfully.");
        }
        NeedVisits(patientID);

    }



    private void NeedVisits(int id) // re code this with an id to do the check for
    {
        foreach (var patientVisits in Visits[id])
        {
            if (patientVisits.Value == false)
            {
                Receptionist.patientData[id].AllVisitsDone = false;
                break;
            }
        }
    }

    public bool NeedVisits()
    {
        int patientID;
        while (true)
        {
            Console.Write("Enter patient ID : ");
            patientID = int.Parse(Console.ReadLine()!);

            if (patientID == 0)
            {
                return false;
            }

            if (Receptionist.SearchpatientData(patientID) is Patient p)
            {
                return p.AllVisitsDone;
            }
            Console.WriteLine("Please enter a valid patient ID or enter 0 to Exit");
        }
    }

    public void PrintHRreport()
    {
        if (!string.IsNullOrWhiteSpace(HRreport))
        {
            Console.WriteLine(HRreport);
            HRreport = string.Empty;
        }
        else
        {
            Console.WriteLine("No repors yet for this month");
        }
    }


    public void Printsalary()
    {
        if (SalaryReceived == true)
        {
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
        Console.WriteLine($"You logged in at {DailyLoginTime} successfully");
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

    public void WriteReport()
    {
        while (true)
        {

            Console.Write($"Enter Patient ID : ");
            int patientID = int.Parse(Console.ReadLine());

            if (patientID == 0)
            {
                return;
            }

            if (Receptionist.SearchpatientData(patientID) is Patient patient)
            {
                Console.WriteLine($"Enter Nurse Report : ");
                string report = Console.ReadLine();

                patient.NurseReport = $"Patient Name: {patient.FullName}\n";
                patient.NurseReport += $"PatientID: {patient.PatientID}\n";
                patient.NurseReport += $"Nurse Report: {report}\n";

                patient.NurseReport += $"Nurse: {FullName}\n";
                patient.NurseReport += $"Date: {DateTime.Now.ToShortDateString()} {DateTime.Now.ToShortTimeString()}\n";

                return;
            }
            Console.WriteLine("Please enter a valid patient ID or enter 0 to Exit");
        }

    }
}