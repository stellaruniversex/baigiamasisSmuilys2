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
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }
        const string Cfd1 = "prekes.txt";
        const int C = 100;
        //string pavadinimas = "";
        //string gamintojas = "";
        //int metai = 0;
        //int menuo = 0;
        //int diena = 0;
        //int kiekis = 0;
        int x = 0;
        double y = 0;
        int n = 0;
        string line = "";

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "" || textBox2.Text == "" || textBox3.Text == "" || textBox4.Text == "" || textBox5.Text == "" || textBox6.Text == "")
            {
                MessageBox.Show("Ne visi langeliai užpildyti!", "Pranešimas");
            }
            if (textBox1.Text.Contains(';') == true || textBox2.Text.Contains(';') == true || textBox3.Text.Contains(';') == true || textBox4.Text.Contains(';') == true || textBox5.Text.Contains(';') == true || textBox6.Text.Contains(';') == true)
            {
                MessageBox.Show("Duomenyse negali būti kabliataškio!", "Pranešimas");
            }
            else
            {
                try // tikrina visus skaičių laukus, ar neįvestį blogi simboliai
                {
                    x = Convert.ToInt32(textBox6.Text);
                    x = Convert.ToInt32(textBox4.Text);
                    x = Convert.ToInt32(textBox3.Text);
                    y = Convert.ToDouble(textBox5.Text);
                }
                catch (FormatException)
                {
                    textBox3.Text = "";
                    textBox4.Text = "";
                    textBox5.Text = "";
                    textBox6.Text = "";
                    MessageBox.Show("Įvedimo klaida!", "Pranešimas");
                    throw;
                }
                dataGridView1.Rows.Add(this.textBox1.Text, this.textBox2.Text, this.textBox6.Text, this.textBox4.Text, this.textBox3.Text, this.textBox5.Text);
                textBox1.Text = "";
                textBox2.Text = "";
                textBox3.Text = "";
                textBox4.Text = "";
                textBox5.Text = "";
                textBox6.Text = "";
                //MessageBox.Show("Sėkmingai įrašyta", "Pranešimas");
            }
        }

        //private void Form2_FormClosing(object sender, FormClosingEventArgs e)
        //{
        //    Application.Exit();
        //}

        static void SkaitytiSandelys(Sandelys[] sand, string fv, out string pavad)
        {
            Sandelys[] sandelys = new Sandelys[C];
            pavad = "";
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            // čia bus skaitymo iš failo funkcija
            dataGridView1.Columns.Add("Column", "Pavadinimas");
            dataGridView1.Columns.Add("Column", "Gamintojas");
            dataGridView1.Columns.Add("Column", "Metai");
            dataGridView1.Columns.Add("Column", "Menuo");
            dataGridView1.Columns.Add("Column", "Diena");
            dataGridView1.Columns.Add("Column", "Kaina");
            SkaitytiIDataGrid(dataGridView1, Cfd1, out n);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            TextWriter fs = new StreamWriter("prekes.txt", false, System.Text.Encoding.GetEncoding(65001));
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
            string pavadinimas, gamintojas;
            double kaina;
            int metai, menuo, diena;
            string[] lines = File.ReadAllLines(fv);
            foreach (string line in lines)
            {
                string[] parts = line.Split(';');
                pavadinimas = parts[0];
                gamintojas = parts[1];
                metai = int.Parse(parts[2]);
                menuo = int.Parse(parts[3]);
                diena = int.Parse(parts[4]);
                kaina = double.Parse(parts[5]);
                dg.Rows.Add(pavadinimas, gamintojas, metai, menuo, diena, kaina);
            }
        }
    }
}
