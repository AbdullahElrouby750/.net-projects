using hospital_classes;

internal partial class Start
{
    private static bool PharmacistWorkSpace(string id)
    {
        var data = HR.searchEmployee(id);
        if (data != null)
        {
            var pharmacist = new Pharmacist(data);

            Console.WriteLine($"Welcome back! {pharmacist.FullName}");
            Console.WriteLine("Working on it ;)");


            while (true)
            {
                Console.WriteLine("\nChoose a jop from the list below :-\n");
                Console.WriteLine("1. Login.");
                Console.WriteLine("2. Logout.");
                Console.WriteLine("3. Print your Salary.");
                Console.WriteLine("4. Print HR report.");
                Console.WriteLine("5. Write a report or a review to the manger.");
                Console.WriteLine("6. Write patients report.");
                Console.WriteLine("7. Print a patient report.");
                Console.WriteLine("8. Requist needed medicin.");
                Console.WriteLine("9. Exit.");

                Console.Write("\nEnter your choice: ");
                string choice = Console.ReadLine()!;

                switch (choice)
                {
                    case "1":
                        pharmacist.login();
                        pressEnterToContinue();
                        break;
                    case "2":
                        pharmacist.logout();
                        pressEnterToContinue();
                        break;
                    case "3":
                        pharmacist.Printsalary();
                        pressEnterToContinue();
                        break;
                    case "4":
                        pharmacist.PrintHRreport();
                        pressEnterToContinue();
                        break;
                    case "5":
                        Manger.WriteReportAndReviewToManger(pharmacist);
                        pressEnterToContinue();
                        break;
                    case "6":
                        pharmacist.WriteReport();
                        pressEnterToContinue();
                        break;
                    case "7":
                        Receptionist.PrintPatientReports();
                        pressEnterToContinue();
                        break;
                    case "8":
                        pharmacist.needMedicin();
                        pressEnterToContinue();
                        break;
                    case "9":
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
