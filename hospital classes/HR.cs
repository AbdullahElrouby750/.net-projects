using System.Runtime.CompilerServices;
using OfficeOpenXml;

namespace hospital_classes;

public class HR : Employee, WritingReports
{
    static public Dictionary<string, Dictionary<string, object>> Employees = new Dictionary<string, Dictionary<string, object>>(); // will be used by the accountatnt to access the employees
    static private Dictionary<string, string> IDsBeckups = new Dictionary<string, string>(); // if someone forgot thier id
    private Dictionary<string,int> WorkSheetIndex = new Dictionary<string, int>();
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

        StoreData(data, jopTitles[jopIndex]);
    }
    public Dictionary<string, dynamic> GetNewEmployeesData(string department)
    {
        Dictionary<string, dynamic> Data = new Dictionary<string, dynamic>();

        Data["HospitalID"] = "";//so the id will be in the first col

        //Date from person class
        Console.Write("\n\nFirst Name : ");
        string FirstName = Console.ReadLine().ToUpper();

        Console.Write("Last Name : ");
        string LastName = Console.ReadLine().ToUpper();

        Data["FullName"] = FirstName + " " + LastName;

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
        Data["BankAccount"] = Console.ReadLine().ToUpper();
        Console.WriteLine();

        Console.Write("Account Number : ");
        Data["AccountNumber"] = Console.ReadLine();
        Console.WriteLine();

        Console.Write("For doctors & Radiologist -> specialization : ");


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

    //*********************************************************************Store data****************************************************************

    public void StoreData(Dictionary<string, dynamic> data, string department)
    {
     string excelFilePath = "D:\\codez\\uni projects\\hospital system my work\\exel files\\EmployeeData.xlsx";

        using (ExcelPackage EmployeeData = new ExcelPackage(excelFilePath))
        {
            ExcelWorksheet sheet;
            int col = 1;
            bool firstTime = true;
            if (!WorkSheetIndex.ContainsKey(department))
            {
                sheet = EmployeeData.Workbook.Worksheets.Add(department);
                WorkSheetIndex[department] = WorkSheetIndex.Count;

                foreach (var key in data)
                {
                    sheet.Cells[1, col].Value = key.Key;
                    col++;
                }
            }
            else
            {
                firstTime = false;
                sheet = EmployeeData.Workbook.Worksheets[WorkSheetIndex[department]];
            }

            int row = sheet.Dimension.Rows + 1;
            foreach (var value in data)
            {
                sheet.Cells[row, col].Value = value;
                col++;
            }
            if (firstTime)
            {
                EmployeeData.SaveAs(excelFilePath);
            }
            else
            {
                EmployeeData.Save();
            }
        }
    }

    //*********************************************************************Firing proccesses****************************************************************

    public void Fire()
    {

        do
        {
            Console.Write($"\n\nEnter employee's ID : ");
            string id = Console.ReadLine().ToUpper();
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
                    int choice = int.Parse(Console.ReadLine());
                    if (choice == 1)
                    {
                        deleteData(id, department);
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

                if (Console.ReadLine().ToLower() == "no")
                {
                    break;
                }
            }
        } while (true);
    }

    private bool deleteData(string id, string department)
    {
        string excelFilePath = "D:\\codez\\uni projects\\hospital system my work\\exel files\\EmployeeData.xlsx";

        using (ExcelPackage package = new ExcelPackage(excelFilePath))
        {
            ExcelWorksheet sheet = package.Workbook.Worksheets[WorkSheetIndex[department]];

            int rowConut = sheet.Dimension.Rows;
            int colCount = sheet.Dimension.Columns;
            for (int row = 2; row <= rowConut; row++)
            {
                string target = sheet.Cells[row, 1].Value.ToString();
                if (target.ToUpper() == id.ToUpper())
                {
                    int targetRow = row;
                    Dictionary<string, dynamic> data = new Dictionary<string, dynamic>();
                    try
                    {
                        sheet.DeleteRow(targetRow);
                        return true;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
            }
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
        object ThisEmployee;
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
                if (Console.ReadLine().ToLower() == "no")
                {
                    return;
                }
            }
        }


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

    public static Dictionary<string, dynamic> searchEmployee(string id) // for the login proccess
    {
        var hr = new HR();
        return hr.searchDataByID(id.ToUpper());
    }
    public static Dictionary<string,dynamic> searchEmployee()
    {
        while (true)
        {
            string id = "";
            var hr = new HR();
            Console.Write("Search by employye's full name or ID:");
            string search = Console.ReadLine().ToUpper();
            if (!search.Any(char.IsDigit))
            {

                var emp = hr.searchDataByName(search);
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

                var emp = hr.searchDataByID(search);
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
    private Dictionary<string, dynamic> searchDataByID(string id)
    {
        string excelFilePath = "D:\\codez\\uni projects\\hospital system my work\\exel files\\EmployeeData.xlsx";

        using (ExcelPackage package = new ExcelPackage(excelFilePath))
        {

            ExcelWorksheet sheet = null;

            bool rightID = true;

            while (rightID)
            {


                switch (id[0..2].ToLower())
                {
                    case "nu":
                        sheet = package.Workbook.Worksheets["Nurse"];
                        rightID = false;
                        break;
                    case "ph":
                        sheet = package.Workbook.Worksheets["Pharmacist"];
                        rightID = false;
                        break;
                    case "ra":
                        sheet = package.Workbook.Worksheets["Radiologist"];
                        rightID = false;
                        break;
                    case "re":
                        sheet = package.Workbook.Worksheets["Receptionist"];
                        rightID = false;
                        break;
                    case "do":
                        sheet = package.Workbook.Worksheets["Doctor"];
                        rightID = false;
                        break;
                    case "hr":
                        sheet = package.Workbook.Worksheets["HR"];
                        rightID = false;
                        break;
                    case "ac":
                        sheet = package.Workbook.Worksheets["Accountant"];
                        rightID = false;
                        break;
                    default:
                        Console.WriteLine("Invalid Id!");
                        Console.Write("Enter a vadil one or type 'no' to stop : ");
                        id = Console.ReadLine();
                        if (id.ToLower() == "no")
                        {
                            return null;
                        }
                        continue;
                }
            }
            int rowCount = sheet.Dimension.Rows;
            int colCount = sheet.Dimension.Columns;
            for (int row = 2; row <= rowCount; row++)
            {
                string target = sheet.Cells[row, 1].Value.ToString();
                if (target.ToUpper() == id.ToUpper())
                {
                    int targetRow = row;
                    Dictionary<string, dynamic> data = new Dictionary<string, dynamic>();
                    for (int col = 1; col <= colCount; col++)
                    {
                        string field = sheet.Cells[1, col].Value.ToString();
                        dynamic val = sheet.Cells[targetRow, col].Value;
                        data[field] = val;
                    }
                    return data;
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

            ExcelWorksheet sheet = null;

            List<Dictionary<string, dynamic>> similarNames = new List<Dictionary<string, dynamic>>();

            foreach (var item in WorkSheetIndex)
            {
                sheet = package.Workbook.Worksheets[item.Value];

                int rowCount = sheet.Dimension.Rows;
                int colCount = sheet.Dimension.Columns;
                for (int row = 2; row <= rowCount; row++)
                {
                    string target = sheet.Cells[row, 2].Value.ToString();
                    if (target.ToUpper() == name.ToUpper())
                    {
                        int targetRow = row;
                        Dictionary<string, dynamic> data = new Dictionary<string, dynamic>();
                        for (int col = 1; col <= colCount; col++)
                        {
                            string field = sheet.Cells[1, col].Value.ToString();
                            dynamic val = sheet.Cells[targetRow, col].Value;
                            data[field] = val;
                        }
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
                    int choice = int.Parse(Console.ReadLine());
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
        data["BankAccount"] = "CIB";
        data["AccountNumber"] = "1234-2345-3456-4567";
        data["Specialization"] = string.Empty;
        data["Department"] = "HR";


        var rouby = new HR();
        rouby.StoreData(data, "HR");

        //Dictionary<string, object> r = new Dictionary<string, object>();
        //r[rouby.HospitalID] = rouby;
        //Employees["HR"] = r;
        //IDsBeckups[rouby.HospitalID] = rouby.FullName;

        // Employees["HR"][rouby.HospitalID] = rouby;
    }
    public static void CrearManger() // alaa
    {
        Dictionary<string, dynamic> data = new Dictionary<string, dynamic>();
        data["HospitalID"] = "MAAS1234";
        data["FirstName"] = "Alaa";
        data["LastName"] = "Saleh";
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
        data["Department"] = "Manager";

        var rouby = new HR();
        rouby.StoreData(data, "Manager");

        //var alaa = new Manger(data);
        //Dictionary<string, object> s = new Dictionary<string, object>();
        //s[alaa.HospitalID] = alaa;
        //Employees["Manager"] = s;
        //IDsBeckups[alaa.HospitalID] = alaa.FullName;
    }

    public static void CreatReceptionist() // heba
    {
        Dictionary<string, dynamic> data = new Dictionary<string, dynamic>();
        data["HospitalID"] = "REHM1234";
        data["FirstName"] = "Heba-Allah";
        data["LastName"] = "Mohamed";
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

        var rouby = new HR();
        rouby.StoreData(data, "Receptionist");

        //var heba = new Receptionist(data);
        //Dictionary<string, object> h = new Dictionary<string, object>();
        //h[heba.HospitalID] = heba;
        //Employees["Receptionist"] = h;
        //IDsBeckups[heba.HospitalID] = heba.FullName;
    }

    public static void CreatAccountant() // bakr
    {
        Dictionary<string, dynamic> data = new Dictionary<string, dynamic>();
        data["HospitalID"] = "ACMB1234";
        data["FirstName"] = "Mahmoud";
        data["LastName"] = "Bakr";
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

        var rouby = new HR();
        rouby.StoreData(data, "Accountant");

        //var mahmoud = new Accountant(data);
        //Dictionary<string, object> m = new Dictionary<string, object>();
        //m[mahmoud.HospitalID] = mahmoud;
        //Employees["Accountant"] = m;
        //IDsBeckups[mahmoud.HospitalID] = mahmoud.FullName;
    }
    public static void CreatDoctor() // menna
    {
        Dictionary<string, dynamic> data = new Dictionary<string, dynamic>();
        data["HospitalID"] = "DOMR1234";
        data["FirstName"] = "Menna-Allah";
        data["LastName"] = "Ragab";
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

        var rouby = new HR();
        rouby.StoreData(data, "Doctor");

        //var menna = new Doctor(data);
        //Dictionary<string, object> m = new Dictionary<string, object>();
        //m[menna.HospitalID] = menna;
        //Employees["Doctor"] = m;
        //IDsBeckups[menna.HospitalID] = menna.FullName;
    }
    public static void CreatPharmacist() // faten
    {
        Dictionary<string, dynamic> data = new Dictionary<string, dynamic>();
        data["HospitalID"] = "PHFM1234";
        data["FirstName"] = "Faten";
        data["LastName"] = "Mohamed";
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

        var rouby = new HR();
        rouby.StoreData(data, "Pharmacist");

        //var faten = new Pharmacist(data);
        //Dictionary<string, object> f = new Dictionary<string, object>();
        //f[faten.HospitalID] = faten;
        //Employees["Pharmacist"] = f;
        //IDsBeckups[faten.HospitalID] = faten.FullName;
    }
    public static void CreatNurse() // radwa
    {
        Dictionary<string, dynamic> data = new Dictionary<string, dynamic>();
        data["HospitalID"] = "NURM1234";
        data["FirstName"] = "Radwa";
        data["LastName"] = "Mohsen";
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

        var rouby = new HR();
        rouby.StoreData(data, "Nurse");

        //var radwa = new Nurse(data);
        //Dictionary<string, object> r = new Dictionary<string, object>();
        //r[radwa.HospitalID] = radwa;
        //Employees["Nurse"] = r;
        //IDsBeckups[radwa.HospitalID] = radwa.FullName;
    }
    public static void CreatRadiologist() // sara
    {
        Dictionary<string, dynamic> data = new Dictionary<string, dynamic>();
        data["HospitalID"] = "RASK1234";
        data["FirstName"] = "Sara";
        data["LastName"] = "Khamees";
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

        var rouby = new HR();
        rouby.StoreData(data, "Radiologist");

        //var sara = new Radiologist(data);
        //Dictionary<string, object> s = new Dictionary<string, object>();
        //s[sara.HospitalID] = sara;
        //Employees["Radiologist"] = s;
        //IDsBeckups[sara.HospitalID] = sara.FullName;
    }
}
