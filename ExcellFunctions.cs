using Excel = Microsoft.Office.Interop.Excel;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System;


public class ExcellFunctions
{
    public static void ExportDataGridViewToExcel(DataGridView dataGridView, string filePath)
    {
        Excel.Application excelApp = null;
        Excel.Workbook workbook = null;
        Excel.Worksheet worksheet = null;

        try
        {
            // Excel uygulamasını başlat
            excelApp = new Excel.Application();
            if (excelApp == null)
            {
                MessageBox.Show("Excel uygulaması başlatılamadı.");
                return;
            }

            // Yeni bir çalışma kitabı oluştur
            workbook = excelApp.Workbooks.Add();
            worksheet = (Excel.Worksheet)workbook.Worksheets[1];

            int columnOffset = 0;

            // Başlıkları yaz (HeaderText içinde "Id" olmayan sütunlar)
            for (int i = 0; i < dataGridView.Columns.Count; i++)
            {
                if (!dataGridView.Columns[i].HeaderText.Contains("Id"))
                {
                    worksheet.Cells[1, i + 1 - columnOffset] = dataGridView.Columns[i].HeaderText;
                }
                else
                {
                    columnOffset++;
                }
            }

            // Verileri yaz (HeaderText içinde "Id" olmayan sütunlar)
            for (int i = 0; i < dataGridView.Rows.Count; i++)
            {
                int rowOffset = 0;
                for (int j = 0; j < dataGridView.Columns.Count; j++)
                {
                    if (!dataGridView.Columns[j].HeaderText.Contains("Id"))
                    {
                        var value = dataGridView.Rows[i].Cells[j].Value;
                        worksheet.Cells[i + 2, j + 1 - rowOffset] = value != null ? value.ToString() : string.Empty;
                    }
                    else
                    {
                        rowOffset++;
                    }
                }
            }

            // Dosyayı kaydet
            workbook.SaveAs(filePath);
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Bir hata oluştu: {ex.Message}");
        }
        finally
        {
            // Kaynakları serbest bırak
            if (worksheet != null)
            {
                Marshal.ReleaseComObject(worksheet);
                worksheet = null;
            }
            if (workbook != null)
            {
                workbook.Close(false);
                Marshal.ReleaseComObject(workbook);
                workbook = null;
            }
            if (excelApp != null)
            {
                excelApp.Quit();
                Marshal.ReleaseComObject(excelApp);
                excelApp = null;
            }

            // Çöp toplayıcıyı çağır
            GC.Collect();
            GC.WaitForPendingFinalizers();
        }

        MessageBox.Show("Veriler başarıyla Excel dosyasına aktarıldı.");
    }


}



