using hospital_classes;

internal partial class Start
{
    private static bool AccountantWorkSpace(string id)
    {
        var data = HR.searchEmployee(id);
        if(data!= null)
        {
            Accountant accountant = new Accountant(data);
            while (true)
            {
                Console.WriteLine("\nChoose a jop from the list below :-\n");
                Console.WriteLine("1. Login");
                Console.WriteLine("2. Logout");
                Console.WriteLine("3. Print your Salary");
                Console.WriteLine("4. Print HR report");
                Console.WriteLine("5. Write a report or a review to the manger");
                Console.WriteLine("6. send Salary to employees");
                Console.WriteLine("7. Exit\n\n");

                Console.Write("Enter your choice : ");
                int choice = int.Parse(Console.ReadLine()!);

                switch (choice)
                {
                    case 1:
                        accountant.login();
                        pressEnterToContinue();
                        break;
                    case 2:
                        accountant.logout();
                        pressEnterToContinue();
                        break;
                    case 3:
                        accountant.Printsalary();
                        pressEnterToContinue();
                        break;
                    case 4:
                        accountant.PrintHRreport();
                        pressEnterToContinue();
                        break;
                    case 5:
                        Manger.WriteReportAndReviewToManger(accountant);
                        pressEnterToContinue();
                        break;
                    case 6:
                        accountant.sendSalary();
                        pressEnterToContinue();
                        break;
                    case 7:
                        return true;
                    default:
                        Console.WriteLine("\nPlease enter a valid choice");
                        continue;
                }
            }
        }
        else
        {
            Console.Write($"\n\n{id} is invalid. plz, enter a valid one: ");
            return false;
        }
    }
    
}