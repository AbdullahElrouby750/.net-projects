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
                string answer = Console.ReadLine().ToLower();
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
    }
}
