using hospitalData;

namespace hospital_classes;

public class Accountant : Employee, WritingReports
{
    private string[] jopTitles;
    public static int NumberOfAccountant;

    //*********************************************************************ctors****************************************************************
    public Accountant()
    {
        jopTitles = ["Nurse", "Pharmacist", "Radiologist", "Receptionist", "Doctor", "HR", "Accountant"];
    }

    public Accountant(Dictionary<string, dynamic> data)
       : base(data)
    {
        jopTitles = ["Nurse", "Pharmacist", "Radiologist", "Receptionist", "Doctor", "HR", "Accountant"];
    }

    //*********************************************************************send salary****************************************************************
    public void sendSalary()// merged apply bouns and diduction for simplifications and less loops
    {
        if (DateTime.Now.Day == 1)
        {
            var IDs = EmployeeData.getIDS();
            foreach (var item in jopTitles)
            {

                var thisJopID = IDs.Keys.Where(jopID => jopID[0..2].ToUpper() == item[0..2].ToUpper()).ToList();
                if (thisJopID == null) continue;

                foreach(var id in thisJopID)
                {
                    double bouns = double.Parse(EmployeeData.accessEmployeeExcelFile(id, item, "Bouns"));
                    double salary = double.Parse(EmployeeData.accessEmployeeExcelFile(id, item, "Salary"));

                    string bankName = EmployeeData.accessEmployeeExcelFile(id, item, "BankAccount");
                    string accountName = EmployeeData.accessEmployeeExcelFile(id, item, "AccountNumber");

                    salary += bouns;

                    //TODO : use bank name and acc no. to send the money to the emp account

                    EmployeeData.accessEmployeeExcelFile(id, item, "SalaryReceived", true);
                }
            }
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