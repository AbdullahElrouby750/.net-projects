using hospitalData;

namespace hospital_classes;

public class Accountant : Employee, WritingReports
{
    private string[] jopTitles;
    public static int NumberOfAccountant;

    //*********************************************************************ctors****************************************************************
    public Accountant()
    {
        jopTitles = ["Nurse", "Pharmacist", "Radiologist", "Receptionist", "Doctor", "HR", "Accountant"];
    }

    public Accountant(Dictionary<string, dynamic> data)
       : base(data)
    {
        jopTitles = ["Nurse", "Pharmacist", "Radiologist", "Receptionist", "Doctor", "HR", "Accountant"];
    }

    //*********************************************************************send salary****************************************************************
    public void sendSalary()// merged apply bouns and diduction for simplifications and less loops
    {
        if (DateTime.Now.Day == 1)
        {
            var IDs = EmployeeData.getIDS();
            foreach (var item in jopTitles)
            {

                var thisJopID = IDs.Keys.Where(jopID => jopID[0..2].ToUpper() == item[0..2].ToUpper()).ToList();
                if (thisJopID == null) continue;

                foreach(var id in thisJopID)
                {
                    double bouns = double.Parse(HandlingExcelClass.accessEmployeeExcelFile(id, item, "Bouns", "emp"));
                    double salary = double.Parse(HandlingExcelClass.accessEmployeeExcelFile(id, item, "Salary", "emp"));

                    string bankName = HandlingExcelClass.accessEmployeeExcelFile(id, item, "BankAccount", "emp");
                    string accountName = HandlingExcelClass.accessEmployeeExcelFile(id, item, "AccountNumber", "emp");

                    salary += bouns;

                    //TODO : use bank name and acc no. to send the money to the emp account

                    HandlingExcelClass.accessEmployeeExcelFile(id, item, "SalaryReceived", true, "emp");
                }
            }
        }
    }


    //********************************************************************* Review orders ****************************************************************

    public void reviewOrders() // just needed medicin now
    {
        int index = 0;
        Dictionary<string, string> data = new Dictionary<string, string>();

        data = HospitalData.getNeededMedicinDict();
        if (data == null) return;

        foreach (var item in data)
        {
            if(item.Key.Length > 12) Console.WriteLine($"{index + 1}. {item.Key}\t:\t\t{item.Value}");
            else Console.WriteLine($"{index + 1}. {item.Key}\t\t:\t\t{item.Value}");
            index++;
        }

        Console.WriteLine("\nPress '1' to accept all");
        Console.WriteLine("Press '2' to deny all");
        Console.WriteLine("Press '3' to accept part of it");
        Console.Write("Choose :");

        string choice = Console.ReadLine()!;
        while (true)
        {
            if (choice == "1")
            {
                HospitalData.deleteAcceptedData(data);
                return;
            }
            else if (choice == "2")
            {
                return;
            }
            else if (choice == "3")
            {
                deleteOnlyTHeChoosenOne(data);
                return;
            }
            else
            {
                Console.WriteLine($"{choice} is invalid. plz, choose a vaild one");
            }
        }

    }

    private void deleteOnlyTHeChoosenOne(Dictionary<string, string> data)
    {
        Dictionary<string, string> subData = new Dictionary<string, string>();

        Console.WriteLine("1. Accept a whole block (ex: 1 ,2, 3, 4 & 5) 'any block in the lsit' ");
        Console.WriteLine("2. Accept apart medicins (ex: 1, 3, 5, 8 ...");
        while (true)
        {

            Console.Write("Choose :");

            string choice = Console.ReadLine()!;
            if (choice == "1")
            {
                int index1, index2;
                while (true)
                {
                    Console.Write("From : ");
                    index1 = int.Parse(Console.ReadLine()!);
                    Console.Write("\nTo : ");
                    index2 = int.Parse(Console.ReadLine()!);
                    if (index1 <= 0 || index2 <= 0 || index2 > data.Count) // make sure from the choice
                    {
                        Console.WriteLine($"Plz enter a valid numbers from 1 to {data.Count}");
                        continue;
                    }
                    else break;
                }

                for (index1 = index1 - 1; index1 < index2; index1++)
                {
                    subData.Add(data.ElementAt(index1).Key, data.ElementAt(index1).Value);
                }
                break;
            }
            else if (choice == "2")
            {
                List<int> list = new List<int>();

                while (true)
                {
                    Console.WriteLine("Plz, enter the choices in this format (ex: 1-2-4-7-13)");
                    string choiceList = Console.ReadLine()!;

                    foreach (var item in choiceList)
                    {
                        if (char.IsDigit(item))
                        {
                            string sThisNum = item.ToString();
                            int thisNum = int.Parse(sThisNum);
                            list.Add(thisNum);
                        }
                    }

                    list.Sort();
                    if (list.Max() > data.Count || list.Any(x => x <= 0)) // make sure from the choice
                    {

                        Console.WriteLine($"Plz enter a valid numbers from 1 to {data.Count}");
                        list.Clear();
                        continue;
                    }
                    else break;

                }

                foreach (var index in list)
                {
                    subData.Add(data.ElementAt(index - 1).Key, data.ElementAt(index - 1).Value);
                }
                break;
            }
            else
            {
                Console.WriteLine($"Plz enter a valid choice");
                continue;
            }
        }
        HospitalData.deleteAcceptedData(subData);
    }



