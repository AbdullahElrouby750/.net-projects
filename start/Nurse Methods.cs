using hospital_classes;

internal partial class Start
{
    private static bool NurseWorkspace(string id)
    {
        if (HR.searchEmployee(id) is Nurse nurse)
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
                Console.WriteLine("7. Write patients report");
                Console.WriteLine("8. Set Visits' number");
                Console.WriteLine("9. Mark current visit as done");
                Console.WriteLine("10. Check if there is visits left for a patient");
                Console.WriteLine("11. Print a patient report.");
                Console.WriteLine("12. Exit\n\n");

                Console.Write("Enter your choice : ");
                int choice = int.Parse(Console.ReadLine()!);
                switch (choice)
                {
                    case 1:
                        nurse.login();
                        pressEnterToContinue();
                        break;
                    case 2:
                        nurse.logout();
                        pressEnterToContinue();
                        break;
                    case 3:
                        nurse.Printsalary();
                        pressEnterToContinue();
                        break;
                    case 4:
                        nurse.PrintHRreport();
                        pressEnterToContinue();
                        break;
                    case 5:
                        Manger.WriteReportToManger(nurse);
                        pressEnterToContinue();
                        break;
                    case 6:
                        Manger.WriteReview(nurse);
                        pressEnterToContinue();
                        break;
                    case 7:
                        nurse.WriteReport();
                        pressEnterToContinue();
                        break;
                    case 8:
                        nurse.AddVisit();
                        pressEnterToContinue();
                        break;
                    case 9:
                        nurse.AddVisit();
                        pressEnterToContinue();
                        break;
                    case 10:
                        nurse.NeedVisits();
                        pressEnterToContinue();
                        break;
                    case 11:
                        Receptionist.PrintPatientReports();
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
    }
}