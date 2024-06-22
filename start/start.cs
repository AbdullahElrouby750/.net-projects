using hospital_classes;
using OfficeOpenXml;
internal partial class Start
{
    private static void Main(string[] args)
    {
        ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

        fakeDataBase();

        Console.WriteLine("Welcome");

        while (true)
        {
            login();
        }



    }
    private static bool isFirstLogin()
    {
        if (File.Exists("D:\\codez\\uni projects\\hospital system my work\\exel files\\EmployeeData.xlsx"))
        {
            return false;
        }
        return true;
    }

    private static void fisrtLogin()
    {
        var hr = new HR();
        Console.WriteLine("\nHI boss! We just need to take a few information before starting\n");

        var data = hr.GetNewEmployeesData("Manger");
        data["HospitalID"] = "MAAS0101";
        var Alaa = new Manger(data);

        HR.Employees["Manger"][Alaa.HospitalID] = Alaa;

        fakeDataBase();
    }

    private static void login()
    {
        if (isFirstLogin())
        {
            fisrtLogin();
        }

        bool stop = false;
        while (true)
        {
        Console.Write("Hi there. Plz, enter your ID : ");
            string id = Console.ReadLine().ToUpper();
            switch (id[0..2])
            {
                case "MA":
                    break;
                case "HR":
                    stop = HRWorkSpace(id);
                    break;
                case "AC":
                    stop = AccountantWorkSpace(id);
                    break;
                case "RE":
                    stop = ReceptionistWorkSpace(id);
                    break;
                case "DO":
                    stop = DoctorsAndRadiologistWorkSpace(id);
                    break;
                case "PH":
                    break;
                case "NU":
                    stop = NurseWorkspace(id);
                    break;
                case "RA":
                    stop = DoctorsAndRadiologistWorkSpace(id);
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

    private static void fakeDataBase()
    {

        HR.CreatManger();
        HR.creatHR();
        HR.CreatAccountant();
        HR.CreatReceptionist();
        HR.CreatDoctor();
        HR.CreatNurse();
        HR.CreatRadiologist();
        HR.CreatPharmacist();
    }
}