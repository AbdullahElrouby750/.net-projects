using hospital_classes;
internal partial class Start
{
    private static bool HRWorkSpace(string id)
    {
        var data = HR.searchEmployee(id);
        if (data != null)
        {
            HR hr = new HR(data);
            Console.WriteLine("Working on it ;)");
            Thread.Sleep(1000); // wait for a moment before printing the messages
            while (true)
            {


                Console.WriteLine("\nChoose a jop from the list below :-\n");
                Console.WriteLine("1. Login");
                Console.WriteLine("2. Logout");
                Console.WriteLine("3. Print your Salary");
                Console.WriteLine("4. Print HR report");
                Console.WriteLine("5. Write a Report about an employee");
                Console.WriteLine("6. Write a report to the manger");
                Console.WriteLine("7. Write a review to the manger");
                Console.WriteLine("8. Apply a pormotion");
                Console.WriteLine("9. Hire");
                Console.WriteLine("10. Fire");
                Console.WriteLine("11. Print ID list");
                Console.WriteLine("12. Exit\n\n");

                Console.Write("Enter your choice : ");
                int choice = int.Parse(Console.ReadLine()!);
                switch (choice)
                {
                    case 1:
                        hr.login();
                        pressEnterToContinue();
                        break;
                    case 2:
                        hr.logout();
                        pressEnterToContinue();
                        break;
                    case 3:
                        hr.Printsalary();
                        pressEnterToContinue();
                        break;
                    case 4:
                        hr.PrintHRreport();
                        pressEnterToContinue();
                        break;
                    case 5:
                        hr.WriteReport();
                        pressEnterToContinue();
                        break;
                    case 6:
                        // WriteReportToManger();
                        // pressEnterToContinue();
                        break;
                    case 7:
                        // WriteReview();
                        // pressEnterToContinue();
                        break;
                    case 8:
                        hr.pormotion();
                        pressEnterToContinue();
                        break;
                    case 9:
                        hr.Hire();
                        pressEnterToContinue();
                        break;
                    case 10:
                        hr.Fire();
                        pressEnterToContinue();
                        break;
                    case 11:
                        hr.PrintIDS();
                        pressEnterToContinue();
                        break;
                    case 12:
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
        return true;
    }

}