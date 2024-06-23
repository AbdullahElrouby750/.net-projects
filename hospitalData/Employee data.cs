using OfficeOpenXml;
using System.Drawing;


namespace hospitalData;


public static class EmployeeData
{
    private static string[] jopTitles = ["Nurse", "Pharmacist", "Radiologist", "Receptionist", "Doctor", "HR", "Accountant"];

    //*********************************************************************Store data****************************************************************

    public static void StoreData(Dictionary<string, dynamic> data, string department) // main method in store data proccess
    {
        string excelFilePath = "D:\\codez\\uni projects\\hospital system my work\\exel files\\EmployeeData.xlsx";

        using (ExcelPackage EmployeeData = new ExcelPackage(excelFilePath))
        {

            ExcelWorksheet PreviousExperienceSheet = null;
            try
            {
                PreviousExperienceSheet = EmployeeData.Workbook.Worksheets["PreviousExperience"];
                if (PreviousExperienceSheet == null)
                {
                    throw new ArgumentNullException(nameof(PreviousExperienceSheet));
                }
                PreviousExperienceSheet = AddEmployeePreviousExperince(excelFilePath, PreviousExperienceSheet, data["HospitalID"], data["PreviousExperience"]);
            }
            catch (IndexOutOfRangeException ex)
            {
                PreviousExperienceSheet = EmployeeData.Workbook.Worksheets.Add("PreviousExperience");
                PreviousExperienceSheet = CreateNewPreviousExperinceSheet(excelFilePath, PreviousExperienceSheet, data["HospitalID"], data["PreviousExperience"]);
            }
            catch (ArgumentNullException ex)
            {
                PreviousExperienceSheet = EmployeeData.Workbook.Worksheets.Add("PreviousExperience");
                PreviousExperienceSheet = CreateNewPreviousExperinceSheet(excelFilePath, PreviousExperienceSheet, data["HospitalID"], data["PreviousExperience"]);
            }

            SaveFile(excelFilePath, EmployeeData);

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

            SaveFile(excelFilePath, EmployeeData);


            ExcelWorksheet sheet = null; // employee data sheet
            try
            {
                sheet = EmployeeData.Workbook.Worksheets[department];
                if (sheet == null)
                {
                    throw new ArgumentNullException(nameof(sheet));
                }

                if (IDAlreadyAdded(sheet, data["HospitalID"])) return;

                sheet = AddToExcistingDepartment(sheet, EmployeeData, data, excelFilePath);
            }
            catch (IndexOutOfRangeException ex)
            {
                sheet = EmployeeData.Workbook.Worksheets.Add(department);
                sheet = CreateNewDepartmentsheet(sheet, data);
                sheet = AddToExcistingDepartment(sheet, EmployeeData, data, excelFilePath);
            }
            catch (ArgumentNullException ex)
            {
                sheet = EmployeeData.Workbook.Worksheets.Add(department);
                sheet = CreateNewDepartmentsheet(sheet, data);
                sheet = AddToExcistingDepartment(sheet, EmployeeData, data, excelFilePath);
            }

            SaveFile(excelFilePath, EmployeeData);

        }
    }


    private static ExcelWorksheet CreateNewDepartmentsheet(ExcelWorksheet sheet, Dictionary<string, dynamic> data)// creates new sheet for a department(ex: hr, doctor ...)
    {
        int col = 1;
        foreach (var key in data)
        {
            if (key.Key == "PreviousExperience")
            {
                continue;
            }
            sheet.Cells[1, col].Value = key.Key;
            sheet.Cells[1, col].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
            sheet.Cells[1, col].Style.Font.Bold = true;
            col++;
        }
        return sheet;
    }


