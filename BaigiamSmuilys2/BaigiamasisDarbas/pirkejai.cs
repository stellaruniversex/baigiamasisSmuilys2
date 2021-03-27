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
    public partial class pirkejai : Form
    {
        public pirkejai()
        {
            InitializeComponent();
        }
        string vardas = "";
        string pavarde = "";
        string gyvvieta = "";
        string telefonas = "";
        const int Cmax = 2048;
        Pirkejas[] pirkejaii = new Pirkejas[Cmax];
        int n = 0;
        const string Cfd = "pirkejai.txt";
        const string Cfs = "ataskaita_pirkejai.txt";
        //Int64 telefonas = 0;

        private void pirkejai_Load(object sender, EventArgs e)
        {
            dataGridView1.Columns.Add("Column", "Vardas");
            dataGridView1.Columns.Add("Column", "Pavardė");
            dataGridView1.Columns.Add("Column", "Gyv. vieta");
            dataGridView1.Columns.Add("Column", "Telefonas");
            skaitytiDuomenis(dataGridView1, pirkejaii, Cfd, out n);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            vardas = textBox1.Text;
            pavarde = textBox2.Text;
            gyvvieta = textBox3.Text;
            telefonas = textBox4.Text;
            Isrinkti(dataGridView1, pirkejaii, vardas, pavarde, gyvvieta, telefonas, n);
            if (textBox5.Text == "")
            {
                TextWriter fs = new StreamWriter(Cfs, false, System.Text.Encoding.GetEncoding(65001));
                SpausdintiDuomenis(dataGridView1, fs);
                fs.Close();
                MessageBox.Show("Sėkmingai įrašyta į ataskaita_pirkejai.txt", "Pranešimas");
            }
            else
            {
                string success_string = "Sėkmingai įrašyta į " + textBox5.Text + ".txt";
                TextWriter fs = new StreamWriter((textBox5.Text + ".txt"), false, System.Text.Encoding.GetEncoding(65001));
                SpausdintiDuomenis(dataGridView1, fs);
                fs.Close();
                //MessageBox.Show("Sėkmingai įrašyta", "Pranešimas");
                MessageBox.Show(success_string, "Pranešimas");
            }
        }

        static void Isrinkti (DataGridView dg, Pirkejas[] pirkejai, string vardas, string pavarde, string gyvvieta, string telefonas, int n)
        {
            dg.Rows.Clear();
            dg.Refresh();
            bool inVardas = false;
            bool inPavarde = false;
            bool inGyvVieta = false;
            bool inTelefonas = false;
            string curTelefonas = "";
            for (int i = 0; i < n; i++)
            {
                inVardas = false;
                inPavarde = false;
                inGyvVieta = false;
                inTelefonas = false;
                inVardas = (pirkejai[i].GetVardas().IndexOf(vardas, StringComparison.OrdinalIgnoreCase) >= 0);
                inPavarde = (pirkejai[i].GetPavarde().IndexOf(pavarde, StringComparison.OrdinalIgnoreCase) >= 0);
                inGyvVieta = (pirkejai[i].GetGyvVieta().IndexOf(gyvvieta, StringComparison.OrdinalIgnoreCase) >= 0);
                curTelefonas = pirkejai[i].GetTelefonas().ToString();
                inTelefonas = (curTelefonas.IndexOf(telefonas, StringComparison.OrdinalIgnoreCase) >= 0);
                if (inVardas == true && inPavarde == true && inGyvVieta == true && inTelefonas == true) dg.Rows.Add(pirkejai[i].GetVardas(), pirkejai[i].GetPavarde(), pirkejai[i].GetGyvVieta(), curTelefonas);
            }
        }
        static void SpausdintiDuomenis(DataGridView dg, TextWriter fs)
        {               //|              |              |                                |                 |
            fs.WriteLine("-----Vardas---------Pavardė-------------Gyvenamoji vieta------------Telefonas-----");
            for (int i = 0; i < (dg.Rows.Count) - 1; i++)
            {
                fs.Write("| ");
                fs.Write("{0,12:d} | ", dg.Rows[i].Cells[0].Value.ToString());
                fs.Write("{0,12:d} | ", dg.Rows[i].Cells[1].Value.ToString());
                fs.Write("{0,30:d} | ", dg.Rows[i].Cells[2].Value.ToString());
                fs.Write("{0,15:d} |\n", dg.Rows[i].Cells[3].Value.ToString());
            }
            fs.WriteLine("----------------------------------------------------------------------------------");
        }
        static void skaitytiDuomenis(DataGridView dg, Pirkejas[] pirkejai, string fv, out int n)
        {
            n = 0;
            string vardas, pavarde, gyvvieta;
            Int64 telefonas;
            string[] lines = File.ReadAllLines(fv);
            foreach (string line in lines)
            {
                string[] parts = line.Split(';');
                vardas = parts[0];
                pavarde = parts[1];
                gyvvieta = parts[2];
                telefonas = Int64.Parse(parts[3]);
                pirkejai[n] = new Pirkejas(vardas, pavarde, gyvvieta, telefonas);
                dg.Rows.Add(vardas, pavarde, gyvvieta, telefonas);
                n++;
            }
        }
    }
}
