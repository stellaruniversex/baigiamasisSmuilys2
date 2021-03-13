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
    public partial class Form5 : Form
    {
        public Form5()
        {
            InitializeComponent();
        }
        Int64 x = 0;
        string line = "";

        private void Form5_Load(object sender, EventArgs e)
        {
            dataGridView1.Columns.Add("Column", "Lentynos");
            dataGridView1.Columns.Add("Column", "Dydis");
            dataGridView1.Columns.Add("Column", "Įmokos");
            dataGridView1.Columns.Add("Column", "Išmokos");
            dataGridView1.Columns.Add("Column", "Telefonas");
            dataGridView1.Columns.Add("Column", "Vieta");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "" || textBox2.Text == "" || textBox3.Text == "" || textBox4.Text == "" || textBox5.Text == "" || textBox6.Text == "")
            {
                MessageBox.Show("Ne visi langeliai užpildyti!", "Pranešimas");
            }
            else
            {
                try // tikrina visus skaičių laukus, ar neįvestį blogi simboliai
                {
                    x = Convert.ToInt32(textBox1.Text);
                    x = Convert.ToInt32(textBox2.Text);
                    x = Convert.ToInt32(textBox3.Text);
                    x = Convert.ToInt32(textBox4.Text);
                    x = Convert.ToInt32(textBox6.Text);
                }
                catch (FormatException)
                {
                    textBox1.Text = "";
                    textBox2.Text = "";
                    textBox3.Text = "";
                    textBox4.Text = "";
                    textBox6.Text = "";
                    MessageBox.Show("Įvedimo klaida!", "Pranešimas");
                    throw;
                }
                dataGridView1.Rows.Add(this.textBox1.Text, this.textBox2.Text, this.textBox3.Text, this.textBox4.Text, this.textBox6.Text, this.textBox5.Text);
                textBox1.Text = "";
                textBox2.Text = "";
                textBox3.Text = "";
                textBox4.Text = "";
                textBox5.Text = "";
                textBox6.Text = "";
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            TextWriter fs = new StreamWriter("parduotuves.txt", false, System.Text.Encoding.GetEncoding(65001));
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
    }
}
