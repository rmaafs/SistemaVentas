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

        private String PATH = "C:\\Users\\ElMaps\\source\\repos\\rmaafs\\SistemaVentas\\Ventas\\img";

        private MySQL mysql;
        private Form preForm;
        private List<Producto> productos;

        //Variable que nos dirá que producto estaremos editando.
        private int editando = 0;

        public Form2(Form preForm) {
            this.preForm = preForm;
            InitializeComponent();
            btnGuardar.Hide();

            productos = new List<Producto>();
            mysql = new MySQL();

            refillTabla();
        }

        private void refillTabla() {
            DataGridView dgv = tablaProd;
            DataTable table = mysql.getTable("SELECT * FROM producto", productos);
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
                refillTabla();
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

        private void btnBorrar_Click(object sender, EventArgs e) {
            int id = int.Parse(Interaction.InputBox("Ingresa el ID", "Ingresa el ID a borrar", "Borrar", -1, -1));
            foreach (Producto p in productos) {
                if (p.id == id) {
                    if (MessageBox.Show("¿Estas seguro que quieres eliminar el producto " + p.nombre + "?", "Salir", MessageBoxButtons.YesNo) == DialogResult.Yes) {
                        if (mysql.query("DELETE FROM producto WHERE ID=" + id)) {
                            MessageBox.Show("Producto eliminado.");
                        }
                    }
                    refillTabla();
                    return;
                }
            }
            MessageBox.Show("No existe este ID.");
        }

        private void btnLimpiar_Click(object sender, EventArgs e) {
            limpiar();
        }

        private void btnEditar_Click(object sender, EventArgs e) {
            int id = int.Parse(Interaction.InputBox("Ingresa el ID", "Ingresa el ID a editar", "Editar", -1, -1));
            foreach (Producto p in productos) {
                if (p.id == id) {
                    editando = id;

                    txtNombre.Text = p.nombre;
                    txtDesc.Text = p.descripcion;
                    txtImg.Text = p.imagen;
                    txtPrecio.Text = p.precio + "";
                    txtExist.Text = p.existencias + "";

                    btnGuardar.Show();
                    btnInsertar.Hide();
                    btnEditar.Hide();
                    btnBorrar.Hide();
                    btnLimpiar.Hide();

                    setIconImage(p.imagen);

                    return;
                }
            }
            MessageBox.Show("No existe este ID.");
        }

        private void btnGuardar_Click(object sender, EventArgs e) {
            String nombre = txtNombre.Text;
            String desc = txtDesc.Text;
            String imagen = txtImg.Text;
            if (!verificar(new string[] { nombre, desc, imagen, txtPrecio.Text, txtExist.Text })) {
                MessageBox.Show("Por favor ingresa todos los datos.");
                return;
            }


            int precio = int.Parse(txtPrecio.Text);
            int existencias = int.Parse(txtExist.Text);

            btnGuardar.Hide();
            btnInsertar.Show();
            btnEditar.Show();
            btnBorrar.Show();
            btnLimpiar.Show();

            String sql = String.Format("UPDATE producto SET pNombre='{0}', pDescripcion='{1}', pImagen='{2}', pPrecio='{3}', pExistencias='{4}' WHERE ID={5}",
                nombre, desc, imagen, precio, existencias, editando);
            if (mysql.query(sql)) {
                MessageBox.Show("Producto con el ID " + editando + " editado.");
                limpiar();
                refillTabla();
            } else {
                MessageBox.Show("No se pudo insertar el producto.");
            }
            editando = 0;
        }

        private void btnFile_Click(object sender, EventArgs e) {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();

            openFileDialog1.InitialDirectory = PATH;
            openFileDialog1.FilterIndex = 0;
            openFileDialog1.RestoreDirectory = true;

            if (openFileDialog1.ShowDialog() == DialogResult.OK) {
                setIconImage(openFileDialog1.SafeFileName);
            }
        }

        private void setIconImage(String name) {
            try {
                Bitmap image = new Bitmap(PATH + "\\" + name);
                picImagen.SizeMode = PictureBoxSizeMode.Zoom;
                picImagen.Image = (Image)image;
                Controls.Add(picImagen);
            } catch (Exception e) {
                MessageBox.Show("La imagen no existe.");
            }
        }
    }
}
