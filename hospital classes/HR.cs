using System.Runtime.CompilerServices;

namespace hospital_classes;

public class HR : Employee, WritingReports
{
    static public Dictionary<string, Dictionary<string, object>> Employees = new Dictionary<string, Dictionary<string, object>>(); // will be used by the accountatnt to access the employees
    private string[] jopTitles;
    public static int NumberofHR;


    //*********************************************************************ctors****************************************************************

    public HR()
    {
        jopTitles = ["Nurse", "Pharmacist", "Radiologist", "Receptionist", "Doctor", "HR", "Accountant"];
    }

    public HR(Dictionary<string, dynamic> data)
       : base(data)
    {
        NumberofHR++;
        jopTitles = ["Nurse", "Pharmacist", "Radiologist", "Receptionist", "Doctor", "HR", "Accountant"];
    }

    //*********************************************************************Hiring proccesses****************************************************************
    public void Hire()
    {
        Console.WriteLine("\nChoose a job title's number from the list below :-\n");
        for (int i = 0; i < jopTitles.Length; i++)
        {
            Console.WriteLine($"{i + 1} - {jopTitles[i]}");
        }

        int jopIndex;

        while (true)
        {
            jopIndex = int.Parse(Console.ReadLine());

            if (jopIndex > 0 && jopIndex < jopTitles.Length)
            {
                break;
            }

            Console.WriteLine("Choice out of range, please select a valid choice");
            Hire();
        }
        Dictionary<string, dynamic> data = GetNewEmployeesData(jopTitles[jopIndex]);

        switch (jopIndex)
        {
            case 1:
                var nurse = new Nurse(data);
                Employees[jopTitles[0]][nurse.HospitalID] = nurse; // nurse khhggh nurse
                break;

            case 2:
                var pharmacist = new Pharmacist(data);
                Employees[jopTitles[1]][pharmacist.HospitalID] = pharmacist;
                break;

            case 3:
                var radiologist = new Radiologist(data);
                Employees[jopTitles[2]][radiologist.HospitalID] = radiologist;
                break;

            case 4:
                var receptionist = new Receptionist(data);
                Employees[jopTitles[3]][receptionist.HospitalID] = receptionist;
                break;

            case 5:
                var doctor = new Doctor(data);
                Employees[jopTitles[4]][doctor.HospitalID] = doctor;
                break;

            case 6:
                var hr = new HR(data);
                Employees[jopTitles[5]][hr.HospitalID] = hr;
                break;

            case 7:
                var accountant = new Accountant(data);
                Employees[jopTitles[6]][accountant.HospitalID] = accountant;
                break;

        }
    }

    public Dictionary<string, dynamic> GetNewEmployeesData(string department)
    {
        Dictionary<string, dynamic> Data = new Dictionary<string, dynamic>();


        //Date from person class
        Console.Write("\n\nFirst Name : ");
        Data["FirstName"] = Console.ReadLine();

        Console.Write("Last Name : ");
        Data["LastName"] = Console.ReadLine();

        Console.Write("Phone Number : ");
        Data["PhoneNumber"] = Console.ReadLine();

        Console.Write("Age : ");
        Data["Age"] = int.Parse(Console.ReadLine());

        Console.Write("Date of Birth in yyyy-mm-dd : ");
        DateTime FullDate = DateTime.Parse(Console.ReadLine());
        Data["DateOfBirth"] = FullDate.Date;

        Console.Write("Gender : ");
        Data["Gender"] = Console.ReadLine();

        Console.Write("Statue : ");
        Data["Statue"] = Console.ReadLine();

        Console.Write("Address : ");
        Data["Address"] = Console.ReadLine();

        Console.Write("Blood Type : ");
        Data["BloodType"] = Console.ReadLine();


        //Data from Employee Class
        Console.Write("Salary : ");
        Data["Salary"] = double.Parse(Console.ReadLine());
        Console.WriteLine();

        while (true)
        {
            try
            {
                Console.Write("Enter the Work Hours in format HH:mm:ss:\t ");
                string input = Console.ReadLine();

                Data["WorkHours"] = TimeSpan.Parse(input);
                break;
            }
            catch (FormatException e)
            {
                Console.WriteLine("Invalid time span format. Please use HH:mm:ss.");
            }
            catch (OverflowException e)
            {
                Console.WriteLine("Time span value is too large or too small.");
            }
        }
        Console.WriteLine();

        Console.Write("Start Date : ");
        Data["StartDate"] = DateOnly.FromDateTime(DateTime.Now);

        Console.Write("Experience : ");
        Data["Experience"] = int.Parse(Console.ReadLine());
        Console.WriteLine();

        Console.Write("Previous Experience :- ");
        Dictionary<string, string> PE = new Dictionary<string, string>();
        while (true)
        {
            Console.Write("Enter the name of the company : ");
            string companyName = Console.ReadLine();
            if (companyName == "")
            {
                break;
            }
            Console.Write("Enter the job title : ");
            string jobTitle = Console.ReadLine();
            PE[companyName] = jobTitle;
            Data["PreviousExperience"] = PE;
        }
        Console.WriteLine();

        Data["HospitalID"] = generateID(Data["FirstName"], Data["LastName"], department);
        Console.Write($"EmployeeID : {Data["HospitalID"]}");
        Console.WriteLine();

        Console.Write("Bank : ");
        Data["BankAccount"] = Console.ReadLine();
        Console.WriteLine();

        Console.Write("Account Number : ");
        Data["AccountNumber"] = Console.ReadLine();
        Console.WriteLine();

        Console.Write("For doctors & Radiologist -> specialization : ");
        Data["Specialization"] = Console.ReadLine();
        Console.WriteLine();

        return Data;
    }

