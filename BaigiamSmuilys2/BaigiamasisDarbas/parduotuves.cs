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
    public partial class parduotuves : Form
    {
        public parduotuves()
        {
            InitializeComponent();
        }
        const int Cmax = 2048;
        int n = 0;
        int minlentynos = 0;
        int maxlentynos = 0;
        int mindydis = 0;
        int maxdydis = 0;
        int minimokos = 0;
        int maximokos = 0;
        int minismokos = 0;
        int maxismokos = 0;
        string telefonas = "";
        string adresas = "";
        const string Cfd = "parduotuves.txt";
        const string Cfs = "ataskaita_parduotuves.txt";
        Parduotuve[] parduotuvess = new Parduotuve[Cmax];

        private void parduotuves_Load(object sender, EventArgs e)
        {
            dataGridView1.Columns.Add("Column", "Lentynos");
            dataGridView1.Columns.Add("Column", "Dydis");
            dataGridView1.Columns.Add("Column", "Įmokos");
            dataGridView1.Columns.Add("Column", "Išmokos");
            dataGridView1.Columns.Add("Column", "Telefonas");
            dataGridView1.Columns.Add("Column", "Adresass");
            skaitytiDuomenis(dataGridView1, parduotuvess, Cfd, out n);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "") minlentynos = 0;
            else minlentynos = int.Parse(textBox1.Text);
            if (textBox2.Text == "") maxlentynos = 2147483647;
            else maxlentynos = int.Parse(textBox2.Text);
            if (textBox3.Text == "") mindydis = 0;
            else mindydis = int.Parse(textBox3.Text);
            if (textBox4.Text == "") maxdydis = 2147483647;
            else maxdydis = int.Parse(textBox4.Text);
            if (textBox5.Text == "") minimokos = 0;
            else minimokos = int.Parse(textBox5.Text);
            if (textBox6.Text == "") maximokos = 2147483647;
            else maximokos = int.Parse(textBox6.Text);
            if (textBox7.Text == "") minismokos = 0;
            else minismokos = int.Parse(textBox7.Text);
            if (textBox8.Text == "") maxismokos = 2147483647;
            else maxismokos = int.Parse(textBox8.Text);
            telefonas = textBox9.Text;
            adresas = textBox10.Text;
            Isrinkti(dataGridView1, parduotuvess, minlentynos, maxlentynos, mindydis, maxdydis, minimokos, maximokos, minismokos, maxismokos, telefonas, adresas, n);
            if (textBox11.Text == "")
            {
                TextWriter fs = new StreamWriter(Cfs, false, System.Text.Encoding.GetEncoding(65001));
                SpausdintiDuomenis(dataGridView1, fs);
                fs.Close();
                MessageBox.Show("Sėkmingai įrašyta į ataskaita_parduotuves.txt", "Pranešimas");
            }
            else
            {
                string success_string = "Sėkmingai įrašyta į " + textBox11.Text + ".txt";
                TextWriter fs = new StreamWriter((textBox11.Text + ".txt"), false, System.Text.Encoding.GetEncoding(65001));
                SpausdintiDuomenis(dataGridView1, fs);
                fs.Close();
                //MessageBox.Show("Sėkmingai įrašyta", "Pranešimas");
                MessageBox.Show(success_string, "Pranešimas");
            }
        }
        static void SpausdintiDuomenis(DataGridView dg, TextWriter fs)
        {               //|          |       |        |         |            |
            fs.WriteLine("--Lentynos---Dydis---Įmokos---Išmokos----Telefonas--------------Adresas------------");
            for (int i = 0; i < (dg.Rows.Count) - 1; i++)
            {
                fs.Write("| ");
                fs.Write("{0,8:d} | ", dg.Rows[i].Cells[0].Value.ToString());
                fs.Write("{0,5:d} | ", dg.Rows[i].Cells[1].Value.ToString());
                fs.Write("{0,6:d} | ", dg.Rows[i].Cells[2].Value.ToString());
                fs.Write("{0,7:d} |", dg.Rows[i].Cells[3].Value.ToString());
                fs.Write("{0,10:d} |", dg.Rows[i].Cells[4].Value.ToString());
                fs.Write("{0,30:d} |\n", dg.Rows[i].Cells[5].Value.ToString());
            }
            fs.WriteLine("-----------------------------------------------------------------------------------");
        }
        static void Isrinkti(DataGridView dg, Parduotuve[] parduotuves, int minlentynos, int maxlentynos, int mindydis, int maxdydis, int minimokos, int maximokos, int minismokos, int maxismokos, string telefonas, string adresas, int n)
        {
            dg.Rows.Clear();
            dg.Refresh();
            bool inLentynos = false;
            bool inDydis = false;
            bool inImokos = false;
            bool inIsmokos = false;
            bool inTelefonas = false;
            bool inAdresas = false;
            string curTelefonas = "";
            for (int i = 0; i < n; i++)
            {
                inLentynos = false;
                inDydis = false;
                inImokos = false;
                inIsmokos = false;
                inTelefonas = false;
                inAdresas = false;
                if (parduotuves[i].GetLentynos() >= minlentynos && parduotuves[i].GetLentynos() <= maxlentynos) inLentynos = true;
                if (parduotuves[i].GetDydis() >= minlentynos && parduotuves[i].GetDydis() <= maxdydis) inDydis = true;
                if (parduotuves[i].GetImokos() >= minimokos && parduotuves[i].GetImokos() <= maximokos) inImokos = true;
                if (parduotuves[i].GetIsmokos() >= minismokos && parduotuves[i].GetIsmokos() <= maxismokos) inIsmokos = true;
                curTelefonas = parduotuves[i].GetTelefonas().ToString();
                inTelefonas = (curTelefonas.IndexOf(telefonas, StringComparison.OrdinalIgnoreCase) >= 0);
                inAdresas = (parduotuves[i].GetVieta().IndexOf(adresas, StringComparison.OrdinalIgnoreCase) >= 0);
                //inGyvVieta = (pirkejai[i].GetVieta().IndexOf(adresas, StringComparison.OrdinalIgnoreCase) >= 0);
                //inVardas = (pirkejai[i].GetVardas().IndexOf(vardas, StringComparison.OrdinalIgnoreCase) >= 0);
                //inPavarde = (pirkejai[i].GetPavarde().IndexOf(pavarde, StringComparison.OrdinalIgnoreCase) >= 0);
                //inGyvVieta = (pirkejai[i].GetGyvVieta().IndexOf(gyvvieta, StringComparison.OrdinalIgnoreCase) >= 0);
                //curTelefonas = pirkejai[i].GetTelefonas().ToString();
                //inTelefonas = (curTelefonas.IndexOf(telefonas, StringComparison.OrdinalIgnoreCase) >= 0);
                if (inLentynos == true && inDydis == true && inImokos == true && inIsmokos == true && inTelefonas == true && inAdresas == true) dg.Rows.Add(parduotuves[i].GetLentynos(), parduotuves[i].GetDydis(), parduotuves[i].GetImokos(), parduotuves[i].GetIsmokos(), curTelefonas, parduotuves[i].GetVieta());
            }
        }
        static void skaitytiDuomenis(DataGridView dg, Parduotuve[] parduotuves, string fv, out int n)
        {
            n = 0;
            int lentynos, dydis, imokos, ismokos;
            Int64 telefonas;
            string vieta;
            string[] lines = File.ReadAllLines(fv);
            foreach (string line in lines)
            {
                string[] parts = line.Split(';');
                lentynos = int.Parse(parts[0]);
                dydis = int.Parse(parts[1]);
                imokos = int.Parse(parts[2]);
                ismokos = int.Parse(parts[3]);
                telefonas = Int64.Parse(parts[4]);
                vieta = parts[5];
                parduotuves[n] = new Parduotuve(lentynos, dydis, imokos, ismokos, telefonas, vieta);
                dg.Rows.Add(lentynos, dydis, imokos, ismokos, telefonas, vieta);
                n++;
            }
        }
    }
}