    //*********************************************************************print hr report****************************************************************

    public void PrintHRreport()
    {
        Console.WriteLine("\n1. Latest report");
        Console.WriteLine("2. Date's report");

        while (true)
        {
            Console.Write("Choose : ");
            int choice = int.Parse(Console.ReadLine()!);
            if (choice == 1)
            {
                HRreport = (HandlingExcelClass.accessEmployeeExcelFile(HospitalID, Department, "HRreport", "emp"));

                if (HRreport == string.Empty)
                {
                    Console.WriteLine("Today's report not done yet");
                    return;
                }
                Console.WriteLine();
                Console.WriteLine(HRreport);
                return;
            }
            else if (choice == 2)
            {
                Console.WriteLine();
                Console.WriteLine(HR.GetHrReport(HospitalID));
                return;
            }
            else
            {
                Console.WriteLine();
                Console.WriteLine("Plz enter a valid Choice!");
            }
        }

    }

    //*********************************************************************print salary****************************************************************
    public void WriteReport() { }
    
    public void Printsalary()
    {
        SalaryReceived = bool.Parse(HandlingExcelClass.accessEmployeeExcelFile(HospitalID, Department, "SalaryReceived", "emp"));
        Salary = double.Parse(HandlingExcelClass.accessEmployeeExcelFile(HospitalID, Department, "Salary", "emp"));
        Bouns = double.Parse(HandlingExcelClass.accessEmployeeExcelFile(HospitalID, Department, "Bouns", "emp"));

        if (SalaryReceived == true)
        {
            double salaryAfterBouns = Salary + Bouns;
            Console.WriteLine($"Salary received successfully: {salaryAfterBouns:c} ");
            Console.WriteLine($"Your main salary: {Salary}");
            Console.WriteLine($"Your bouns: {Bouns}");
            SalaryReceived = false;

            HandlingExcelClass.accessEmployeeExcelFile(HospitalID, Department, "SalaryReceived", false, "emp");
        }
        else
        {
            Console.WriteLine("salary not sent yet :(");
            Console.WriteLine($"Your salary with bouns and diduction for so far : {Salary:c}");
        }
    }

    //*********************************************************************login****************************************************************
    public void login()
    {
        if (DailyLoginTime.Date == DateTime.Now.Date)
        {
            Console.WriteLine($"You have already logged-in today at {DailyLoginTime}");
            return;
        }
        DailyLoginTime = DateTime.Now;
        Console.WriteLine($"You logged in at {DailyLoginTime} successfully");
        HandlingExcelClass.accessEmployeeExcelFile(HospitalID, Department, "DailyLoginTime", DailyLoginTime, "emp");
    }


    //*********************************************************************logout****************************************************************
    public void logout()
    {
        if (DailyLoginTime.Day != DateTime.Now.Day)
        {
            Console.WriteLine("Warning!!! You did not log-in for today!");
            Console.WriteLine("Can't log-out");
            return;
        }

        if (DailyLogoutTime.Date == DateTime.Now.Date)
        {
            Console.WriteLine($"You have already logged-in today at {DailyLogoutTime}");
            return;
        }

        int workedHours = DateTime.Now.Hour - DailyLoginTime.Hour;
        TimeSpan? hoursWorkedToday = new TimeSpan(workedHours, 0, 0);
        if (hoursWorkedToday < WorkHours)
        {
            Console.WriteLine($"Warning! your logging out {WorkHours - hoursWorkedToday} hours earlier");
            Console.WriteLine("Are you sure you want to log out? y/n");
            while (true)
            {
                string answer = Console.ReadLine()!.ToLower();
                if (answer == "y")
                {
                    DailyLogoutTime = DateTime.Now;
                    Console.WriteLine($"You logged out at {DailyLogoutTime} successfully");

                    break;
                }
                else if (answer == "n")
                {
                    break;
                }
                else
                {
                    Console.WriteLine("please enter y or n");
                }
            }
        }
        DailyLogoutTime = DateTime.Now;
        Console.WriteLine($"You logged out at {DailyLogoutTime} successfully");
        HandlingExcelClass.accessEmployeeExcelFile(HospitalID, Department, "DailyLogoutTime", DailyLoginTime, "emp");
    }


}