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
    public partial class Form5 : Form
    {
        public Form5()
        {
            InitializeComponent();
        }
        Int64 x = 0;
        int n = 0;
        string line = "";
        const string Cfd1 = "parduotuves.txt";

        private void Form5_Load(object sender, EventArgs e)
        {
            dataGridView1.Columns.Add("Column", "Lentynos");
            dataGridView1.Columns.Add("Column", "Dydis");
            dataGridView1.Columns.Add("Column", "Įmokos");
            dataGridView1.Columns.Add("Column", "Išmokos");
            dataGridView1.Columns.Add("Column", "Telefonas");
            dataGridView1.Columns.Add("Column", "Vieta");
            SkaitytiIDataGrid(dataGridView1, Cfd1, out n);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "" || textBox2.Text == "" || textBox3.Text == "" || textBox4.Text == "" || textBox5.Text == "" || textBox6.Text == "")
            {
                MessageBox.Show("Ne visi langeliai užpildyti!", "Pranešimas");
            }
            else
            {
                try // tikrina visus skaičių laukus, ar neįvestį blogi simboliai
                {
                    x = Convert.ToInt32(textBox1.Text);
                    x = Convert.ToInt32(textBox2.Text);
                    x = Convert.ToInt32(textBox3.Text);
                    x = Convert.ToInt32(textBox4.Text);
                    x = Convert.ToInt32(textBox6.Text);
                }
                catch (FormatException)
                {
                    textBox1.Text = "";
                    textBox2.Text = "";
                    textBox3.Text = "";
                    textBox4.Text = "";
                    textBox6.Text = "";
                    MessageBox.Show("Įvedimo klaida!", "Pranešimas");
                    throw;
                }
                dataGridView1.Rows.Add(this.textBox1.Text, this.textBox2.Text, this.textBox3.Text, this.textBox4.Text, this.textBox6.Text, this.textBox5.Text);
                textBox1.Text = "";
                textBox2.Text = "";
                textBox3.Text = "";
                textBox4.Text = "";
                textBox5.Text = "";
                textBox6.Text = "";
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            TextWriter fs = new StreamWriter("parduotuves.txt", false, System.Text.Encoding.GetEncoding(65001));
            SpausdintiDuomenis(dataGridView1, fs, line);
            fs.Close();
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
                line += dg.Rows[i].Cells[4].Value.ToString() + ";";
                line += dg.Rows[i].Cells[5].Value.ToString() + ";";
                fs.WriteLine(line);
            }
        }

        static void SkaitytiIDataGrid(DataGridView dg, string fv, out int n)
        {
            n = 0;
            string vieta;
            int lentynos, dydis, imokos, ismokos;
            Int64 telefonas;
            string[] lines = File.ReadAllLines(fv);
            foreach (string line in lines)
            {
                string[] parts = line.Split(';');
                lentynos = int.Parse(parts[0]);
                dydis = int.Parse(parts[1]);
                imokos = int.Parse(parts[2]);
                ismokos = int.Parse(parts[3]);
                telefonas = Int64.Parse(parts[4]);
                vieta = parts[5];
                dg.Rows.Add(lentynos, dydis, imokos, ismokos, telefonas, vieta);
            }
        }
    }
}
