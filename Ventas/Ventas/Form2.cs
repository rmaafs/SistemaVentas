using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace Ventas {
    public partial class Form2 : Form {

        private MySQL mysql;
        private Form preForm;

        public Form2(Form preForm) {
            this.preForm = preForm;
            InitializeComponent();
            mysql = new MySQL();

            refillTabla();
        }

        private void refillTabla() {
            DataGridView dgv = tablaProd;
            DataTable table = mysql.getTable("SELECT * FROM producto");
            dgv.DataSource = table;
            dgv.AutoResizeColumns();
        }
    }
}
