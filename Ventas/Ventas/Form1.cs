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
using System.Security.Cryptography;
using System.Diagnostics;

namespace Ventas {
    public partial class Form1 : Form {

        private MySQL mysql = null;

        public Form1() {
            InitializeComponent();
            mysql = new MySQL();
        }

        private void btnIngresar_Click_1(object sender, EventArgs e) {
            String user = txtUsuario.Text;
            String pass = Encripter.Crypt(txtPass.Text);
            String sql = "SELECT * FROM usuario WHERE user = '" + user + "' AND passwd = '" + pass + "'";

            if (mysql.count(sql) == 0) {
                MessageBox.Show("Usuario y/o contraseña incorrecto.");
            } else {
                new Form2(this).Show();

                mysql.cerrar();
                this.Hide();
            }
        }

        private void btnTienda_Click(object sender, EventArgs e) {
            new FormProductos(this).Show();

            mysql.cerrar();
            this.Hide();
        }
    }
}
