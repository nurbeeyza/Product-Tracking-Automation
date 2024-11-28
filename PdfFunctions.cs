using System;
using System.IO;
using System.Windows.Forms;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace Product_Tracking_Automation
{
    public class PdfFunctions
    {
        public void ExportDataGridViewToPdf(DataGridView dataGridView, string filePath)
        {
            try
            {
                // Dosya yolunun geçerli olup olmadığını kontrol etme
                if (string.IsNullOrWhiteSpace(filePath) || !filePath.EndsWith(".pdf", StringComparison.OrdinalIgnoreCase))
                {
                    throw new ArgumentException("Geçersiz dosya yolu.");
                }

                using (FileStream stream = new FileStream(filePath, FileMode.Create, FileAccess.Write, FileShare.None))
                {
                    using (Document document = new Document())
                    {
                        PdfWriter.GetInstance(document, stream);
                        document.Open();

                        Paragraph title = new Paragraph("ŞİRLET");
                        title.Alignment = Element.ALIGN_CENTER;
                        document.Add(title);

                        PdfPTable table = new PdfPTable(dataGridView.Columns.Count);

                        foreach (DataGridViewColumn column in dataGridView.Columns)
                        {
                            if (column.Visible)
                            {
                                table.AddCell(new Phrase(column.HeaderText));
                            }
                        }

                        foreach (DataGridViewRow row in dataGridView.Rows)
                        {
                            if (!row.IsNewRow)
                            {
                                foreach (DataGridViewCell cell in row.Cells)
                                {
                                    if (dataGridView.Columns[cell.ColumnIndex].Visible)
                                    {
                                        table.AddCell(new Phrase(cell.Value != null ? cell.Value.ToString() : string.Empty));
                                    }
                                }
                            }
                        }

                        document.Add(table);
                        document.Close();
                    }
                }

                MessageBox.Show("Veriler başarıyla PDF dosyasına aktarıldı.");
            }
            catch (IOException ioEx)
            {
                MessageBox.Show($"Dosya hatası oluştu: {ioEx.Message}");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Bir hata oluştu: {ex.Message}");
            }
        }
    }
}