    private static ExcelWorksheet AddToExcistingDepartment(ExcelWorksheet sheet, ExcelPackage package, Dictionary<string, dynamic> data, string path) //Adds employees to to thier department that alreadyexist
    {

        int row = sheet.Dimension.End.Row;
        row++;
        int col = 1;

        foreach (var value in data)
        {
            if (value.Key == "PreviousExperience")
            {
                continue;
            }
            else if (value.Key == "WorkHours")
            {
                if (value.Value != null)
                {
                    TimeSpan strValue = value.Value;
                    sheet.Cells[row, col].Value = strValue.ToString();
                    sheet.Cells[row, col].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                    col++;
                    continue;
                }
            }
            else if (value.Key == "DailyLoginTime" || value.Key == "DailyLogoutTime")
            {
                if (value.Value != null)
                {
                    DateTime strValue = value.Value;
                    sheet.Cells[row, col].Value = strValue.ToString();
                    sheet.Cells[row, col].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                    col++;
                    continue;
                }
            }

            sheet.Cells[row, col].Value = value.Value;

            sheet.Cells[row, col].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;

            col++;
        }


        return sheet;
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

        sheet.Cells[2, col].Formula = $"=SUM(A2:{col - 1}2)";
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


    // save the file in the sended path
    private static void SaveFile(string path, ExcelPackage package)
    {
        if (File.Exists(path))
        {
            package.Save();
            return;
        }
        package.SaveAs(path);
    }
    public static void accessEmployeeExcelFile(string id, string deprtment, string field, object value) // edit a targted data
    {
        string excelFilePath = "D:\\codez\\uni projects\\hospital system my work\\exel files\\EmployeeData.xlsx";
        using (ExcelPackage package = new ExcelPackage(excelFilePath))
        {
            ExcelWorksheet sheet = package.Workbook.Worksheets[deprtment];
            int colCount = sheet.Dimension.End.Column;
            int rowCount = sheet.Dimension.End.Row;

            for (int row = 2; row <= colCount; row++)
            {
                if (sheet.Cells[row, 1].Value.ToString() == id)
                {
                    for (int col = 0; col <= colCount; col++)
                    {
                        if (sheet.Cells[1, col].Value.ToString() == field)
                        {
                            if (field == "WorkHours" || field == "DailyLoginTime" || field == "DailyLogoutTime")
                            {
                                string strValue = value.ToString();
                                sheet.Cells[row, col].Value = strValue;
                            }
                            else
                            {
                                sheet.Cells[row, col].Value = value;
                            }
                            sheet.Cells[row, col].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;

                            package.Save();
                            return;
                        }
                    }

                }
            }
            Console.WriteLine($"{id} not in the data!\nMake sure u entered it right");
            return;
        }
    }

    private static ExcelWorksheet CreateNewPreviousExperinceSheet(string excelFilePath, ExcelWorksheet sheet, string id, Dictionary<string, string> PreviousExperience)// creating Previous Experince Sheet for the first time
    {
        sheet.Cells["A1"].Value = "ID";
        sheet.Cells["A1"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
        sheet.Cells["A1"].Style.Font.Bold = true;

        sheet.Cells["B1"].Value = id;
        sheet.Cells["B1"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
        sheet.Cells["B1"].Style.Font.Bold = true;

        int row = 2;
        int mapIndex = 0;
        while (row <= PreviousExperience.Count * 2 + 1)
        {
            sheet.Cells[row, 1].Value = "Place";
            sheet.Cells[row, 1].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
            sheet.Cells[row, 1].Style.Font.Bold = true;
            sheet.Cells[row, 1].Style.Font.Color.SetColor(Color.Green);

            sheet.Cells[row + 1, 1].Value = "Title";
            sheet.Cells[row + 1, 1].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
            sheet.Cells[row + 1, 1].Style.Font.Bold = true;
            sheet.Cells[row + 1, 1].Style.Font.Color.SetColor(Color.Blue);

            sheet.Cells[row, 2].Value = PreviousExperience.ElementAt(mapIndex).Key;
            sheet.Cells[row, 2].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;

            sheet.Cells[row + 1, 2].Value = PreviousExperience.ElementAt(mapIndex).Value;
            sheet.Cells[row + 1, 2].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;

            row += 2;
            mapIndex++;
        }
        // package.SaveAs(excelFilePath);
        return sheet;
    }


    public static ExcelWorksheet AddEmployeePreviousExperince(string excelFilePath, ExcelWorksheet sheet, string id, Dictionary<string, string> PreviousExperience) // add employee's previous experince to the sheet
    {
        int rowCount = sheet.Dimension.End.Row;
        int targetCol = sheet.Dimension.End.Column + 1;

        int row = 2;
        int mapIndex = 0;

        sheet.Cells[1, targetCol].Value = id;
        sheet.Cells[1, targetCol].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
        sheet.Cells[1, targetCol].Style.Font.Bold = true;

        while (true)
        {
            if (mapIndex == PreviousExperience.Count) break; // All set

            if (row > rowCount) // open new cells for more Experince
            {
                int col = 2;

                sheet.Cells[row, 1].Value = "Place";
                sheet.Cells[row, 1].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                sheet.Cells[row, 1].Style.Font.Bold = true;
                sheet.Cells[row, 1].Style.Font.Color.SetColor(Color.Green);

                sheet.Cells[row + 1, 1].Value = "Title";
                sheet.Cells[row + 1, 1].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                sheet.Cells[row + 1, 1].Style.Font.Bold = true;
                sheet.Cells[row + 1, 1].Style.Font.Color.SetColor(Color.Blue);

                while (col < targetCol)// set cells as "NS" for previous employee ** p.S "NS" => Not Set.
                {
                    sheet.Cells[row, col].Value = "NS";
                    sheet.Cells[row, col].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                    sheet.Cells[row, col].Style.Font.Color.SetColor(Color.Red);

                    sheet.Cells[row + 1, col].Value = "NS";
                    sheet.Cells[row + 1, col].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                    sheet.Cells[row + 1, col].Style.Font.Color.SetColor(Color.Red);

                    col++;
                }


                sheet.Cells[row, targetCol].Value = PreviousExperience.ElementAt(mapIndex).Key;
                sheet.Cells[row, targetCol].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;

                sheet.Cells[row + 1, targetCol].Value = PreviousExperience.ElementAt(mapIndex).Value;
                sheet.Cells[row + 1, targetCol].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;

                row += 2;
                rowCount += 2;
                mapIndex++;

                continue;
            }
            sheet.Cells[row, targetCol].Value = PreviousExperience.ElementAt(mapIndex).Key;
            sheet.Cells[row, targetCol].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;

            sheet.Cells[row + 1, targetCol].Value = PreviousExperience.ElementAt(mapIndex).Value;
            sheet.Cells[row + 1, targetCol].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;

            row += 2;
            mapIndex++;
        }

        while (row <= rowCount) // if experince is less than the mak row count then set the remain cells for this employee as "NS"
        {
            sheet.Cells[row, targetCol].Value = "NS";
            sheet.Cells[row, targetCol].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
            sheet.Cells[row, targetCol].Style.Font.Color.SetColor(Color.Red);
            row++;
        }

        return sheet;
    }

    private static bool IDAlreadyAdded(ExcelWorksheet sheet, string TargetID) // check if this id already added
    {
        return sheet.Cells[1, 1, sheet.Dimension.End.Row, 1].Any(cell => cell.Value != null && cell.Value.ToString().Equals(TargetID));
    }

    //*********************************************************************Firing proccesses****************************************************************

    public static bool deleteData(string id, string department) // delete from database
    {
        string excelFilePath = "D:\\codez\\uni projects\\hospital system my work\\exel files\\EmployeeData.xlsx";

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

    public static void creatNewXLSXfile() // create new excel file for the reports if not already exist
    {
        string excelFilePath = "D:\\codez\\uni projects\\hospital system my work\\exel files\\Reports Sheet.xlsx";

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
        string excelFilePath = "D:\\codez\\uni projects\\hospital system my work\\exel files\\Reports Sheet.xlsx";

        using (ExcelPackage package = new ExcelPackage(excelFilePath))
        {
            ExcelWorksheet reportsSheet = package.Workbook.Worksheets["Reports_Sheet"];
            int colCount = reportsSheet.Dimension.End.Column;
            int rowCount = reportsSheet.Dimension.End.Row;

            for (int row = 2; row <= rowCount; row++)
            {
                string target = reportsSheet.Cells[row, 1].Value.ToString();
                if (target == id)
                {
                    reportsSheet.Cells[1, colCount].Value = DateOnly.FromDateTime(DateTime.Now.Date);
                    reportsSheet.Cells[1, colCount].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                    reportsSheet.Cells[1, colCount].Style.Font.Bold = true;

                    reportsSheet.Cells[row, colCount + 1].Value = report;
                    reportsSheet.Cells[row, colCount].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;

                    package.SaveAs(excelFilePath);
                    return;
                }
            }
            reportsSheet.Cells[rowCount + 1, 1].Value = id;
            reportsSheet.Cells[rowCount + 1, 1].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
            reportsSheet.Cells[rowCount + 1, 1].Style.Font.Bold = true;
            for (int col = 2; col <= colCount; col++)
            {
                reportsSheet.Cells[rowCount + 1, col].Value = $"first report at {DateTime.Now.Date.ToString()}";
                reportsSheet.Cells[rowCount + 1, col].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
            }
            reportsSheet.Cells[rowCount + 1, colCount + 1].Value = report;
            reportsSheet.Cells[rowCount + 1, colCount + 1].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;

            package.SaveAs(excelFilePath);
        }
    }

    public static string getDataFromReportExcelFile(string id, DateOnly? date) // get the targeted report for an employee
    {
        string excelFilePath = "D:\\codez\\uni projects\\hospital system my work\\exel files\\Reports Sheet.xlsx";
        using (ExcelPackage package = new ExcelPackage(excelFilePath))
        {
            ExcelWorksheet sheet = package.Workbook.Worksheets[0];
            int colCount = sheet.Dimension.End.Column;
            int rowCount = sheet.Dimension.End.Row;
            for (int row = 2; row <= rowCount; row++)
            {
                if (sheet.Cells[row, 1].Value.ToString() == id)
                {
                    for (int col = 2; col <= colCount; col++)
                    {
                        if ((DateOnly)sheet.Cells[1, col].Value == date)
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

    public static Dictionary<string, dynamic> getTargetDate(int targetRow, int colCount, string id, ExcelWorksheet sheet) // get the data for the wanted employee
    {

        Dictionary<string, dynamic> data = new Dictionary<string, dynamic>();
        for (int col = 1; col <= colCount; col++)
        {
            string field = sheet.Cells[1, col].Value.ToString();
            dynamic val = sheet.Cells[targetRow, col].Value;

            if (field == "WorkHours")
            {
                if (val != null)
                {
                    TimeSpan workhours = TimeSpan.Parse(val);
                    data[field] = workhours;
                    continue;
                }
            }
            else if (field == "DailyLogoutTime" || field == "DailyLoginTime")
            {
                if (val != null)
                {
                    DateTime date = DateTime.Parse(val);
                    data[field] = date;
                    continue;
                }
            }
            else if (field == "DateOfBirth" || field == "StartDate")
            {
                if (val != null)
                {
                    DateOnly date = DateOnly.Parse(val);
                    data[field] = date;
                    continue;
                }
            }

            data[field] = val;
        }
        data["PreviousExperience"] = getPreviousExprince(id);
        return data;
    }

    private static Dictionary<string, string> getPreviousExprince(string id)
    {
        string path = "D:\\codez\\uni projects\\hospital system my work\\exel files\\EmployeeData.xlsx";

        Dictionary<string, string> data = new Dictionary<string, string>();
        using (ExcelPackage package = new ExcelPackage(path))
        {
            ExcelWorksheet sheet = package.Workbook.Worksheets["PreviousExperience"];

            int colCount = sheet.Dimension.End.Column;
            for (int col = 2; col <= colCount; col++)
            {
                string target = sheet.Cells[1, col].Value.ToString()!;
                if (target.ToUpper() == id.ToUpper())
                {
                    if (sheet.Cells[2, col].Value.ToString() == "NS") // have no previous experience
                    {
                        return null;
                    }

                    int targetCol = col;
                    int row = 2;
                    while (row <= sheet.Dimension.End.Row)
                    {
                        if (sheet.Cells[row, targetCol].Value.ToString() == "NS") break; // got all the previous experience and the rest is just fillers

                        string place = sheet.Cells[row, targetCol].Value.ToString()!;
                        string title = sheet.Cells[row + 1, targetCol].Value.ToString()!;
                        data[place] = title;

                        row += 2;

                    }
                    break;
                }
            }
        }
        return data;
    }

}
