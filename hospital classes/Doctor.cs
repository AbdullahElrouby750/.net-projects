using hospital_classes;

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