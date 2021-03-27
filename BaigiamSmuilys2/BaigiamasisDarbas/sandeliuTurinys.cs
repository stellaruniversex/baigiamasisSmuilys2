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
        int x = 0;
        double y = 0;
        string sandelys = "";
        string pavadinimas = "";
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
            skaitytiDuomenis(dataGridView1, prekes, sandeliai, turinys, comboBox1, Cfd1, Cfd2, Cfd3, out n, out m, out l);
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
            if (textBox10.Text == "") maxkaina = 2147483647;
            else maxkaina = double.Parse(textBox10.Text);
            if (textBox12.Text == "") minkiekis = 0;
            else minkiekis = int.Parse(textBox12.Text);
            if (textBox11.Text == "") maxkiekis = 2147483647;
            else maxkiekis = int.Parse(textBox11.Text);
            isrinkti(dataGridView1, turinys, pavadinimas, sandelys, minkiekis, maxkiekis, l);
            if (textBox13.Text == "")
            {
                TextWriter fs = new StreamWriter(Cfs, false, System.Text.Encoding.GetEncoding(65001));
                SpausdintiDuomenis(dataGridView1, fs);
                fs.Close();
                MessageBox.Show("Sėkmingai įrašyta į ataskaita_prekes.txt", "Pranešimas");
            }
            else
            {
                string success_string = "Sėkmingai įrašyta į " + textBox11.Text + ".txt";
                TextWriter fs = new StreamWriter((textBox13.Text + ".txt"), false, System.Text.Encoding.GetEncoding(65001));
                SpausdintiDuomenis(dataGridView1, fs);
                fs.Close();
                //MessageBox.Show("Sėkmingai įrašyta", "Pranešimas");
                MessageBox.Show(success_string, "Pranešimas");
            }
        }
        static void isrinkti(DataGridView dg, SandeliuTuriniai[] turiniai, string pavadinimas, string adresas, int minkiekis, int maxkiekis, int n)
        {
            dg.Rows.Clear();
            dg.Refresh();
            bool inSandelys = false;
            bool inPavadinimas = false;
            bool inKiekis = false;
            for (int i = 0; i < n; i++)
            {
                inSandelys = false;
                inPavadinimas = false;
                inKiekis = false;
                inPavadinimas = (turiniai[i].GetPrekPavad().IndexOf(pavadinimas, StringComparison.OrdinalIgnoreCase) >= 0);
                inSandelys = (turiniai[i].GetAdresas().IndexOf(adresas, StringComparison.OrdinalIgnoreCase) >= 0);
                if (turiniai[i].GetKiekis() >= minkiekis && turiniai[i].GetKiekis() <= maxkiekis) inKiekis = true;
                //inPavadinimas = false;
                //inGamintojas = false;
                //inMinData = false;
                //inMaxData = false;
                //inKaina = false;
                ////2021-03-26 > 2024-07-10; 2022-02-06?
                //inPavadinimas = (prekes[i].GetPavadinimas().IndexOf(pavadinimas, StringComparison.OrdinalIgnoreCase) >= 0);
                //inGamintojas = (prekes[i].GetGamintojas().IndexOf(gamintojas, StringComparison.OrdinalIgnoreCase) >= 0);
                if (inPavadinimas == true && inSandelys == true && inKiekis == true) dg.Rows.Add(turiniai[i].GetPrekPavad(), turiniai[i].GetAdresas(), turiniai[i].GetKiekis());
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
        static void skaitytiDuomenis(DataGridView dg, Preke[] prekes, Sandelys[] sandeliai, SandeliuTuriniai[] turinys, ComboBox cb1, string fv1, string fv2, string fv3, out int n, out int m, out int l)
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
                //dg.Rows.Add(pavadinimas, gamintojas, metai, menuo, diena, kaina);
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