    private string generateID(string firstName, string lastName, string department)
    // first two letters in department + first letter from first name and last name + four random digits
    {
        string emp_ID = department[0..2];
        emp_ID += firstName[0];
        emp_ID += lastName[0];
        for (int i = 0; i < 4; i++)
        {
            Random r = new Random();
            emp_ID += r.Next(0, 9);
        }
        if (!Employees.ContainsKey(emp_ID))// make sure the id is unique
        {
            return emp_ID;
        }
        else
        {
            return generateID(firstName, lastName, department);
        }

    }

    //*********************************************************************Firing proccesses****************************************************************

    public bool Fire(string emp_ID, string name)
    {
        if (Employees.ContainsKey(emp_ID))
        {
            Employees.Remove(emp_ID);
            Console.WriteLine($"!!!WARNING!!! Your about to delete {name} from the system.\n Are you sure you wanto to continue?");
            Console.WriteLine("1 - Yes");
            Console.WriteLine("2 - No");

            bool answer;
            while (true)
            {
                int choice = int.Parse(Console.ReadLine());
                if (choice == 1)
                {
                    answer = true;
                    break;
                }
                else if (choice == 2)
                {
                    answer = false;
                    break;
                }
                else
                {
                    Console.WriteLine("Please enter a valid choice");
                }
            }

            return answer;
        }
        else
        {
            return false;
        }
    }

    //*********************************************************************Counting employee****************************************************************
    public static int numberOfEmployyes()
    {
        Console.WriteLine($"Number of total employees:\t {NumberOfEmployees}");
        Console.WriteLine($"Number of total receptionists:\t {Receptionist.NumberofReceptionist}");
        Console.WriteLine($"Number of total HRs:\t {NumberofHR}");
        Console.WriteLine($"Number of total accountants:\t {Accountant.NumberOfAccountant}");
        Console.WriteLine($"Number of total doctors:\t {Doctor.numberOfDoctors}");
        Console.WriteLine($"Number of total Pharmacists:\t {Pharmacist.NumberOfPharmacists}");
        Console.WriteLine($"Number of total nurses:\t {Nurse.NumberOfNurses}");
        Console.WriteLine($"Number of total radiologists:\t {Radiologist.numberOfRadiologist}");

        return NumberOfEmployees;
    }

    //*********************************************************************preformance reports****************************************************************
    public void WriteReport() // feutures !!! make this a work hours auto checker methods and the preformance method check the work quality of every employee
    {
        string report = string.Empty;
        object ThisEmployee = searchEmployee();


        dynamic name = ThisEmployee.GetType().GetProperty("FullName")?.GetValue(ThisEmployee)!;
        dynamic salary = ThisEmployee.GetType().GetProperty("Salary")?.GetValue(ThisEmployee)!;

        dynamic loginTime = ThisEmployee.GetType().GetProperty("DailyLoginTime")?.GetValue(ThisEmployee)!;
        dynamic logoutTime = ThisEmployee.GetType().GetProperty("DailyLogoutTime")?.GetValue(ThisEmployee)!;
        dynamic workhours = ThisEmployee.GetType().GetProperty("WorkHours")?.GetValue(ThisEmployee)!;

        TimeSpan elapsedTime = new TimeSpan(0, 0, 0);
        try
        {
            elapsedTime = loginTime - logoutTime;

        }
        catch (Exception)
        {
            Console.WriteLine($"{name} has not log-out yet");
            return;

        }

        // login and logout time will be added in the future!!!
        double deductionAndBounsPercentage = 0.05;

        if (elapsedTime < workhours)
        {
            dynamic warning = ThisEmployee.GetType().GetProperty("Warnings")?.GetValue(ThisEmployee)!; // warning will be added in the future!!!
            if (++warning < 3)
            {
                ThisEmployee.GetType().GetProperty("Warnings")?.SetValue(ThisEmployee, ++warning);
                report += $"{name} logged in at {loginTime}\n{name} logged out at {logoutTime}\n{name} today work hours {workhours}\nanother worning sent.\n{name} has {++warning} warnings now";
            }
            else
            {
                ThisEmployee.GetType().GetProperty("Warnings")?.SetValue(ThisEmployee, 0);
                ThisEmployee.GetType().GetProperty("Bouns")?.SetValue(ThisEmployee, salary * -deductionAndBounsPercentage);
                report += $"{name} logged in at {loginTime}.\n{name} logged out at {logoutTime}.\n{name} today work hours {workhours}.\n{name} has already passed the limited warning.\n Applying deduction by 5%{salary * deductionAndBounsPercentage}\n{name} has 0 warnings now";
            }
        }
        else if (elapsedTime > workhours)
        {
            double bounsvalue = (elapsedTime - workhours) * deductionAndBounsPercentage * salary;
            ThisEmployee.GetType().GetProperty("Bouns")?.SetValue(ThisEmployee, bounsvalue);
            report += $"{name} logged in at {loginTime}.\n{name} logged out at {logoutTime}.\n{name} today work hours {workhours}.\n Adding bouns by 5%{bounsvalue}.";
        }
        else
        {
            report += $"{name} logged in at {loginTime}.\n{name} logged out at {logoutTime}.\n{name} today work hours {workhours}.";
        }

        var timeWritten = DateTime.Now;
        report += $"\n\n HR : {FullName}\nDate : {timeWritten}";
        ThisEmployee.GetType().GetProperty("HRreport")?.SetValue(ThisEmployee, report);
    }

