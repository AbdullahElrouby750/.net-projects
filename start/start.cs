using hospital_classes;

internal partial class Start
{
    private static void Main(string[] args)
    {
        HR.creatRouby();

        Console.WriteLine("Welcome");

        login();





    }
    private static bool isFirstLogin()
    {
        if (HR.Employees.Count == 0)
        {
            return true;
        }
        return false;
    }

    private static void fisrtLogin()
    {
        var hr = new HR();
        Console.WriteLine("\nHI boss! We just need to take a few information before starting\n");

        var data = hr.GetNewEmployeesData("Manger");
        data["HospitalID"] = "MAAS0101";
        var Alaa = new Manger(data);

        HR.Employees["Manger"][Alaa.HospitalID] = Alaa;

        HR.creatRouby();
        // an hr rouby will be added automatically when first login
    }

    private static void login()
    {
        if (isFirstLogin())
        {
            fisrtLogin();
        }

        bool stop = false;
        Console.Write("Hi there. Plz, enter your ID : ");
        while (true)
        {
            string id = Console.ReadLine().ToUpper();
            switch (id[0..2])
            {
                case "MA":
                    break;
                case "HR":
                    stop = HRWorkSpace(id);
                    break;

                case "AC":
                    break;
                case "RE":
                    break;
                case "DO":
                    break;
                case "PH":
                    break;
                case "NU":
                    break;
                case "RA":
                    break;
                default:
                    Console.WriteLine("Access denied!");
                    Console.Write("Please enter a valid ID : ");
                    continue;
            }
            if (stop)
            {
                break;
            }
        }
        Console.WriteLine("\nThank you for using our system");
    }

    private static bool continueOrStop()
    {
        Console.WriteLine("Do you want to continue? (yes/no)");
        while (true)
        {
            string answer = Console.ReadLine().ToLower();
            if (answer == "yes")
            {
                return true;
            }
            else if (answer == "no")
            {
                return false;
            }
            else
            {
                Console.WriteLine("Please enter a valid choice");
            }
        }
    }
    private static bool pressEnterToContinue()
    {
        Console.WriteLine("\n\nPress enter to continue");
        while (true)
        {
            if (Console.ReadKey(true).Key == ConsoleKey.Enter)
            {
                return true;
            }
        }
    }
}