using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace BaigiamasisDarbas
{
    public partial class Form7 : Form
    {
        public Form7()
        {
            InitializeComponent();
        }
        const string Cfd1 = "pirkejai.txt";
        Int64 x = 0;
        int n = 0;
        string line = "";

        private void Form7_Load(object sender, EventArgs e)
        {
            dataGridView1.Columns.Add("Column", "Vardas");
            dataGridView1.Columns.Add("Column", "Pavarde");
            dataGridView1.Columns.Add("Column", "Gyv. vieta");
            dataGridView1.Columns.Add("Column", "Telefonas");
            SkaitytiIDataGrid(dataGridView1, Cfd1, out n);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //while (f)
            //{
            //    try
            //    {
            //        x = Convert.ToInt64(textBox4.Text);
            //        f = false;
            //    }
            //    catch (FormatException)
            //    {
            //        MessageBox.Show("Įvedimo klaida!", "Pranešimas");
            //        throw;
            //    }
            //    finally
            //    {
            //        dataGridView1.Rows.Add(this.textBox1.Text, this.textBox2.Text, this.textBox3.Text, this.textBox4.Text);
            //        textBox1.Text = "";
            //        textBox2.Text = "";
            //        textBox3.Text = "";
            //        textBox4.Text = "";
            //    }
            //}
            if (textBox1.Text == "" || textBox2.Text == "" || textBox3.Text == "" || textBox4.Text == "")
            {
                MessageBox.Show("Ne visi langeliai užpildyti!", "Pranešimas");
            }
            if (textBox1.Text.Contains(';') == true || textBox2.Text.Contains(';') == true || textBox3.Text.Contains(';') == true || textBox4.Text.Contains(';') == true)
            {
                MessageBox.Show("Duomenyse negali būti kabliataškio!", "Pranešimas");
            }
            else
            {
                try {
                    x = Convert.ToInt64(textBox4.Text);
                }
                catch (FormatException) {  // tikrina visus skaičių laukus, ar neįvestį blogi simboliai
                    textBox4.Text = "";
                    MessageBox.Show("Įvedimo klaida!", "Pranešimas");
                    throw;
                }
                dataGridView1.Rows.Add(this.textBox1.Text, this.textBox2.Text, this.textBox3.Text, this.textBox4.Text);
                textBox1.Text = "";
                textBox2.Text = "";
                textBox3.Text = "";
                textBox4.Text = "";
            }

        }

        private void button3_Click(object sender, EventArgs e)
        {
            TextWriter fs = new StreamWriter("pirkejai.txt", false, System.Text.Encoding.GetEncoding(65001));
            SpausdintiDuomenis(dataGridView1, fs, line);
            fs.Close();
            MessageBox.Show("Sėkmingai įrašyta į pirkejai.txt", "Pranešimas");
        }

        static void SpausdintiDuomenis(DataGridView dg, TextWriter fs, string line)
        {
            for (int i = 0; i < (dg.Rows.Count) - 1; i++)
            {
                line = "";
                line += dg.Rows[i].Cells[0].Value.ToString() + ";";
                line += dg.Rows[i].Cells[1].Value.ToString() + ";";
                line += dg.Rows[i].Cells[2].Value.ToString() + ";";
                line += dg.Rows[i].Cells[3].Value.ToString() + ";";
                fs.WriteLine(line);
            }

        }
        static void SkaitytiIDataGrid(DataGridView dg, string fv, out int n)
        {
            n = 0;
            string vardas, pavarde, gyvvieta;
            Int64 telefonas;
            string[] lines = File.ReadAllLines(fv);
            foreach (string line in lines)
            {
                string[] parts = line.Split(';');
                vardas = parts[0];
                pavarde = parts[1];
                gyvvieta = parts[2];
                telefonas = Int64.Parse(parts[3]);
                dg.Rows.Add(vardas, pavarde, gyvvieta, telefonas);
            }
        }
    }
}
