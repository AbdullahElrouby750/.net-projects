using hospital_classes;
internal partial class Start
{
    private static bool HRWorkSpace(string id)
    {
        var data = HR.searchEmployee(id);
        if (data != null)
        {
            HR hr = new HR(data);
            Console.WriteLine($"Welcome back! {hr.FullName}");
            Console.WriteLine("Working on it ;)");
            while (true)
            {


                Console.WriteLine("\nChoose a jop from the list below :-\n");
                Console.WriteLine("1. Login");
                Console.WriteLine("2. Logout");
                Console.WriteLine("3. Print your Salary");
                Console.WriteLine("4. Print HR report");
                Console.WriteLine("5. Write a Report about an employee");
                Console.WriteLine("6. Write a report or a review to the manger");
                Console.WriteLine("7. Apply a pormotion");
                Console.WriteLine("8. Hire");
                Console.WriteLine("9. Fire");
                Console.WriteLine("10. Print ID list");
                Console.WriteLine("11. Exit\n\n");

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
                        Manger.WriteReportAndReviewToManger(hr);
                        pressEnterToContinue();
                        break;
                    case 7:
                        hr.pormotion();
                        pressEnterToContinue();
                        break;
                    case 8:
                        hr.Hire();
                        pressEnterToContinue();
                        break;
                    case 9:
                        hr.Fire();
                        pressEnterToContinue();
                        break;
                    case 10:
                        hr.PrintIDS();
                        pressEnterToContinue();
                        break;
                    case 11:
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