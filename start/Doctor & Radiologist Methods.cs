using System.Globalization;
using hospital_classes;

internal partial class Start
{
    private static bool DoctorsAndRadiologistWorkSpace(string id)
    {
        dynamic DocOrRad;
        var data = HR.searchEmployee(id);
        if (data != null)
        {
            if (id[0..2].ToUpper() == "DO")
            {
                DocOrRad = new Doctor(data);
            }
            else if (id[0..2].ToUpper() == "RA")
            {
                DocOrRad = new Radiologist(data);
            }
            else
            {
                Console.Write($"\n\n{id} is invalid. plz, enter a valid one: ");
                return false;
            }
        }
        else
        {
            Console.Write($"\n\n{id} is invalid. plz, enter a valid one: ");
            return false;
        }

        while (true)
        {
            Console.WriteLine("\nChoose a jop from the list below :-\n");
            Console.WriteLine("1. Login.");
            Console.WriteLine("2. Logout.");
            Console.WriteLine("3. Print your Salary.");
            Console.WriteLine("4. Print HR report.");
            Console.WriteLine("5. Write a report to the manger.");
            Console.WriteLine("6. Write a review to the manger.");
            Console.WriteLine("7. Write patients report.");
            Console.WriteLine("8. Print a patient report.");
            Console.WriteLine("9. Exit.");

            Console.Write("\nEnter your choice: ");
            string choice = Console.ReadLine()!;

            switch (choice)
            {
                case "1":
                    DocOrRad.login();
                    pressEnterToContinue();
                    break;
                case "2":
                    DocOrRad.logout();
                    pressEnterToContinue();
                    break;
                case "3":
                    DocOrRad.Printsalary();
                    pressEnterToContinue();
                    break;
                case "4":
                    DocOrRad.PrintHRreport();
                    pressEnterToContinue();
                    break;
                case "5":
                    // WriteReportToManger();
                    // pressEnterToContinue();
                    break;
                case "6":
                    // WriteReview();
                    // pressEnterToContinue();
                    break;
                case "7":
                    DocOrRad.WriteReport();
                    pressEnterToContinue();
                    break;
                case "8":
                    Receptionist.PrintPatientReports();
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
}