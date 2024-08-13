using OfficeOpenXml;

namespace WinFormsAppCSVToEXCEL
{
    public partial class Form1 : Form
    {
        static String location;

        static DateTime time;
        public Form1()
        {
            InitializeComponent();
            lblSuccessMsg.Hide();
                    
        }
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                var test = location == null ? "location is null" : location;
                var tt = time == null ? "time is null" : time.ToString();
                ConvertCsvToExcel(txtCSVFilePath.Text, txtExcelFilePath.Text);
                lblSuccessMsg.Text = "File exported successfully";
            }
            catch (Exception ex)
            {
                lblSuccessMsg.Text = "Something went wrong";
            }
            lblSuccessMsg.Show();
        }
        private void ConvertCsvToExcel(string folderPath, string outputFilePath)
        {
            var csvFiles = Directory.GetFiles(folderPath, "*.csv");
            if (csvFiles.Length == 0)
            {
                Console.WriteLine("No CSV files found in the specified folder.");
                return;
            }
            ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;
            using (var package = new ExcelPackage())
            {
                foreach (var csvFile in csvFiles)
                {
                    var worksheetName = Path.GetFileNameWithoutExtension(csvFile);
                    var worksheet = package.Workbook.Worksheets.Add(worksheetName);

                    using (var reader = new StreamReader(csvFile))
                    {
                        int row = 1;
                        while (!reader.EndOfStream)
                        {
                            var line = reader.ReadLine();
                            var values = line.Split(',');

                            for (int col = 0; col < values.Length; col++)
                            {
                                worksheet.Cells.Style.Numberformat.Format = "@";
                                worksheet.Cells[row, col + 1].Value = values[col];
                            }

                            row++;
                        }
                    }
                }

                // Save the Excel package to the output file
                package.SaveAs(new FileInfo(outputFilePath));
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            txtCSVFilePath.Text = string.Empty;
            txtExcelFilePath.Text = string.Empty;
            lblSuccessMsg.Hide();
        }
    }
}