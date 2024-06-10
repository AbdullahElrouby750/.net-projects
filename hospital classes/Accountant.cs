namespace hospital_classes;

public class Accountant : Employee , WritingReports
{
    private string[] jopTitles;
    public static int NumberOfAccountant;

    //*********************************************************************ctors****************************************************************
    public Accountant()
    {
        jopTitles = ["Nurse", "Pharmacist", "Radiologist", "Receptionist", "Doctor", "HR", "Accountant"];
        NumberOfAccountant++;
    }

    public Accountant(Dictionary<string, dynamic> data)
       : base(data)
    {
        NumberOfAccountant++;
        jopTitles = ["Nurse", "Pharmacist", "Radiologist", "Receptionist", "Doctor", "HR", "Accountant"];
    }

    //*********************************************************************send salary****************************************************************
    public void sendSalary()// merged apply bouns and diduction for simplifications and less loops
    {
        DateTime today = DateTime.Now;
        if (today.Day == 1)
        {
            foreach (var item in HR.Employees)
            {
                foreach (var IDs in item.Value)
                {
                    object employee = IDs.Value;

                    dynamic bouns = employee.GetType().GetProperty("Bouns")?.GetValue(employee)!;
                    dynamic salary = employee.GetType().GetProperty("Salary")?.GetValue(employee)!;
                    salary += bouns;

                    employee.GetType().GetProperty("SalaryReceived")?.SetValue(employee, true);
                }
            }
        }
    }


//*********************************************************************other interface methods****************************************************************

    //*********************************************************************print hr report****************************************************************
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

    public void WriteReport(){}
    
    //*********************************************************************print salary****************************************************************
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

    //*********************************************************************login****************************************************************
    public void login()
    {
        DailyLoginTime = DateTime.Now;
    }


    //*********************************************************************logout****************************************************************
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
