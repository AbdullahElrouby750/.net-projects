using OfficeOpenXml;
using System.Drawing;
using System.Xml.Linq;


namespace hospitalData;


public static class EmployeeData
{
    private static string[] jopTitles = ["Nurse", "Pharmacist", "Radiologist", "Receptionist", "Doctor", "HR", "Accountant"];

    //*********************************************************************Store data****************************************************************

    public static void StoreData(Dictionary<string, dynamic> data, string department) // main method in store data proccess
    {

        string excelFilePath = Path.Combine(HandlingExcelClass.baseDir, @"..\..\..\..\excel files\EmployeeData.xlsx");
        excelFilePath = Path.GetFullPath(excelFilePath);

        using (ExcelPackage EmployeeData = new ExcelPackage(excelFilePath))
        {

            ExcelWorksheet PreviousExperienceSheet = null;
            try
            {
                PreviousExperienceSheet = EmployeeData.Workbook.Worksheets["PreviousExperience"];
                if (HandlingExcelClass.IDAlreadyAdded(PreviousExperienceSheet, data["HospitalID"])) return;

                if (PreviousExperienceSheet == null)
                {
                    throw new ArgumentNullException(nameof(PreviousExperienceSheet));
                }
                PreviousExperienceSheet = HandlingExcelClass.AddTosecondaryInfo(PreviousExperienceSheet, data["HospitalID"], data["PreviousExperience"], "emp");
            }
            catch (IndexOutOfRangeException ex)
            {
                PreviousExperienceSheet = EmployeeData.Workbook.Worksheets.Add("PreviousExperience");
                PreviousExperienceSheet = HandlingExcelClass.CreateNewSecondaryInfoSheet(PreviousExperienceSheet, data["HospitalID"], data["PreviousExperience"], "emp");
            }
            catch (ArgumentNullException ex)
            {
                PreviousExperienceSheet = EmployeeData.Workbook.Worksheets.Add("PreviousExperience");
                PreviousExperienceSheet = HandlingExcelClass.CreateNewSecondaryInfoSheet(PreviousExperienceSheet, data["HospitalID"], data["PreviousExperience"], "emp");
            }

            HandlingExcelClass.SaveFile(excelFilePath, EmployeeData);

            ExcelWorksheet empSheet = null; // employee count sheet
            try
            {
                empSheet = EmployeeData.Workbook.Worksheets["NumberOfEmployee"];
                if (empSheet == null)
                {
                    throw new ArgumentNullException(nameof(empSheet));
                }
                empSheet = AddEmployeeToEmployeeCountSheet(empSheet, department);
            }
            catch (IndexOutOfRangeException ex)
            {
                empSheet = EmployeeData.Workbook.Worksheets.Add("NumberOfEmployee");
                empSheet = CreateNewEmplyeeCountSheet(empSheet, department);
                empSheet = AddEmployeeToEmployeeCountSheet(empSheet, department);
            }
            catch (ArgumentNullException ex)
            {
                empSheet = EmployeeData.Workbook.Worksheets.Add("NumberOfEmployee");
                empSheet = CreateNewEmplyeeCountSheet(empSheet, department);
                empSheet = AddEmployeeToEmployeeCountSheet(empSheet, department);
            }

            HandlingExcelClass.SaveFile(excelFilePath, EmployeeData);


            ExcelWorksheet sheet = null; // employee data sheet
            try
            {
                sheet = EmployeeData.Workbook.Worksheets[department];
                if (sheet == null)
                {
                    throw new ArgumentNullException(nameof(sheet));
                }

                if (HandlingExcelClass.IDAlreadyAdded(sheet, data["HospitalID"])) return;

                sheet = HandlingExcelClass.AddToExcistingDepartment(sheet, data);
            }
            catch (IndexOutOfRangeException ex)
            {
                sheet = EmployeeData.Workbook.Worksheets.Add(department);
                sheet = HandlingExcelClass.CreateNewDepartmentsheet(sheet, data);
                sheet = HandlingExcelClass.AddToExcistingDepartment(sheet, data);
            }
            catch (ArgumentNullException ex)
            {
                sheet = EmployeeData.Workbook.Worksheets.Add(department);
                sheet = HandlingExcelClass.CreateNewDepartmentsheet(sheet, data);
                sheet = HandlingExcelClass.AddToExcistingDepartment(sheet, data);
            }

            HandlingExcelClass.SaveFile(excelFilePath, EmployeeData);

        }
    }

