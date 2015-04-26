using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using ICSharpCode.SharpZipLib.Zip;
using NPOI.HSSF.UserModel;
using SimpleStock.Common.Entities;
using SimpleStock.Common.Mongo;

namespace SimpleStock.Downloader
{
    public class Downloader
    {
        public void DownloadStock()
        {
            const string url = "http://115.29.204.48/amac/csrcindustry.zip";
            var zip = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "csrcindustry.zip");

            try
            {
                var webClient = new WebClient();
                webClient.DownloadFile(url, zip);
            }
            catch 
            {
                Console.WriteLine("Download file failed.");
            }

            ExtractZip(zip, AppDomain.CurrentDomain.BaseDirectory);

            var filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "csrcindustry.xls");

            var stocks = new List<Stock>();

            using (var stream = new FileStream(filePath, FileMode.Open))
            {
                var workbook = new HSSFWorkbook(stream);
                var sheet = workbook.GetSheetAt(0);
                for (var i = sheet.FirstRowNum + 1; i <= sheet.LastRowNum; i++)
                {
                    var row = sheet.GetRow(i);
                    var stock = new Stock
                    {
                        EffectiveDate = DateTime.Parse(row.GetCell(0).StringCellValue),
                        SecuritiesCode = row.GetCell(1).StringCellValue,
                        SecuritiesName = row.GetCell(2).StringCellValue,
                        SecuritiesNameEng = row.GetCell(3).StringCellValue,
                        Exchange = row.GetCell(4).StringCellValue,
                        IndustryCode = row.GetCell(5).StringCellValue,
                        IndustryName = row.GetCell(6).StringCellValue,
                        IndustryNameEng = row.GetCell(7).StringCellValue,
                        FullIndustryCode = row.GetCell(8).StringCellValue,
                        FullIndustryName = row.GetCell(9).StringCellValue,
                        FullIndustryNameEng = row.GetCell(10).StringCellValue
                    };
                    stocks.Add(stock);
                }
            }

            if (stocks.Count > 0)
            {
                var stockRepository = new MongoRepository<Stock>();
                stockRepository.RemoveAll();
                stockRepository.InsertBatch(stocks);
            }
        }

        public void DownloadStockPrice()
        {

        }

        private void ExtractZip(string zip, string directory, string password = null, string fileFilter = null)
        {
            var fastZip = new FastZip();

            if (!string.IsNullOrEmpty(password))
            {
                fastZip.Password = password;
            }

            fastZip.ExtractZip(zip, directory, fileFilter);
        }
    }
}
