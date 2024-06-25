namespace hospital_classes;

public class Manger : Employee, WritingReports
{
    private static Dictionary<string, string> EmployeesReviews = new Dictionary<string, string>();
    private static Dictionary<string, string> EmployeesReports = new Dictionary<string, string>();
    private List<string> approvedFiles = new List<string>();
    public Manger() {}
    public Manger(Dictionary<string, dynamic> data) : base(data)
    {
        Console.WriteLine("hi Boss,how are you today ");
    }




    //********************************************** writing reoprorts and reviews********************************************


    public static void WriteReportToManger(dynamic ThisEmployee)
    {
        string name = ThisEmployee.FullName;
        string id = ThisEmployee.HospitalID;

        Console.WriteLine("Plz, Enter the report's Title");
        string title = Console.ReadLine()!.ToUpper();

        Console.WriteLine("Plz, Enter the report");
        string reportBody = Console.ReadLine()!;

        string reportDate = DateTime.Now.ToString();

        string report = $"\t\t\t\t{title}\n{reportBody}\n\nBy : {name}\nID : {id}\nDate : {reportDate}";

        EmployeesReports[title] = report;
    }


    public static void WriteReview(dynamic ThisEmployee)
    {
        string name = ThisEmployee.FullName;
        string id = ThisEmployee.HospitalID;

        Console.WriteLine("Plz, Enter the review's Title");
        string title = Console.ReadLine()!.ToUpper();

        Console.WriteLine("Please enter your feedback:");
        string feedback = Console.ReadLine();

        Console.WriteLine($"Feedback received successfully");
        string reviewDate = DateTime.Now.ToString();

        string message = "Thank you for writing to us and we promise to work to solve the problem.";

        string review = $"\t\t\t\t{title}\n{feedback}\n\nBy : {name}\nID : {id}\nDate : {reviewDate}";

        EmployeesReviews[title] = review;
    }

    //********************************************** reviewing reoprorts and reviews********************************************

    public void approveReports()
    {
        if (EmployeesReports.Count == 0)
        {
            Console.WriteLine("No reports available for approval.");
            return;
        }

        Console.WriteLine("Here you will see a list of the most recent reports that require your approval:");
        int index = 1;
        foreach (var report in EmployeesReports)
        {
            Console.WriteLine($"{index}. Title: {report.Key}\n Report: {report.Value}");
            index++;
        }

        Console.WriteLine("Please choose the report number you want to approve or reject:");
        int choice = 0;
        while (true)
        {
            choice = int.Parse(Console.ReadLine());
            Console.WriteLine();

            if (choice < 1 || choice > EmployeesReviews.Count)
            {
                Console.WriteLine("Invalid choice.");

                Console.Write("Enter  valid Choice : ");
            }
            else
            {
                break;
            }

        }

        var selectedReport = EmployeesReports.ElementAt(choice - 1);
        Console.WriteLine($"You selected the report titled: {selectedReport.Key}");
        Console.WriteLine("Please choose one of the following options:");
        Console.WriteLine("1. Approve the report");
        Console.WriteLine("2. Reject the report");

        Console.Write("Enter Choice : ");
        int action = int.Parse(Console.ReadLine());
        Console.WriteLine(); // one line down
        while (true)
        {

            if (action == 1)
            {
                Console.WriteLine("You chose to approve the report.");
                approvedFiles.Add(selectedReport.Key);
                EmployeesReports.Remove(selectedReport.Key);
                return;
            }
            else if (action == 2)
            {
                Console.WriteLine("You chose to reject the report.");
                EmployeesReports.Remove(selectedReport.Key);
                return;
            }
            else
            {
                Console.WriteLine("Invalid choice.");
                Console.Write("Enter valid Choice : ");
            }

        }

    }


    public void ReadReviews() // same as reports
    {
        if (EmployeesReviews.Count == 0)
        {
            Console.WriteLine("No reports available for approval.");
            return;
        }

        Console.WriteLine("Here you will see a list of the most recent reports that require your approval:");
        int index = 1;
        foreach (var report in EmployeesReviews)
        {
            Console.WriteLine($"{index}. Title: {report.Key}\n Report: {report.Value}");
            index++;
        }

        Console.Write("Please choose the review number you want to read :");
        int choice = 0;
        while (true)
        {
            choice = int.Parse(Console.ReadLine());
            Console.WriteLine();

            if (choice < 1 || choice > EmployeesReviews.Count)
            {
                Console.WriteLine("Invalid choice.");

                Console.Write("Enter  valid Choice : ");
            }
            else
            {
                break;
            }
        }
        var selectedReport = EmployeesReviews.ElementAt(choice - 1);
        Console.WriteLine($"You selected the report titled: {selectedReport.Key}");
        Console.WriteLine("Please choose one of the following options:");
        Console.WriteLine("1. Approve the review");
        Console.WriteLine("2. Reject the review");

        Console.Write("Enter Choice : ");
        while (true)
        {
            int action = int.Parse(Console.ReadLine());
            Console.WriteLine(); // one line down

            if (action == 1)
            {
                Console.WriteLine("You chose to approve the review.");
                approvedFiles.Add(selectedReport.Key);
                EmployeesReviews.Remove(selectedReport.Key);
                return;
            }
            else if (action == 2)
            {
                Console.WriteLine("You chose to reject the review.");
                EmployeesReviews.Remove(selectedReport.Key);
                return;
            }
            else
            {
                Console.WriteLine("Invalid choice.");
                Console.Write("Enter valid Choice : ");
            }

        }

    }


    //*********************************************************************interface methods****************************************************************

    public void WriteReport() { }
    public void PrintHRreport() // no hr report for the manger
    {
        //Console.WriteLine(HRreport);
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