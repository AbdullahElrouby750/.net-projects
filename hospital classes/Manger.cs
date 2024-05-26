using System.Reflection.Emit;
namespace hospital_classes;

public class Manger : Employee, WritingReports
{
    List<string> approvedFiles; // added
    public Manger()
    {
        approvedFiles = new List<string>();

    }
    public Manger(Dictionary<string, dynamic> data) : base(data)
    {
        Console.WriteLine("hi Boss,how are you today ");
        approvedFiles = new List<string>();                 // added

    }

    // public string Message(int num)
    // {
    //     Console.WriteLine("Choose an option:");
    //     Console.WriteLine("1. Approve List");
    //     Console.WriteLine("2. Review List");

    //     string input = Console.ReadLine();

    //     if (input == "1")
    //     {

    //         return approve();
    //     }
    //     else if (input == "2")
    //     {

    //         return review();
    //     }
    //     else
    //     {
    //         return "Invalid choice!";
    //     }
    // }

    public bool approve(string file)
    {
        Console.WriteLine("Here you will see a list of the most recent messages that require your approval");
        string newMessage = file;
        Console.WriteLine($"this is last message{newMessage}");
        Console.WriteLine("Please choose one of the following options:");
        Console.WriteLine("1. If you agree with the decision taken");
        Console.WriteLine("2. If you do not agree with the decision taken");
        int choice = int.Parse(Console.ReadLine());
        switch (choice)
        {
            case 1:
                Console.WriteLine("You chose to approve the request.");
                approvedFiles.Add(newMessage);
                return true;
            case 2:
                Console.WriteLine("You chose to reject the request.");

               return false;
            default:
                Console.WriteLine("Invalid choice.");
              return approve(file);
        }
    }

    public void review(string feedback)
    {
        Console.WriteLine($"Feedback received: {feedback}");
        string massage = "Thank you for writing to us and we promise to work to solve the problem ";
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
            int Salary = 0;
            int Bouns = 0;
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