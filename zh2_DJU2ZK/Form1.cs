using Excel = Microsoft.Office.Interop.Excel;
using System.Reflection;
using System.Text.RegularExpressions;
using zh2_DJU2ZK.Models;

namespace zh2_DJU2ZK
{
    public partial class Form1 : Form
    {
        studentContext sc = new();
        Excel.Application xlApp;Excel.Workbook wb; Excel.Worksheet ws;

        public Form1()
        {
            InitializeComponent();

            workBindingSource.DataSource = sc.Works.Distinct().ToList();

            nevSzur();
            munkaSzur();
            
        }

        void gExcel()
        {
            try
            {
                xlApp = new Excel.Application();
                wb = xlApp.Workbooks.Add(Missing.Value);
                ws = wb.ActiveSheet;

                gTable();

                xlApp.Visible = true;
                xlApp.UserControl = true;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                wb.Close(false, Type.Missing, Type.Missing);
                xlApp.Quit();
                wb = null;
                xlApp = null;
            }
        }

        void gTable()
        {

            //fejléc
            string[] fejlécek = new string[]
            {
                "Id",
                "Név",
                "Születési idõ"
            };
            for (int i = 0; i < fejlécek.Length; i++)
            {
                ws.Cells[1, i + 1] = fejlécek[i];
            }

            //adatbázis

            var mindenTanulo = sc.Students.Distinct().ToList();
            object[,] adatok = new object[mindenTanulo.Count(), fejlécek.Count()];

            for (int i = 0; i < mindenTanulo.Count(); i++)
            {
                adatok[i, 0] = mindenTanulo[i].StudentId;
                adatok[i, 1] = mindenTanulo[i].Name;
                adatok[i, 2] = mindenTanulo[i].Birthdate.ToString();

            }
            int öO = adatok.GetLength(0); int öS = adatok.GetLength(1);
            Excel.Range adatRange = ws.get_Range("A2", Type.Missing).get_Resize(öO, öS);
            adatRange.Value2 = adatok;
            adatRange.Columns.AutoFit();

            //formázás

            Excel.Range fejlécRange = ws.get_Range("A1", Type.Missing).get_Resize(1, 3);
            fejlécRange.Font.Bold = true;
            fejlécRange.Interior.Color = Color.Fuchsia;
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if(MessageBox.Show("Biztosan kilép?", "Üzenet",MessageBoxButtons.YesNo) == DialogResult.No)
            {
                e.Cancel = true;
                return;
            }
        }

        void nevSzur()
        {
            var ki = (from x in sc.Students
                      join y in sc.Works
                      on x.StudentId equals y.StudentId
                      where x.Name.Contains(textBox1.Text) && y.JobTitle.Contains(comboBox1.Text)
                      select x).Distinct();

            studentBindingSource.DataSource = ki.ToList();

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            nevSzur();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            nevSzur();
        }
        void munkaSzur()
        {
            Student st = (Student)listBox1.SelectedItem;

            var ki = (from x in sc.Students
                      join y in sc.Works
                      on x.StudentId equals y.StudentId
                      where x.StudentId == st.StudentId
                      select new
                      {
                          Azonosító = x.StudentId,
                          Név = x.Name,
                          Születési_idõ = x.Birthdate,
                          Órabér = y.PricePerHour,
                          Munkaórák = y.Hours
                      }).Distinct();

            dataGridView1.DataSource = bindingSource1;
            try
            {
                bindingSource1.DataSource = ki.ToList();
            }
            catch
            {

            }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            munkaSzur();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Regex rx = new("[0-9]");
            Student st = new();
            if (rx.IsMatch(textBox2.Text))
            {
                st.StudentId = int.Parse(textBox2.Text);
                st.Name = textBox3.Text;
                st.Birthdate = dateTimePicker1.Value;
                sc.Students.Add(st);
                try
                {
                    sc.SaveChanges();
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                nevSzur();
                munkaSzur();
            }
            else
            {
                MessageBox.Show("Nem megfelelõ bemenet!");
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            gExcel();
        }
    }
}