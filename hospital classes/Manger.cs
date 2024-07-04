using hospitalData;

namespace hospital_classes;

public class Manger : Employee, WritingReports
{
    public Manger() { }
    public Manger(Dictionary<string, dynamic> data) : base(data) { }




    //********************************************** writing reoprorts and reviews********************************************


    public static void WriteReportAndReviewToManger(dynamic ThisEmployee)
    {
        string name = ThisEmployee.FullName;
        string id = ThisEmployee.HospitalID;
        string contentType = string.Empty;

        while (true)
        {

            Console.WriteLine("For writing a report press 1\nFor  writing a review press 2");
            Console.Write("Choose : ");

            int choice = int.Parse(Console.ReadLine()!);
            if (choice == 1)
            {
                contentType = "Report";
                break;
            }
            else if (choice == 2)
            {
                contentType = "Review";
                break;
            }
            else
            {
                Console.WriteLine("Invalid Choice!");
            }

        }
        Console.WriteLine($"Plz, Enter the {contentType}'s Title");
        string title = Console.ReadLine()!.ToUpper();

        Console.WriteLine($"Plz, Enter the {contentType}");
        string Body = Console.ReadLine()!;

        string Date = DateTime.Now.ToString();

        string report = $"{title}\n{Body}\n\nBy : {name}\nID : {id}\nDate : {Date}";

        MangerReportsAndReviewsFile.accessMangerReportsAndReviewsFile(contentType, title, report, Date);
    }

    //********************************************** reviewing reoprorts and reviews********************************************

    public void approveReportsAndReviews()
    {
        int choice = 0;
        string contentType = string.Empty;
        string contentStatue = string.Empty;


        Console.WriteLine("1. Reports\n2. Reviews");
        while (true) // make sure its a avlid choice
        {
            Console.Write("\nYour Choice : ");
            choice = int.Parse(Console.ReadLine()!);
            if (choice == 1)
            {
                contentType = "Report";
                break;
            }
            else if (choice == 2)
            {
                contentType = "Review";
                break;
            }
            else
            {
                Console.WriteLine("plz, enter a valid choice!");
            }
        }
            Console.WriteLine("1. Accepted\n2. Denied\n3. Not set.\n4. All");

            while (true) // make sure its a avlid choice
            {
                Console.Write("\nYour Choice : ");
                choice = int.Parse(Console.ReadLine()!);
                if (choice < 1 || choice > 4)
                {
                    Console.WriteLine("plz, enter a valid choice!");
                    continue;
                }

                break;
            }

            switch (choice)
            {
                case 1:
                    contentStatue = "Accepted";
                    break;
                case 2:
                    contentStatue = "Denied";
                    break;
                case 3:
                    contentStatue = "NS";
                    break;
                case 4:
                    contentStatue = "All";
                    break;
            }

            MangerReportsAndReviewsFile.accessMangerReportsAndReviewsFile(contentType, contentStatue);
    }

    //*********************************************************************interface methods****************************************************************

    public void WriteReport() { }
    public void PrintHRreport() // no hr report for the manger
    {
        //Console.WriteLine(HRreport);
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