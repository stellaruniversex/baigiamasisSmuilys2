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
    public partial class pirkejai : Form
    {
        public pirkejai()
        {
            InitializeComponent();
        }

        private void pirkejai_Load(object sender, EventArgs e)
        {
            dataGridView1.Columns.Add("Column", "Vardas");
            dataGridView1.Columns.Add("Column", "Pavardė");
            dataGridView1.Columns.Add("Column", "Gyv. vieta");
            dataGridView1.Columns.Add("Column", "Telefonas");
        }
    }
}
