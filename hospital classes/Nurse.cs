using hospitalData;

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

    //*********************************************************************print hr report****************************************************************

    public void PrintHRreport()
    {
        Console.WriteLine("\n1. Latest report");
        Console.WriteLine("2. Date's report");

        while (true)
        {
            Console.Write("Choose : ");
            int choice = int.Parse(Console.ReadLine()!);
            if (choice == 1)
            {
                HRreport = (EmployeeData.accessEmployeeExcelFile(HospitalID, Department, "HRreport"));

                if (HRreport == string.Empty)
                {
                    Console.WriteLine("Today's report not done yet");
                    return;
                }
                Console.WriteLine();
                Console.WriteLine(HRreport);
                return;
            }
            else if (choice == 2)
            {
                Console.WriteLine();
                Console.WriteLine(HR.GetHrReport(HospitalID));
                return;
            }
            else
            {
                Console.WriteLine();
                Console.WriteLine("Plz enter a valid Choice!");
            }
        }

    }

    //*********************************************************************print salary****************************************************************
    public void Printsalary()
    {
        SalaryReceived = bool.Parse(EmployeeData.accessEmployeeExcelFile(HospitalID, Department, "SalaryReceived"));
        Salary = double.Parse(EmployeeData.accessEmployeeExcelFile(HospitalID, Department, "Salary"));
        Bouns = double.Parse(EmployeeData.accessEmployeeExcelFile(HospitalID, Department, "Bouns"));

        if (SalaryReceived == true)
        {
            double salaryAfterBouns = Salary + Bouns;
            Console.WriteLine($"Salary received successfully: {salaryAfterBouns:c} ");
            Console.WriteLine($"Your main salary: {Salary}");
            Console.WriteLine($"Your bouns: {Bouns}");
            SalaryReceived = false;

            EmployeeData.accessEmployeeExcelFile(HospitalID, Department, "SalaryReceived", false);
        }
        else
        {
            Console.WriteLine("salary not sent yet :(");
            Console.WriteLine($"Your salary with bouns and diduction for so far : {Salary:c}");
        }
    }

    //*********************************************************************login****************************************************************
    public void login()
    {
        if (DailyLoginTime.Date == DateTime.Now.Date)
        {
            Console.WriteLine($"You have already logged-in today at {DailyLoginTime}");
            return;
        }
        DailyLoginTime = DateTime.Now;
        Console.WriteLine($"You logged in at {DailyLoginTime} successfully");
        EmployeeData.accessEmployeeExcelFile(HospitalID, Department, "DailyLoginTime", DailyLoginTime);
    }


    //*********************************************************************logout****************************************************************
    public void logout()
    {
        if (DailyLoginTime.Day != DateTime.Now.Day)
        {
            Console.WriteLine("Warning!!! You did not log-in for today!");
            Console.WriteLine("Can't log-out");
            return;
        }

        if (DailyLogoutTime.Date == DateTime.Now.Date)
        {
            Console.WriteLine($"You have already logged-in today at {DailyLogoutTime}");
            return;
        }

        int workedHours = DateTime.Now.Hour - DailyLoginTime.Hour;
        TimeSpan? hoursWorkedToday = new TimeSpan(workedHours, 0, 0);
        if (hoursWorkedToday < WorkHours)
        {
            Console.WriteLine($"Warning! your logging out {WorkHours - hoursWorkedToday} hours earlier");
            Console.WriteLine("Are you sure you want to log out? y/n");
            while (true)
            {
                string answer = Console.ReadLine()!.ToLower();
                if (answer == "y")
                {
                    DailyLogoutTime = DateTime.Now;
                    Console.WriteLine($"You logged out at {DailyLogoutTime} successfully");

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
        DailyLogoutTime = DateTime.Now;
        Console.WriteLine($"You logged out at {DailyLogoutTime} successfully");
        EmployeeData.accessEmployeeExcelFile(HospitalID, Department, "DailyLogoutTime", DailyLoginTime);
    }

}