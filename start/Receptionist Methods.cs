using hospital_classes;

internal partial class Start
{
    private static bool ReceptionistWorkSpace(string id)
    {
        if (HR.searchEmployee(id) is Receptionist receptionist)
        {

            while (true)
            {
                Console.WriteLine("\nChoose a jop from the list below :-\n");
                Console.WriteLine("1. Login");
                Console.WriteLine("2. Logout");
                Console.WriteLine("3. Print your Salary");
                Console.WriteLine("4. Print HR report");
                Console.WriteLine("5. Write a report to the manger");
                Console.WriteLine("6. Write a review to the manger");
                Console.WriteLine("7. Add a new patient");
                Console.WriteLine("8. Exit\n\n");

                Console.Write("Enter your choice : ");
                int choice = int.Parse(Console.ReadLine()!);

                switch (choice)
                {
                    case 1:
                        receptionist.login();
                        pressEnterToContinue();
                        break;
                    case 2:
                        receptionist.logout();
                        pressEnterToContinue();
                        break;
                    case 3:
                        receptionist.Printsalary();
                        pressEnterToContinue();
                        break;
                    case 4:
                        receptionist.PrintHRreport();
                        pressEnterToContinue();
                        break;
                    case 5:
                        // WriteReportToManger();
                        // pressEnterToContinue();
                        break;
                    case 6:
                        // WriteReview();
                        // pressEnterToContinue();
                        break;
                    case 7:
                        receptionist.hipatient();
                        pressEnterToContinue();
                        break;
                    case 8:
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