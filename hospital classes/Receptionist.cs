using hospitalData;
namespace hospital_classes;

public class Receptionist : Employee, WritingReports
{
    public Receptionist() {}

    public Receptionist(Dictionary<string, dynamic> data) : base(data) {}

    //********************************************************************* store new patient data ****************************************************************

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

        Data["Recet"] = string.Empty;

        Data["Visits"] = new Dictionary<int, bool>();

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
        Console.WriteLine($"Patient ID: {data["HospitalID"]}");

    }
    //********************************************************************* search patient ****************************************************************

    internal static Patient SearchpatientData()
    {
        while (true)
        {
            string id = "";
            var receprionist = new Receptionist();
            Console.Write("Search by patient's full name or ID:");
            string search = Console.ReadLine()!.ToUpper();
            if (!search.Any(char.IsDigit))
            {

                var data = patientData.GetNameDate(search);
                if (data != null)
                {
                    return receprionist.choosePatient(data);
                }

                Console.WriteLine("Patient not found! Make sure you entered the name right or that this Patient does exist.");

                Console.Write("enter 0 to Exit 1 to continue : ");
                string exit = Console.ReadLine()!;
                if (exit == "0")
                {
                    break;
                }


            }
            else if (search.Any(char.IsDigit))
            {

                var data = patientData.GetIdDate(search);
                if (data != null)
                {
                    var patient = new Patient(data);
                    return patient;
                }

                Console.WriteLine("Patient not found! Make sure you entered the id right or that this Patient does exist.");

                Console.Write("enter 0 to Exit 1 to continue : ");
                string exit = Console.ReadLine()!;
                if (exit == "0")
                {
                    break;
                }

            }
            else
            {
                Console.WriteLine("Patient not found! Make sure you entered the id or full name right. Or, that this Patient does exist.");

                Console.Write("enter 0 to Exit 1 to continue : ");
                string exit = Console.ReadLine()!;
                if (exit == "0")
                {
                    break;
                }
            }
        }
        return null;
    }

    private Patient choosePatient(List<Dictionary<string, dynamic>> similarNames)
    {
        Patient patient = null;
        if (similarNames.Count == 0)
        {
            Console.WriteLine("No patient with such name!");
            return null;
        }
        else if (similarNames.Count == 1)
        {
            patient = new Patient(similarNames[0]);
            return patient;
        }

        Console.WriteLine("Choose the wanted employee :-");
        int index = 0;
        foreach (var item in similarNames)
        {
            Console.WriteLine($"{index + 1}{item["HospitalID"]}\t :\t {item["FullName"]}");
            index++;
        }
        while (true)
        {
            try
            {
                int choice = int.Parse(Console.ReadLine()!);
                if (choice == 0)
                {
                    return null;
                }
                patient = new Patient(similarNames[choice]);
                return patient;

            }
            catch (IndexOutOfRangeException ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine("Choose a valid choice! or enter null to return");
            }
        }

    }

    public static void PrintPatientReports()
    {
        string patientID;
        while (true)
        {
            if(Console.ReadKey(true).Key == ConsoleKey.Escape)
            {
                return;
            }
            Console.Write("Enter Patient ID : ");
            patientID = Console.ReadLine();
            var data = patientData.GetIdDate(patientID);
            if (data != null)
            {
                var patient = new Patient(data);
                patient.PrintReports();
                break;
            }
            else
            {
                Console.WriteLine($"Patient with ID {patientID} not found in the data.\nEnter a valid Patient ID.");
            }
        }
    }
    //********************************************************************* Bill ****************************************************************

    public void setBill()
    {
        Patient patient = null;
        patient = SearchpatientData();

        if (patient == null) return;

        Console.WriteLine("Here's the patient reports\n");
        patient.PrintReports();

        setBillDetils(patient);

    }

    private void setBillDetils(Patient patient)
    {
        double dBill = 0;
        string value = string.Empty;
        string[] billDetils = { "Operation", "Medical X-ray", "Medecin", "Service", "Others" };
        foreach(var section in billDetils)
        {
            value = "";
            while (true)
            {
                try
                {
                    Console.Write($"{section} : ");
                    value = Console.ReadLine()!;
                    if (double.TryParse(value, out double bill))
                    {
                        dBill += bill;
                        patient.Recet += $"{section}\t\t:\t\t{value}";
                        break;
                    }
                    else
                    {
                        throw new FormatException($"Input {value} is not a valid number. Plz, enter a valid one");
                    }
                }
                catch (FormatException ex)
                {
                    Console.WriteLine($"{ex.Message}");
                }
            }
        }

        patient.setBill(dBill);
        HandlingExcelClass.accessEmployeeExcelFile(patient.PatientID, "Patient data", "Bill", dBill, "patient");
        HandlingExcelClass.accessEmployeeExcelFile(patient.PatientID, "Patient data", "Recet", patient.Recet, "patient");
    }

    public void printPatientBill()
    {
        Patient patient = null;
        patient = SearchpatientData();

        if (patient == null) return;

        patient.PrintBill();
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