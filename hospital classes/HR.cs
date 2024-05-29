using System.Runtime.CompilerServices;

namespace hospital_classes;

public class HR : Employee, WritingReports
{
    static public Dictionary<string, Dictionary<string, object>> Employees = new Dictionary<string, Dictionary<string, object>>(); // will be used by the accountatnt to access the employees
    static private Dictionary<string, string> IDsBeckups = new Dictionary<string, string>(); // if someone forgot thier id
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
                Dictionary<string, object> nur = new Dictionary<string, object>();
                nur[nurse.HospitalID] = nurse;
                Employees[jopTitles[0]] = nur; // nurse khhggh nurse
                IDsBeckups[nurse.HospitalID] = nurse.FullName;
                break;

            case 2:
                var pharmacist = new Pharmacist(data);
                Dictionary<string, object> pha = new Dictionary<string, object>();
                pha[pharmacist.HospitalID] = pharmacist;
                Employees[jopTitles[1]] = pha;
                IDsBeckups[pharmacist.HospitalID] = pharmacist.FullName;
                break;

            case 3:
                var radiologist = new Radiologist(data);
                Dictionary<string, object> rad = new Dictionary<string, object>();
                rad[radiologist.HospitalID] = radiologist;
                Employees[jopTitles[2]] = rad;
                IDsBeckups[radiologist.HospitalID] = radiologist.FullName;
                break;

            case 4:
                var receptionist = new Receptionist(data);
                Dictionary<string, object> rec = new Dictionary<string, object>();
                rec[receptionist.HospitalID] = receptionist;
                Employees[jopTitles[3]] = rec;
                IDsBeckups[receptionist.HospitalID] = receptionist.FullName;
                break;

            case 5:
                var doctor = new Doctor(data);
                Dictionary<string, object> doc = new Dictionary<string, object>();
                doc[doctor.HospitalID] = doctor;
                Employees[jopTitles[4]] = doc;
                IDsBeckups[doctor.HospitalID] = doctor.FullName;
                break;

            case 6:
                var hr = new HR(data);
                Employees[jopTitles[5]][hr.HospitalID] = hr;
                IDsBeckups[hr.HospitalID] = hr.FullName;
                break;

            case 7:
                var accountant = new Accountant(data);
                Dictionary<string, object> acc = new Dictionary<string, object>();
                acc[accountant.HospitalID] = accountant;
                Employees[jopTitles[6]] = acc;
                IDsBeckups[accountant.HospitalID] = accountant.FullName;
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
        DateOnly FullDate = DateOnly.Parse(Console.ReadLine());
        Data["DateOfBirth"] = FullDate;

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
        Data["StartDate"] = DateOnly.Parse(Console.ReadLine());

        Console.Write("Experience : ");
        Data["Experience"] = int.Parse(Console.ReadLine());
        Console.WriteLine();

        Console.Write("Previous Experience :- ");
        Dictionary<string, string> PE = new Dictionary<string, string>();
        while (true)
        {
            Console.Write("\nEnter the name of the company (press enter if none) : ");
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

        Data["Department"] = department;

        return Data;
    }

