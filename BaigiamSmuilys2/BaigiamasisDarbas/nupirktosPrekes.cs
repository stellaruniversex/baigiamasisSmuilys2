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
    public partial class nupirktosPrekes : Form
    {
        public nupirktosPrekes()
        {
            InitializeComponent();
        }
        int n = 0;
        string parduotuve = "";
        string pavadinimas = "";
        string vardas = "";
        string pavarde = "";
        const string Cfd1 = "parduotuves.txt";
        const string Cfd3 = "nupirktosprekes.txt";
        const string Cfs = "ataskaita_nupprekes.txt";
        int minmetai = 0;
        int maxmetai = 0;
        int minmenuo = 0;
        int maxmenuo = 0;
        int mindiena = 0;
        int maxdiena = 0;
        int minkiekis = 0;
        int maxkiekis = 0;
        const int Cmax = 2048;
        NupirktaPreke[] nupirktosprekes = new NupirktaPreke[Cmax];
        private void nupirktosPrekes_Load(object sender, EventArgs e)
        {
            dataGridView1.Columns.Add("Column", "Pavadinimas");
            dataGridView1.Columns.Add("Column", "Parduotuve");
            dataGridView1.Columns.Add("Column", "Vardas");
            dataGridView1.Columns.Add("Column", "Pavarde");
            dataGridView1.Columns.Add("Column", "Kiekis");
            dataGridView1.Columns.Add("Column", "Metai");
            dataGridView1.Columns.Add("Column", "Menuo");
            dataGridView1.Columns.Add("Column", "Diena");
            SkaitytiDuomenis(dataGridView1, nupirktosprekes, comboBox2, Cfd1, Cfd3, out n);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            pavadinimas = textBox0.Text;
            parduotuve = comboBox2.Text;
            vardas = textBox10.Text;
            pavarde = textBox11.Text;
            if (textBox3.Text == "") minmetai = 1;
            else minmetai = int.Parse(textBox3.Text);
            if (textBox4.Text == "") minmenuo = 1;
            else minmenuo = int.Parse(textBox4.Text);
            if (textBox5.Text == "") mindiena = 1;
            else mindiena = int.Parse(textBox5.Text);
            if (textBox6.Text == "") maxmetai = 9999;
            else maxmetai = int.Parse(textBox6.Text);
            if (textBox7.Text == "") maxmenuo = 12;
            else maxmenuo = int.Parse(textBox7.Text);
            if (textBox8.Text == "") maxdiena = 31;
            else maxdiena = int.Parse(textBox8.Text);
            if (textBox1.Text == "") minkiekis = 0;
            else minkiekis = int.Parse(textBox1.Text);
            if (textBox2.Text == "") maxkiekis = 2147483647;
            else maxkiekis = int.Parse(textBox2.Text);
            isrinkti(dataGridView1, nupirktosprekes, pavadinimas, parduotuve, vardas, pavarde, minkiekis, maxkiekis, minmetai, maxmetai, minmenuo, maxmenuo, mindiena, maxdiena, n);
            if (textBox9.Text == "")
            {
                TextWriter fs = new StreamWriter(Cfs, false, System.Text.Encoding.GetEncoding(65001));
                fs.WriteLine("Filtrai:");
                fs.WriteLine("Pavadinimas: *{0}*", pavadinimas);
                fs.WriteLine("Parduotuvė: *{0}*", parduotuve);
                fs.WriteLine("Pirkėjas: *{0}*", vardas + " " + pavarde);
                fs.WriteLine("Min. metai: {0}", minmetai);
                fs.WriteLine("Min. mėnuo: {0}", minmenuo);
                fs.WriteLine("Min. diena: {0}", mindiena);
                fs.WriteLine("Maks. metai: {0}", maxmetai);
                fs.WriteLine("Maks. mėnuo: {0}", maxmenuo);
                fs.WriteLine("Maks. diena: {0}", maxdiena);
                fs.WriteLine("Min. kiekis: {0}", minkiekis);
                fs.WriteLine("Maks. kiekis: {0}", maxkiekis);
                SpausdintiDuomenis(dataGridView1, fs);
                fs.Close();
                MessageBox.Show("Sėkmingai įrašyta į ataskaita_nupprekes.txt", "Pranešimas");
            }
            else
            {
                string success_string = "Sėkmingai įrašyta į " + textBox9.Text + ".txt";
                TextWriter fs = new StreamWriter((textBox9.Text + ".txt"), false, System.Text.Encoding.GetEncoding(65001));
                fs.WriteLine("Filtrai:");
                fs.WriteLine("Pavadinimas: *{0}*", pavadinimas);
                fs.WriteLine("Parduotuvė: *{0}*", parduotuve);
                fs.WriteLine("Pirkėjas: *{0}*", vardas + " " + pavarde);
                fs.WriteLine("Min. metai: {0}", minmetai);
                fs.WriteLine("Min. mėnuo: {0}", minmenuo);
                fs.WriteLine("Min. diena: {0}", mindiena);
                fs.WriteLine("Maks. metai: {0}", maxmetai);
                fs.WriteLine("Maks. mėnuo: {0}", maxmenuo);
                fs.WriteLine("Maks. diena: {0}", maxdiena);
                fs.WriteLine("Min. kiekis: {0}", minkiekis);
                fs.WriteLine("Maks. kiekis: {0}", maxkiekis);
                SpausdintiDuomenis(dataGridView1, fs);
                fs.Close();
                //MessageBox.Show("Sėkmingai įrašyta", "Pranešimas");
                MessageBox.Show(success_string, "Pranešimas");
            }
        }
        static void SpausdintiDuomenis(DataGridView dg, TextWriter fs)
        {               //|             30                 |                   40                     |      12      |      12      |   6    |   5   |   5   |   5   |
            fs.WriteLine("-----------Pavadinimas------------------------------Sandėlis----------------------Vardas--------Pavardė-----Kiekis---Metai---Menuo---Diena--");
            for (int i = 0; i < (dg.Rows.Count) - 1; i++)
            {
                fs.Write("| ");
                fs.Write("{0,30:d} | ", dg.Rows[i].Cells[0].Value.ToString());
                fs.Write("{0,40:d} | ", dg.Rows[i].Cells[1].Value.ToString());
                fs.Write("{0,12:d} | ", dg.Rows[i].Cells[2].Value.ToString());
                fs.Write("{0,12:d} | ", dg.Rows[i].Cells[3].Value.ToString());
                fs.Write("{0,6:d} | ", dg.Rows[i].Cells[4].Value.ToString());
                fs.Write("{0,5:d} | ", dg.Rows[i].Cells[5].Value.ToString());
                fs.Write("{0,5:d} | ", dg.Rows[i].Cells[6].Value.ToString());
                fs.Write("{0,5:d} |\n", dg.Rows[i].Cells[7].Value.ToString());
            }
            fs.WriteLine("--------------------------------------------------------------------------------------------------------------------------------------------");
        }
        static void isrinkti(DataGridView dg, NupirktaPreke[] nupirktosprekes, string pavadinimas, string parduotuve, string vardas, string pavarde, int minkiekis, int maxkiekis, int minmetai, int maxmetai, int minmenuo, int maxmenuo, int mindiena, int maxdiena, int n)
        {
            dg.Rows.Clear();
            dg.Refresh();
            bool inPavadinimas = false;
            bool inParduotuve = false;
            bool inVardas = false;
            bool inPavarde = false;
            bool inKiekis = false;
            bool inMinData = false;
            bool inMaxData = false;
            //bool inSandelys = false;
            //bool inPavadinimas = false;
            //bool inKiekis = false;
            for (int i = 0; i < n; i++)
            {
                //inSandelys = false;
                //inPavadinimas = false;
                //inKiekis = false;
                inPavadinimas = false;
                inParduotuve = false;
                inVardas = false;
                inPavarde = false;
                inKiekis = false;
                inMinData = false;
                inMaxData = false;
                inPavadinimas = (nupirktosprekes[i].GetPrekesTipas().IndexOf(pavadinimas, StringComparison.OrdinalIgnoreCase) >= 0);
                inParduotuve = (nupirktosprekes[i].GetParduotuve().IndexOf(parduotuve, StringComparison.OrdinalIgnoreCase) >= 0);
                inVardas = (nupirktosprekes[i].GetVardas().IndexOf(vardas, StringComparison.OrdinalIgnoreCase) >= 0);
                inPavarde = (nupirktosprekes[i].GetPavarde().IndexOf(pavarde, StringComparison.OrdinalIgnoreCase) >= 0);
                if (nupirktosprekes[i].GetMetai() > minmetai || nupirktosprekes[i].GetMetai() == minmetai && nupirktosprekes[i].GetMenuo() > minmenuo
                || nupirktosprekes[i].GetMetai() == minmetai && nupirktosprekes[i].GetMenuo() == minmenuo && nupirktosprekes[i].GetDiena() >= mindiena) inMinData = true;
                if (nupirktosprekes[i].GetMetai() == maxmetai && nupirktosprekes[i].GetMenuo() == maxmenuo && nupirktosprekes[i].GetDiena() <= maxdiena || nupirktosprekes[i].GetMetai() == maxmetai && nupirktosprekes[i].GetMenuo() <= maxmenuo || nupirktosprekes[i].GetMetai() < maxmetai) inMaxData = true;
                if (nupirktosprekes[i].GetKiekis() >= minkiekis && nupirktosprekes[i].GetKiekis() <= maxkiekis) inKiekis = true;
                if (inPavadinimas == true && inParduotuve == true && inVardas == true && inPavarde == true && inMinData == true && inMaxData == true && inKiekis == true) dg.Rows.Add(nupirktosprekes[i].GetPrekesTipas(), nupirktosprekes[i].GetParduotuve(), nupirktosprekes[i].GetVardas(), nupirktosprekes[i].GetPavarde(), nupirktosprekes[i].GetKiekis(), nupirktosprekes[i].GetMetai(), nupirktosprekes[i].GetMenuo(), nupirktosprekes[i].GetDiena());
                //inPavadinimas = false;
                //inGamintojas = false;
                //inMinData = false;
                //inMaxData = false;
                //inKaina = false;
                ////2021-03-26 > 2024-07-10; 2022-02-06?
                //inPavadinimas = (prekes[i].GetPavadinimas().IndexOf(pavadinimas, StringComparison.OrdinalIgnoreCase) >= 0);
                //inGamintojas = (prekes[i].GetGamintojas().IndexOf(gamintojas, StringComparison.OrdinalIgnoreCase) >= 0);
                //if (inPavadinimas == true && inSandelys == true && inKiekis == true) dg.Rows.Add(turiniai[i].GetPrekPavad(), turiniai[i].GetAdresas(), turiniai[i].GetKiekis());
            }
        }
        void SkaitytiDuomenis(DataGridView dg, NupirktaPreke[] nupirktosPrekes, ComboBox cb1, string fv1, string fv2, out int n) // skaitys prekių tipus ir sandėlius
        {
            n = 0;
            string pavadinimas, parduotuve, vardas, pavarde;
            int kiekis, metai, menuo, diena;
            string[] lines = File.ReadAllLines(fv1);
            foreach (string line in lines)
            {
                string[] parts = line.Split(';');
                cb1.Items.Add(parts[5]);
            }
            lines = File.ReadAllLines(fv2);
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
                nupirktosPrekes[n] = new NupirktaPreke(pavadinimas, parduotuve, vardas, pavarde, kiekis, metai, menuo, diena);
                n++;
            }
        }
    }
}
