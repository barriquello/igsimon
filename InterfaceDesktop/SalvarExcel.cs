using DocumentFormat.OpenXml.Packaging;
using Ap = DocumentFormat.OpenXml.ExtendedProperties;
using Vt = DocumentFormat.OpenXml.VariantTypes;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Spreadsheet;
using A = DocumentFormat.OpenXml.Drawing;

namespace InterfaceDesktop
{
    public class SalvarExcel
    {
        private static System.UInt32 Inicio = 0;
        private static System.UInt32 Final = 0;
        private static string[] Colunas = { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z", "AA", "AB", "AC", "AD", "AE", "AF", "AG", "AH", "AI", "AJ", "AK", "AL", "AM", "AN", "AO", "AP", "AQ", "AR", "AS", "AT", "AU", "AV", "AW", "AX", "AY", "AZ" };

        public void SalvarXLSX(string Arquivo, System.UInt32 __Inicio, System.UInt32 __Final)
        {
            Inicio = __Inicio;
            Final = __Final;
            CreatePackage(Arquivo);
        }

        // Generates content of worksheetPart3.
        private void ____GenerateWorksheetPart3Content(WorksheetPart worksheetPart3)
        {

            SheetData sheetData3 = new SheetData();
            System.Globalization.CultureInfo separadorDecimal = System.Globalization.CultureInfo.InvariantCulture;
            FeedServidor[] Feeds = Variaveis.strVariaveis();
            // Primeira linha: nomes dos feeds
            Row row1 = new Row() { RowIndex = (UInt32Value)1U, Spans = new ListValue<StringValue>() { InnerText = "1:" + (Feeds.Length + 1).ToString() } };
            Cell cell1 = new Cell() { CellReference = Colunas[0] + "1", DataType = CellValues.String };//.SharedString };
            CellValue cellValue1 = new CellValue();
            cellValue1.Text = "Horário";

            cell1.Append(cellValue1);
            row1.Append(cell1);
            for (int jj = 0; jj < Feeds.Length; jj++)
            {
                cell1 = new Cell() { CellReference = Colunas[jj + 1] + "1", DataType = CellValues.String };//.SharedString };
                cellValue1 = new CellValue();
                cellValue1.Text = Feeds[jj].NomeFeed;
                cell1.Append(cellValue1);
                row1.Append(cell1);
            }
            sheetData3.Append(row1);
            UInt32Value Linha = 2;
            for (int kk = 0; kk < frmMain.Registros.Count; kk++)
            {
                RegistroDB Registro = frmMain.Registros[kk];
                if ((Registro.Horario > Inicio) & (Registro.Horario < Final))
                {
                    Row row2 = new Row() { RowIndex = (UInt32Value)Linha++, Spans = new ListValue<StringValue>() { InnerText = "1:" + (Feeds.Length + 1).ToString() } };
                    cell1 = new Cell() { CellReference = Colunas[0] + (kk + 2).ToString(), StyleIndex = (UInt32Value)1U, DataType = CellValues.Number };
                    cellValue1 = new CellValue();
                    cellValue1.Text = Registro.Horario.ToString(separadorDecimal);
                    cell1.Append(cellValue1);
                    row2.Append(cell1);
                    for (int jj = 0; jj < Feeds.Length; jj++)
                    {
                        cell1 = new Cell() { CellReference = Colunas[jj + 1] + (kk + 2).ToString(), StyleIndex = (UInt32Value)1U, DataType = CellValues.Number };
                        cellValue1 = new CellValue();
                        cellValue1.Text = Registro.P[Feeds[jj].indice].ToString(separadorDecimal);
                        cell1.Append(cellValue1);
                        row2.Append(cell1);
                    }
                    sheetData3.Append(row2);
                }
            }

        }


        // Creates a SpreadsheetDocument.
        private void CreatePackage(string filePath)
        {
            using (SpreadsheetDocument package = SpreadsheetDocument.Create(filePath, SpreadsheetDocumentType.Workbook))
            {
                CreateParts(package);
            }
        }
        // Adds child parts and generates content of the specified part.
        private void CreateParts(SpreadsheetDocument document)
        {
            ExtendedFilePropertiesPart extendedFilePropertiesPart1 = document.AddNewPart<ExtendedFilePropertiesPart>("rId3");
            GenerateExtendedFilePropertiesPart1Content(extendedFilePropertiesPart1);

            WorkbookPart workbookPart1 = document.AddWorkbookPart();
            GenerateWorkbookPart1Content(workbookPart1);

            WorksheetPart worksheetPart1 = workbookPart1.AddNewPart<WorksheetPart>("rId3");
            GenerateWorksheetPart1Content(worksheetPart1);

            WorksheetPart worksheetPart2 = workbookPart1.AddNewPart<WorksheetPart>("rId2");
            GenerateWorksheetPart2Content(worksheetPart2);

            WorksheetPart worksheetPart3 = workbookPart1.AddNewPart<WorksheetPart>("rId1");
            GenerateWorksheetPart3Content(worksheetPart3);

            SharedStringTablePart sharedStringTablePart1 = workbookPart1.AddNewPart<SharedStringTablePart>("rId6");
            GenerateSharedStringTablePart1Content(sharedStringTablePart1);

            WorkbookStylesPart workbookStylesPart1 = workbookPart1.AddNewPart<WorkbookStylesPart>("rId5");
            GenerateWorkbookStylesPart1Content(workbookStylesPart1);

            //SetPackageProperties(document);
        }


        // Generates content of extendedFilePropertiesPart1.
        private void GenerateExtendedFilePropertiesPart1Content(ExtendedFilePropertiesPart extendedFilePropertiesPart1)
        {
            Ap.Properties properties1 = new Ap.Properties();
            properties1.AddNamespaceDeclaration("vt", "http://schemas.openxmlformats.org/officeDocument/2006/docPropsVTypes");
            Ap.Application application1 = new Ap.Application();
            application1.Text = "Microsoft Excel";
            Ap.DocumentSecurity documentSecurity1 = new Ap.DocumentSecurity();
            documentSecurity1.Text = "0";
            Ap.ScaleCrop scaleCrop1 = new Ap.ScaleCrop();
            scaleCrop1.Text = "false";

            Ap.HeadingPairs headingPairs1 = new Ap.HeadingPairs();

            Vt.VTVector vTVector1 = new Vt.VTVector() { BaseType = Vt.VectorBaseValues.Variant, Size = (UInt32Value)2U };

            Vt.Variant variant1 = new Vt.Variant();
            Vt.VTLPSTR vTLPSTR1 = new Vt.VTLPSTR();
            vTLPSTR1.Text = "Planilhas";

            variant1.Append(vTLPSTR1);

            Vt.Variant variant2 = new Vt.Variant();
            Vt.VTInt32 vTInt321 = new Vt.VTInt32();
            vTInt321.Text = "3";

            variant2.Append(vTInt321);

            vTVector1.Append(variant1);
            vTVector1.Append(variant2);

            headingPairs1.Append(vTVector1);

            Ap.TitlesOfParts titlesOfParts1 = new Ap.TitlesOfParts();

            Vt.VTVector vTVector2 = new Vt.VTVector() { BaseType = Vt.VectorBaseValues.Lpstr, Size = (UInt32Value)3U };
            Vt.VTLPSTR vTLPSTR2 = new Vt.VTLPSTR();
            vTLPSTR2.Text = "Plan1";
            Vt.VTLPSTR vTLPSTR3 = new Vt.VTLPSTR();
            vTLPSTR3.Text = "Plan2";
            Vt.VTLPSTR vTLPSTR4 = new Vt.VTLPSTR();
            vTLPSTR4.Text = "Plan3";

            vTVector2.Append(vTLPSTR2);
            vTVector2.Append(vTLPSTR3);
            vTVector2.Append(vTLPSTR4);

            titlesOfParts1.Append(vTVector2);
            Ap.LinksUpToDate linksUpToDate1 = new Ap.LinksUpToDate();
            linksUpToDate1.Text = "false";
            Ap.SharedDocument sharedDocument1 = new Ap.SharedDocument();
            sharedDocument1.Text = "false";
            Ap.HyperlinksChanged hyperlinksChanged1 = new Ap.HyperlinksChanged();
            hyperlinksChanged1.Text = "false";
            Ap.ApplicationVersion applicationVersion1 = new Ap.ApplicationVersion();
            applicationVersion1.Text = "12.0000";

            properties1.Append(application1);
            properties1.Append(documentSecurity1);
            properties1.Append(scaleCrop1);
            properties1.Append(headingPairs1);
            properties1.Append(titlesOfParts1);
            properties1.Append(linksUpToDate1);
            properties1.Append(sharedDocument1);
            properties1.Append(hyperlinksChanged1);
            properties1.Append(applicationVersion1);

            extendedFilePropertiesPart1.Properties = properties1;
        }

        // Generates content of workbookPart1.
        private void GenerateWorkbookPart1Content(WorkbookPart workbookPart1)
        {
            Workbook workbook1 = new Workbook();
            workbook1.AddNamespaceDeclaration("r", "http://schemas.openxmlformats.org/officeDocument/2006/relationships");
            FileVersion fileVersion1 = new FileVersion() { ApplicationName = "xl", LastEdited = "4", LowestEdited = "4", BuildVersion = "4506" };
            WorkbookProperties workbookProperties1 = new WorkbookProperties() { DefaultThemeVersion = (UInt32Value)124226U };

            BookViews bookViews1 = new BookViews();
            WorkbookView workbookView1 = new WorkbookView() { XWindow = 240, YWindow = 90, WindowWidth = (UInt32Value)19980U, WindowHeight = (UInt32Value)8070U };

            bookViews1.Append(workbookView1);

            Sheets sheets1 = new Sheets();
            Sheet sheet1 = new Sheet() { Name = "Plan1", SheetId = (UInt32Value)1U, Id = "rId1" };
            Sheet sheet2 = new Sheet() { Name = "Plan2", SheetId = (UInt32Value)2U, Id = "rId2" };
            Sheet sheet3 = new Sheet() { Name = "Plan3", SheetId = (UInt32Value)3U, Id = "rId3" };

            sheets1.Append(sheet1);
            sheets1.Append(sheet2);
            sheets1.Append(sheet3);
            CalculationProperties calculationProperties1 = new CalculationProperties() { CalculationId = (UInt32Value)125725U };

            workbook1.Append(fileVersion1);
            workbook1.Append(workbookProperties1);
            workbook1.Append(bookViews1);
            workbook1.Append(sheets1);
            workbook1.Append(calculationProperties1);

            workbookPart1.Workbook = workbook1;
        }

        // Generates content of worksheetPart1.
        private void GenerateWorksheetPart1Content(WorksheetPart worksheetPart1)
        {
            Worksheet worksheet1 = new Worksheet();
            worksheet1.AddNamespaceDeclaration("r", "http://schemas.openxmlformats.org/officeDocument/2006/relationships");
            SheetDimension sheetDimension1 = new SheetDimension() { Reference = "A1" };

            SheetViews sheetViews1 = new SheetViews();
            SheetView sheetView1 = new SheetView() { WorkbookViewId = (UInt32Value)0U };

            sheetViews1.Append(sheetView1);
            SheetFormatProperties sheetFormatProperties1 = new SheetFormatProperties() { DefaultRowHeight = 15D };
            SheetData sheetData1 = new SheetData();
            PageMargins pageMargins1 = new PageMargins() { Left = 0.511811024D, Right = 0.511811024D, Top = 0.78740157499999996D, Bottom = 0.78740157499999996D, Header = 0.31496062000000002D, Footer = 0.31496062000000002D };

            worksheet1.Append(sheetDimension1);
            worksheet1.Append(sheetViews1);
            worksheet1.Append(sheetFormatProperties1);
            worksheet1.Append(sheetData1);
            worksheet1.Append(pageMargins1);

            worksheetPart1.Worksheet = worksheet1;
        }

        // Generates content of worksheetPart2.
        private void GenerateWorksheetPart2Content(WorksheetPart worksheetPart2)
        {
            Worksheet worksheet2 = new Worksheet();
            worksheet2.AddNamespaceDeclaration("r", "http://schemas.openxmlformats.org/officeDocument/2006/relationships");
            SheetDimension sheetDimension2 = new SheetDimension() { Reference = "A1" };

            SheetViews sheetViews2 = new SheetViews();
            SheetView sheetView2 = new SheetView() { WorkbookViewId = (UInt32Value)0U };

            sheetViews2.Append(sheetView2);
            SheetFormatProperties sheetFormatProperties2 = new SheetFormatProperties() { DefaultRowHeight = 15D };
            SheetData sheetData2 = new SheetData();
            PageMargins pageMargins2 = new PageMargins() { Left = 0.511811024D, Right = 0.511811024D, Top = 0.78740157499999996D, Bottom = 0.78740157499999996D, Header = 0.31496062000000002D, Footer = 0.31496062000000002D };

            worksheet2.Append(sheetDimension2);
            worksheet2.Append(sheetViews2);
            worksheet2.Append(sheetFormatProperties2);
            worksheet2.Append(sheetData2);
            worksheet2.Append(pageMargins2);

            worksheetPart2.Worksheet = worksheet2;
        }

        // Generates content of worksheetPart3.
        private void GenerateWorksheetPart3Content(WorksheetPart worksheetPart3)
        {
            System.Globalization.CultureInfo SeparadorDecimal = System.Globalization.CultureInfo.InvariantCulture;
            FeedServidor[] Feeds = Variaveis.strVariaveis();
            Worksheet worksheet3 = new Worksheet();
            worksheet3.AddNamespaceDeclaration("r", "http://schemas.openxmlformats.org/officeDocument/2006/relationships");

            SheetViews sheetViews3 = new SheetViews();

            SheetView sheetView3 = new SheetView() { TabSelected = true, WorkbookViewId = (UInt32Value)0U };
            Selection selection1 = new Selection() { ActiveCell = "A1", SequenceOfReferences = new ListValue<StringValue>() { InnerText = "A1" } };

            sheetView3.Append(selection1);

            sheetViews3.Append(sheetView3);
            SheetFormatProperties sheetFormatProperties3 = new SheetFormatProperties() { DefaultRowHeight = 20D };

            SheetData sheetData3 = new SheetData();

            //Row row1 = new Row() { RowIndex = (UInt32Value)1U, Spans = new ListValue<StringValue>() { InnerText = "1:4" } };
            Row row1 = new Row() { RowIndex = 1U, Spans = new ListValue<StringValue>() { InnerText = "1:" + (Feeds.Length + 1).ToString() } };
            //Cell cell1 = new Cell() { CellReference = "A1", DataType = CellValues.SharedString };
            Cell cell1 = new Cell() { CellReference = Colunas[0] + "1", DataType = CellValues.String };
            CellValue cellValue1 = new CellValue();
            cellValue1.Text = "Horário";
            cell1.Append(cellValue1);
            row1.Append(cell1);
            for (int jj = 0; jj < Feeds.Length; jj++)
            {
                cell1 = new Cell() { CellReference = Colunas[jj + 1] + "1", DataType = CellValues.String };
                cellValue1 = new CellValue(Feeds[jj].NomeFeed);
                cell1.Append(cellValue1);
                row1.Append(cell1);
            }
            sheetData3.Append(row1);
            UInt32Value NumeroLinhas = 1;
            for (int kk = 0;kk<frmMain.Registros.Count;kk++)
            {
                RegistroDB registro = frmMain.Registros[kk];
                if ((Inicio < registro.Horario) & (Final > registro.Horario))
                {
                    row1 = new Row() { RowIndex = ++NumeroLinhas, Spans = new ListValue<StringValue>() { InnerText = "1:" + (Feeds.Length + 1).ToString() } };
                    cell1 = new Cell() { CellReference = Colunas[0] + NumeroLinhas.ToString(), DataType = CellValues.String };
                    cellValue1= new CellValue();
                    cellValue1.Text = Uteis.Unix2time(registro.Horario).ToString();
                    cell1.Append(cellValue1);
                    row1.Append(cell1);
                    for (int jj = 0; jj < Feeds.Length; jj++)
                    {
                        cell1 = new Cell() { CellReference = Colunas[jj + 1] + NumeroLinhas.ToString() };
                        cellValue1 = new CellValue();
                        cellValue1.Text = registro.P[Feeds[jj].indice].ToString(SeparadorDecimal);
                        cell1.Append(cellValue1);
                        row1.Append(cell1);
                    }
                    sheetData3.Append(row1);
                }
            }
            SheetDimension sheetDimension3 = new SheetDimension() { Reference = "A1:" + Colunas[Feeds.Length + 1] + NumeroLinhas.ToString() };

            //Row row2 = new Row() { RowIndex = (UInt32Value)2U, Spans = new ListValue<StringValue>() { InnerText = "1:4" } };

            Cell cell5 = new Cell() { CellReference = "A2" };
            CellValue cellValue5 = new CellValue();
            cellValue5.Text = "123";

            PageMargins pageMargins3 = new PageMargins() { Left = 0.511811024D, Right = 0.511811024D, Top = 0.78740157499999996D, Bottom = 0.78740157499999996D, Header = 0.31496062000000002D, Footer = 0.31496062000000002D };

            worksheet3.Append(sheetDimension3);
            worksheet3.Append(sheetViews3);
            worksheet3.Append(sheetFormatProperties3);
            worksheet3.Append(sheetData3);
            worksheet3.Append(pageMargins3);

            worksheetPart3.Worksheet = worksheet3;
        }

        // Generates content of sharedStringTablePart1.
        //tentar remover isso
        private void GenerateSharedStringTablePart1Content(SharedStringTablePart sharedStringTablePart1)
        {
            SharedStringTable sharedStringTable1 = new SharedStringTable() { Count = (UInt32Value)4U, UniqueCount = (UInt32Value)4U };

            SharedStringItem sharedStringItem1 = new SharedStringItem();
            Text text1 = new Text();
            text1.Text = "Coluna1";

            sharedStringItem1.Append(text1);

            SharedStringItem sharedStringItem2 = new SharedStringItem();
            Text text2 = new Text();
            text2.Text = "Coluna2";

            sharedStringItem2.Append(text2);

            SharedStringItem sharedStringItem3 = new SharedStringItem();
            Text text3 = new Text();
            text3.Text = "Coluna3";

            sharedStringItem3.Append(text3);

            SharedStringItem sharedStringItem4 = new SharedStringItem();
            Text text4 = new Text();
            text4.Text = "colunan";

            sharedStringItem4.Append(text4);

            sharedStringTable1.Append(sharedStringItem1);
            sharedStringTable1.Append(sharedStringItem2);
            sharedStringTable1.Append(sharedStringItem3);
            sharedStringTable1.Append(sharedStringItem4);

            sharedStringTablePart1.SharedStringTable = sharedStringTable1;
        }

        // Generates content of workbookStylesPart1.
        private void GenerateWorkbookStylesPart1Content(WorkbookStylesPart workbookStylesPart1)
        {
            Stylesheet stylesheet1 = new Stylesheet();
            workbookStylesPart1.Stylesheet = stylesheet1;
        }

        private void SetPackageProperties(OpenXmlPackage document)
        {
            document.PackageProperties.Creator = "UFSM";
            document.PackageProperties.Created =
            document.PackageProperties.Modified = System.DateTime.Now;
            document.PackageProperties.LastModifiedBy = "UFSM";
        }
        
    }
}
