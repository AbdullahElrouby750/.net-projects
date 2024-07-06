using hospitalData;

namespace hospital_classes;
public class Nurse : Employee, WritingReports
{
    private Dictionary<int, bool> Visits;
    

    public Nurse()
    {

        Visits = new Dictionary<int, bool>();

    }

    public Nurse(Dictionary<string, dynamic> data) : base(data)
    {

        Visits = new Dictionary<int, bool>();

    }


    private void NumberOfVisits(string ID)
    {
        int numberOfVisits;

        Console.Write("Enter number of visits for patient ID " + ID + " : ");
        numberOfVisits = int.Parse(Console.ReadLine()!);
        for (int i = 1; i <= numberOfVisits; i++)
        {
            Visits[i] = false;
        }

        Dictionary<string, string> sVisits = new Dictionary<string, string>();

        foreach (var item in Visits)
        {
            sVisits[item.Key.ToString()] = item.Value.ToString();
        }

        patientData.setNurseVisitsSheet(sVisits,ID);
    }



    public void AddVisit() // re make from the scratch
    {
        Patient patient = null;
        patient = Receptionist.SearchpatientData();
        if (patient != null)
        {
            Visits = patient.Visits;
            int visitTargrted = 1;

            if (Visits.Count == 0)
            {
                NumberOfVisits(patient.PatientID);
            }

            Console.WriteLine("Visits\t\tstatue");
            foreach (var visit in Visits)
            {
                Console.WriteLine($"{visit.Key}\t\t{visit.Value}");
                if (visit.Value == true)
                {
                    visitTargrted = visit.Key + 1;
                }
            }
            Console.WriteLine($"mark visit number {visitTargrted} as done? yes / no ");

            string answer = Console.ReadLine()!;
            if (answer == "yes")
            {

                patientData.accessNurseVisitSheet(patient.PatientID, visitTargrted);
                Visits[visitTargrted] = true; 

                Console.WriteLine($"Visit for {patient.FullName} with ID {patient.PatientID} done successfully.");
            }
            patient.AllVisitsDone =  NeedVisits();
        }

    }

    private bool NeedVisits() // re code this with an id to do the check for
    {
        foreach (var patientVisits in Visits)
        {
            if (patientVisits.Value == false)
            {
                return false;
            }
        }
        return true;
    }

    public void needVisits()
    {
        Patient patient = null;
        patient = Receptionist.SearchpatientData();
        if (patient != null)
        {
            if (patient.AllVisitsDone)
            {
                Console.WriteLine("All visits done for this patient");
                return;
            }

            Console.WriteLine("Patient still have some visits");
            foreach (var visit in patient.Visits)
            {
                Console.WriteLine($"visit number {visit.Key}\t\t:\t\t{visit.Value}");
            }
        }
    }

    public void WriteReport()
    {
        Patient patient = null;
        patient = Receptionist.SearchpatientData();
        if (patient != null)
        {
            Console.WriteLine($"Enter Nurse Report : ");
            string report = Console.ReadLine();

            patient.NurseReport = $"Patient Name: {patient.FullName}\n";
            patient.NurseReport += $"PatientID: {patient.PatientID}\n";
            patient.NurseReport += $"Nurse Report: {report}\n";

            patient.NurseReport += $"Nurse: {FullName}\n";
            patient.NurseReport += $"Date: {DateTime.Now.ToShortDateString()} {DateTime.Now.ToShortTimeString()}\n";

            HandlingExcelClass.accessEmployeeExcelFile(patient.PatientID, "Patient data", "NurseReport", patient.NurseReport, "patient");

            return;
        }
        Console.WriteLine("Please enter a valid patient ID or enter 0 to Exit");
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
                HRreport = (HandlingExcelClass.accessEmployeeExcelFile(HospitalID, Department, "HRreport", "emp"));

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
        SalaryReceived = bool.Parse(HandlingExcelClass.accessEmployeeExcelFile(HospitalID, Department, "SalaryReceived", "emp"));
        Salary = double.Parse(HandlingExcelClass.accessEmployeeExcelFile(HospitalID, Department, "Salary", "emp"));
        Bouns = double.Parse(HandlingExcelClass.accessEmployeeExcelFile(HospitalID, Department, "Bouns", "emp"));

        if (SalaryReceived == true)
        {
            double salaryAfterBouns = Salary + Bouns;
            Console.WriteLine($"Salary received successfully: {salaryAfterBouns:c} ");
            Console.WriteLine($"Your main salary: {Salary}");
            Console.WriteLine($"Your bouns: {Bouns}");
            SalaryReceived = false;

            HandlingExcelClass.accessEmployeeExcelFile(HospitalID, Department, "SalaryReceived", false, "emp");
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
        HandlingExcelClass.accessEmployeeExcelFile(HospitalID, Department, "DailyLoginTime", DailyLoginTime, "emp");
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
        HandlingExcelClass.accessEmployeeExcelFile(HospitalID, Department, "DailyLogoutTime", DailyLoginTime, "emp");
    }



}