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
    public partial class sandeliuTurinys : Form
    {
        // Užduotis: susieti sandėlio turinį su užsakymais, kad būtų galima žinoti, kiek sandėlys turi prekių po visų užsakymų
        // Užduotis: įspėti, kai kokio sąrašo elementas neegzistuoja
        public sandeliuTurinys()
        {
            InitializeComponent();
        }
        int n = 0;
        int m = 0;
        int l = 0;
        string sandelys = "";
        string pavadinimas = "";
        string gamintojas = "";
        //string line = "";
        int minmetai = 0;
        int maxmetai = 0;
        int minmenuo = 0;
        int maxmenuo = 0;
        int mindiena = 0;
        int maxdiena = 0;
        double minkaina = 0;
        double maxkaina = 0;
        int minkiekis = 0;
        int maxkiekis = 0;
        const int Cmax = 2048;
        Preke[] prekes = new Preke[Cmax];
        Sandelys[] sandeliai = new Sandelys[Cmax];
        SandeliuTuriniai[] turinys = new SandeliuTuriniai[Cmax];
        const string Cfd1 = "prekes.txt";
        const string Cfd2 = "sandeliai.txt";
        const string Cfd3 = "sandeliaiprekes.txt";
        const string Cfs = "ataskaita_sandprekes.txt";

        private void sandeliuTurinys_Load(object sender, EventArgs e)
        {
            dataGridView1.Columns.Add("Column", "Sandėlis");
            dataGridView1.Columns.Add("Column", "Pavadinimas");
            dataGridView1.Columns.Add("Column", "Kiekis");
            dataGridView2.Columns.Add("Column", "Pavadinimas");
            dataGridView2.Columns.Add("Column", "Gamintojas");
            dataGridView2.Columns.Add("Column", "Metai");
            dataGridView2.Columns.Add("Column", "Menuo");
            dataGridView2.Columns.Add("Column", "Diena");
            dataGridView2.Columns.Add("Column", "Kaina");
            skaitytiDuomenis(dataGridView1, dataGridView2, prekes, sandeliai, turinys, comboBox1, Cfd1, Cfd2, Cfd3, out n, out m, out l);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //po paskaitos reikės susieti prekių tipus su turiniu
            //if (checkBox1.Checked == true)
            //{
            //
            //}
            sandelys = comboBox1.Text;
            pavadinimas = textBox1.Text;
            gamintojas = textBox2.Text;
            if (textBox6.Text == "") minmetai = 1;
            else minmetai = int.Parse(textBox6.Text);
            if (textBox4.Text == "") minmenuo = 1;
            else minmenuo = int.Parse(textBox4.Text);
            if (textBox3.Text == "") mindiena = 1;
            else mindiena = int.Parse(textBox3.Text);
            if (textBox9.Text == "") maxmetai = 9999;
            else maxmetai = int.Parse(textBox9.Text);
            if (textBox7.Text == "") maxmenuo = 12;
            else maxmenuo = int.Parse(textBox7.Text);
            if (textBox8.Text == "") maxdiena = 31;
            else maxdiena = int.Parse(textBox8.Text);
            if (textBox5.Text == "") minkaina = 0;
            else minkaina = double.Parse(textBox5.Text);
            if (textBox10.Text == "") maxkaina = 1.7976931348623156E+308;
            else maxkaina = double.Parse(textBox10.Text);
            if (textBox12.Text == "") minkiekis = 0;
            else minkiekis = int.Parse(textBox12.Text);
            if (textBox11.Text == "") maxkiekis = 2147483647;
            else maxkiekis = int.Parse(textBox11.Text);
            isrinkti(dataGridView1, turinys, prekes, pavadinimas, gamintojas, sandelys, minkiekis, maxkiekis, minmetai, maxmetai, minmenuo, maxmenuo, mindiena, maxdiena, minkaina, maxkaina, l, n);
            if (textBox13.Text == "")
            {
                TextWriter fs = new StreamWriter(Cfs, false, System.Text.Encoding.GetEncoding(65001));
                fs.WriteLine("Filtrai:");
                fs.WriteLine("Pavadinimas: *{0}*", pavadinimas);
                fs.WriteLine("Gamintojas: *{0}*", gamintojas);
                fs.WriteLine("Sandėlis: {0}", sandelys);
                fs.WriteLine("Min. metai: {0}", minmetai);
                fs.WriteLine("Min. mėnuo: {0}", minmenuo);
                fs.WriteLine("Min. diena: {0}", mindiena);
                fs.WriteLine("Maks. metai: {0}", maxmetai);
                fs.WriteLine("Maks. mėnuo: {0}", maxmenuo);
                fs.WriteLine("Maks. diena: {0}", maxdiena);
                fs.WriteLine("Min. kaina: {0}", minkaina);
                fs.WriteLine("Maks. kaina: {0}", maxkaina);
                fs.WriteLine("Min. kiekis: {0}", minkiekis);
                fs.WriteLine("Maks. kiekis: {0}", maxkiekis);
                SpausdintiDuomenis(dataGridView1, fs);
                fs.Close();
                MessageBox.Show("Sėkmingai įrašyta į ataskaita_sandprekes.txt", "Pranešimas");
            }
            else
            {
                string success_string = "Sėkmingai įrašyta į " + textBox13.Text + ".txt";
                TextWriter fs = new StreamWriter((textBox13.Text + ".txt"), false, System.Text.Encoding.GetEncoding(65001));
                fs.WriteLine("Filtrai:");
                fs.WriteLine("Min. metai: {0}", minmetai);
                fs.WriteLine("Min. mėnuo: {0}", minmenuo);
                fs.WriteLine("Min. diena: {0}", mindiena);
                fs.WriteLine("Maks. metai: {0}", maxmetai);
                fs.WriteLine("Maks. mėnuo: {0}", maxmenuo);
                fs.WriteLine("Maks. diena: {0}", maxdiena);
                fs.WriteLine("Min. kaina: {0}", minkaina);
                fs.WriteLine("Maks. kaina: {0}", maxkaina);
                fs.WriteLine("Min. kiekis: {0}", minkiekis);
                fs.WriteLine("Maks. kiekis: {0}", maxkiekis);
                SpausdintiDuomenis(dataGridView1, fs);
                fs.Close();
                //MessageBox.Show("Sėkmingai įrašyta", "Pranešimas");
                MessageBox.Show(success_string, "Pranešimas");
            }
        }
        static void isrinkti(DataGridView dg, SandeliuTuriniai[] turiniai, Preke[] prekes, string pavadinimas, string gamintojas, string adresas, int minkiekis, int maxkiekis, int minmetai, int maxmetai, int minmenuo, int maxmenuo, int mindiena, int maxdiena, double minkaina, double maxkaina, int n, int m)
        {
            dg.Rows.Clear();
            dg.Refresh();
            bool inSandelys = false;
            bool inPavadinimas = false;
            bool inKiekis = false;
            bool inPreke = false;
            for (int i = 0; i < n; i++)
            {
                inSandelys = false;
                inPavadinimas = false;
                inKiekis = false;
                inPreke = false;
                inPavadinimas = (turiniai[i].GetPrekPavad().IndexOf(pavadinimas, StringComparison.OrdinalIgnoreCase) >= 0);
                inSandelys = (turiniai[i].GetAdresas().IndexOf(adresas, StringComparison.OrdinalIgnoreCase) >= 0);
                if (turiniai[i].GetKiekis() >= minkiekis && turiniai[i].GetKiekis() <= maxkiekis) inKiekis = true;
                isrinktiPrekes(prekes, turiniai, pavadinimas, gamintojas, minmetai, maxmetai, minmenuo, maxmenuo, mindiena, maxdiena, minkaina, maxkaina, m, i, out inPreke);
                //inPavadinimas = false;
                //inGamintojas = false;
                //inMinData = false;
                //inMaxData = false;
                //inKaina = false;
                ////2021-03-26 > 2024-07-10; 2022-02-06?
                //inPavadinimas = (prekes[i].GetPavadinimas().IndexOf(pavadinimas, StringComparison.OrdinalIgnoreCase) >= 0);
                //inGamintojas = (prekes[i].GetGamintojas().IndexOf(gamintojas, StringComparison.OrdinalIgnoreCase) >= 0);
                if (inPavadinimas == true && inSandelys == true && inKiekis == true && inPreke == true) dg.Rows.Add(turiniai[i].GetPrekPavad(), turiniai[i].GetAdresas(), turiniai[i].GetKiekis());
            }
        }
        static void SpausdintiDuomenis(DataGridView dg, TextWriter fs)
        {
            fs.WriteLine("-----------Pavadinimas------------------------------Sandėlis------------------Kiekis--");
            for (int i = 0; i < (dg.Rows.Count) - 1; i++)
            {
                fs.Write("| ");
                fs.Write("{0,30:d} | ", dg.Rows[i].Cells[0].Value.ToString());
                fs.Write("{0,40:d} | ", dg.Rows[i].Cells[1].Value.ToString());
                fs.Write("{0,6:d} |\n", dg.Rows[i].Cells[2].Value.ToString());
            }
            fs.WriteLine("--------------------------------------------------------------------------------------");
        }
        static void isrinktiPrekes(Preke[] prekes, SandeliuTuriniai[] turinys, string pavadinimas, string gamintojas, int minmetai, int maxmetai, int minmenuo, int maxmenuo, int mindiena, int maxdiena, double minkaina, double maxkaina, int n, int m, out bool inPreke)
        {
            inPreke = false;
            bool inPavadinimas = false;
            bool inGamintojas = false;
            bool inMinData = false;
            bool inMaxData = false;
            bool inKaina = false;
            for (int i = 0; i < n; i++)
            {
                inPavadinimas = false;
                inGamintojas = false;
                inMinData = false;
                inMaxData = false;
                inKaina = false;
                inPavadinimas = (prekes[i].GetPavadinimas().IndexOf(turinys[m].GetPrekPavad(), StringComparison.OrdinalIgnoreCase) >= 0);
                inGamintojas = (prekes[i].GetGamintojas().IndexOf(gamintojas, StringComparison.OrdinalIgnoreCase) >= 0);
                if (prekes[i].GetMetai() > minmetai || prekes[i].GetMetai() == minmetai && prekes[i].GetMenuo() > minmenuo
                    || prekes[i].GetMetai() == minmetai && prekes[i].GetMenuo() == minmenuo && prekes[i].GetDiena() >= mindiena) inMinData = true;
                if (prekes[i].GetMetai() == maxmetai && prekes[i].GetMenuo() == maxmenuo && prekes[i].GetDiena() <= maxdiena || prekes[i].GetMetai() == maxmetai && prekes[i].GetMenuo() <= maxmenuo || prekes[i].GetMetai() < maxmetai) inMaxData = true;
                if (prekes[i].GetKaina() >= minkaina && prekes[i].GetKaina() <= maxkaina) inKaina = true;
                if (inPavadinimas == true && inGamintojas == true && inMinData == true && inMaxData == true && inKaina == true)
                {
                    inPreke = true;
                    break;
                }
            }
        }
        static void skaitytiDuomenis(DataGridView dg, DataGridView dg2, Preke[] prekes, Sandelys[] sandeliai, SandeliuTuriniai[] turinys, ComboBox cb1, string fv1, string fv2, string fv3, out int n, out int m, out int l)
        {
            n = 0;
            m = 0;
            l = 0;
            string pavadinimas, gamintojas, adresas;
            int metai, menuo, diena, plotas, kiekis;
            double kaina;
            string[] lines = File.ReadAllLines(fv1);
            cb1.Items.Add("");
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
                dg2.Rows.Add(pavadinimas, gamintojas, metai, menuo, diena, kaina);
                n++;
            }
            lines = File.ReadAllLines(fv2);
            foreach (string line in lines)
            {
                string[] parts = line.Split(';');
                plotas = int.Parse(parts[0]);
                adresas = parts[1];
                cb1.Items.Add(parts[1]);
                sandeliai[m] = new Sandelys(plotas, adresas);
                //dg.Rows.Add(pavadinimas, gamintojas, metai, menuo, diena, kaina);
                m++;
            }
            lines = File.ReadAllLines(fv3);
            foreach (string line in lines)
            {
                string[] parts = line.Split(';');
                pavadinimas = parts[0]; // prekės pavadinimas
                adresas = parts[1]; // sandėlio adresas
                kiekis = int.Parse(parts[2]); // prekių kiekis sandėlyje
                turinys[l] = new SandeliuTuriniai(pavadinimas, adresas, kiekis);
                //pavadinimas = parts[0];
                //gamintojas = parts[1];
                //metai = int.Parse(parts[2]);
                //menuo = int.Parse(parts[3]);
                //diena = int.Parse(parts[4]);
                //kaina = double.Parse(parts[5]);
                //prekes[n] = new Preke(pavadinimas, gamintojas, kaina, metai, menuo, diena);
                dg.Rows.Add(pavadinimas, adresas, kiekis);
                l++;
            }
        }
    }
}
