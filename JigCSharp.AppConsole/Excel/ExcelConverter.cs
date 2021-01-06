using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using ClosedXML;
using ClosedXML.Excel;
using ClosedXML.Report;
using DocumentFormat.OpenXml.Wordprocessing;
using JigCSharp.Parser.SyntaxData.Class;
using JigCSharp.Parser.SyntaxData.Namespace;
using MoreLinq;

namespace JigCSharp.AppConsole.Excel
{
    public class ExcelConverter
    {
        public static void Convert(NamespaceDataList namespaceDataList, string outputFilename)
        {
            var namespaceDto = namespaceDataList.ToDto();
            

            using (var workbook = new XLWorkbook())
            {
                WritePackageSheet(workbook, namespaceDto);
                WriteAllSheet(workbook, namespaceDto);
                // TODO: Enumeration型を列挙
                
                workbook.SaveAs(outputFilename);
            }
        }
        
        /// <summary>
        /// パッケージシートを出力
        /// </summary>
        /// <param name="workbook"></param>
        /// <param name="namespaceDtos"></param>
        private static void WritePackageSheet(XLWorkbook workbook, IEnumerable<NamespaceDto> namespaceDtos)
        {
            var worksheet = workbook.AddWorksheet("パッケージ");

            var row = 1;
            worksheet.Cell(row, 1).SetValue("パッケージ");
            worksheet.Cell(row, 2).SetValue("パッケージ別名");
            worksheet.Cell(row, 3).SetValue("クラス数");

            row++;

            foreach (var namespaceData in namespaceDtos)
            {
                worksheet.Cell(row, 1).SetValue(namespaceData.Name);
                worksheet.Cell(row, 2).SetValue(namespaceData.DisplayName);
                worksheet.Cell(row, 3).SetValue(namespaceData.ClassDtos.Count().ToString());
                row++;
            }

            worksheet.ColumnsUsed().AdjustToContents();
        }

        private static void WriteAllSheet(XLWorkbook workbook, IEnumerable<NamespaceDto> namespaceDtos)
        {
            var worksheet = workbook.AddWorksheet("ALL");

            var row = 1;
            worksheet.Cell(row, 1).SetValue("パッケージ名");
            worksheet.Cell(row, 2).SetValue("クラス名");
            worksheet.Cell(row, 3).SetValue("クラス別名");
            worksheet.Cell(row, 4).SetValue("値の種類");
            worksheet.Cell(row, 5).SetValue("レイヤ");

            row++;

            foreach (var namespaceData in namespaceDtos)
            {
                foreach (var classDto in namespaceData.ClassDtos)
                {
                    worksheet.Cell(row, 1).SetValue(namespaceData.Name);
                    worksheet.Cell(row, 2).SetValue(classDto.Name);
                    worksheet.Cell(row, 3).SetValue(classDto.DisplayName);
                    worksheet.Cell(row, 4).SetValue(classDto.ValueKind);
                    worksheet.Cell(row, 5).SetValue(classDto.ClassAttributeKind);
                    row++;
                }
            }

            worksheet.ColumnsUsed().AdjustToContents();
        }

        private static void AdjustToContents(IXLWorksheet worksheet, IEnumerable<int> toAdjustColumns)
        {
            foreach (var column in toAdjustColumns)
            {
                worksheet.Column(column).AdjustToContents();
            }
        }

        /// <summary>
        /// ENUMERATIONクラスを継承している型をすべて列挙する
        /// </summary>
        /// <param name="workbook"></param>
        /// <param name="namespaceDtos"></param>
        /// <exception cref="NotImplementedException"></exception>
        private static void WriteEnumeration(XLWorkbook workbook, IEnumerable<NamespaceDto> namespaceDtos)
        {
            throw new NotImplementedException();
            
            // パッケージ名, クラス名, クラス別名, 定数宣言(リスト), 使用箇所数, 使用箇所, パラメータあり, 振舞あり
        }

        /// <summary>
        /// コントローラクラス内容を列挙する
        /// </summary>
        /// <param name="workbook"></param>
        /// <param name="namespaceDtos"></param>
        private static void WriteController(XLWorkbook workbook, IEnumerable<NamespaceDto> namespaceDtos)
        {
            throw new NotImplementedException();
            // パッケージ名, クラス名, メソッド名, クラス別名, パス
        }

        /// <summary>
        /// サービス内容を列挙する
        /// </summary>
        /// <param name="workbook"></param>
        /// <param name="namespaceDtos"></param>
        /// <exception cref="NotImplementedException"></exception>
        private static void WriteService(XLWorkbook workbook, IEnumerable<NamespaceDto> namespaceDtos)
        {
            throw new NotImplementedException();
            
            //パッケージ名, クラス名, メソッド名, メソッド戻り値, メソッド別名, メソッド戻り値 別名, 使用しているサービス名, 使用しているリポジトリ名
        }
        
        
    }
}