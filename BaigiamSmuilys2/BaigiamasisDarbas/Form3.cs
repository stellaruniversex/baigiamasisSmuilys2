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
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }
        int n = 0;
        Int64 x = 0;
        string line = "";
        const string Cfd1 = "sandeliai.txt";

        private void Form3_Load(object sender, EventArgs e)
        {
            dataGridView1.Columns.Add("Column", "Plotas");
            dataGridView1.Columns.Add("Column", "Adresas");
            SkaitytiIDataGrid(dataGridView1, Cfd1, out n);
        }

        static void SpausdintiDuomenis(DataGridView dg, TextWriter fs, string line)
        {
            for (int i = 0; i < (dg.Rows.Count) - 1; i++)
            {
                line = "";
                line += dg.Rows[i].Cells[0].Value.ToString() + ";";
                line += dg.Rows[i].Cells[1].Value.ToString() + ";";
                fs.WriteLine(line);
            }
        }

        static void SkaitytiIDataGrid(DataGridView dg, string fv, out int n)
        {
            n = 0;
            int plotas;
            string vieta;
            string[] lines = File.ReadAllLines(fv);
            foreach (string line in lines)
            {
                string[] parts = line.Split(';');
                plotas = int.Parse(parts[0]);
                vieta = parts[1];
                dg.Rows.Add(plotas, vieta);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "" || textBox2.Text == "")
            {
                MessageBox.Show("Ne visi langeliai užpildyti!", "Pranešimas");
            }
            if (textBox1.Text.Contains(';') == true || textBox2.Text.Contains(';') == true)
            {
                MessageBox.Show("Duomenyse negali būti kabliataškio!", "Pranešimas");
            }
            else
            {
                try // tikrina visus skaičių laukus, ar neįvestį blogi simboliai
                {
                    x = Convert.ToInt32(textBox1.Text);
                }
                catch (FormatException)
                {
                    textBox1.Text = "";
                    MessageBox.Show("Įvedimo klaida!", "Pranešimas");
                    throw;
                }
                dataGridView1.Rows.Add(this.textBox1.Text, this.textBox2.Text);
                textBox1.Text = "";
            }
        }
    }
}
