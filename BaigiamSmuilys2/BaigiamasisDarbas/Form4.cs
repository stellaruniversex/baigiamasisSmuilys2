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
    public partial class Form4 : Form
    {
        public Form4()
        {
            InitializeComponent();
        }
        int x = 0;
        //int n = 0;
        string line = "";
        const string Cfd1 = "prekes.txt";
        const string Cfd2 = "sandeliai.txt";
        const string Cfx = "sandeliaiprekes.txt";
           
        private void Form4_Load(object sender, EventArgs e)
        {
            dataGridView1.Columns.Add("Column", "Pavadinimas");
            dataGridView1.Columns.Add("Column", "Kiekis");
            dataGridView1.Columns.Add("Column", "Sandėlys");
            SkaitytiDuomenis(dataGridView1, comboBox1, comboBox2, Cfd1, Cfd2, Cfx);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "" || comboBox1.Text == "" || comboBox2.Text == "")
            {
                MessageBox.Show("Ne visi langeliai užpildyti!", "Pranešimas");
            }
            if (textBox1.Text.Contains(';') == true)
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
                dataGridView1.Rows.Add(this.comboBox1.Text, this.comboBox2.Text, this.textBox1.Text);
                comboBox1.Text = "";
                comboBox2.Text = "";
                textBox1.Text = "";
            }
        }

        static void SpausdintiDuomenis(DataGridView dg, TextWriter fs, string line)
        {
            for (int i = 0; i < (dg.Rows.Count) - 1; i++)
            {
                line = "";
                line += dg.Rows[i].Cells[0].Value.ToString() + ";";
                line += dg.Rows[i].Cells[1].Value.ToString() + ";";
                line += dg.Rows[i].Cells[2].Value.ToString() + ";";
                fs.WriteLine(line);
            }
        }

        void SkaitytiDuomenis(DataGridView dg, ComboBox cb1, ComboBox cb2, string fv1, string fv2, string fv3) // skaitys prekių tipus ir sandėlius
        {
            string pavadinimas, gamintojas;
            int kiekis;
            string[] lines = File.ReadAllLines(fv1);
            foreach(string line in lines)
            {
                string[] parts = line.Split(';');
                cb1.Items.Add(parts[0]);
            }
            lines = File.ReadAllLines(fv2);
            foreach (string line in lines)
            {
                string[] parts = line.Split(';');
                cb2.Items.Add(parts[1]);
            }
            lines = File.ReadAllLines(fv3);
            foreach (string line in lines)
            {
                string[] parts = line.Split(';');
                pavadinimas = parts[0];
                gamintojas = parts[1];
                kiekis = int.Parse(parts[2]);
                dg.Rows.Add(pavadinimas, gamintojas, kiekis);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            TextWriter fs = new StreamWriter("sandeliaiprekes.txt", false, System.Text.Encoding.GetEncoding(65001));
            SpausdintiDuomenis(dataGridView1, fs, line);
            fs.Close();
        }
    }
}
