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
    public partial class uzsakymai : Form
    {
        public uzsakymai()
        {
            InitializeComponent();
        }
        int n = 0;
        string sandelys = "";
        string pavadinimas = "";
        string parduotuve = "";
        //string line = "";
        int minmetai = 0;
        int maxmetai = 0;
        int minmenuo = 0;
        int maxmenuo = 0;
        int mindiena = 0;
        int maxdiena = 0;
        int minkiekis = 0;
        int maxkiekis = 0;
        const int Cmax = 2048;
        //Preke[] prekes = new Preke[Cmax];
        //Sandelys[] sandeliai = new Sandelys[Cmax];
        Uzsakyma[] uzsakymaii = new Uzsakyma[Cmax];
        const string Cfd1 = "parduotuves.txt";
        const string Cfd2 = "sandeliai.txt";
        const string Cfd3 = "uzsakymai.txt";
        const string Cfs = "ataskaita_uzsakymai.txt";

        private void button1_Click(object sender, EventArgs e)
        {
            //po paskaitos reikės susieti prekių tipus su turiniu
            //if (checkBox1.Checked == true)
            //{
            //
            //}
            sandelys = comboBox2.Text;
            pavadinimas = textBox10.Text;
            parduotuve = comboBox1.Text;
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
            //if (textBox5.Text == "") minkaina = 0;
            //else minkaina = double.Parse(textBox5.Text);
            //if (textBox10.Text == "") maxkaina = 1.7976931348623156E+308;
            //else maxkaina = double.Parse(textBox10.Text);
            if (textBox1.Text == "") minkiekis = 0;
            else minkiekis = int.Parse(textBox1.Text);
            if (textBox2.Text == "") maxkiekis = 2147483647;
            else maxkiekis = int.Parse(textBox2.Text);
            isrinkti(dataGridView1, uzsakymaii, pavadinimas, parduotuve, sandelys, minkiekis, maxkiekis, minmetai, maxmetai, minmenuo, maxmenuo, mindiena, maxdiena, n);
            if (textBox9.Text == "")
            {
                TextWriter fs = new StreamWriter(Cfs, false, System.Text.Encoding.GetEncoding(65001));
                fs.WriteLine("Filtrai:");
                fs.WriteLine("Pavadinimas: *{0}*", pavadinimas);
                fs.WriteLine("parduotuve: *{0}*", parduotuve);
                fs.WriteLine("Sandėlis: {0}", sandelys);
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
                MessageBox.Show("Sėkmingai įrašyta į ataskaita_uzsakymai.txt", "Pranešimas");
            }
            else
            {
                string success_string = "Sėkmingai įrašyta į " + textBox9.Text + ".txt";
                TextWriter fs = new StreamWriter((textBox9.Text + ".txt"), false, System.Text.Encoding.GetEncoding(65001));
                fs.WriteLine("Filtrai:");
                fs.WriteLine("Pavadinimas: *{0}*", pavadinimas);
                fs.WriteLine("Parduotuvė: *{0}*", parduotuve);
                fs.WriteLine("Sandėlis: {0}", sandelys);
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

        static void isrinkti(DataGridView dg, Uzsakyma[] uzsakymai, string pavadinimas, string parduotuve, string adresas, int minkiekis, int maxkiekis, int minmetai, int maxmetai, int minmenuo, int maxmenuo, int mindiena, int maxdiena, int n)
        {
            dg.Rows.Clear();
            dg.Refresh();
            bool inSandelys = false;
            bool inParduotuve = false;
            bool inPavadinimas = false;
            bool inKiekis = false;
            bool inPreke = false;
            bool inMinData = false;
            bool inMaxData = false;
            for (int i = 0; i < n; i++)
            {
                inSandelys = false;
                inPavadinimas = false;
                inParduotuve = false;
                inKiekis = false;
                inMinData = false;
                inMaxData = false;
                inPavadinimas = (uzsakymai[i].GetPrekesPavad().IndexOf(pavadinimas, StringComparison.OrdinalIgnoreCase) >= 0);
                inParduotuve = (uzsakymai[i].GetPardPavad().IndexOf(parduotuve, StringComparison.OrdinalIgnoreCase) >= 0);
                inSandelys = (uzsakymai[i].GetSandPavad().IndexOf(adresas, StringComparison.OrdinalIgnoreCase) >= 0);
                if (uzsakymai[i].GetKiekis() >= minkiekis && uzsakymai[i].GetKiekis() <= maxkiekis) inKiekis = true;
                if (uzsakymai[i].GetMetai() > minmetai || uzsakymai[i].GetMetai() == minmetai && uzsakymai[i].GetMenuo() > minmenuo
                || uzsakymai[i].GetMetai() == minmetai && uzsakymai[i].GetMenuo() == minmenuo && uzsakymai[i].GetDiena() >= mindiena) inMinData = true;
                if (uzsakymai[i].GetMetai() == maxmetai && uzsakymai[i].GetMenuo() == maxmenuo && uzsakymai[i].GetDiena() <= maxdiena || uzsakymai[i].GetMetai() == maxmetai && uzsakymai[i].GetMenuo() <= maxmenuo || uzsakymai[i].GetMetai() < maxmetai) inMaxData = true;
                //inPavadinimas = false;
                //inparduotuve = false;
                //inMinData = false;
                //inMaxData = false;
                //inKaina = false;
                ////2021-03-26 > 2024-07-10; 2022-02-06?
                //inPavadinimas = (prekes[i].GetPavadinimas().IndexOf(pavadinimas, StringComparison.OrdinalIgnoreCase) >= 0);
                //inparduotuve = (prekes[i].Getparduotuve().IndexOf(parduotuve, StringComparison.OrdinalIgnoreCase) >= 0);
                if (inPavadinimas == true && inSandelys == true && inParduotuve == true && inKiekis == true && inMinData == true && inMaxData == true) dg.Rows.Add(uzsakymai[i].GetPrekesPavad(), uzsakymai[i].GetPardPavad(), uzsakymai[i].GetSandPavad(), uzsakymai[i].GetKiekis(), uzsakymai[i].GetMetai(), uzsakymai[i].GetMenuo(), uzsakymai[i].GetDiena());
            }
        }
        static void SpausdintiDuomenis(DataGridView dg, TextWriter fs)
        {               //|             30                 |                    40                    |                    40                    |   6    |   5   |   5   |   5   |
            fs.WriteLine("-----------Pavadinimas----------------------------Parduotuvė----------------------------------Sandėlis-------------------Kiekis---Metai---Mėnuo---Diena--");
            for (int i = 0; i < (dg.Rows.Count) - 1; i++)
            {
                fs.Write("| ");
                fs.Write("{0,30:d} | ", dg.Rows[i].Cells[0].Value.ToString());
                fs.Write("{0,40:d} | ", dg.Rows[i].Cells[1].Value.ToString());
                fs.Write("{0,40:d} | ", dg.Rows[i].Cells[2].Value.ToString());
                fs.Write("{0,6:d} | ", dg.Rows[i].Cells[3].Value.ToString());
                fs.Write("{0,5:d} | ", dg.Rows[i].Cells[4].Value.ToString());
                fs.Write("{0,5:d} | ", dg.Rows[i].Cells[5].Value.ToString());
                fs.Write("{0,5:d} |\n", dg.Rows[i].Cells[6].Value.ToString());
            }
            fs.WriteLine("---------------------------------------------------------------------------------------------------------------------------------------------------------");
        }
        private void uzsakymai_Load(object sender, EventArgs e)
        {
            dataGridView1.Columns.Add("Column", "Pavadinimas");
            dataGridView1.Columns.Add("Column", "Parduotuvė");
            dataGridView1.Columns.Add("Column", "Sandėlis");
            dataGridView1.Columns.Add("Column", "Kiekis");
            dataGridView1.Columns.Add("Column", "Metai");
            dataGridView1.Columns.Add("Column", "Mėnuo");
            dataGridView1.Columns.Add("Column", "Diena");
            SkaitytiDuomenis(dataGridView1, uzsakymaii, comboBox1, comboBox2, Cfd1, Cfd2, Cfd3, out n);
        }

        void SkaitytiDuomenis(DataGridView dg, Uzsakyma[] Uzsakymai, ComboBox cb1, ComboBox cb2, string fv1, string fv2, string fv3, out int n) // skaitys prekių tipus ir sandėlius
        {
            n = 0;
            string pavadinimas, parduotuve, sandelis;
            int kiekis, metai, menuo, diena;
            string[] lines = File.ReadAllLines(fv1);
            cb1.Items.Add("");
            cb2.Items.Add("");
            foreach (string line in lines)
            {
                string[] parts = line.Split(';');
                cb1.Items.Add(parts[5]);
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
                parduotuve = parts[1];
                sandelis = parts[2];
                kiekis = int.Parse(parts[3]);
                metai = int.Parse(parts[4]);
                menuo = int.Parse(parts[5]);
                diena = int.Parse(parts[6]);
                dg.Rows.Add(pavadinimas, parduotuve, sandelis, kiekis, metai, menuo, diena);
                Uzsakymai[n] = new Uzsakyma(pavadinimas, parduotuve, sandelis, kiekis, metai, menuo, diena);
                n++;
            }
        }
    }
}
