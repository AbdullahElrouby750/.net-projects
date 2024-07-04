using hospitalData;

namespace hospital_classes;

public class Pharmacist : Employee, WritingReports
{

    public static int NumberOfPharmacists;
    private Dictionary<string, int> NeedMedicin = new Dictionary<string, int>();


    public Pharmacist()
    {
        NumberOfPharmacists++;
    }

    public Pharmacist(Dictionary<string, dynamic> data) : base(data)
    {
        NumberOfPharmacists++;
    }

    public void needMedicin()
    {
        Console.WriteLine("Write the needed medicin and the Quantity wanted");


        while (true)
        {
            Console.Write("Mecdicin Name: ");

            if (Console.ReadKey(true).Key == ConsoleKey.Escape) // stop entering
            {
                break;
            }

            string medicinName = Console.ReadLine();

            Console.Write("Quantity: ");
            while (true) // make sure the user enterd a number
            {
                try
                {
                    int quantity = int.Parse(Console.ReadLine());
                    NeedMedicin[medicinName] = quantity;
                    break;
                }
                catch (Exception e)
                {
                    Console.WriteLine("Error: " + e.Message);
                }
            }
        }
    }
    private string WriteMedicinDose()
    {
        string medicinDose = string.Empty;
        string medicin;
        string dose;
        Console.WriteLine("Write the patient prescribtion");
        while (true)
        {
            Console.Write("Medicin: ");

            if (Console.ReadKey(true).Key == ConsoleKey.Escape) // stop entering
            {
                break;
            }

            medicin = Console.ReadLine();

            Console.Write("Dose: ");
            dose = Console.ReadLine();
            medicinDose += $"{medicin}: {dose}\n";
        }
        return medicinDose;
    }

    public void WriteReport() {
        Patient patient = null;
        patient = Receptionist.SearchpatientData();
        if (patient != null)
        {

            Console.WriteLine($"Enter Pharmacist Report : ");
            string report = Console.ReadLine();

            patient.PharmacistReport = $"Patient Name: {patient.FullName}\n";
            patient.PharmacistReport += $"PatientID: {patient.PatientID}\n";

            string medicineReport = WriteMedicinDose();

            patient.PharmacistReport += $"Pharmacist Report: {medicineReport}\n";

            patient.PharmacistReport += $"Pharmacist: {FullName}\n";
            patient.PharmacistReport += $"Date: {DateTime.Now.ToShortDateString()} {DateTime.Now.ToShortTimeString()}\n";

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