    private static ExcelWorksheet CreateNewEmplyeeCountSheet(ExcelWorksheet sheet, string department)// create Employee count sheet with the first employee
    {
        int col = 1;

        foreach (var title in jopTitles)
        {
            sheet.Cells[1, col].Value = title;
            sheet.Cells[1, col].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
            sheet.Cells[1, col].Style.Font.Bold = true;

            if (title == department)
            {
                sheet.Cells[2, col].Value = 1;
            }
            else
            {
                sheet.Cells[2, col].Value = 0;
            }
            sheet.Cells[2, col].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;


            col++;
        }

        sheet.Cells[1, col].Value = "Employee";
        sheet.Cells[1, col].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
        sheet.Cells[1, col].Style.Font.Bold = true;

        sheet.Cells[2, col].Formula = $"=SUM(A2:G2)";
        sheet.Cells[2, col].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;

        return sheet;
    }


    private static ExcelWorksheet AddEmployeeToEmployeeCountSheet(ExcelWorksheet sheet, string department)// change the count for the added employee's department
    {

        int colCount = sheet.Dimension.End.Column;
        int col = 1;

        while (col <= colCount)
        {
            if (department == "Manger") break;

            if (sheet.Cells[1, col].Value.ToString() == department)
            {
                double val = (double)sheet.Cells[2, col].Value;
                sheet.Cells[2, col].Value = val + 1;
                sheet.Cells[2, col].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;

                break;
            }
            col++;
        }

        return sheet;
    }


    //*********************************************************************Firing proccesses****************************************************************

