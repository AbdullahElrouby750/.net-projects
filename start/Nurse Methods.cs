using hospital_classes;

internal partial class Start
{
    private static bool NurseWorkspace(string id)
    {
        var data = HR.searchEmployee(id);
        if (data != null)
        {
            var nurse = new Nurse(data);
            while (true)
            {
                Console.WriteLine("\nChoose a jop from the list below :-\n");
                Console.WriteLine("1. Login");
                Console.WriteLine("2. Logout");
                Console.WriteLine("3. Print your Salary");
                Console.WriteLine("4. Print HR report");
                Console.WriteLine("5. Write a report or a review to the manger");
                Console.WriteLine("6. Write patients report");
                Console.WriteLine("7. Mark current visit as done");
                Console.WriteLine("8. Check if there is visits left for a patient");
                Console.WriteLine("9. Print a patient report.");
                Console.WriteLine("10. Exit\n\n");

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
                        Manger.WriteReportAndReviewToManger(nurse);
                        pressEnterToContinue();
                        break;
                    case 6:
                        nurse.WriteReport();
                        pressEnterToContinue();
                        break;
                    case 7:
                        nurse.AddVisit();
                        pressEnterToContinue();
                        break;
                    case 8:
                        nurse.needVisits();
                        pressEnterToContinue();
                        break;
                    case 9:
                        Receptionist.PrintPatientReports();
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