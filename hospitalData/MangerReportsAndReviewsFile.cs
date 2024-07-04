using OfficeOpenXml;
using System.Drawing;

namespace hospitalData
{
    public static class MangerReportsAndReviewsFile
    {
        private static ExcelWorksheet CreatMangerReportsAndReviewsFile(ExcelPackage package)
        {
            using (ExcelWorksheet sheet = package.Workbook.Worksheets.Add("Manger reports and reviews"))
            {
                sheet.Cells[1, 1].Value = "Title";
                sheet.Cells[1, 1].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                sheet.Cells[1, 1].Style.Font.Bold = true;

                sheet.Cells[1, 2].Value = "Type";
                sheet.Cells[1, 2].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                sheet.Cells[1, 2].Style.Font.Bold = true;

                sheet.Cells[1, 3].Value = "Content";
                sheet.Cells[1, 3].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                sheet.Cells[1, 3].Style.Font.Bold = true;

                sheet.Cells[1, 4].Value = "Statue";
                sheet.Cells[1, 4].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                sheet.Cells[1, 4].Style.Font.Bold = true;

                sheet.Cells[1, 5].Value = "Date";
                sheet.Cells[1, 5].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                sheet.Cells[1, 5].Style.Font.Bold = true;

                package.SaveAs(package.File.FullName);

                return sheet;
            }
        }
        //********************************************** writing reoprorts and reviews********************************************

        public static void accessMangerReportsAndReviewsFile(string contenType, string contentTitle, string content, string contentDate) // add report/review to the sheet
        {
            string excelFilePath = Path.Combine(HandlingExcelClass.baseDir, @"..\..\..\..\excel files\Manger reports and reviews.xlsx");
            excelFilePath = Path.GetFullPath(excelFilePath);

            using (ExcelPackage package = new ExcelPackage(excelFilePath))
            {
                ExcelWorksheet sheet = null;
                try
                {
                    sheet = package.Workbook.Worksheets["Manger reports and reviews"];
                    if (sheet == null)
                    {
                        throw new ArgumentNullException(nameof(sheet));
                    }
                }
                catch (ArgumentNullException e)
                {
                    sheet = CreatMangerReportsAndReviewsFile(package);
                }

                int targetRow = sheet.Dimension.End.Row;
                targetRow += 1;

                var cellColor = Color.White;
                if(contenType == "Report")
                {
                    cellColor = Color.Red;
                }
                if(contenType == "Review")
                {
                    cellColor = Color.Green;
                }


                sheet.Cells[targetRow, 1].Value = contentTitle;
                sheet.Cells[targetRow, 1].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                sheet.Cells[targetRow, 1].Style.Font.Bold = true;
                sheet.Cells[targetRow, 1].Style.Fill.SetBackground(cellColor);

                sheet.Cells[targetRow, 2].Value = contenType;
                sheet.Cells[targetRow, 2].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                sheet.Cells[targetRow, 2].Style.Fill.SetBackground(cellColor);

                sheet.Cells[targetRow, 3].Value = content;
                sheet.Cells[targetRow, 3].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                sheet.Cells[targetRow, 3].Style.Fill.SetBackground(cellColor);

                sheet.Cells[targetRow, 4].Value = "NS";
                sheet.Cells[targetRow, 4].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                sheet.Cells[targetRow, 4].Style.Fill.SetBackground(cellColor);

                sheet.Cells[targetRow, 5].Value = contentDate;
                sheet.Cells[targetRow, 5].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                sheet.Cells[targetRow, 5].Style.Fill.SetBackground(cellColor);

                package.Save();

            }
        }



        //********************************************** reviewing reoprorts and reviews********************************************

        public static void accessMangerReportsAndReviewsFile(string contenType, string targetedStatue) // Access reports/reviews with wanted statues
        {
            string excelFilePath = Path.Combine(HandlingExcelClass.baseDir, @"..\..\..\..\excel files\Manger reports and reviews.xlsx");
            excelFilePath = Path.GetFullPath(excelFilePath);
            using (ExcelPackage package = new ExcelPackage(excelFilePath))
            {
                ExcelWorksheet sheet = null;

                try
                {
                    sheet = package.Workbook.Worksheets["Manger reports and reviews"];
                    if(sheet == null)
                    {
                        throw new ArgumentNullException(nameof(sheet));
                    }
                }
                catch (ArgumentNullException e)
                {
                    Console.WriteLine(e.Message);
                    Console.WriteLine("No reports or reviews so far");
                    return;
                }

                //loop throw and get a dict with the wanted statue method
                var motherDict = loopThrowDate(sheet, contenType, targetedStatue); // contain a dictionary(content title & content) and a list(content sataue).

                if (motherDict.Count == 0)
                {
                    if(targetedStatue != "All")
                    {
                        Console.WriteLine($"There is no {targetedStatue} {contenType} right now");
                    }
                    else
                    {
                        Console.WriteLine($"No {contenType} written so far");
                    }
                    return;
                }

                //start reviewing the list as long as the user want method
                chooseContentToReview(sheet, motherDict, contenType);

                package.Save();

            }
        }

