
using OfficeOpenXml;
using System.Drawing;
using static OfficeOpenXml.ExcelErrorValue;


// instead of rewriting some methods twice, jsut gather them in one class
namespace hospitalData
{
    public static class HandlingExcelClass
    {
        public static string baseDir = AppDomain.CurrentDomain.BaseDirectory;
        //*********************************************************************Store data****************************************************************



        internal static ExcelWorksheet CreateNewDepartmentsheet(ExcelWorksheet sheet, Dictionary<string, dynamic> data)// creates new sheet for a department(ex: hr, doctor ...)  ... work with patients now
        {
            int col = 1;
            foreach (var key in data)
            {
                if (key.Key == "PreviousExperience" || key.Key == "MedicalHistory" || key.Key == "EmergencyContact" || key.Key == "Visits")
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


        internal static ExcelWorksheet AddToExcistingDepartment(ExcelWorksheet sheet, Dictionary<string, dynamic> data) //Adds employees to to thier department that alreadyexist ... work with patients now
        {

            int row = sheet.Dimension.End.Row;
            row++;
            int col = 1;

            foreach (var value in data)
            {
                if (value.Key == "PreviousExperience" || value.Key == "MedicalHistory" || value.Key == "EmergencyContact" || value.Key == "Visits")
                {
                    continue;
                }
                else if (value.Key == "WorkHours")
                {
                    TimeSpan strValue;
                    if (value.Value != null)
                    {
                        strValue = value.Value;
                        sheet.Cells[row, col].Value = strValue.ToString();

                    }
                    else
                    {
                        sheet.Cells[row, col].Value = string.Empty;

                    }
                    sheet.Cells[row, col].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                    col++;
                    continue;
                }
                else if (value.Key == "DailyLoginTime" || value.Key == "DailyLogoutTime")
                {
                    DateTime strValue;
                    if (value.Value != null)
                    {
                        strValue = value.Value;
                        sheet.Cells[row, col].Value = strValue.ToString();

                    }
                    else
                    {
                        sheet.Cells[row, col].Value = string.Empty;

                    }
                    sheet.Cells[row, col].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                    col++;
                    continue;

                }

                sheet.Cells[row, col].Value = value.Value;

                sheet.Cells[row, col].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;

                col++;
            }


            return sheet;
        }



        internal static ExcelWorksheet CreateNewSecondaryInfoSheet(ExcelWorksheet sheet, string id, Dictionary<string, string> data, string workType)// creating Previous Experince Sheet for the first time // can deal with patiend data now (medic histo & emergen contact)
        {
            string title = string.Empty;
            string content = string.Empty;


            switch (workType)
            {
                case "emp": //employee previous excperince
                    title = "Place";
                    content = "Title";
                    break;
                case "histo": //patient medical history
                    title = "Disease";
                    content = "Info";
                    break;
                case "emr": //patient emrgency contact
                    title = "Name";
                    content = "Info";
                    break;
                case "visits": // nurse visits
                    title = "Visit";
                    content = "Done";
                    break;
            }


            sheet.Cells["A1"].Value = "ID";
            sheet.Cells["A1"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
            sheet.Cells["A1"].Style.Font.Bold = true;

            sheet.Cells["B1"].Value = id;
            sheet.Cells["B1"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
            sheet.Cells["B1"].Style.Font.Bold = true;

            int row = 2;
            int mapIndex = 0;
            while (row <= data.Count * 2 + 1)
            {
                sheet.Cells[row, 1].Value = title;
                sheet.Cells[row, 1].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                sheet.Cells[row, 1].Style.Font.Bold = true;
                sheet.Cells[row, 1].Style.Font.Color.SetColor(Color.Green);

                sheet.Cells[row + 1, 1].Value = content;
                sheet.Cells[row + 1, 1].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                sheet.Cells[row + 1, 1].Style.Font.Bold = true;
                sheet.Cells[row + 1, 1].Style.Font.Color.SetColor(Color.Blue);

                sheet.Cells[row, 2].Value = data.ElementAt(mapIndex).Key;
                sheet.Cells[row, 2].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;

                sheet.Cells[row + 1, 2].Value = data.ElementAt(mapIndex).Value;
                sheet.Cells[row + 1, 2].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;

                row += 2;
                mapIndex++;
            }

            if(data.Count == 0) // rethink of better solution!!!!
            {
                sheet.Cells[row, 1].Value = title;
                sheet.Cells[row, 1].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                sheet.Cells[row, 1].Style.Font.Bold = true;
                sheet.Cells[row, 1].Style.Font.Color.SetColor(Color.Green);

                sheet.Cells[row + 1, 1].Value = content;
                sheet.Cells[row + 1, 1].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                sheet.Cells[row + 1, 1].Style.Font.Bold = true;
                sheet.Cells[row + 1, 1].Style.Font.Color.SetColor(Color.Blue);

                sheet.Cells[row, 2].Value = "NS";
                sheet.Cells[row, 2].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;

                sheet.Cells[row + 1, 2].Value = "NS";
                sheet.Cells[row + 1, 2].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
            }
            return sheet;
        }


        internal static ExcelWorksheet AddTosecondaryInfo(ExcelWorksheet sheet, string id, Dictionary<string, string> data, string workType) // add employee's previous experince to the sheet // can deal with patiend data now (medic histo & emergen contact)
        {
            string title = string.Empty;
            string content = string.Empty;

            switch(workType)
            {
                case "emp": //employee previous excperince
                    title = "Place";
                    content = "Title";
                    break;
                case "histo": //patient medical history
                    title = "Disease";
                    content = "Info";
                    break;
                case "emr": //patient emrgency contact
                    title = "Name";
                    content = "Info";
                    break;
                case "visits": // nurse visits
                    title = "Visit";
                    content = "Done";
                    break;
            }



            int rowCount = sheet.Dimension.End.Row;
            int targetCol = sheet.Dimension.End.Column + 1;

            int row = 2;
            int mapIndex = 0;

            sheet.Cells[1, targetCol].Value = id;
            sheet.Cells[1, targetCol].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
            sheet.Cells[1, targetCol].Style.Font.Bold = true;

            while (true)
            {
                if (mapIndex == data.Count && row >= rowCount) break; // All set

                if (row > rowCount) // open new cells for more Experince
                {
                    int col = 2;

                    sheet.Cells[row, 1].Value = title;
                    sheet.Cells[row, 1].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                    sheet.Cells[row, 1].Style.Font.Bold = true;
                    sheet.Cells[row, 1].Style.Font.Color.SetColor(Color.Green);

                    sheet.Cells[row + 1, 1].Value = content;
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


                    sheet.Cells[row, targetCol].Value = data.ElementAt(mapIndex).Key;
                    sheet.Cells[row, targetCol].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;

                    sheet.Cells[row + 1, targetCol].Value = data.ElementAt(mapIndex).Value;
                    sheet.Cells[row + 1, targetCol].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;

                    row += 2;
                    rowCount += 2;
                    mapIndex++;

                    continue;
                }
                sheet.Cells[row, targetCol].Value = data.ElementAt(mapIndex).Key;
                sheet.Cells[row, targetCol].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;

                sheet.Cells[row + 1, targetCol].Value = data.ElementAt(mapIndex).Value;
                sheet.Cells[row + 1, targetCol].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;

                row += 2;
                mapIndex++;
            }

            while (row <= rowCount) // if experince is less than the make row count then set the remain cells for this employee as "NS"
            {
                sheet.Cells[row, targetCol].Value = "NS";
                sheet.Cells[row, targetCol].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                sheet.Cells[row, targetCol].Style.Font.Color.SetColor(Color.Red);
                row++;
            }

            return sheet;
        }



        //*********************************************************************search data****************************************************************

        public static Dictionary<string, string> getSecondaryInfoData(string id, ExcelWorksheet sheet)
        {
            if (sheet == null) return new Dictionary<string, string>();
            Dictionary<string, string> data = new Dictionary<string, string>();
            int colCount = sheet.Dimension.End.Column;
            for (int col = 2; col <= colCount; col++)
            {
                string target = sheet.Cells[1, col].Value.ToString()!;
                if (target.ToUpper() == id.ToUpper())
                {
                    if (sheet.Cells[2, col].Value.ToString() == "NS") // have no secondary info
                    {
                        return null;
                    }

                    int targetCol = col;
                    int row = 2;
                    while (row <= sheet.Dimension.End.Row)
                    {
                        if (sheet.Cells[row, targetCol].Value.ToString() == "NS" || sheet.Cells[row + 1, targetCol].Value.ToString() == "NS") break; // got all the Secondary info and the rest is just fillers

                        string place = sheet.Cells[row, targetCol].Value.ToString()!;
                        string title = sheet.Cells[row + 1, targetCol].Value.ToString()!;
                        data[place] = title;

                        row += 2;

                    }
                    break;
                }
            }

            return data;
        }

        //********************************************************************* Accessing ****************************************************************



        //To be edited to fit work with patient to      // Done

        public static void accessEmployeeExcelFile(string id, string deprtment, string field, object value, string workType) // edit a targted data
        {
            string excelFilePath = string.Empty;

            if (workType == "emp")
            {
                excelFilePath = Path.Combine(baseDir, @"..\..\..\..\excel files\EmployeeData.xlsx");
                excelFilePath = Path.GetFullPath(excelFilePath);
            }

            else if (workType == "patient")
            {
                excelFilePath = Path.Combine(baseDir, @"..\..\..\..\excel files\PatientData.xlsx");
                excelFilePath = Path.GetFullPath(excelFilePath);
            }

            using (ExcelPackage package = new ExcelPackage(excelFilePath))
            {
                ExcelWorksheet sheet = package.Workbook.Worksheets[deprtment];
                int colCount = sheet.Dimension.End.Column;
                int rowCount = sheet.Dimension.End.Row;

                for (int row = 2; row <= colCount; row++)
                {
                    if (sheet.Cells[row, 1].Value.ToString() == id)
                    {
                        for (int col = 2; col <= colCount; col++)
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


        public static string accessEmployeeExcelFile(string id, string deprtment, string field, string workType) // get a targted data
        {
            string excelFilePath = string.Empty;

            if (workType == "emp")
            {
                excelFilePath = Path.Combine(baseDir, @"..\..\..\..\excel files\EmployeeData.xlsx");
                excelFilePath = Path.GetFullPath(excelFilePath);
            }

            else if (workType == "patient")
            {
                excelFilePath = Path.Combine(baseDir, @"..\..\..\..\excel files\PatientData.xlsx");
                excelFilePath = Path.GetFullPath(excelFilePath);
            }

            using (ExcelPackage package = new ExcelPackage(excelFilePath))
            {
                ExcelWorksheet sheet = package.Workbook.Worksheets[deprtment];
                int colCount = sheet.Dimension.End.Column;
                int rowCount = sheet.Dimension.End.Row;

                for (int row = 2; row <= colCount; row++)
                {
                    if (sheet.Cells[row, 1].Value.ToString() == id)
                    {
                        for (int col = 2; col <= colCount; col++)
                        {
                            if (sheet.Cells[1, col].Value.ToString() == field)
                            {

                                return sheet.Cells[row, col].Value.ToString()!;
                            }
                        }

                    }
                }
                Console.WriteLine($"{id} not in the data!\nMake sure u entered it right");
                return null;
            }
        }



        //*********************************************************************Others****************************************************************


        internal static void SaveFile(string path, ExcelPackage package)    // save the file in the sended path
        {
            if (File.Exists(path))
            {
                package.Save();
                return;
            }
            package.SaveAs(path);
        }


        internal static bool IDAlreadyAdded(ExcelWorksheet sheet, string TargetID) // check if this id already added
        {
            if (sheet == null) return false;

            int colCount = sheet.Dimension.End.Column;

            for (int col = 2; col <= colCount; col++)
            {
                if (sheet.Cells[1, col].Value.ToString() == TargetID) return true;
            }
            return false;
        }

    }
}
