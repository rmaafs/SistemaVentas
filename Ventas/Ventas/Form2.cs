using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.VisualBasic;
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

        private void button1_Click(object sender, EventArgs e) {
            String nombre = txtNombre.Text;
            String desc = txtDesc.Text;
            String imagen = txtImg.Text;
            if (!verificar(new string[] { nombre, desc, imagen, txtPrecio.Text, txtExist.Text })) {
                MessageBox.Show("Por favor ingresa todos los datos.");
                return;
            }


            int precio = int.Parse(txtPrecio.Text);
            int existencias = int.Parse(txtExist.Text);

            

            String sql = String.Format("INSERT INTO producto(pNombre, pDescripcion, pImagen, pPrecio, pExistencias) VALUES ('{0}', '{1}', '{2}', {3}, {4})",
                nombre, desc, imagen, precio, existencias);
            if (mysql.query(sql)) {
                MessageBox.Show("Producto insertado.");
                limpiar();
            } else {
                MessageBox.Show("No se pudo insertar el producto.");
            }
        }

        private void limpiar() {
            txtNombre.Text = "";
            txtDesc.Text = "";
            txtImg.Text = "";
            txtPrecio.Text = "";
            txtExist.Text = "";
        }

        /// <summary>
        /// Función que valida si los argumentos no están vacios.
        /// </summary>
        /// <param name="args">Parámetros a validar</param>
        /// <returns>Retornará verdadero si ningun parámetro es vacío.</returns>
        private bool verificar(String[] args) {
            foreach (var s in args) {
                if (s.Equals("")) return false;
            }
            return true;
        }

        private void txtLimpiar_Click(object sender, EventArgs e) {
            limpiar();
        }

        private void button2_Click(object sender, EventArgs e) {
            string input = Interaction.InputBox("Prompt", "Title", "Default", -1, -1);

        }
    }
}
