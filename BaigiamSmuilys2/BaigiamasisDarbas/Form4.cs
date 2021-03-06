using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BaigiamasisDarbas
{
    public partial class Form4 : Form
    {
        public Form4()
        {
            InitializeComponent();
        }

        private void Form4_Load(object sender, EventArgs e)
        {
            dataGridView1.Columns.Add("Column", "Pavadinimas");
            dataGridView1.Columns.Add("Column", "Gamintojas");
            dataGridView1.Columns.Add("Column", "Metai");
            dataGridView1.Columns.Add("Column", "Mėnuo");
            dataGridView1.Columns.Add("Column", "Diena");
            dataGridView1.Columns.Add("Column", "Kaina");
            dataGridView1.Columns.Add("Column", "Kiekis");
            dataGridView1.Columns.Add("Column", "Sandėlys");
        }
    }
}