        private static Dictionary<Dictionary<string, string>, List<string>> loopThrowDate(ExcelWorksheet sheet, string contenType, string targetedStatue)
        {
            var data = new Dictionary<string, string>();
            var statueList = new List<string>(); // every index parallel to the one at "data" dictionary..

            int rowCount = sheet.Dimension.End.Row;

            for (int row = 2; row <= rowCount; row++)
            {
                string Type = sheet.Cells[row, 2].Value.ToString()!;

                if (Type == contenType)
                {
                    string statue = sheet.Cells[row, 4].Value.ToString()!;
                    if (targetedStatue == "All") // get all statue types of the wanted content
                    {
                        string title = sheet.Cells[row, 1].Value.ToString()!;
                        string content = sheet.Cells[row, 3].Value.ToString()!;
                        string thisContentStatue = sheet.Cells[row, 4].Value.ToString()!;

                        data[title] = content;
                        statueList.Add(thisContentStatue);
                    }
                    else //get only the wnated statue
                    {
                        if (statue == targetedStatue)
                        {
                            string title = sheet.Cells[row, 1].Value.ToString()!;
                            string content = sheet.Cells[row, 3].Value.ToString()!;
                            string thisContentStatue = sheet.Cells[row, 4].Value.ToString()!;

                            data[title] = content;
                            statueList.Add(thisContentStatue);
                        }
                    }
                }
            }
            var motherDict = new Dictionary<Dictionary<string, string>, List<string>>();

            if(data.Count != 0)  motherDict[data] = statueList;

            return motherDict;
        }

        private static ExcelWorksheet chooseContentToReview(ExcelWorksheet sheet, Dictionary<Dictionary<string, string>, List<string>> motherDict, string contentType) //user will choose any content from the list they spicefied earlier
        {
            int choice = 0;
            while (true) // keep chooseing as long as the user want
            {
                int index = 1;

                var contentDict = motherDict.ElementAt(0).Key;
                var statueList = motherDict.ElementAt(0).Value;

                Console.WriteLine($"Choose a {contentType} from bellow");
                Console.WriteLine("no.\t\tTitle\t\tStatue");
                foreach (var item in contentDict)
                {
                    Console.WriteLine($"{index}.\t\t{item.Key}.\t\t{statueList[index-1]}");
                    index++;
                }
                Console.WriteLine($"{index}. Exist");
                choice = 0;
                while (true) // make sure its a avlid choice
                {
                    Console.Write("\nYour Choice : ");
                    choice = int.Parse(Console.ReadLine()!);
                    if (choice == index) return sheet;
                    else if (choice < 1 || choice > contentDict.Count)
                    {
                        Console.WriteLine("plz, enter a valid choice!");
                        continue;
                    }
                    break;
                }

                string wantdetContent = contentDict.ElementAt(choice - 1).Value;
                string wantdetContentTitle = contentDict.ElementAt(choice - 1).Key;
                Console.WriteLine($"\n\n{wantdetContent}\nCurrent Statue : {statueList[choice - 1]}");
                Console.WriteLine("\n1. Accept\n2. Deny\n3. Not Set");

                while (true) // make sure its a avlid choice
                {
                    Console.Write("\nYour Choice : ");
                    choice = int.Parse(Console.ReadLine()!);
                    if (choice < 1 || choice > 3)
                    {
                        Console.WriteLine("plz, enter a valid choice!");
                        continue;
                    }

                    break;
                }

                string newStatue = string.Empty;
                switch (choice)
                {
                    case 1:
                        newStatue = "Accepted";
                        break;
                    case 2:
                        newStatue = "Denied";
                        break;
                    case 3:
                        newStatue = "NS";
                        break;
                }
                //travers for the content title and change the statue if changed
                return setContentSatue(sheet, wantdetContentTitle, newStatue);
            }
        }

        private static ExcelWorksheet setContentSatue(ExcelWorksheet sheet, string title, string statue) // change the content statue
        {
            int rowCount = sheet.Dimension.End.Row;

            for (int row = 2; row <= rowCount; row++)
            {
                string target = sheet.Cells[row, 1].Value.ToString()!;
                if (target == title)
                {
                    sheet.Cells[row, 4].Value = statue;
                    break;
                }
            }
            return sheet;
        }
    }
}
