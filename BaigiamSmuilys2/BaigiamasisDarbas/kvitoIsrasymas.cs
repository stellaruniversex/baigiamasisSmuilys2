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
    public partial class kvitoIsrasymas : Form
    {
        // jei nėra numatytas failo pavadinimas, kvito failo pavadinimas VardasPavarde_YYYYMMDD.txt
        public kvitoIsrasymas()
        {
            InitializeComponent();
        }
        int n = 0;
        const int Cmax = 2048;
        Preke[] prekes = new Preke[Cmax];
        const string Cfd1 = "prekes.txt";
        const string Cfd2 = "parduotuves.txt";
        const string Cfd3 = "pirkejai.txt";
        string pavadinimas = "";
        int kiekis = 0;
        double totKaina = 0;
        string currData = "";
        string currVardas = "";
        string currPavarde = "";
        string currParduotuve = "";
        private void kvitoIsrasymas_Load(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();
            dataGridView1.Refresh();
            dataGridView1.Columns.Add("Column", "Pavadinimas");
            dataGridView1.Columns.Add("Column", "Kaina");
            dataGridView1.Columns.Add("Column", "Kiekis");
            dataGridView1.Columns.Add("Column", "Suma");
            SkaitytiDuomenis(dataGridView1, prekes, comboBox1, comboBox2, comboBox3, Cfd1, Cfd2, Cfd3, out n);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox10.Text == "") kiekis = 1;
            else kiekis = int.Parse(textBox10.Text);
            pavadinimas = comboBox1.Text;
            IsrinktiPreke(dataGridView1, prekes, pavadinimas, kiekis, n);
            textBox10.Text = "";
            comboBox1.Text = "";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < (dataGridView1.Rows.Count) - 1; i++)
            {
                totKaina += Convert.ToDouble(dataGridView1.Rows[i].Cells[3].Value);
            }
            currData = textBox1.Text + "-" + textBox3.Text + "-" + textBox2.Text;
            currParduotuve = comboBox3.Text;
            string[] totvardas = comboBox2.Text.Split(' ');
            currVardas = totvardas[0];
            currPavarde = totvardas[1];
            if (textBox4.Text == "")
            {
                string filestring = currVardas + currPavarde + "_" + currData + ".txt";
                TextWriter fs = new StreamWriter(filestring, false, System.Text.Encoding.GetEncoding(65001));
                SpausdintiDuomenis(dataGridView1, currVardas, currPavarde, currData, currParduotuve, totKaina, fs);
                fs.Close();
                string successString = "Sėkmingai įrašyta į " + filestring;
                MessageBox.Show(successString, "Pranešimas");
                dataGridView1.Rows.Clear();
                dataGridView1.Refresh();
                comboBox3.Text = "";
                comboBox2.Text = "";
            }
            else
            {
                string filestring = textBox4.Text + ".txt";
                TextWriter fs = new StreamWriter(filestring, false, System.Text.Encoding.GetEncoding(65001));
                SpausdintiDuomenis(dataGridView1, currVardas, currPavarde, currData, currParduotuve, totKaina, fs);
                fs.Close();
                string successString = "Sėkmingai įrašyta į " + filestring;
                MessageBox.Show(successString, "Pranešimas");
                dataGridView1.Rows.Clear();
                dataGridView1.Refresh();
                comboBox3.Text = "";
                comboBox2.Text = "";
            }
        }

        void IsrinktiPreke(DataGridView dg, Preke[] prekes, string preke, int kiekis, int n)
        {
            bool inPavadinimas = false;
            for (int i = 0; i < n; i++)
            {
                inPavadinimas = false;
                if (prekes[i].GetPavadinimas() == preke)
                {
                    inPavadinimas = true;
                    dg.Rows.Add(prekes[i].GetPavadinimas(), prekes[i].GetKaina(), kiekis, prekes[i].GetKaina()*kiekis);
                    break;
                }
            }
        }

        void SpausdintiDuomenis(DataGridView dg, string currVardas, string currPavarde, string currData, string currParduotuve, double suma, TextWriter fs)
        {
            fs.WriteLine("---K V I T A S---");
            fs.WriteLine("Išrašymo data: {0}", currData);
            fs.WriteLine("Kliento vardas: {0} {1}", currVardas, currPavarde);
            fs.WriteLine("Parduotuvė: {0}", currParduotuve);
            fs.WriteLine("-----------Pavadinimas--------------Kaina----Kiekis----Suma---");
            for (int i = 0; i < (dg.Rows.Count)-1; i++)
            {
                fs.Write("| ");
                fs.Write("{0,30:d} | ", dg.Rows[i].Cells[0].Value.ToString());
                fs.Write("{0,7:d} | ", dg.Rows[i].Cells[1].Value.ToString());
                fs.Write("{0,6:d} | ", dg.Rows[i].Cells[2].Value.ToString());
                fs.Write("{0,6:d} |\n", dg.Rows[i].Cells[3].Value.ToString());
            }
            fs.WriteLine("---------------------------------------------------------------");
            fs.WriteLine("Suma: {0}", suma);
        }

        void SkaitytiDuomenis(DataGridView dg, Preke[] prekes, ComboBox cb1, ComboBox cb2, ComboBox cb3, string fv1, string fv2, string fv3, out int n) // skaitys prekių tipus ir sandėlius
        {
            n = 0;
            string pavadinimas, gamintojas;
            int metai, menuo, diena;
            double kaina;
            string[] lines = File.ReadAllLines(fv1);
            cb1.Items.Add("");
            cb2.Items.Add("");
            cb3.Items.Add("");
            foreach (string line in lines)
            {
                string[] parts = line.Split(';');
                cb1.Items.Add(parts[0]);
            }
            lines = File.ReadAllLines(fv2);
            foreach (string line in lines)
            {
                string[] parts = line.Split(';');
                cb3.Items.Add(parts[5]);
            }
            lines = File.ReadAllLines(fv3);
            foreach (string line in lines)
            {
                string[] parts = line.Split(';');
                cb2.Items.Add(parts[0] + " " + parts[1]);
            }
            lines = File.ReadAllLines(fv1);
            foreach (string line in lines)
            {
                string[] parts = line.Split(';');
                pavadinimas = parts[0];
                gamintojas = parts[1];
                metai = int.Parse(parts[2]);
                menuo = int.Parse(parts[3]);
                diena = int.Parse(parts[4]);
                kaina = double.Parse(parts[5]);
                prekes[n] = new Preke(pavadinimas, gamintojas, kaina, metai, menuo, diena);
                n++;
            }
        }
    }
}
