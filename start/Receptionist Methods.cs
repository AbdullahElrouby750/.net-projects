using hospital_classes;

internal partial class Start
{
    private static bool ReceptionistWorkSpace(string id)
    {
        var data = HR.searchEmployee(id);
        if (data != null)
        {
            var receptionist = new Receptionist(data);
            Console.WriteLine($"Welcome back! {receptionist.FullName}");
            Console.WriteLine("Working on it ;)");

            while (true)
            {
                Console.WriteLine("\nChoose a jop from the list below :-\n");
                Console.WriteLine("1. Login");
                Console.WriteLine("2. Logout");
                Console.WriteLine("3. Print your Salary");
                Console.WriteLine("4. Print HR report");
                Console.WriteLine("5. Write a report or a review to the manger");
                Console.WriteLine("6. Add a new patient");
                Console.WriteLine("7. Set patient's Bill");
                Console.WriteLine("8. Print patient's Bill");
                Console.WriteLine("9. Exit\n\n");

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
                        Manger.WriteReportAndReviewToManger(receptionist);
                        pressEnterToContinue();
                        break;
                    case 6:
                        receptionist.hipatient();
                        pressEnterToContinue();
                        break;
                    case 7:
                        receptionist.setBill();
                        pressEnterToContinue();
                        break;
                    case 8:
                        receptionist.printPatientBill();
                        pressEnterToContinue();
                        break;
                    case 9:
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