using hospital_classes;
using hospitalData;

public class Doctor : Employee, WritingReports
{
    public static int numberOfDoctors = 0;
    public string specialization;


    public Doctor(Dictionary<string, dynamic> data)
        : base(data)
    {
        specialization = data["Specialization"];
        numberOfDoctors++;
    }

    public Doctor()
    {
        this.specialization = "Unknown";
        numberOfDoctors++;
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
                Console.WriteLine($"Does the patient need operation? : yes : no");
                string OperationInput = Console.ReadLine().ToLower();
                if (OperationInput == "yes")
                {
                    patient.Operation = true;
                }

                Console.WriteLine($"Enter Doctor Report : ");
                string report = Console.ReadLine();

                patient.DoctorReport = $"Patient Name: {patient.FullName}\n";
                patient.DoctorReport += $"PatientID: {patient.PatientID}\n";
                patient.DoctorReport += $"Doctor Report: {report}\n";

                patient.DoctorReport += $"Doctor: {FullName}\n";
                patient.DoctorReport += $"Date: {DateTime.Now.ToShortDateString()} {DateTime.Now.ToShortTimeString()}\n";

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