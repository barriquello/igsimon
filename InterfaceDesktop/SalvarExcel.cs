using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Spreadsheet;

namespace InterfaceDesktop
{
    public class SalvarExcel
    {
        public void SalvarXLSX(string Arquivo, System.UInt32 Inicio, System.UInt32 Final, FormSalvarExcel formulario)
        {
            using (SpreadsheetDocument excel = SpreadsheetDocument.Create(Arquivo, DocumentFormat.OpenXml.SpreadsheetDocumentType.Workbook))
            {
                System.Globalization.CultureInfo SeparadorDecimal = System.Globalization.CultureInfo.InvariantCulture;
                FeedServidor[] feeds = Variaveis.strVariaveis();
                int NumRegs = 0;
                switch (formulario)
                {
                    case FormSalvarExcel.frmMain:
                        NumRegs = frmMain.Registros.Count;
                        break;
                    case FormSalvarExcel.frmGraficos:
                        NumRegs = frmGraficos.Registros.Count;
                        break;
                    case FormSalvarExcel.frmComparacao:
                        NumRegs = 2;
                        break;
                }

                WorkbookPart Bparte = excel.AddWorkbookPart();
                Bparte.Workbook = new Workbook();
                Sheets planilhas = Bparte.Workbook.AppendChild<Sheets>(new Sheets());

                WorksheetPart Sparte = Bparte.AddNewPart<WorksheetPart>();
                Sheet planilha = new Sheet() { Id = excel.WorkbookPart.GetIdOfPart(Sparte), SheetId = 1, Name = "Plan_1"};
                planilhas.Append(planilha);
                OpenXmlWriter Writer = OpenXmlWriter.Create(Sparte);
                {
                    Writer.WriteStartElement(new Worksheet());
                    {
                        {
                            Writer.WriteStartElement(new SheetViews());
                            {
                                Writer.WriteStartElement(new SheetView() { TabSelected = true, WorkbookViewId = (UInt32Value)0U });
                                Writer.WriteElement(new Pane() { HorizontalSplit = 1D, VerticalSplit = 1D, TopLeftCell = "B2", ActivePane = PaneValues.BottomRight, State = PaneStateValues.FrozenSplit });
                                Writer.WriteEndElement();
                            }
                            Writer.WriteEndElement();
                        }

                        {
                            Writer.WriteStartElement(new Columns());
                            Writer.WriteElement(new Column() { Min = (UInt32Value)1U, Max = (UInt32Value)1U, Width = 18D, CustomWidth = true });
                            Writer.WriteEndElement();
                        }

                        Writer.WriteStartElement(new SheetData());
                        {
                            {
                                Writer.WriteStartElement(new Row());

                                Writer.WriteElement(new Cell { CellValue = new CellValue("Horário"), DataType = CellValues.String });

                                for (int jj = 0; jj < feeds.Length; jj++)
                                {
                                    Writer.WriteElement(new Cell { CellValue = new CellValue(feeds[jj].NomeFeed), DataType = CellValues.String });
                                }
                                Writer.WriteEndElement();
                            }
                        }

                        for (int jj = 0; jj < NumRegs; jj++)
                        {
                            RegistroDB registro;
                            switch (formulario)
                            {
                                case FormSalvarExcel.frmMain:
                                    registro = frmMain.Registros[jj];
                                    break;
                                case FormSalvarExcel.frmGraficos:
                                    registro = frmGraficos.Registros[jj];
                                    break;
                                case FormSalvarExcel.frmComparacao:
                                    registro = frmCompara.reg1[jj];
                                    break;
                                default:
                                    registro = new RegistroDB();
                                    break;
                            }
                            if ((Inicio <= registro.Horario) & (Final >= registro.Horario))
                            {
                                {
                                    Writer.WriteStartElement(new Row());
                                    Writer.WriteElement(new Cell { CellValue = new CellValue(Uteis.Unix2time(registro.Horario).ToString()), DataType = CellValues.String });
                                    for (int kk = 0; kk < feeds.Length; kk++)
                                    {
                                        if (float.IsNaN(registro.P[feeds[kk].indice]))
                                        {
                                            Writer.WriteElement(new Cell { CellValue = new CellValue(""), DataType = CellValues.String });
                                        }
                                        else
                                        {
                                            Writer.WriteElement(new Cell { CellValue = new CellValue(registro.P[feeds[kk].indice].ToString(SeparadorDecimal)), DataType = CellValues.Number });
                                        }
                                    }
                                    Writer.WriteEndElement();
                                }
                            }
                        }
                        Writer.WriteEndElement();
                    }
                    Writer.WriteEndElement();
                }
                Writer.Close();
            }
        }
    }
}
