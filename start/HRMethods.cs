using hospital_classes;

internal partial class Start
{
    private static bool HRWorkSpace(string id)
    {
        if (HR.Employees["HR"].ContainsKey(id))
        {
            if (HR.searchEmployee(id) is HR hr)
            {
                Console.WriteLine("Choose a jop from the list below :-\n");
                Console.WriteLine("1. Login");
                Console.WriteLine("2. Logout");
                Console.WriteLine("3. Print your Salary");
                Console.WriteLine("4. Print HR report");
                Console.WriteLine("5. Write a Report about an employee");
                Console.WriteLine("7. Write a report to the manger");
                Console.WriteLine("8. Write a review to the manger");
                Console.WriteLine("9. Apply a pormotion");
                Console.WriteLine("10. Hire");
                Console.WriteLine("11. Fire");
                Console.WriteLine("12. Exit");

                while (true)
                {
                    int choice = int.Parse(Console.ReadLine());
                    switch (choice)
                    {
                        case 1:
                            hr.login();
                            if (continueOrStop())
                            {
                                continue;
                            }
                            break;
                        case 2:
                            hr.logout();
                            if (continueOrStop())
                            {
                                continue;
                            }
                            break;
                        case 3:
                            hr.Printsalary();
                            if (continueOrStop())
                            {
                                continue;
                            }
                            break;
                        case 4:
                            hr.PrintHRreport();
                            if (continueOrStop())
                            {
                                continue;
                            }
                            break;
                        case 5:
                            hr.WriteReport();
                            if (continueOrStop())
                            {
                                continue;
                            }
                            break;
                        case 7:
                            WriteReportToManger();
                            if (continueOrStop())
                            {
                                continue;
                            }
                            break;
                        case 8:
                            WriteReview();
                            if (continueOrStop())
                            {
                                continue;
                            }
                            break;
                        case 9:
                            hr.pormotion();
                            if (continueOrStop())
                            {
                                continue;
                            }
                            break;
                        case 10:
                            hr.Hire();
                            if (continueOrStop())
                            {
                                continue;
                            }
                            break;
                        case 11:
                            hr.Fire();
                            if (continueOrStop())
                            {
                                continue;
                            }
                            break;
                        case 12:
                            return true;
                        default:
                            Console.WriteLine("Please enter a valid choice");
                            continue;
                    }
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