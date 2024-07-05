

using hospitalData;
using OfficeOpenXml;

/// <summary>
/// for hospital equipments tools and Medicins and stuff like this 
/// </summary>
public static class HospitalData // right now only medicin sheet applied !!
{
    //*********************************************************************Store data****************************************************************
    private static string[] sheetsNames = { "Needed Medicin" };
    public static void storeDate(Dictionary<string, string> data)
    {

        string excelFilePath = Path.Combine(HandlingExcelClass.baseDir, @"..\..\..\..\excel files\HospitalData.xlsx");
        excelFilePath = Path.GetFullPath(excelFilePath);

        using (ExcelPackage package = new ExcelPackage(excelFilePath))
        {
            ExcelWorksheet neededMedicinSheet = null;
            try
            {
                neededMedicinSheet = package.Workbook.Worksheets[0];
                if (neededMedicinSheet == null)
                {
                    {
                        throw new ArgumentNullException(nameof(neededMedicinSheet));
                    }
                }


            }
            catch (IndexOutOfRangeException ex)
            {
                neededMedicinSheet = package.Workbook.Worksheets.Add(sheetsNames[0]);
                neededMedicinSheet = addToNeededMedicinSheet(neededMedicinSheet, data);
            }
            catch (ArgumentNullException ex)
            {
                neededMedicinSheet = package.Workbook.Worksheets.Add(sheetsNames[0]);
                neededMedicinSheet = addToNeededMedicinSheet(neededMedicinSheet, data);
            }

            HandlingExcelClass.SaveFile(excelFilePath, package);
        }
    }

    private static ExcelWorksheet creatNeddedMedicinSheet(ExcelWorksheet sheet, Dictionary<string, string> data)
    {
        sheet.Cells["A1"].Value = "Medicin Name";
        sheet.Cells["A1"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
        sheet.Cells["A1"].Style.Font.Bold = true;

        sheet.Cells["B1"].Value = "Quantity";
        sheet.Cells["B1"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
        sheet.Cells["B1"].Style.Font.Bold = true;

        return sheet;
    }

    private static ExcelWorksheet addToNeededMedicinSheet(ExcelWorksheet sheet, Dictionary<string, string> data)
    {
        if (sheet.Dimension.End.Row == 0) sheet = creatNeddedMedicinSheet(sheet, data);

        int row = sheet.Dimension.End.Row;
        row++;

        foreach (var item in data)
        {
            sheet.Cells[row, 1].Value = item.Key;
            sheet.Cells[row, 1].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;

            sheet.Cells[row, 2].Value = item.Value;
            sheet.Cells[row, 2].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
        }

        return sheet;
    }

    //********************************************************************* Accissing data ****************************************************************

    public static Dictionary<string, string> getNeededMedicinDict()
    {

        string excelFilePath = Path.Combine(HandlingExcelClass.baseDir, @"..\..\..\..\excel files\HospitalData.xlsx");
        excelFilePath = Path.GetFullPath(excelFilePath);

        using (ExcelPackage package = new ExcelPackage(excelFilePath))
        {
            ExcelWorksheet neededMedicinSheet = null;
            try
            {
                neededMedicinSheet = package.Workbook.Worksheets[0];
                if (neededMedicinSheet == null)
                {
                    {
                        throw new ArgumentNullException(nameof(neededMedicinSheet));
                    }
                }
                return getMedicinData(neededMedicinSheet);
            }
            catch (IndexOutOfRangeException ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine("No needed medicin been added Yet!");
                return null;
            }
            catch (ArgumentNullException ex)
            {

                Console.WriteLine(ex.Message);
                Console.WriteLine("No needed medicin been added Yet!");
                return null;
            }
        }
    }

    private static Dictionary<string, string> getMedicinData(ExcelWorksheet sheet)
    {
        int rowCount = sheet.Dimension.End.Row;
        if (rowCount <= 1)
        {
            Console.WriteLine("No needed medicin been added Yet!");
            return null;
        }

        Dictionary<string, string> data = new Dictionary<string, string>();

        for (int row = 2; row <= rowCount; row++)
        {
            string medicin = sheet.Cells[row, 1].Value.ToString()!;
            string quantity = sheet.Cells[row, 2].Value.ToString()!;

            data[medicin] = quantity;
        }

        return data;
    }

    //********************************************************************* Delete data ****************************************************************

    public static void deleteAcceptedData(Dictionary<string, string> data)
    {
        string excelFilePath = Path.Combine(HandlingExcelClass.baseDir, @"..\..\..\..\excel files\HospitalData.xlsx");
        excelFilePath = Path.GetFullPath(excelFilePath);

        using (ExcelPackage package = new ExcelPackage(excelFilePath))
        {
            ExcelWorksheet neededMedicinSheet = null;
            try
            {
                neededMedicinSheet = package.Workbook.Worksheets[0];
                if (neededMedicinSheet == null)
                {
                    {
                        throw new ArgumentNullException(nameof(neededMedicinSheet));
                    }
                }
                neededMedicinSheet = deleteData(neededMedicinSheet, data);
            }
            catch (IndexOutOfRangeException ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine("No needed medicin been added Yet!");
            }
            catch (ArgumentNullException ex)
            {

                Console.WriteLine(ex.Message);
                Console.WriteLine("No needed medicin been added Yet!");
            }

            HandlingExcelClass.SaveFile(excelFilePath, package);
        }
    }

    private static ExcelWorksheet deleteData(ExcelWorksheet sheet, Dictionary<string, string> data)
    {
        int rowCount = sheet.Dimension.End.Row;

        if (rowCount == data.Count+1) // all needed medicin accepted
        {
            sheet.Cells[2, 1, rowCount, 2].Delete(eShiftTypeDelete.Up);
            return sheet;
        }

        int row = 2;


        while (row <= rowCount || data.Count == 0) // delete the accepted one only
        {
            string target = sheet.Cells[row, 1].Value.ToString()!;

            if(data.ContainsKey(target))
            {
                sheet.DeleteRow(row);
                data.Remove(target);
            }

            row++;
        }
        return sheet;
    }
}
