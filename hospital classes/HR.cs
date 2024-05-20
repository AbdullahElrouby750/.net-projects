using System.Diagnostics;

namespace hospital_classes;

public class HR : Employee, WritingReports
{
    static public Dictionary<string, Dictionary<string, object>> Employees { get; set; } // will be used by the accountatnt to access the employees
    private string[] jopTitles;
    public static int NumberofHR;


    //*********************************************************************ctors****************************************************************

    public HR()
    {
        Employees = new Dictionary<string, Dictionary<string, object>>();
        NumberofHR++;
        jopTitles = ["Nurse", "Pharmacist", "Radiologist", "Receptionist", "Doctor", "HR", "Accountant"];
    }

    public HR(Dictionary<string, dynamic> data)
       : base(data)
    {
        Employees = new Dictionary<string, Dictionary<string, object>>();
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
        Dictionary<string, dynamic> data = GetNewEmployeesData();

        switch (jopIndex)
        {
            case 1:
                var nurse = new Nurse(data);
                Employees[jopTitles[0]][nurse.HospitalID] = nurse;
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

    private Dictionary<string, dynamic> GetNewEmployeesData()
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

        Console.WriteLine("Gender : ");
        Data["Gender"] = Console.ReadLine();

        Console.WriteLine("Statue : ");
        Data["Statue"] = Console.ReadLine();

        Console.WriteLine("Address : ");
        Data["Address"] = Console.ReadLine();

        Console.WriteLine("Blood Type : ");
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
            Data["PreviousExperience"].Add(companyName, jobTitle);
        }
        Console.WriteLine();

        Data["HospitalID"] = generateID(Data["FirstName"], Data["LastName"]);
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

    private string generateID(string firstName, string lastName)
    {
        string emp_ID = "";
        emp_ID += firstName[0..2];
        emp_ID += lastName[0..2];
        for (int i = 0; i < 4; i++)
        {
            Random r = new Random();
            emp_ID += r.Next(0, 9);
        }
        if (Employees.ContainsKey(emp_ID))// make sure the id is unique
        {
            return emp_ID;
        }
        else
        {
            return generateID(firstName, lastName);
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
    public int numberOfEmployyes()
    {
        Console.WriteLine($"Number of total employees:\t {NumberOfEmployees}");
        Console.WriteLine($"Number of total receptionists:\t {Receptionist.NumberOfRecipients}");
        Console.WriteLine($"Number of total HRs:\t {NumberofHR}");
        Console.WriteLine($"Number of total accountants:\t {Accountant.NumberOfAccountant}");
        Console.WriteLine($"Number of total doctors:\t {Doctor.NumberOfDocter}");
        Console.WriteLine($"Number of total Pharmacists:\t {Pharmacist.NumberOfPharmacist}");
        Console.WriteLine($"Number of total nurses:\t {Nurse.NumberOfNurse}");
        Console.WriteLine($"Number of total radiologists:\t {Radiologist.NumberOfRadiologist}");

        return NumberOfEmployees;
    }

    //*********************************************************************preformance reports****************************************************************
    public void WriteReport() // feutures !!! make this a work hours auto checker methods and the preformance method check the work quality of every employee
    {
        string report = string.Empty;
        while (true)
        {
            string id = "";
            Console.Write("Search by employye's full name or ID:");
            string search = Console.ReadLine();
            if (!search.Any(char.IsDigit))
            {
                if (searchByName(search) != null)
                {
                    var ThisEmployee = searchByName(search);
                    id = ThisEmployee.GetType().GetProperty("HospitalID")?.GetValue(ThisEmployee).ToString();
                }
                else
                {
                    Console.WriteLine("Employee not found! Make sure you entered the name right or that this employee does exist.");

                    Console.WriteLine("enter 0 to Exit...");
                    string exit = Console.ReadLine();
                    if (exit == "0")
                    {
                        break;
                    }

                    continue;
                }
            }
            else if (search.Any(char.IsDigit))
            {
                if (searchByID(search) != null)
                {
                    id = search;
                }
                else
                {
                    Console.WriteLine("Employee not found! Make sure you entered the id right or that this employee does exist.");

                    Console.WriteLine("enter 0 to Exit...");
                    string exit = Console.ReadLine();
                    if (exit == "0")
                    {
                        break;
                    }

                    continue;
                }
            }
            var thisEmployee = searchByID(id);

            dynamic name = thisEmployee.GetType().GetProperty("FullName")?.GetValue(thisEmployee);
            dynamic salary = thisEmployee.GetType().GetProperty("Salary")?.GetValue(thisEmployee);

            dynamic loginTime = thisEmployee.GetType().GetProperty("DailyloginTime")?.GetValue(thisEmployee);
            dynamic logoutTime = thisEmployee.GetType().GetProperty("DailylogoutTime")?.GetValue(thisEmployee);
            dynamic workhours = thisEmployee.GetType().GetProperty("WorkHours")?.GetValue(thisEmployee);

            TimeSpan elapsedTime = loginTime - logoutTime;

            // login and logout time will be added in the future!!!
            double deductionAndBounsPercentage = 0.05;

            if (elapsedTime < workhours)
            {
                dynamic warning = thisEmployee.GetType().GetProperty("Warnings")?.GetValue(thisEmployee); // warning will be added in the future!!!
                if (++warning < 3)
                {
                    thisEmployee.GetType().GetProperty("Warnings")?.SetValue(thisEmployee, ++warning);
                    report += $"{name} logged in at {loginTime}\n{name} logged out at {logoutTime}\n{name} today work hours {workhours}\nanother worning sent.\n{name} has {++warning} warnings now";
                }
                else
                {
                    thisEmployee.GetType().GetProperty("Warnings")?.SetValue(thisEmployee, 0);
                    thisEmployee.GetType().GetProperty("Bouns")?.SetValue(thisEmployee, salary * -deductionAndBounsPercentage);
                    report += $"{name} logged in at {loginTime}.\n{name} logged out at {logoutTime}.\n{name} today work hours {workhours}.\n{name} has already passed the limited warning.\n Applying deduction by 5%{salary * deductionAndBounsPercentage}\n{name} has 0 warnings now";
                }
            }
            else if (elapsedTime > workhours)
            {
                double bounsvalue = (elapsedTime - workhours) * deductionAndBounsPercentage * salary;
                thisEmployee.GetType().GetProperty("Bouns")?.SetValue(thisEmployee, bounsvalue);
                report += $"{name} logged in at {loginTime}.\n{name} logged out at {logoutTime}.\n{name} today work hours {workhours}.\n Adding bouns by 5%{bounsvalue}.";
            }
            else
            {
                report += $"{name} logged in at {loginTime}.\n{name} logged out at {logoutTime}.\n{name} today work hours {workhours}.";
            }

            var timeWritten = DateTime.Now;
            report += $"\n\n HR : {this.FullName}\nDate : {timeWritten}";
            thisEmployee.GetType().GetProperty("HRreport")?.SetValue(thisEmployee,report);
            break;
        }
    }

    //*********************************************************************search employee****************************************************************

    public static object searchByID(string id)
    {
        foreach (var joptitle in Employees)
        {
            if (Employees[joptitle.ToString()].ContainsKey(id))
                return Employees[joptitle.ToString()][id];
        }
        return null;
    }
    public static object searchByName(string name)
    {
        foreach (var joptitle in Employees)
        {
            foreach (var id in Employees[joptitle.ToString()])
            {
                if (Employees[joptitle.ToString()][id.ToString()] != null)
                {
                    object result = Employees[joptitle.ToString()][id.ToString()];

                    return result.GetType().GetProperty("FullName")?.GetValue(result) != null ? result.GetType().GetProperty("FullName")?.GetValue(result) : null;
                    //object is unknown so propertis are not accessable
                }
            }
        }
        return null;
    }
}
