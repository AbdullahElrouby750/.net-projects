namespace hospital_classes;

public class Receptionist : Employee, WritingReports
{
    static public Dictionary<int, Patient> patientData = new Dictionary<int, Patient>();
    public static int NumberofReceptionist;

    public Receptionist()
    {
        NumberofReceptionist++;
    }

    public Receptionist(Dictionary<string, dynamic> data)
       : base(data)
    {
        NumberofReceptionist++;
    }
    private Dictionary<string, dynamic> GetNewpatientData()
    {
        Dictionary<string, dynamic> Data = new Dictionary<string, dynamic>();


        //Date from patient class
        Console.Write("\n\nFirst Name : ");
        Data["FirstName"] = Console.ReadLine().ToUpper();

        Console.Write("Last Name : ");
        Data["LastName"] = Console.ReadLine().ToUpper();

        Console.Write("Phone Number : ");
        Data["PhoneNumber"] = Console.ReadLine();

        Console.Write("Age : ");
        Data["Age"] = int.Parse(Console.ReadLine());

        Console.Write("Date of Birth in yyyy-mm-dd : ");
        DateOnly FullDate = DateOnly.Parse(Console.ReadLine());
        Data["DateOfBirth"] = FullDate;

        Console.Write("Gender : ");
        Data["Gender"] = Console.ReadLine();

        Console.Write("Statue : ");
        Data["Statue"] = Console.ReadLine();

        Console.Write("Address : ");
        Data["Address"] = Console.ReadLine();

        Console.Write("Blood Type : ");
        Data["BloodType"] = Console.ReadLine().ToUpper();

        Console.Write("Weight in KG : ");
        Data["Weight"] = double.Parse(Console.ReadLine());

        Console.Write("Height in CM : ");
        Data["Height"] = double.Parse(Console.ReadLine());

        Console.Write("CurrentProblem : ");
        Data["CurrentProblem"] = Console.ReadLine();

        Console.Write("Disabilities : ");
        Data["Disabilities"] = Console.ReadLine();

        return Data;
    }

    public void hipatient()
    {
        var data = GetNewpatientData();
        var patient = new Patient(data);

        while (true)
        {
            string disease;
            string info;
            Dictionary<string, string> medicalHistory = new Dictionary<string, string>();

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
            else if (answer == "no")
            {
                patient.MedicalHistory = medicalHistory;
                break;
            }
            else
            {
                patient.MedicalHistory = medicalHistory;
                break;
            }
        }

        while (true)
        {
            string contact;
            string info;
            Dictionary<string, string> emergencyContact = new Dictionary<string, string>();

            Console.WriteLine("Enter contact name : ");
            contact = Console.ReadLine();

            Console.WriteLine("Enter contact info : ");
            info = Console.ReadLine();

            emergencyContact[contact] = info;
            Console.WriteLine("Do you want to continue ? (yes or no)");
            string answer = Console.ReadLine();
            if (answer == "yes")
            {
                continue;
            }
            else if (answer == "no")
            {
                patient.EmergencyContact = emergencyContact;
                break;
            }
            else
            {
                patient.EmergencyContact = emergencyContact;
                break;
            }
        }
        patientData[patient.PatientID] = patient;
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

}