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
    public partial class Form8 : Form
    {
        public Form8()
        {
            InitializeComponent();
        }
        string line = "";
        string entry = "";
        int x = 0;
        int n = 0;
        const string Cfd1 = "prekes.txt";
        const string Cfd2 = "parduotuves.txt";
        const string Cfd3 = "pirkejai.txt";
        const string Cfx = "nupirktosprekes.txt";
        
        private void Form8_Load(object sender, EventArgs e)
        {
            dataGridView1.Columns.Add("Column", "Pavadinimas");
            dataGridView1.Columns.Add("Column", "Parduotuve");
            dataGridView1.Columns.Add("Column", "Vardas");
            dataGridView1.Columns.Add("Column", "Pavarde");
            dataGridView1.Columns.Add("Column", "Kiekis");
            dataGridView1.Columns.Add("Column", "Metai");
            dataGridView1.Columns.Add("Column", "Menuo");
            dataGridView1.Columns.Add("Column", "Diena");
            SkaitytiDuomenis(dataGridView1, comboBox1, comboBox2, comboBox3, Cfd1, Cfd2, Cfd3, Cfx);
        }

        void SkaitytiDuomenis(DataGridView dg, ComboBox cb1, ComboBox cb2, ComboBox cb3, string fv1, string fv2, string fv3, string fv4) // skaitys prekių tipus ir sandėlius
        {
            string pavadinimas, parduotuve, vardas, pavarde;
            int kiekis, metai, menuo, diena;
            string[] lines = File.ReadAllLines(fv1);
            foreach (string line in lines)
            {
                string[] parts = line.Split(';');
                cb1.Items.Add(parts[0]);
            }
            lines = File.ReadAllLines(fv2);
            foreach (string line in lines)
            {
                string[] parts = line.Split(';');
                cb2.Items.Add(parts[5]);
            }
            lines = File.ReadAllLines(fv3);
            foreach (string line in lines)
            {
                string[] parts = line.Split(';');
                cb3.Items.Add(parts[0]+" "+parts[1]);
            }
            lines = File.ReadAllLines(fv4);
            foreach (string line in lines)
            {
                string[] parts = line.Split(';');
                pavadinimas = parts[0];
                parduotuve = parts[1];
                vardas = parts[2];
                pavarde = parts[3];
                kiekis = int.Parse(parts[4]);
                metai = int.Parse(parts[5]);
                menuo = int.Parse(parts[6]);
                diena = int.Parse(parts[7]);
                dg.Rows.Add(pavadinimas, parduotuve, vardas, pavarde, kiekis, metai, menuo, diena);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
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
                try // tikrina visus skaičių laukus, ar neįvestį blogi simboliai
                {
                    x = Convert.ToInt32(textBox1.Text);
                    x = Convert.ToInt32(textBox2.Text);
                    x = Convert.ToInt32(textBox3.Text);
                    x = Convert.ToInt32(textBox4.Text);
                }
                catch (FormatException)
                {
                    textBox1.Text = "";
                    MessageBox.Show("Įvedimo klaida!", "Pranešimas");
                    throw;
                }
                entry = comboBox3.Text;
                string[] parts = entry.Split(' '); //
                dataGridView1.Rows.Add(this.comboBox1.Text, this.comboBox2.Text, parts[0], parts[1], this.textBox1.Text, this.textBox4.Text, this.textBox3.Text, this.textBox2.Text);
                comboBox1.Text = "";
                //comboBox2.Text = "";
                //comboBox3.Text = "";
                textBox1.Text = "";
                textBox2.Text = "";
                textBox3.Text = "";
                textBox4.Text = "";
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            TextWriter fs = new StreamWriter("nupirktosprekes.txt", false, System.Text.Encoding.GetEncoding(65001));
            SpausdintiDuomenis(dataGridView1, fs, line);
            fs.Close();
            MessageBox.Show("Sėkmingai įrašyta į nupirktosprekes.txt", "Pranešimas");
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
                line += dg.Rows[i].Cells[6].Value.ToString() + ";";
                line += dg.Rows[i].Cells[7].Value.ToString() + ";";
                fs.WriteLine(line);
            }
        }
    }
}