    private string generateID(string firstName, string lastName, string department)
    // first two letters in department + first letter from first name and last name + four random digits
    {
        string emp_ID = department[0..2].ToUpper();
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

    public void Fire()
    {

        do
        {
            Console.Write($"\n\nEnter employee's ID : ");
            string id = Console.ReadLine();

            if (Employees.ContainsKey(id))
            {

                object ThisEmployee = searchByID(id);
                string name = ThisEmployee.GetType().GetProperty("FullName")?.GetValue(ThisEmployee)!.ToString();
                string department = ThisEmployee.GetType().GetProperty("Department")?.GetValue(ThisEmployee)!.ToString();

                Console.WriteLine($"!!!WARNING!!! Your about to delete {name} from the system.\n Are you sure you wanto to continue?");
                Console.WriteLine("1 - Yes");
                Console.WriteLine("2 - No");

                while (true)
                {
                    int choice = int.Parse(Console.ReadLine());
                    if (choice == 1)
                    {
                        Employees[department].Remove(id);
                        break;
                    }
                    else if (choice == 2)
                    {
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Please enter a valid choice");
                    }
                }
                break;

            }
            else
            {
                Console.WriteLine("Employee not found");
                Console.WriteLine("do you want to continue? (yes/no): ");

                if (Console.ReadLine().ToLower() == "no")
                {
                    break;
                }
            }
        } while (true);
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
            elapsedTime = logoutTime - loginTime;

        }
        catch (Exception)
        {
            Console.WriteLine($"{name} has not log-out yet");
            return;

        }

        // login and logout time will be added in the future!!!
        double deductionAndBounsPercentage;

        if (elapsedTime < workhours)
        {
            deductionAndBounsPercentage = 0.01;

            int warning;
            try
            {
                warning = int.Parse(ThisEmployee.GetType().GetProperty("Warnings")?.GetValue(ThisEmployee)!.ToString()!);
            }
            catch (Exception)
            {
                warning = 0;
            }

            if (warning < 3)
            {
                warning++;
                ThisEmployee.GetType().GetProperty("Warnings")?.SetValue(ThisEmployee, warning);
                report += $"{name} logged in at {loginTime}\n{name} logged out at {logoutTime}\n{name} today work hours {elapsedTime}\nanother worning sent.\n{name} has {warning} warnings now";
            }
            else
            {
                ThisEmployee.GetType().GetProperty("Warnings")?.SetValue(ThisEmployee, 0);
                ThisEmployee.GetType().GetProperty("Bouns")?.SetValue(ThisEmployee, salary * -deductionAndBounsPercentage); //deduction after 3 warnengs only
                report += $"{name} logged in at {loginTime}.\n{name} logged out at {logoutTime}.\n{name} today work hours {workhours}.\n{name} has already passed the limited warning.\n Applying deduction by 5% = {salary * deductionAndBounsPercentage}\n{name} has 0 warnings now";
            }
        }
        else if (elapsedTime > workhours)
        {
            deductionAndBounsPercentage = 0.05;
            double bounsvalue = (elapsedTime - workhours) * deductionAndBounsPercentage * salary;
            ThisEmployee.GetType().GetProperty("Bouns")?.SetValue(ThisEmployee, bounsvalue);
            report += $"{name} logged in at {loginTime}.\n{name} logged out at {logoutTime}.\n{name} today work hours {workhours}.\n Adding bouns by 5%{bounsvalue}.";
        }
        else
        {
            report += $"{name} logged in at {loginTime}.\n{name} logged out at {logoutTime}.\n{name} today work hours {workhours}.";
        }

        var timeWritten = DateTime.Now;
        report += $"\n\nHR : {FullName}\nDate : {timeWritten}";
        ThisEmployee.GetType().GetProperty("HRreport")?.SetValue(ThisEmployee, report);
    }

    //*********************************************************************search employee****************************************************************

    public static object searchEmployee(string id) // for the login proccess
    {
        var hr = new HR();
        return hr.searchByID(id.ToUpper());
    }
    public static object searchEmployee()
    {
        while (true)
        {
            string id = "";
            var hr = new HR();
            Console.Write("Search by employye's full name or ID:");
            string search = Console.ReadLine().ToUpper();
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
                    string emp_name = result.GetType().GetProperty("FullName")?.GetValue(result)!.ToString()!.ToUpper()!;
                    if (emp_name == name)
                    {
                        return result;
                    }
                    Console.WriteLine("Not found");
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
        Console.WriteLine($"You logged in at {DailyLoginTime} successfully");
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
    //*********************************************************************others****************************************************************

    public static void creatRouby()
    {

        Dictionary<string, dynamic> data = new Dictionary<string, dynamic>();
        var hr = new HR();
        data["FirstName"] = "Abdullah";
        data["LastName"] = "Elrouby";
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
        data["HospitalID"] = "HRAE1706";
        data["BankAccount"] = "CIB";
        data["AccountNumber"] = "1234-2345-3456-4567";
        data["Specialization"] = string.Empty;
        data["Department"] = "HR";

        var rouby = new HR(data);
        Dictionary<string, object> r = new Dictionary<string, object>();
        r[rouby.HospitalID] = rouby;
        Employees["HR"] = r;
        IDsBeckups[rouby.HospitalID] = rouby.FullName;
        // Employees["HR"][rouby.HospitalID] = rouby;
    }

    public void PrintIDS()
    {

        var id = IDsBeckups.OrderBy(x => x.Value).ToList();
        Console.WriteLine("Name\t\t\t :\t ID");
        foreach (var item in id)
        {
            Console.WriteLine($"{item.Value}\t :\t {item.Key}");
        }
    }


}
