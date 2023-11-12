using SpoRkotLibrary.Data;
using SpoRkotLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using Excel = Microsoft.Office.Interop.Excel;


namespace ExcelLibrary
{
    /// <summary>
    /// Статический класс для чтения данных из Excel-файла
    /// </summary>
    public static class ExcelReader
    {
        /// <summary>
        /// Метод для сравнения отчёта с уже добавленными отчётами
        /// </summary>
        /// <param name="fileName">XLS/XLSX файл</param>
        /// <returns>Возвращает false если отчёт который пытаемся добавить уже существует
        /// возвращает true если такого отчёта еще не было добавлено</returns>
        public async static Task<bool> ImportFromExcel(string fileName)
        {
            using (var context = new RkotContext())
            {
                var reportInfos = context.ReportInfos;

                var reportInfo = LoadDataFromExcel(fileName);
                if (reportInfos.FirstOrDefault(ri=> ri.StartDate == reportInfo.StartDate 
                    && ri.EndDate == reportInfo.EndDate
                    && ri.Location == reportInfo.Location
                    && ri.FederalDistrict == reportInfo.FederalDistrict) != null)
                {
                    return false;
                }

                reportInfos.Add(reportInfo);

                await context.SaveChangesAsync();
                return true;
            }
        }

        /// <summary>
        /// Метод для открыти excel файл 
        /// </summary>
        /// <param name="fileName">XLS/XLSX файл</param>
        /// <returns></returns>
        private static ReportInfo LoadDataFromExcel(string fileName)
        {
            var excelApp = new Excel.Application();

            ReportInfo reportInfo;

            try
            {
                var workbook = excelApp.Workbooks.Open(fileName);
                var worksheet = workbook.Worksheets[1];
                reportInfo = LoadDataFromCells(worksheet);
                workbook.Close();
            }
            finally { excelApp.Quit(); }

            excelApp = null;

            return reportInfo;
        }

        /// <summary>
        /// Метод для выборки нужных данных из excel-файла
        /// </summary>
        /// <param name="worksheet"></param>
        /// <returns></returns>
        private static ReportInfo LoadDataFromCells(dynamic worksheet)
        {
            using var context = new RkotContext();

            var operators = context.Operators;

            var dataColumn = 3;

            string operatorName;

            Operator currentOperator;

            bool isExistOperator;

            List<Report> reports = new List<Report>();
            do
            {
                operatorName = worksheet.Cells[dataColumn][18].Value;

                isExistOperator = operators.FirstOrDefault(o => o.Name == operatorName) != null;

                currentOperator = operators.FirstOrDefault(o => o.Name == operatorName);

                reports.Add(new Report
                {
                    Operator = isExistOperator ? null : new Operator
                    {
                        Name = operatorName
                    },
                    OperatorId = isExistOperator ? currentOperator.OperatorId : 0,
                    VoiceQuality = new VoiceQuality
                    {
                        NonAccessibilityRatio = Convert.ToDecimal(Math.Round(worksheet.Cells[dataColumn][19].Value, 1)),
                        CutoffRatio = Convert.ToDecimal(Math.Round(worksheet.Cells[dataColumn][20].Value, 1)),
                        MOSPOLQA = Convert.ToDecimal(Math.Round(worksheet.Cells[dataColumn][21].Value, 1)),
                        NegativeMOSSamplesRatio = Convert.ToDecimal(Math.Round(worksheet.Cells[dataColumn][22].Value, 1))
                    },
                    SmsQuality = new SmsQuality
                    {
                        ShareUndelivered = Convert.ToDecimal(Math.Round(worksheet.Cells[dataColumn][24].Value, 1)),
                        AverageDeliveryTime = Convert.ToDecimal(Math.Round(worksheet.Cells[dataColumn][25].Value, 1))
                    },
                    HttpQuality = new HttpQuality
                    {
                        SessionFailureRatio = Convert.ToDecimal(Math.Round(worksheet.Cells[dataColumn][27].Value, 1)),
                        ULMeanUserDataRate = Convert.ToDecimal(Math.Round(worksheet.Cells[dataColumn][28].Value, 1)),
                        DLMeanUserDataRate = Convert.ToDecimal(Math.Round(worksheet.Cells[dataColumn][29].Value, 1)),
                        SessionTime = Convert.ToDecimal(Math.Round(worksheet.Cells[dataColumn][30].Value, 1))
                    },
                    Stat = new Stat
                    {
                        CountTestConnection = Convert.ToInt16(worksheet.Cells[dataColumn][32].Value),
                        POLQA = Convert.ToInt32(worksheet.Cells[dataColumn][33].Value),
                        NegativeMOSSamplesCount = Convert.ToInt16(worksheet.Cells[dataColumn][34].Value),
                        CountSMS = Convert.ToInt16(worksheet.Cells[dataColumn][35].Value),
                        CountLoadFile = Convert.ToInt16(worksheet.Cells[dataColumn][36].Value),
                        CountWebBrowsing = Convert.ToInt16(worksheet.Cells[dataColumn][37].Value)
                    }
                });
                dataColumn++;
            } while (worksheet.Cells[dataColumn][18].Value != null);
            ReportInfo reportInfo = new ReportInfo
            {
                StartDate = Convert.ToDateTime(worksheet.Cells[1][13].Value.Substring(30, 10)),
                EndDate = Convert.ToDateTime(worksheet.Cells[1][13].Value.Substring(44, 10)),
                Location = worksheet.Cells[1][14].Value.Substring(27),
                FederalDistrict = GetAbbreveature(worksheet.Cells[1][8].Value.Substring(21)),
                Reports = reports
            };
            return reportInfo;
        }

        private static string GetAbbreveature(string inputString)
        {
            string abbreveature = "";
            var list = inputString.Replace('-', ' ').Split(' ');
            foreach (string item in list)
            {
                abbreveature += item.Trim().Substring(0, 1);
            }
            return abbreveature;
        }
    }
}