    //*********************************************************************search employee****************************************************************

    public static object searchEmployee()
    {
        while (true)
        {
            string id = "";
            var hr = new HR();
            Console.Write("Search by employye's full name or ID:");
            string search = Console.ReadLine();
            if (!search.Any(char.IsDigit))
            {

                var emp = hr.searchByName(search);
                if (emp != null)
                {
                    return emp;
                }

                Console.WriteLine("Employee not found! Make sure you entered the name right or that this employee does exist.");

                Console.Write("enter 0 to Exit 1 to continue : ");
                string exit = Console.ReadLine();
                if (exit == "0")
                {
                    break;
                }


            }
            else if (search.Any(char.IsDigit))
            {

                var emp = hr.searchByID(search);
                if (emp != null)
                {
                    return emp;
                }

                Console.WriteLine("Employee not found! Make sure you entered the id right or that this employee does exist.");

                Console.Write("enter 0 to Exit 1 to continue : ");
                string exit = Console.ReadLine();
                if (exit == "0")
                {
                    break;
                }

            }
            else
            {
                Console.WriteLine("Employee not found! Make sure you entered the id or full name right. Or, that this employee does exist.");

                Console.Write("enter 0 to Exit 1 to continue : ");
                string exit = Console.ReadLine();
                if (exit == "0")
                {
                    break;
                }
            }
        }
        return null;
    }

    private object searchByID(string id)
    {
        foreach (var joptitle in Employees)
        {
            if (Employees[joptitle.Key.ToString()].ContainsKey(id))
            {
                object result = Employees[joptitle.Key.ToString()][id];
                return result;
            }
        }
        return null;
    }
    private object searchByName(string name)
    {
        foreach (var joptitle in Employees)
        {
            foreach (var id in Employees[joptitle.Key.ToString()])
            {
                if (Employees[joptitle.Key.ToString()][id.Key.ToString()] != null)
                {
                    object result = Employees[joptitle.Key.ToString()][id.Key.ToString()];

                    if (result.GetType().GetProperty("FullName")?.GetValue(result) == name)
                    {
                        return result;
                    }
                }
            }
        }
        return null;
    }
    //*********************************************************************promotion****************************************************************
    public void pormotion()
    {
        var ThisEmployee = searchEmployee();

        dynamic startDate = ThisEmployee.GetType().GetProperty("StartingDate")?.GetValue(ThisEmployee)!;
        TimeSpan yearsSpent = DateTime.Now.Year - startDate.Year;

        double rasieValue = yearsSpent.Days / 365 * 0.2;

        ThisEmployee.GetType().GetProperty("Salary")?.SetValue(ThisEmployee, rasieValue);
    }



    //*********************************************************************other interface methods****************************************************************

    //*********************************************************************print hr report****************************************************************
    public void PrintHRreport()
    {
        Console.WriteLine(HRreport);
    }

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

    public static void creatRouby()
    {

        Dictionary<string, dynamic> data = new Dictionary<string, dynamic>();
        var hr = new HR();
        data["FirstName"] = "Abdullah";
        data["LastName"] = "Rouby";
        data["PhoneNumber"] = "01220200683";
        data["Age"] = 20;
        data["DateOfBirth"] = new DateOnly(2003, 9, 1);
        data["Gender"] = "Male";
        data["Statue"] = "Single";
        data["Address"] = "Egypt. Behira. Wadi-Elnatrun";
        data["BloodType"] = "A+";
        data["Salary"] = 10000;
        data["WorkHours"] = new TimeSpan(8, 0, 0);
        data["StartDate"] = new DateOnly(2023, 1, 1);
        data["Experience"] = 1;
        Dictionary<string, string> PE = new Dictionary<string, string>();
        PE["the hospital"] = "HR";
        data["PreviousExperience"] = PE;
        data["HospitalID"] = "HRAR1706";
        data["BankAccount"] = "CIB";
        data["AccountNumber"] = "1234-2345-3456-4567";
        data["Specialization"] = string.Empty;

        var rouby = new HR(data);
        Dictionary<string, object> r = new Dictionary<string, object>();
        r.Add(rouby.HospitalID, rouby);
        Employees.Add("HR", r);
        // Employees["HR"][rouby.HospitalID] = rouby;
    }

}
