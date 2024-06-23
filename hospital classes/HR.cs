using System.Drawing;
using OfficeOpenXml;

using hospitalData;

namespace hospital_classes;

public class HR : Employee, WritingReports
{
    static public Dictionary<string, Dictionary<string, object>> Employees = new Dictionary<string, Dictionary<string, object>>(); // will be used by the accountatnt to access the employees
    static private Dictionary<string, string> IDsBeckups = new Dictionary<string, string>(); // if someone forgot thier id
    //private static Dictionary<string, int> WorkSheetIndex = new Dictionary<string, int>();
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
            jopIndex = int.Parse(Console.ReadLine()!);

            if (jopIndex > 0 && jopIndex < jopTitles.Length)
            {
                break;
            }

            Console.WriteLine("Choice out of range, please select a valid choice");
            Hire();
        }
        Dictionary<string, dynamic> data = GetNewEmployeesData(jopTitles[jopIndex]);

        EmployeeData.StoreData(data, jopTitles[jopIndex]);
    }
    public Dictionary<string, dynamic> GetNewEmployeesData(string department) // get new date from employee
    {
        Dictionary<string, dynamic> Data = new Dictionary<string, dynamic>();

        Data["HospitalID"] = "";//so the id will be in the first col

        //Date from person class
        Console.Write("\n\nFirst Name : ");
        string FirstName = Console.ReadLine()!.ToUpper();

        Console.Write("Last Name : ");
        string LastName = Console.ReadLine()!.ToUpper();

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


        //Data from Employee Class
        Console.Write("Salary : ");
        Data["Salary"] = double.Parse(Console.ReadLine()!);
        Console.WriteLine();

        while (true)
        {
            try
            {
                Console.Write("Enter the Work Hours in format HH:mm:ss:\t ");
                string input = Console.ReadLine()!;

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
        Data["StartDate"] = DateOnly.Parse(Console.ReadLine()!);

        Console.Write("Experience : ");
        Data["Experience"] = int.Parse(Console.ReadLine()!);
        Console.WriteLine();

        Console.Write("Previous Experience :- ");
        Dictionary<string, string> PE = new Dictionary<string, string>();
        while (true)
        {
            Console.Write("\nEnter the name of the company (press enter if none) : ");
            string companyName = Console.ReadLine()!;
            if (companyName == "")
            {
                break;
            }
            Console.Write("Enter the job title : ");
            string jobTitle = Console.ReadLine()!;
            PE[companyName] = jobTitle;
            Data["PreviousExperience"] = PE;
        }
        Console.WriteLine();

        Data["HospitalID"] = generateID(Data["FirstName"], Data["LastName"], department);
        Console.Write($"EmployeeID : {Data["HospitalID"]}");
        Console.WriteLine();

        Console.Write("Bank : ");
        Data["BankAccount"] = Console.ReadLine()!.ToUpper();
        Console.WriteLine();

        Console.Write("Account Number : ");
        Data["AccountNumber"] = Console.ReadLine()!;
        Console.WriteLine();

        Console.Write("For doctors & Radiologist -> specialization : ");


        Console.WriteLine();

        Data["Department"] = department;

        //other prop needed
        Data["Bouns"] = 0.0;

        TimeSpan? time = null;
        Data["WorkHours"] = time;

        DateTime? date = null;
        Data["DailyLoginTime"] = date;
        Data["DailyLogoutTime"] = date;

        Data["HRreport"] = string.Empty;
        Data["SalaryReceived"] = false;
        Data["Warnings"] = 0;

        return Data;
    }

    private string generateID(string firstName, string lastName, string department)// first two letters in department + first letter from first name and last name + four random digits
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
            string id = Console.ReadLine()!.ToUpper();
            try
            {

                Dictionary<string, dynamic> ThisEmployee = searchDataByID(id);
                string name = ThisEmployee["FullName"];
                string department = ThisEmployee["Department"];

                Console.WriteLine($"!!!WARNING!!! Your about to delete {name} from the system.\n Are you sure you wanto to continue?");
                Console.WriteLine("1 - Yes");
                Console.WriteLine("2 - No");

                while (true)
                {
                    int choice = int.Parse(Console.ReadLine()!);
                    if (choice == 1)
                    {
                        EmployeeData.deleteData(id, department);
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
            catch (Exception e)
            {
                Console.WriteLine($"Exception {e.Message}");
                Console.WriteLine("Employee not found");
                Console.WriteLine("do you want to continue? (yes/no): ");

                if (Console.ReadLine()!.ToLower() == "no")
                {
                    break;
                }
            }
        } while (true);
    }

   
    //*********************************************************************Counting employee****************************************************************
    public static void numberOfEmployyes()
    {
        string excelFilePath = "D:\\codez\\uni projects\\hospital system my work\\exel files\\EmployeeData.xlsx";

        using (ExcelPackage EmployeeData = new ExcelPackage(excelFilePath))
        {
            ExcelWorksheet sheet = EmployeeData.Workbook.Worksheets["Number of Employee"];

            int colCount = sheet.Dimension.End.Column;

            for (int col = 1; col <= colCount; col++)
            {
                Console.WriteLine($"{sheet.Cells[1, col].Value.ToString()}\t:\t{sheet.Cells[2, col].Value.ToString()}");
            }

        }
    }

    //*********************************************************************preformance reports****************************************************************
    public void WriteReport() // feutures !!! make this a work hours auto checker methods and the preformance method check the work quality of every employee
    {
        string report = string.Empty;
        Dictionary<String, dynamic> ThisEmployee = new Dictionary<string, dynamic>();
        while (true)
        {
            try
            {
                ThisEmployee = searchEmployee();
                break;
            }
            catch (Exception e)
            {
                Console.WriteLine($"Exception {e.Message}");
                Console.WriteLine("Employee not found");
                Console.WriteLine("do you want to continue? (yes/no): ");
                if (Console.ReadLine()!.ToLower() == "no")
                {
                    return;
                }
            }
        }

        string name = ThisEmployee["FullName"];
        dynamic salary = ThisEmployee["Salary"];

        dynamic loginTime = ThisEmployee["DailyLoginTime"];
        dynamic logoutTime = ThisEmployee["DailyLogoutTime"];
        dynamic workhours = ThisEmployee["WorkHours"];

        if (loginTime == null)
        {
            int timeNow = DateTime.Now.Hour;
            if (timeNow > 10)
            {
                Console.WriteLine($"{name} is abscent today");
                return;
            }
        }

        if (loginTime == null)
        {
            Console.WriteLine($"{name} has not log-out yet");
            return;
        }

        TimeSpan elapsedTime = logoutTime - loginTime;

        // login and logout time will be added in the future!!!
        double deductionAndBounsPercentage;

        if (elapsedTime < workhours)
        {
            deductionAndBounsPercentage = 0.01;

            int warning;
            try
            {
                warning = ThisEmployee["Warnings"];
            }
            catch (Exception)
            {
                warning = 0;
            }

            if (warning < 3)
            {
                warning++;
                ThisEmployee["Warnings"] = warning;
                report += $"{name} logged in at {loginTime}\n{name} logged out at {logoutTime}\n{name} today work hours {elapsedTime}\nanother worning sent.\n{name} has {warning} warnings now";
            }
            else
            {
                ThisEmployee["Warnings"] = 0;
                ThisEmployee["Bouns"] = salary * -deductionAndBounsPercentage; //deduction after 3 warnengs only
                report += $"{name} logged in at {loginTime}.\n{name} logged out at {logoutTime}.\n{name} today work hours {workhours}.\n{name} has already passed the limited warning.\n Applying deduction by 5% = {salary * deductionAndBounsPercentage}\n{name} has 0 warnings now";
            }
        }
        else if (elapsedTime > workhours)
        {
            deductionAndBounsPercentage = 0.05;
            double bounsvalue = (elapsedTime - workhours) * deductionAndBounsPercentage * salary;
            ThisEmployee["Bouns"] = bounsvalue;
            report += $"{name} logged in at {loginTime}.\n{name} logged out at {logoutTime}.\n{name} today work hours {workhours}.\n Adding bouns by 5%{bounsvalue}.";
        }
        else
        {
            report += $"{name} logged in at {loginTime}.\n{name} logged out at {logoutTime}.\n{name} today work hours {workhours}.";
        }

        var timeWritten = DateTime.Now;
        report += $"\n\nHR : {FullName}\nDate : {timeWritten}";
        ThisEmployee["HRreport"] = report;

        if (!File.Exists("D:\\codez\\uni projects\\hospital system my work\\exel files\\Reports Sheet.xlsx")) EmployeeData.creatNewXLSXfile();

        // store the reoprt in the report sheet
        EmployeeData.addTodayesReport(ThisEmployee["HospitalID"], report);

        // update the report fieald in employee data which hold the latest report.
        EmployeeData.accessEmployeeExcelFile(ThisEmployee["HospitalID"], ThisEmployee["Department"], "HRreport", report);

    }


    //*********************************************************************search employee****************************************************************

    public static Dictionary<string, dynamic> searchEmployee(string id) // for the login proccess
    {
        var hr = new HR();
        return hr.searchDataByID(id.ToUpper());
    }
    public static Dictionary<string, dynamic> searchEmployee() // for inner methods in this namespace
    {
        while (true)
        {
            string id = "";
            var hr = new HR();
            Console.Write("Search by employye's full name or ID:");
            string search = Console.ReadLine()!.ToUpper();
            if (!search.Any(char.IsDigit))
            {

                var emp = hr.searchDataByName(search);
                if (emp != null)
                {
                    return emp;
                }

                Console.WriteLine("Employee not found! Make sure you entered the name right or that this employee does exist.");

                Console.Write("enter 0 to Exit 1 to continue : ");
                string exit = Console.ReadLine()!;
                if (exit == "0")
                {
                    break;
                }


            }
            else if (search.Any(char.IsDigit))
            {

                var emp = hr.searchDataByID(search);
                if (emp != null)
                {
                    return emp;
                }

                Console.WriteLine("Employee not found! Make sure you entered the id right or that this employee does exist.");

                Console.Write("enter 0 to Exit 1 to continue : ");
                string exit = Console.ReadLine()!;
                if (exit == "0")
                {
                    break;
                }

            }
            else
            {
                Console.WriteLine("Employee not found! Make sure you entered the id or full name right. Or, that this employee does exist.");

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
    private Dictionary<string, dynamic> searchDataByID(string id)
    {
        string excelFilePath = "D:\\codez\\uni projects\\hospital system my work\\exel files\\EmployeeData.xlsx";

        using (ExcelPackage package = new ExcelPackage(excelFilePath))
        {

            ExcelWorksheet sheet = null;

            bool rightID = true;

            while (rightID)
            {


                switch (id[0..2])
                {
                    case "NU":
                        sheet = package.Workbook.Worksheets["Nurse"];
                        rightID = false;
                        break;
                    case "PH":
                        sheet = package.Workbook.Worksheets["Pharmacist"];
                        rightID = false;
                        break;
                    case "RA":
                        sheet = package.Workbook.Worksheets["Radiologist"];
                        rightID = false;
                        break;
                    case "RE":
                        sheet = package.Workbook.Worksheets["Receptionist"];
                        rightID = false;
                        break;
                    case "DO":
                        sheet = package.Workbook.Worksheets["Doctor"];
                        rightID = false;
                        break;
                    case "HR":
                        sheet = package.Workbook.Worksheets["HR"];
                        rightID = false;
                        break;
                    case "AC":
                        sheet = package.Workbook.Worksheets["Accountant"];
                        rightID = false;
                        break;
                    default:
                        Console.WriteLine("Invalid Id!");
                        Console.Write("Enter a vadil one or type 'no' to stop : ");
                        id = Console.ReadLine()!;
                        if (id.ToLower() == "no")
                        {
                            return null;
                        }
                        continue;
                }
            }
            int rowCount = sheet.Dimension.End.Row;
            int colCount = sheet.Dimension.End.Column;
            for (int row = 2; row <= rowCount; row++)
            {
                string target = sheet.Cells[row, 1].Value.ToString();
                if (target.ToUpper() == id.ToUpper())
                {
                    int targetRow = row;

                    return EmployeeData.getTargetDate(targetRow, colCount, id, sheet);
                }
            }
            return null;
        }
    }

    private Dictionary<string, dynamic> searchDataByName(string name)
    {
        string excelFilePath = "D:\\codez\\uni projects\\hospital system my work\\exel files\\EmployeeData.xlsx";

        using (ExcelPackage package = new ExcelPackage(excelFilePath))
        {

            ExcelWorksheets sheets = package.Workbook.Worksheets;
            ExcelWorksheet sheet = null;

            List<Dictionary<string, dynamic>> similarNames = new List<Dictionary<string, dynamic>>();

            foreach (var item in sheets)
            {
                sheet = package.Workbook.Worksheets[item.Name];

                int rowCount = sheet.Dimension.End.Row;
                int colCount = sheet.Dimension.End.Column;
                for (int row = 2; row <= rowCount; row++)
                {
                    string target = sheet.Cells[row, 2].Value.ToString();
                    if (target.ToUpper() == name.ToUpper())
                    {
                        int targetRow = row;
                        Dictionary<string, dynamic> data = new Dictionary<string, dynamic>();

                        string id = sheet.Cells[targetRow, 1].Value.ToString();
                        data = EmployeeData.getTargetDate(targetRow, colCount, id, sheet);

                        similarNames.Add(data);
                    }
                }
            }

            if (similarNames.Count == 0)
            {
                Console.WriteLine("no employee with such name!");
                return null;
            }
            else if (similarNames.Count == 1)
            {
                return similarNames[0];
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
                    return similarNames[choice];

                }
                catch (IndexOutOfRangeException ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.WriteLine("Choose a valid choice! or enter null to return");
                }
            }
        }
    }

   

    //*********************************************************************promotion****************************************************************
    public void pormotion()
    {
        var ThisEmployee = searchEmployee();

        dynamic startDate = ThisEmployee["StartingDate"];
        TimeSpan yearsSpent = DateTime.Now.Year - startDate.Year;

        double rasieValue = yearsSpent.Days / 365 * 0.2;
        try
        {
            EmployeeData.accessEmployeeExcelFile(ThisEmployee["HospitalID"], ThisEmployee["Department"], "Salary", rasieValue);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }

    //*********************************************************************other interface methods****************************************************************

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

                Console.WriteLine(HRreport);
                return;
            }
            else if (choice == 2)
            {
                Console.WriteLine(GetHrReport(HospitalID));
                return;
            }
            else
            {
                Console.WriteLine("Plz enter a valid Choice!");
            }
        }

    }

    private string GetHrReport(string id)
    {
        Console.WriteLine("Enter the wanted date in yyyy-mm--dd");
        while (true)
        {
            Console.Write("Enter date here : ");
            string stringdate = Console.ReadLine()!;
            DateOnly? date = null;
            try
            {
                date = DateOnly.Parse(stringdate);
            }
            catch (FormatException ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine("Plz, enter the date in the right formate");
                continue;
            }
            return EmployeeData.getDataFromReportExcelFile(id, date);
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

            EmployeeData.accessEmployeeExcelFile(HospitalID, Department, "SalaryReceived", false);
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
        EmployeeData.accessEmployeeExcelFile(HospitalID, Department, "DailyLoginTime", DailyLoginTime);
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
    //*********************************************************************others****************************************************************

    public void PrintIDS()
    {

        var id = IDsBeckups.OrderBy(x => x.Value).ToList();
        Console.WriteLine("\nName\t\t\t :\t ID");
        foreach (var item in id)
        {
            if (item.Value.Length > 15)
            {
                Console.WriteLine($"{item.Value}\t :\t {item.Key}");
            }
            else
            {
                Console.WriteLine($"{item.Value}\t\t :\t {item.Key}");
            }
        }
    }
    public static void creatHR() // rouby
    {

        Dictionary<string, dynamic> data = new Dictionary<string, dynamic>();
        data["HospitalID"] = "HRAE1706";
        data["FullName"] = "Abdullah Elrouby";
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
        data["BankAccount"] = "CIB";
        data["AccountNumber"] = "1234-2345-3456-4567";
        data["Specialization"] = string.Empty;
        data["Department"] = "HR";

        //other prop needed
        data["Bouns"] = 0.0;

        TimeSpan? time = null;
        data["WorkHours"] = time;

        DateTime? date = null;
        data["DailyLoginTime"] = date;
        data["DailyLogoutTime"] = date;

        data["HRreport"] = string.Empty;
        data["SalaryReceived"] = false;
        data["Warnings"] = 0;

        EmployeeData.StoreData(data, "HR");

    }
    public static void CreatManger() // alaa
    {
        Dictionary<string, dynamic> data = new Dictionary<string, dynamic>();
        data["HospitalID"] = "MAAS1234";
        data["FullName"] = "Alaa Saleh";
        data["PhoneNumber"] = "01220200683";
        data["Age"] = 20;
        data["DateOfBirth"] = new DateOnly(2003, 9, 1);
        data["Gender"] = "Fe-Male";
        data["Statue"] = "Single";
        data["Address"] = "Egypt. Kafr-Elshikh";
        data["BloodType"] = "A+";
        data["Salary"] = 20000;
        data["WorkHours"] = new TimeSpan(8, 0, 0);
        data["StartDate"] = new DateOnly(2023, 1, 1);
        data["Experience"] = 1;
        Dictionary<string, string> PE = new Dictionary<string, string>();
        PE["the hospital"] = "Manger";
        data["PreviousExperience"] = PE;
        data["BankAccount"] = "CIB";
        data["AccountNumber"] = "1234-2345-3456-4567";
        data["Specialization"] = string.Empty;
        data["Department"] = "Manger";

        //other prop needed
        data["Bouns"] = 0.0;

        TimeSpan? time = null;
        data["WorkHours"] = time;

        DateTime? date = null;
        data["DailyLoginTime"] = date;
        data["DailyLogoutTime"] = date;

        data["HRreport"] = string.Empty;
        data["SalaryReceived"] = false;
        data["Warnings"] = 0;

        EmployeeData.StoreData(data, "Manger");

    }

    public static void CreatReceptionist() // heba
    {
        Dictionary<string, dynamic> data = new Dictionary<string, dynamic>();
        data["HospitalID"] = "REHM1234";
        data["FullName"] = "Heba-Allah Mohamed";
        data["PhoneNumber"] = "01220200683";
        data["Age"] = 23;
        data["DateOfBirth"] = new DateOnly(2003, 9, 1);
        data["Gender"] = "Fe-Male";
        data["Statue"] = "Single";
        data["Address"] = "Egypt. Behira. Edko";
        data["BloodType"] = "A+";
        data["Salary"] = 60000;
        data["WorkHours"] = new TimeSpan(8, 0, 0);
        data["StartDate"] = new DateOnly(2023, 1, 1);
        data["Experience"] = 1;
        Dictionary<string, string> PE = new Dictionary<string, string>();
        PE["the hospital"] = "Receptionist";
        data["PreviousExperience"] = PE;
        data["BankAccount"] = "CIB";
        data["AccountNumber"] = "1234-2345-3456-4567";
        data["Specialization"] = string.Empty;
        data["Department"] = "Receptionist";

        //other prop needed
        data["Bouns"] = 0.0;

        TimeSpan? time = null;
        data["WorkHours"] = time;

        DateTime? date = null;
        data["DailyLoginTime"] = date;
        data["DailyLogoutTime"] = date;

        data["HRreport"] = string.Empty;
        data["SalaryReceived"] = false;
        data["Warnings"] = 0;

        EmployeeData.StoreData(data, "Receptionist");

    }

    public static void CreatAccountant() // bakr
    {
        Dictionary<string, dynamic> data = new Dictionary<string, dynamic>();
        data["HospitalID"] = "ACMB1234";
        data["FullName"] = "Mahmoud Bakr";
        data["PhoneNumber"] = "01220200683";
        data["Age"] = 20;
        data["DateOfBirth"] = new DateOnly(2003, 9, 1);
        data["Gender"] = "Male";
        data["Statue"] = "Single";
        data["Address"] = "Egypt. Alexandria. Borj Al-arab";
        data["BloodType"] = "A+";
        data["Salary"] = 90000;
        data["WorkHours"] = new TimeSpan(8, 0, 0);
        data["StartDate"] = new DateOnly(2023, 1, 1);
        data["Experience"] = 1;
        Dictionary<string, string> PE = new Dictionary<string, string>();
        PE["the hospital"] = "Accountant";
        data["PreviousExperience"] = PE;
        data["BankAccount"] = "CIB";
        data["AccountNumber"] = "1234-2345-3456-4567";
        data["Specialization"] = string.Empty;
        data["Department"] = "Accountant";

        //other prop needed
        data["Bouns"] = 0.0;

        TimeSpan? time = null;
        data["WorkHours"] = time;

        DateTime? date = null;
        data["DailyLoginTime"] = date;
        data["DailyLogoutTime"] = date;

        data["HRreport"] = string.Empty;
        data["SalaryReceived"] = false;
        data["Warnings"] = 0;

        EmployeeData.StoreData(data, "Accountant");


    }
    public static void CreatDoctor() // menna
    {
        Dictionary<string, dynamic> data = new Dictionary<string, dynamic>();
        data["HospitalID"] = "DOMR1234";
        data["FullName"] = "Menna-Allah Ragab";
        data["PhoneNumber"] = "01220200683";
        data["Age"] = 20;
        data["DateOfBirth"] = new DateOnly(2003, 9, 1);
        data["Gender"] = "Fe-Male";
        data["Statue"] = "Single";
        data["Address"] = "Egypt. Behira. Al-noubaria";
        data["BloodType"] = "A+";
        data["Salary"] = 15000;
        data["WorkHours"] = new TimeSpan(8, 0, 0);
        data["StartDate"] = new DateOnly(2023, 1, 1);
        data["Experience"] = 1;
        Dictionary<string, string> PE = new Dictionary<string, string>();
        PE["the hospital"] = "Doctor";
        data["PreviousExperience"] = PE;
        data["BankAccount"] = "CIB";
        data["AccountNumber"] = "1234-2345-3456-4567";
        data["Specialization"] = string.Empty;
        data["Department"] = "Doctor";

        //other prop needed
        data["Bouns"] = 0.0;

        TimeSpan? time = null;
        data["WorkHours"] = time;

        DateTime? date = null;
        data["DailyLoginTime"] = date;
        data["DailyLogoutTime"] = date;

        data["HRreport"] = string.Empty;
        data["SalaryReceived"] = false;
        data["Warnings"] = 0;

        EmployeeData.StoreData(data, "Doctor");

    }
    public static void CreatPharmacist() // faten
    {
        Dictionary<string, dynamic> data = new Dictionary<string, dynamic>();
        data["HospitalID"] = "PHFM1234";
        data["FullName"] = "Faten Mohamed";
        data["LastName"] = "";
        data["PhoneNumber"] = "01220200683";
        data["Age"] = 20;
        data["DateOfBirth"] = new DateOnly(2003, 9, 1);
        data["Gender"] = "Fe-Male";
        data["Statue"] = "Single";
        data["Address"] = "Egypt. Behira. Kom-Hamada";
        data["BloodType"] = "A+";
        data["Salary"] = 11000;
        data["WorkHours"] = new TimeSpan(8, 0, 0);
        data["StartDate"] = new DateOnly(2023, 1, 1);
        data["Experience"] = 1;
        Dictionary<string, string> PE = new Dictionary<string, string>();
        PE["the hospital"] = "Pharmacist";
        data["PreviousExperience"] = PE;
        data["BankAccount"] = "CIB";
        data["AccountNumber"] = "1234-2345-3456-4567";
        data["Specialization"] = string.Empty;
        data["Department"] = "Pharmacist";

        //other prop needed
        data["Bouns"] = 0.0;

        TimeSpan? time = null;
        data["WorkHours"] = time;

        DateTime? date = null;
        data["DailyLoginTime"] = date;
        data["DailyLogoutTime"] = date;

        data["HRreport"] = string.Empty;
        data["SalaryReceived"] = false;
        data["Warnings"] = 0;

        EmployeeData.StoreData(data, "Pharmacist");

    }
    public static void CreatNurse() // radwa
    {
        Dictionary<string, dynamic> data = new Dictionary<string, dynamic>();
        data["HospitalID"] = "NURM1234";
        data["FullName"] = "Radwa Mohsen";
        data["PhoneNumber"] = "01220200683";
        data["Age"] = 20;
        data["DateOfBirth"] = new DateOnly(2003, 9, 1);
        data["Gender"] = "Fe-Male";
        data["Statue"] = "Single";
        data["Address"] = "Egypt. Behira. Damanhor";
        data["BloodType"] = "A+";
        data["Salary"] = 12500;
        data["WorkHours"] = new TimeSpan(8, 0, 0);
        data["StartDate"] = new DateOnly(2023, 1, 1);
        data["Experience"] = 1;
        Dictionary<string, string> PE = new Dictionary<string, string>();
        PE["the hospital"] = "Nurse";
        data["PreviousExperience"] = PE;
        data["BankAccount"] = "CIB";
        data["AccountNumber"] = "1234-2345-3456-4567";
        data["Specialization"] = string.Empty;
        data["Department"] = "Nurse";

        //other prop needed
        data["Bouns"] = 0.0;

        TimeSpan? time = null;
        data["WorkHours"] = time;

        DateTime? date = null;
        data["DailyLoginTime"] = date;
        data["DailyLogoutTime"] = date;

        data["HRreport"] = string.Empty;
        data["SalaryReceived"] = false;
        data["Warnings"] = 0;

        EmployeeData.StoreData(data, "Nurse");

    }
    public static void CreatRadiologist() // sara
    {
        Dictionary<string, dynamic> data = new Dictionary<string, dynamic>();
        data["HospitalID"] = "RASK1234";
        data["FullName"] = "Sara Khamees";
        data["PhoneNumber"] = "01220200683";
        data["Age"] = 20;
        data["DateOfBirth"] = new DateOnly(2003, 9, 1);
        data["Gender"] = "Fe-Male";
        data["Statue"] = "Single";
        data["Address"] = "Egypt. Behira. Damanhor";
        data["BloodType"] = "A+";
        data["Salary"] = 14000;
        data["WorkHours"] = new TimeSpan(8, 0, 0);
        data["StartDate"] = new DateOnly(2023, 1, 1);
        data["Experience"] = 1;
        Dictionary<string, string> PE = new Dictionary<string, string>();
        PE["the hospital"] = "Radiologist";
        data["PreviousExperience"] = PE;
        data["BankAccount"] = "CIB";
        data["AccountNumber"] = "1234-2345-3456-4567";
        data["Specialization"] = string.Empty;
        data["Department"] = "Radiologist";

        //other prop needed
        data["Bouns"] = 0.0;

        TimeSpan? time = null;
        data["WorkHours"] = time;

        DateTime? date = null;
        data["DailyLoginTime"] = date;
        data["DailyLogoutTime"] = date;

        data["HRreport"] = string.Empty;
        data["SalaryReceived"] = false;
        data["Warnings"] = 0;

        EmployeeData.StoreData(data, "Radiologist");

    }
}
