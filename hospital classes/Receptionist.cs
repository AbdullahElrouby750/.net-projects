using hospitalData;
namespace hospital_classes;

public class Receptionist : Employee, WritingReports
{
    public Receptionist() {}

    public Receptionist(Dictionary<string, dynamic> data) : base(data) {}


    private Dictionary<string, dynamic> GetNewpatientData()
    {
        Dictionary<string, dynamic> Data = new Dictionary<string, dynamic>();

        Data["HospitalID"] = patientData.generateID(); //so the id will be in the first col

        //Date from person class
        Console.Write("\n\nFirst Name : ");
        string FirstName = Console.ReadLine()!;

        Console.Write("Last Name : ");
        string LastName = Console.ReadLine()!;

        Data["FullName"] = FirstName + " " + LastName;

        Console.Write("Phone Number : ");
        Data["PhoneNumber"] = Console.ReadLine()!;

        Console.Write("Age : ");
        Data["Age"] = int.Parse(Console.ReadLine()!);

        Console.Write("Date of Birth in yyyy-mm-dd : ");
        DateOnly FullDate = DateOnly.Parse(Console.ReadLine()!);
        Data["DateOfBirth"] = FullDate;

        Console.Write("Gender : ");
        Data["Gender"] = Console.ReadLine()!;

        Console.Write("Statue : ");
        Data["Statue"] = Console.ReadLine()!;

        Console.Write("Address : ");
        Data["Address"] = Console.ReadLine()!;

        Console.Write("Blood Type : ");
        Data["BloodType"] = Console.ReadLine()!;

        Console.Write("Weight in KG : ");
        Data["Weight"] = double.Parse(Console.ReadLine());

        Console.Write("Height in CM : ");
        Data["Height"] = double.Parse(Console.ReadLine());

        Console.Write("CurrentProblem : ");
        Data["CurrentProblem"] = Console.ReadLine();

        Console.Write("Disabilities : ");
        Data["Disabilities"] = Console.ReadLine();

        Data["Operation"] = false;

        Data["MedicalXray"] = false;

        Data["AllVisitsDone"] = false;

        Data["Bill"] = 0.0;

        Data["MedicalHistory"] = getNewPatientMedicalHestory();


        Dictionary<string, string> emergencyContact = new Dictionary<string, string>();

        string contact;
        string info;

        Console.WriteLine("Enter Emergency contact name : ");
        contact = Console.ReadLine();

        Console.WriteLine("Enter Emergency contact info : ");
        info = Console.ReadLine();

        emergencyContact[contact] = info;

        Data["EmergencyContact"] = emergencyContact;


        return Data;
    }

    private Dictionary<string, string> getNewPatientMedicalHestory()
    {
        Dictionary<string, string> medicalHistory = new Dictionary<string, string>();

        Console.WriteLine("For Medical History : ");

        while (true)
        {
            string disease;
            string info;

            Console.WriteLine("Enter disease name : ");
            disease = Console.ReadLine();

            Console.WriteLine("Enter disease info : ");
            info = Console.ReadLine();

            medicalHistory[disease] = info;
            Console.WriteLine("Do you want to continue ? (yes or no) ");
            string answer = Console.ReadLine();
            if (answer == "yes")
            {
                continue;
            }
            else
            {
               
               break;
            }
        }
        return medicalHistory;
    }
    public void hipatient()
    {
        var data = GetNewpatientData();
        Console.WriteLine("Patient was added successfully");
        Console.WriteLine($"Patient ID: {patient.PatientID}");

    }

    public static Patient SearchpatientData(int patientID)
    {
        if (patientData.ContainsKey(patientID))
        {
            return patientData[patientID];
        }
        else
        {
            Console.WriteLine($"Patient with ID {patientID} not found in the data.");
            return null;
        }
    }

    public static void PrintPatientReports()
    {
        int patientID;
        while (true)
        {
            Console.Write("Enter Patient ID : ");
            patientID = int.Parse(Console.ReadLine());
            if (patientData.ContainsKey(patientID))
            {
                patientData[patientID].PrintReports();
                break;
            }
            else
            {
                Console.WriteLine($"Patient with ID {patientID} not found in the data.\nEnter a valid Patient ID");
            }
        }
    }
    public void WriteReport() { }

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