    public static bool deleteData(string id, string department) // delete from database
    {
        string excelFilePath = Path.Combine(HandlingExcelClass.baseDir, @"..\..\..\..\excel files\EmployeeData.xlsx");
        excelFilePath = Path.GetFullPath(excelFilePath);

        using (ExcelPackage package = new ExcelPackage(excelFilePath))
        {
            ExcelWorksheet sheet = package.Workbook.Worksheets[department];

            int rowConut = sheet.Dimension.End.Row;
            int colCount = sheet.Dimension.End.Column;
            for (int row = 2; row <= rowConut; row++)
            {
                string target = sheet.Cells[row, 1].Value.ToString();
                if (target.ToUpper() == id.ToUpper())
                {
                    int targetRow = row;
                    Dictionary<string, dynamic> data = new Dictionary<string, dynamic>();
                    try
                    {
                        sheet.DeleteRow(targetRow);
                        package.Save();
                        return true;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
            }
            return false;
        }
    }

    //*********************************************************************preformance reports****************************************************************

    public static void creatNewReportXLSXsheet() // create new excel file for the reports if not already exist
    {
        string excelFilePath = Path.Combine(HandlingExcelClass.baseDir, @"..\..\..\..\excel files\Reports Sheet.xlsx");
        excelFilePath = Path.GetFullPath(excelFilePath);

        using (ExcelPackage package = new ExcelPackage(excelFilePath))
        {
            ExcelWorksheet reportsSheet = package.Workbook.Worksheets.Add("Reports_Sheet");
            reportsSheet.Cells["A1"].Value = "ID";
            reportsSheet.Cells["A1"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
            reportsSheet.Cells["A1"].Style.Font.Bold = true;

            package.SaveAs(excelFilePath);
        }
    }

    public static void addTodayesReport(string id, string report) // add newest report to target id
    {
        string excelFilePath = Path.Combine(HandlingExcelClass.baseDir, @"..\..\..\..\excel files\Reports Sheet.xlsx");
        excelFilePath = Path.GetFullPath(excelFilePath);

        using (ExcelPackage package = new ExcelPackage(excelFilePath))
        {
            ExcelWorksheet reportsSheet = package.Workbook.Worksheets["Reports_Sheet"];
            int colCount = reportsSheet.Dimension.End.Column;
            int rowCount = reportsSheet.Dimension.End.Row;
            DateTime lastCellDate;
            if(colCount == 1)
            {
                lastCellDate = new DateTime();
            }
            else
            {
                lastCellDate = DateTime.Parse(reportsSheet.Cells[1, colCount].Value.ToString()!);
            }

            for (int row = 2; row <= rowCount; row++)
            {
                string target = reportsSheet.Cells[row, 1].Value.ToString();
                if (target == id)
                {
                    if (reportsSheet.Cells[row, colCount].Value.ToString() != null)
                    {
                        Console.WriteLine("Today's report have been added already for this Employee");
                        return;
                    }

                    if (DateTime.Now.Date > lastCellDate.Date || lastCellDate == DateTime.MinValue) // first report written for today
                    {

                        reportsSheet.Cells[1, colCount + 1].Value = DateOnly.FromDateTime(DateTime.Now.Date).ToString();
                        reportsSheet.Cells[1, colCount + 1].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                        reportsSheet.Cells[1, colCount + 1].Style.Font.Bold = true;

                        reportsSheet.Cells[row, colCount + 1].Value = report;
                        reportsSheet.Cells[row, colCount + 1].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;

                    }
                    else if(DateTime.Now.Day == lastCellDate.Day) // allready there is reports for today
                    {
                        reportsSheet.Cells[row, colCount].Value = report;
                        reportsSheet.Cells[row, colCount].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                    }

                    package.SaveAs(excelFilePath);
                    return;
                }
            }

            if (DateTime.Now.Date > lastCellDate.Date || lastCellDate == DateTime.MinValue) // first report written for today
            {
                reportsSheet.Cells[1, colCount + 1].Value = DateOnly.FromDateTime(DateTime.Now.Date).ToString();
                reportsSheet.Cells[1, colCount + 1].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                reportsSheet.Cells[1, colCount + 1].Style.Font.Bold = true;
                colCount++;
            }

            reportsSheet.Cells[rowCount + 1, 1].Value = id;
            reportsSheet.Cells[rowCount + 1, 1].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
            reportsSheet.Cells[rowCount + 1, 1].Style.Font.Bold = true;
            for (int col = 2; col < colCount; col++)
            {
                reportsSheet.Cells[rowCount + 1, col].Value = $"first report at {DateTime.Now.Date.ToString()}";
                reportsSheet.Cells[rowCount + 1, col].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
            }
            reportsSheet.Cells[rowCount + 1, colCount].Value = report;
            reportsSheet.Cells[rowCount + 1, colCount].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;

            package.SaveAs(excelFilePath);
        }
    }

    public static string getDataFromReportExcelFile(string id, DateOnly? date) // get the targeted report for an employee
    {
        string excelFilePath = Path.Combine(HandlingExcelClass.baseDir, @"..\..\..\..\excel files\Reports Sheet.xlsx");
        excelFilePath = Path.GetFullPath(excelFilePath);

        using (ExcelPackage package = new ExcelPackage(excelFilePath))
        {
            ExcelWorksheet sheet = package.Workbook.Worksheets["Reports_Sheet"];

            if(sheet == null)
            {
                Console.WriteLine("No reports for you yet");
                return string.Empty;
            }

            int colCount = sheet.Dimension.End.Column;
            int rowCount = sheet.Dimension.End.Row;
            for (int row = 2; row <= rowCount; row++)
            {
                if (sheet.Cells[row, 1].Value.ToString() == id)
                {
                    for (int col = 2; col <= colCount; col++)
                    {
                        if (DateOnly.Parse(sheet.Cells[1, col].Value.ToString()) == date)
                        {
                            return sheet.Cells[row, col].Value.ToString();
                        }
                    }
                    Console.WriteLine($"Some thing wrong with this date {date.ToString()}!\nplz, make sure u entered it right");
                    return null;
                }
            }
            Console.WriteLine($"This id {id} does not exist in the data!\nPlz. make sure u entered it right");
            return null;
        }
    }


    //*********************************************************************search employee****************************************************************


    public static Dictionary<string, dynamic> GetIdDate(string id) // get the data for the wanted employee by ID
    {
        string excelFilePath = Path.Combine(HandlingExcelClass.baseDir, @"..\..\..\..\excel files\EmployeeData.xlsx");
        excelFilePath = Path.GetFullPath(excelFilePath);

        using (ExcelPackage package = new ExcelPackage(excelFilePath))
        {

            ExcelWorksheet sheet = null;


            switch (id[0..2])
            {
                case "MA":
                    sheet = package.Workbook.Worksheets["Manger"];
                    break;
                case "NU":
                    sheet = package.Workbook.Worksheets["Nurse"];
                    break;
                case "PH":
                    sheet = package.Workbook.Worksheets["Pharmacist"];
                    break;
                case "RA":
                    sheet = package.Workbook.Worksheets["Radiologist"];
                    break;
                case "RE":
                    sheet = package.Workbook.Worksheets["Receptionist"];
                    break;
                case "DO":
                    sheet = package.Workbook.Worksheets["Doctor"];
                    break;
                case "HR":
                    sheet = package.Workbook.Worksheets["HR"];
                    break;
                case "AC":
                    sheet = package.Workbook.Worksheets["Accountant"];
                    break;
                default:
                    Console.WriteLine("Invalid Id!");
                    return null;

            }

            if(sheet == null)
            {
                Console.WriteLine("ERROR!!! No employees yet in the data");
                return null;
            }

            int rowCount = sheet.Dimension.End.Row;
            int colCount = sheet.Dimension.End.Column;
            for (int row = 2; row <= rowCount; row++)
            {
                string target = sheet.Cells[row, 1].Value.ToString();
                if (target.ToUpper() == id.ToUpper())
                {
                    int targetRow = row;

                    return getDate(targetRow, colCount, id, sheet, package);
                }
            }
            return null;
        }

    }

    public static List<Dictionary<string, dynamic>> GetNameDate(string name) // get the data for the wanted employee by Name
    {

        string excelFilePath = Path.Combine(HandlingExcelClass.baseDir, @"..\..\..\..\excel files\EmployeeData.xlsx");
        excelFilePath = Path.GetFullPath(excelFilePath);

        using (ExcelPackage package = new ExcelPackage(excelFilePath))
        {

            ExcelWorksheets sheets = package.Workbook.Worksheets;
            ExcelWorksheet sheet = null;

            List<Dictionary<string, dynamic>> similarNames = new List<Dictionary<string, dynamic>>();

            foreach (var item in sheets)
            {
                sheet = package.Workbook.Worksheets[item.Name];

                int rowCount = sheet.Dimension.End.Row;
                int colCount = sheet.Dimension.End.Column;
                for (int row = 2; row <= rowCount; row++)
                {
                    string target = sheet.Cells[row, 2].Value.ToString();
                    if (target.ToUpper() == name.ToUpper())
                    {
                        int targetRow = row;
                        Dictionary<string, dynamic> data = new Dictionary<string, dynamic>();

                        string id = sheet.Cells[targetRow, 1].Value.ToString();
                        data = getDate(targetRow, colCount, id, sheet, package);

                        similarNames.Add(data);
                    }
                }
            }
            return similarNames;
        }
    }

    private static Dictionary<string, dynamic> getDate(int targetRow, int colCount, string id, ExcelWorksheet sheet, ExcelPackage package) // get the data for the wanted employee
    {

        Dictionary<string, dynamic> data = new Dictionary<string, dynamic>();
        for (int col = 1; col <= colCount; col++)
        {
            string field = sheet.Cells[1, col].Value.ToString();
            dynamic val = sheet.Cells[targetRow, col].Value;

            if (field == "WorkHours")
            {
                if (val != string.Empty)
                {
                    TimeSpan workhours = TimeSpan.Parse(val);
                    data[field] = workhours;
                    continue;
                }
                data[field] = new TimeSpan();
                continue;
            }
            else if (field == "DailyLogoutTime" || field == "DailyLoginTime")
            {
                if (val != string.Empty)
                {
                    DateTime date = DateTime.Parse(val);
                    data[field] = date;
                    continue;
                }
                data[field] = new DateTime();
                continue;
            }
            else if (field == "DateOfBirth" || field == "StartingDate")
            {
                if (val != null)
                {
                    DateOnly date = DateOnly.Parse(val);
                    data[field] = date;
                    continue;
                }
                data[field] = new DateOnly();
                continue;
            }

            data[field] = val;
        }
        sheet = package.Workbook.Worksheets["PreviousExperience"];
        data["PreviousExperience"] = HandlingExcelClass.getSecondaryInfoData(id, sheet);

        return data;
    }

    public static Dictionary<string, string> getIDS()// return a dictionary containing the ids and the name of the employees
    {
        string excelFilePath = Path.Combine(HandlingExcelClass.baseDir, @"..\..\..\..\excel files\EmployeeData.xlsx");
        excelFilePath = Path.GetFullPath(excelFilePath);

        if (!File.Exists(excelFilePath))
        {
            return null;
        }


        Dictionary<string, string> IDandName = new Dictionary<string, string>();

        using (ExcelPackage package = new ExcelPackage(excelFilePath))
        {
            ExcelWorksheet sheets = null;


            int sheetsCount = package.Workbook.Worksheets.Count;
            int startIndex = 2;

            while (startIndex < sheetsCount)
            {
                sheets = package.Workbook.Worksheets[startIndex];

                int rowConut = sheets.Dimension.End.Row;
                for (int row = 2; row <= rowConut; row++)
                {
                    IDandName[sheets.Cells[row, 1].Value.ToString()!] = sheets.Cells[row, 2].Value.ToString()!;
                }

                startIndex++;
            }
        }
        return IDandName;
    }

    //*********************************************************************Counting employee****************************************************************
    public static void numberOfEmployyes()
    {
        string excelFilePath = Path.Combine(HandlingExcelClass.baseDir, @"..\..\..\..\excel files\EmployeeData.xlsx");
        excelFilePath = Path.GetFullPath(excelFilePath);

        using (ExcelPackage EmployeeData = new ExcelPackage(excelFilePath))
        {
            ExcelWorksheet sheet = EmployeeData.Workbook.Worksheets["NumberOfEmployee"];

            int colCount = sheet.Dimension.End.Column;
            int totalCount = 0;

            for (int col = 1; col < colCount; col++)
            {
                string department = sheet.Cells[1, col].Value.ToString()!;
                int count = int.Parse(sheet.Cells[2, col].Value.ToString()!);
                if (department.Length < 8)
                {
                    Console.WriteLine($"{department}\t\t:\t{count}");
                }
                else
                {
                    Console.WriteLine($"{department}\t:\t{count}");

                }
                totalCount++;
            }
            Console.WriteLine($"{"Employee"}\t:\t{totalCount}");


        }
    }


}
