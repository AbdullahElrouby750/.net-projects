using OfficeOpenXml;

namespace hospitalData;

public static class patientData
{
    private static string[] sheetsNames = { "Medical history", "Emergency contact", "Patient data" };


    //*********************************************************************Store data****************************************************************

    public static void storeData(Dictionary<string, dynamic> data)
    {
        string excelFilePath = "D:\\codez\\uni projects\\hospital system my work\\exel files\\PatientData.xlsx";

        using (ExcelPackage package = new ExcelPackage(excelFilePath))
        {

            ExcelWorksheet medicalHistorySheet = null;
            try // medical history sheet
            {
                medicalHistorySheet = package.Workbook.Worksheets[0];
                if (medicalHistorySheet == null)
                {
                    {
                        throw new ArgumentNullException(nameof(medicalHistorySheet));
                    }
                }

                medicalHistorySheet = HandlingExcelClass.AddTosecondaryInfo(medicalHistorySheet, data["patientID"], data["MedicalHistory"], "histo");

            }
            catch (IndexOutOfRangeException ex)
            {
                medicalHistorySheet = package.Workbook.Worksheets.Add(sheetsNames[0]);
                medicalHistorySheet = HandlingExcelClass.CreateNewSecondaryInfoSheet(medicalHistorySheet, data["patientID"], data["MedicalHistory"], "histo");
            }
            catch (ArgumentNullException ex)
            {
                medicalHistorySheet = package.Workbook.Worksheets.Add(sheetsNames[0]);
                medicalHistorySheet = HandlingExcelClass.CreateNewSecondaryInfoSheet(medicalHistorySheet, data["patientID"], data["MedicalHistory"], "histo");
            }

            HandlingExcelClass.SaveFile(excelFilePath, package);

            ExcelWorksheet emergencyContactSheet = null;
            try // emergencu contact sheet
            {
                emergencyContactSheet = package.Workbook.Worksheets[1];
                if (emergencyContactSheet == null)
                {
                    {
                        throw new ArgumentNullException(nameof(emergencyContactSheet));
                    }
                }

                emergencyContactSheet = HandlingExcelClass.AddTosecondaryInfo(emergencyContactSheet, data["patientID"], data["EmergencyContact"], "emr");

            }
            catch (IndexOutOfRangeException ex)
            {
                emergencyContactSheet = package.Workbook.Worksheets.Add(sheetsNames[1]);
                emergencyContactSheet = HandlingExcelClass.CreateNewSecondaryInfoSheet(emergencyContactSheet, data["patientID"], data["EmergencyContact"], "emr");
            }
            catch (ArgumentNullException ex)
            {
                emergencyContactSheet = package.Workbook.Worksheets.Add(sheetsNames[1]);
                emergencyContactSheet = HandlingExcelClass.CreateNewSecondaryInfoSheet(emergencyContactSheet, data["patientID"], data["EmergencyContact"], "emr");
            }

            HandlingExcelClass.SaveFile(excelFilePath, package);

            ExcelWorksheet patientDataSheet = null;
            try // patient data sheet
            {
                patientDataSheet = package.Workbook.Worksheets[2];
                if (patientDataSheet == null)
                {
                    {
                        throw new ArgumentNullException(nameof(patientDataSheet));
                    }
                }

                patientDataSheet = HandlingExcelClass.AddToExcistingDepartment(patientDataSheet, data);
            }
            catch (IndexOutOfRangeException ex)
            {
                patientDataSheet = package.Workbook.Worksheets.Add(sheetsNames[2]);
                patientDataSheet = HandlingExcelClass.CreateNewDepartmentsheet(patientDataSheet, data);
                patientDataSheet = HandlingExcelClass.AddToExcistingDepartment(patientDataSheet, data);
            }
            catch (ArgumentNullException ex)
            {
                patientDataSheet = package.Workbook.Worksheets.Add(sheetsNames[2]);
                patientDataSheet = HandlingExcelClass.CreateNewDepartmentsheet(patientDataSheet, data);
                patientDataSheet = HandlingExcelClass.AddToExcistingDepartment(patientDataSheet, data);
            }

            HandlingExcelClass.SaveFile(excelFilePath, package);
        }
    }

    //*********************************************************************search employee****************************************************************

    public static Dictionary<string, dynamic> GetIdDate(string id) // get the data for the wanted patient by ID
    {
        string excelFilePath = "D:\\codez\\uni projects\\hospital system my work\\exel files\\PatientData.xlsx";

        using (ExcelPackage package = new ExcelPackage(excelFilePath))
        {

            ExcelWorksheet sheet = package.Workbook.Worksheets[sheetsNames[2]];


            if (sheet == null)
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

    public static List<Dictionary<string, dynamic>> GetNameDate(string name) // get the data for the wanted patient by Name
    {
        string excelFilePath = "D:\\codez\\uni projects\\hospital system my work\\exel files\\PatientData.xlsx";

        using (ExcelPackage package = new ExcelPackage(excelFilePath))
        {
            ExcelWorksheet sheet = package.Workbook.Worksheets[sheetsNames[2]];

            if (sheet == null)
            {
                Console.WriteLine("ERROR!!! No employees yet in the data");
                return null;
            }

            List<Dictionary<string, dynamic>> similarNames = new List<Dictionary<string, dynamic>>();

            int rowCount = sheet.Dimension.End.Row;
            int colCount = sheet.Dimension.End.Column;
            for (int row = 2; row <= rowCount; row++)
            {
                string target = sheet.Cells[row, 2].Value.ToString();
                if (target.ToUpper() == name.ToUpper())
                {
                    int targetRow = row;
                    Dictionary<string, dynamic> data = new Dictionary<string, dynamic>();

                    string id = sheet.Cells[targetRow, 1].Value.ToString()!;
                    data = getDate(targetRow, colCount, id, sheet, package);

                    similarNames.Add(data);
                }
            }
            return similarNames;
        }
    }

    private static Dictionary<string, dynamic> getDate(int targetRow, int colCount, string id, ExcelWorksheet sheet, ExcelPackage package) // get the data for the wanted patient
    {
        Dictionary<string, dynamic> data = new Dictionary<string, dynamic>();
        for (int col = 1; col <= colCount; col++)
        {
            string field = sheet.Cells[1, col].Value.ToString();
            dynamic val = sheet.Cells[targetRow, col].Value;

            if (field == "DateOfBirth")
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
        sheet = package.Workbook.Worksheets[sheetsNames[0]];
        data["MedicalHistory"] = HandlingExcelClass.getSecondaryInfoData(id, sheet);

        sheet = package.Workbook.Worksheets[sheetsNames[1]];
        data["EmergencyContact"] = HandlingExcelClass.getSecondaryInfoData(id, sheet);

        return data;

    }
    

    //********************************************************************* Generate IDS ****************************************************************
    private static int getLastID(ExcelWorksheet sheet)
    {
        int targetCol = sheet.Dimension.End.Column;

        int newID = int.Parse(sheet.Cells[1, targetCol].Value.ToString()!);

        return newID + 1;

    }

    public static string generateID()
    {
        string excelFilePath = "D:\\codez\\uni projects\\hospital system my work\\exel files\\PatientData.xlsx";

        using(ExcelPackage package = new ExcelPackage(excelFilePath))
        {
            ExcelWorksheet sheet = package.Workbook.Worksheets[2];
            if (sheet == null || sheet.Dimension.End.Column <= 1) return "1";

            else return getLastID(sheet).ToString();
        }

    }
}
