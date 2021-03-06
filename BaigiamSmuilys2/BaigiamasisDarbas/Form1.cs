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
    public partial class Form1 : Form
    {
        const string CFd1 = "prekes.txt";
        const string CFd2 = "sandeliai.txt";
        const string CFd3 = "sandeliaiprekes.txt";
        const string CFd4 = "sandeliaitransportas.txt";
        const string CFd5 = "uzsakymai.txt";
        const string CFd6 = "pirkejai.txt";
        const string CFd7 = "nupirktosprekes.txt";
        const string CFd8 = "parduotuves.txt";
        const string CFd9 = "parduotuvesprekes.txt";
        const string CFr1 = "PrekesAtaskaita.txt";
        const string CFr2 = "PirkejaiAtaskaita.txt";
        const string CFr3 = "NupirktosAtaskaita.txt";
        const string CFr4 = "SandeliaiAtaskaita.txt";
        const string CFr5 = "ParduotuvesAtaskaita.txt";
        const string CKvitas = "Kvitas.txt";
        public Form1()
        {
            InitializeComponent();
        }

        private void prekiuTipaiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form2 f2 = new Form2();
            f2.ShowDialog();
            //this.Hide();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void sandeliaiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form3 f3 = new Form3();
            f3.ShowDialog();
            //this.Hide();
        }

        private void sandeliųTurinysToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form4 f4 = new Form4();
            f4.ShowDialog();
            //this.Hide();
        }

        private void parduotuvesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form5 f5 = new Form5();
            f5.ShowDialog();
        }

        private void parduotuviuTurinysToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form6 f6 = new Form6();
            f6.ShowDialog();
        }

        private void pirkejaiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form7 f7 = new Form7();
            f7.ShowDialog();
        }

        private void nupirktosPrekesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form8 f8 = new Form8();
            f8.ShowDialog();
        }

        private void uzsakymaiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form9 f9 = new Form9();
            f9.ShowDialog();
        }
    }
}
