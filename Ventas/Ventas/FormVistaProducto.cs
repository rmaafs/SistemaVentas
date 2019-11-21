using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Ventas {
    public partial class FormVistaProducto : Form {

        private String PATH = "";
        private Producto p;
        private int cantidad = 1;

        public FormVistaProducto(Producto p, String PATH) {
            InitializeComponent();

            this.p = p;
            this.PATH = PATH;
            
            load();
        }

        private void load() {
            txtNombre.Text = p.nombre;
            txtDesc.Text = p.descripcion;
            txtUnitario.Text = "$" + p.precio;
            txtTotal.Text = "$" + (cantidad * p.precio);
            txtExistencias.Text = "Existencias: " + p.existencias;

            //Ponemos la imagen
            try {
                Bitmap image = new Bitmap(PATH + "\\" + p.imagen);
                picImg.SizeMode = PictureBoxSizeMode.Zoom;
                picImg.Image = (Image)image;
                Controls.Add(picImg);
            } catch (Exception e) {
                MessageBox.Show("La imagen no existe. " + e.Message);
            }
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e) {
            cantidad = int.Parse(numericUpDown1.Value.ToString());
            txtTotal.Text = "$" + (cantidad * p.precio);
        }
    }
}
