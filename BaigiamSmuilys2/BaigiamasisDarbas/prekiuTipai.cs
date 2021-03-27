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
    public partial class prekiuTipai : Form
    {
        public prekiuTipai()
        {
            InitializeComponent();
        }
        int n = 0;
        int x = 0;
        double y = 0;
        string pavadinimas = "";
        string gamintojas = "";
        string line = "";
        int minmetai = 0;
        int maxmetai = 0;
        int minmenuo = 0;
        int maxmenuo = 0;
        int mindiena = 0;
        int maxdiena = 0;
        double minkaina = 0;
        double maxkaina = 0;
        const int Cmax = 2048;
        Preke[] prekes = new Preke[Cmax];
        const string Cfd = "prekes.txt";
        const string Cfs = "ataskaita_prekes.txt";
        // įdėtas: failo pavadinimo įterpimas, jei nėra nurodyto failo, numatomas ataskaita_prekes.txt
        private void button1_Click(object sender, EventArgs e)
        {
            //pavadinimas
            try // tikrina visus skaičių laukus, ar neįvestį blogi simboliai
            {
                //x = Convert.ToInt32(textBox6.Text);
                //x = Convert.ToInt32(textBox4.Text);
                //x = Convert.ToInt32(textBox3.Text);
                //x = Convert.ToInt32(textBox9.Text);
                //x = Convert.ToInt32(textBox7.Text);
                //x = Convert.ToInt32(textBox8.Text);
                //y = Convert.ToDouble(textBox10.Text);
                //y = Convert.ToDouble(textBox5.Text);
            }
            catch (FormatException)
            {
                //textBox3.Text = "";
                //textBox4.Text = "";
                //textBox5.Text = "";
                //textBox6.Text = "";
                //textBox7.Text = "";
                //textBox8.Text = "";
                //textBox9.Text = "";
                MessageBox.Show("Įvedimo klaida!", "Pranešimas");
                throw;
            }
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
            if (textBox10.Text == "") maxkaina = 2147483647;
            else maxkaina = double.Parse(textBox10.Text);
            isrinkti(dataGridView1, prekes, pavadinimas, gamintojas, minmetai, maxmetai, minmenuo, maxmenuo, mindiena, maxdiena, minkaina, maxkaina, n);
            if (textBox11.Text == "")
            {
                TextWriter fs = new StreamWriter(Cfs, false, System.Text.Encoding.GetEncoding(65001));
                SpausdintiDuomenis(dataGridView1, fs, line);
                fs.Close();
            }
            else
            {
                TextWriter fs = new StreamWriter((textBox11.Text + ".txt"), false, System.Text.Encoding.GetEncoding(65001));
                SpausdintiDuomenis(dataGridView1, fs, line);
                fs.Close();
            }
            //dataGridView1.Rows.Add(this.textBox1.Text, this.textBox2.Text, this.textBox6.Text, this.textBox4.Text, this.textBox3.Text, this.textBox5.Text);
            //textBox1.Text = "";
            //textBox2.Text = "";
            //textBox3.Text = "";
            //textBox4.Text = "";
            //textBox5.Text = "";
            //textBox6.Text = "";
            //MessageBox.Show("Sėkmingai įrašyta", "Pranešimas");
        }

        static void isrinkti(DataGridView dg, Preke[] prekes, string pavadinimas, string gamintojas, int minmetai, int maxmetai, int minmenuo, int maxmenuo, int mindiena, int maxdiena, double minkaina, double maxkaina, int n)
        {
            dg.Rows.Clear();
            dg.Refresh();
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
                //2021-03-26 > 2024-07-10; 2022-02-06?
                inPavadinimas = (prekes[i].GetPavadinimas().IndexOf(pavadinimas, StringComparison.OrdinalIgnoreCase) >= 0);
                inGamintojas = (prekes[i].GetGamintojas().IndexOf(gamintojas, StringComparison.OrdinalIgnoreCase) >= 0);
                if (prekes[i].GetMetai() > minmetai || prekes[i].GetMetai() == minmetai && prekes[i].GetMenuo() > minmenuo
                    || prekes[i].GetMetai() == minmetai && prekes[i].GetMenuo() == minmenuo && prekes[i].GetDiena() >= mindiena) inMinData = true;
                if (prekes[i].GetMetai() == maxmetai && prekes[i].GetMenuo() == maxmenuo && prekes[i].GetDiena() <= maxdiena || prekes[i].GetMetai() == maxmetai && prekes[i].GetMenuo() <= maxmenuo || prekes[i].GetMetai() < maxmetai) inMaxData = true;
                if (prekes[i].GetKaina() >= minkaina && prekes[i].GetKaina() <= maxkaina) inKaina = true;
                if(inPavadinimas == true && inGamintojas == true && inMinData == true && inMaxData == true && inKaina == true) dg.Rows.Add(prekes[i].GetPavadinimas(), prekes[i].GetGamintojas(), prekes[i].GetMetai(), prekes[i].GetMenuo(), prekes[i].GetDiena(), prekes[i].GetKaina());
            }
        }

        static void skaitytiDuomenis(DataGridView dg, Preke[] prekes, string fv, out int n)
        {
            n = 0;
            string pavadinimas, gamintojas;
            int metai, menuo, diena;
            double kaina;
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
                prekes[n] = new Preke(pavadinimas,gamintojas,kaina,metai,menuo,diena);
                dg.Rows.Add(pavadinimas, gamintojas, metai, menuo, diena, kaina);
                n++;
            }
        }
        static void SpausdintiDuomenis(DataGridView dg, TextWriter fs, string line)
        {                                                //v 30
            fs.WriteLine("-----------Pavadinimas------------------------Gamintojas------------Metai---Menuo---Diena----Kaina--");
            for (int i = 0; i < (dg.Rows.Count) - 1; i++)
            {
                fs.Write("| ");
                fs.Write("{0,30:d} | ", dg.Rows[i].Cells[0].Value.ToString());
                fs.Write("{0,30:d} | ", dg.Rows[i].Cells[1].Value.ToString());
                fs.Write("{0,5:d} | ", dg.Rows[i].Cells[2].Value.ToString());
                fs.Write("{0,5:d} | ", dg.Rows[i].Cells[3].Value.ToString());
                fs.Write("{0,5:d} | ", dg.Rows[i].Cells[4].Value.ToString());
                fs.Write("{0,6:d} |\n", dg.Rows[i].Cells[5].Value.ToString());
            }
            fs.WriteLine("----------------------------------------------------------------------------------------------------");
        }

        private void prekiuTipai_Load(object sender, EventArgs e)
        {
            dataGridView1.Columns.Add("Column", "Pavadinimas");
            dataGridView1.Columns.Add("Column", "Gamintojas");
            dataGridView1.Columns.Add("Column", "Metai");
            dataGridView1.Columns.Add("Column", "Menuo");
            dataGridView1.Columns.Add("Column", "Diena");
            dataGridView1.Columns.Add("Column", "Kaina");
            skaitytiDuomenis(dataGridView1, prekes, Cfd, out n);
        }
    }
}
