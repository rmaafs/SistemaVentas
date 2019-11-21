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
    public partial class FormProductos : Form {

        public static String PATH = "C:\\Users\\ElMaps\\source\\repos\\rmaafs\\SistemaVentas\\Ventas\\img";

        private Form preForm;
        private MySQL mysql;
        private List<Producto> productos;

        public FormProductos(Form preForm) {
            InitializeComponent();
            this.preForm = preForm;

            productos = new List<Producto>();
            mysql = new MySQL();

            loadTable();
        }

        public void loadTable() {
            //Llenamos la lista productos.
            mysql.getTable("SELECT * FROM producto", productos);

            listView.View = View.Details;
            listView.CheckBoxes = true;

            listView.Columns.Add("", 200);
            listView.Columns.Add("", 100);
            listView.Columns.Add("", 100);

            //Añadimos las imagenes a una lista
            ImageList imgs = new ImageList();
            imgs.ImageSize = new Size(100, 100);

            foreach (Producto p in productos) {
                imgs.Images.Add(Image.FromFile(PATH + "\\" + p.imagen));
            }
            listView.SmallImageList = imgs;
            //--------------


            int i = 0;
            ListViewItem lvi;
            foreach (Producto p in productos) {
                lvi = new ListViewItem(p.nombre);
                lvi.SubItems.Add(p.descripcion);
                lvi.SubItems.Add("$" + p.precio);
                lvi.ImageIndex = i;
                listView.Items.Add(lvi);
                i++;
            }
        }

        ~FormProductos() {
            //preForm.Close();
        }

        private void listView_DoubleClick(object sender, EventArgs e) {
            String s = listView.SelectedItems[0].SubItems[0].Text;
            MessageBox.Show("Seleccionado: " + s + ", ");
        }
    }
}
