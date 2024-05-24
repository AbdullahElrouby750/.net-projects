namespace hospital_classes;

public class Receptionist : Employee , WritingReports
{
    static public Dictionary<int,Patient> patientData =new Dictionary<int, Patient>();
    public static int NumberofReceptionist;

       public Receptionist ()
    {
        NumberofReceptionist++;
       
       
    }

    public Receptionist (Dictionary<string, dynamic> data)
       : base(data)
    {
        NumberofReceptionist++;
    }
    private Dictionary<string, dynamic> GetNewpatientData()
    {
        Dictionary<string, dynamic> Data = new Dictionary<string, dynamic>();


        //Date from patient class
        Console.Write("\n\nFirst Name : ");
        Data["FirstName"] = Console.ReadLine();

        Console.Write("Last Name : ");
        Data["LastName"] = Console.ReadLine();

        Console.Write("Phone Number : ");
        Data["PhoneNumber"] = Console.ReadLine();

        Console.Write("Age : ");
        Data["Age"] = int.Parse(Console.ReadLine());

        Console.Write("Date of Birth in yyyy-mm-dd : ");
        DateTime FullDate = DateTime.Parse(Console.ReadLine());
        Data["DateOfBirth"] = FullDate.Date;

        Console.WriteLine("Gender : ");
        Data["Gender"] = Console.ReadLine();

        Console.Write("Statue : ");
        Data["Statue"] = Console.ReadLine();

        Console.Write("Address : ");
        Data["Address"] = Console.ReadLine();

        Console.Write("Blood Type : ");
        Data["BloodType"] = Console.ReadLine();
     
        Console.Write("Weight : ");
        Data["Weight"] = double.Parse(Console.ReadLine());

        Console.Write("Height  : ");
        Data["Height"] = double.Parse(Console.ReadLine());

        Console.Write("CurrentProblem   : ");
        Data["CurrentProblem"] = Console.ReadLine();

        Console.Write("Disabilities  : ");
        Data["Disabilities"] = Console.ReadLine();

          return Data;
    }

    public void hipatient()
    {
        var data = GetNewpatientData ();
        var patient = new Patient(data);
        
        while (true){
            string disease;
            string info ;
            Console.WriteLine("Enter disease name :  ");
            disease =  Console.ReadLine();
           
           Console.WriteLine("Enter disease info :  ");
           info= Console.ReadLine();

           Console.WriteLine("Do you want to continue ? (yes or no) ");
            string answer= Console.ReadLine();
            if (answer== "yes"){
                continue;
            }
          else if (answer== "no"){
            break;
          } 
          else{
            break ;
          }
        }
        
         while (true){
            string contact;
            string info ;
            Console.WriteLine("Enter contact name :  ");
            contact =  Console.ReadLine();
           
           Console.WriteLine("Enter contact info : ");
           info= Console.ReadLine();

           Console.WriteLine("Do you want to continue ? (yes or no)");
            string answer= Console.ReadLine();
            if (answer== "yes"){
                continue;
            }
          else if (answer== "no"){
            break;
          } 
          else{
            break ;
          }
        }
        patientData[patient.PatientID]=patient;

    }
     public void WriteReport() {}

        public void PrintHRreport()
        {
             Console.WriteLine(HRreport);
          
        }

        public void Printsalary()
        {
             if (SalaryReceived == true)
        {
            double salaryAfterBouns = Salary + Bouns;
            Console.WriteLine($"Salary received successfully: {salaryAfterBouns:c} ");
            Console.WriteLine($"Your main salary: {Salary}");
            Console.WriteLine($"Your bouns: {Bouns}");
            SalaryReceived = false;
        }
        else
        {
            Console.WriteLine($"salary not sent yet :(");
        }     
        }

        public void login()
        {
            DailyLoginTime = DateTime.Now;
          
        }

        public void logout()
        {
              
        TimeSpan? hoursWorkedToday = DateTime.Now - DailyLoginTime;
        if (hoursWorkedToday < WorkHours)
        {
            Console.WriteLine($"Warning! your logging out {WorkHours - hoursWorkedToday} hours earlier");
            Console.WriteLine("Are you sure you want to log out? y/n");
            while (true)
            {
                string answer = Console.ReadLine();
                if (answer == "y")
                {
                    DailyLogoutTime = DateTime.Now;
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
    }  

    }