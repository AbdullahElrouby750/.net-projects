using hospital_classes;
internal partial class Start
{
    private static bool MangerWorkSpace(string id)
    {
        var data = HR.searchEmployee(id);
        if (data != null)
        {
            Manger manger = new Manger(data);
            HR hr = new HR();
            while (true)
            {
                Console.WriteLine("\nChoose a jop from the list below :-\n");
                Console.WriteLine("1. Login");
                Console.WriteLine("2. Logout");
                Console.WriteLine("3. Print your Salary");
                Console.WriteLine("4. Write a Report about an employee");
                Console.WriteLine("5. approve Reports & read reviews");
                Console.WriteLine("6. Apply a pormotion");
                Console.WriteLine("7. Hire");
                Console.WriteLine("8. Fire");
                Console.WriteLine("9. Print ID list");
                Console.WriteLine("10. Exit\n\n");

                Console.Write("Enter your choice : ");
                int choice = int.Parse(Console.ReadLine()!);
                switch (choice)
                {
                    case 1:
                        manger.login();
                        pressEnterToContinue();
                        break;
                    case 2:
                        manger.logout();
                        pressEnterToContinue();
                        break;
                    case 3:
                        manger.Printsalary();
                        pressEnterToContinue();
                        break;
                    case 4:
                        hr.WriteReport();
                        pressEnterToContinue();
                        break;
                    case 5:
                        manger.approveReportsAndReviews();
                        pressEnterToContinue();
                        break;
                    case 6:
                        hr.pormotion();
                        pressEnterToContinue();
                        break;
                    case 7:
                        hr.Hire();
                        pressEnterToContinue();
                        break;
                    case 8:
                        hr.Fire();
                        pressEnterToContinue();
                        break;
                    case 9:
                        hr.PrintIDS();
                        pressEnterToContinue();
                        break;
                    case 10:
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

