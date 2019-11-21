using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace Ventas {
    public class MySQL {

        private String HOST = "localhost";
        private String DATABASE = "ventas";
        private String USER = "root";
        private String PASS = "";

        private MySqlConnection con;

        public MySQL() {
            con = new MySqlConnection();
            con.ConnectionString = "server = " + HOST + "; database = " + DATABASE + "; uid = " + USER + "; pwd = " + PASS + ";";
        }

        ~MySQL() {
            if (con.State == ConnectionState.Open) cerrar();
        }

        public bool conectar() {
            cerrar();
            if (con.State == ConnectionState.Closed) {
                try {
                    con.Open();
                    return true;
                } catch (Exception ex) {
                    MessageBox.Show(ex.Message);
                }
            }
            return false;
        }

        public bool cerrar() {
            if (con.State == ConnectionState.Open) {
                try {
                    con.Close();
                    con.Dispose();

                    Debug.WriteLine("DB Cerrada.");

                    return true;
                } catch (Exception ex) {
                    MessageBox.Show(ex.Message);
                }
            }
            return false;
        }

        public MySqlDataReader select(String sql) {
            conectar();
            MySqlCommand cmd = new MySqlCommand(sql, con);
            return cmd.ExecuteReader();
        }

        public MySqlCommand getCMD(String sql) {
            conectar();
            return new MySqlCommand(sql, con);
        }

        public bool query(String sql) {
            conectar();
            MySqlCommand cmd = new MySqlCommand(sql, con);
            try {
                cmd.ExecuteReader();
                return true;
            } catch (Exception e) {
                MessageBox.Show(e.Message);
            }
            return false;
        }

        public int count(String sql) {
            conectar();
            int i = 0;
            MySqlDataReader reader = select(sql);
            while (reader.Read()) {
                i++;
            }
            return i;
        }

        public void llenarProductos(List<Producto> productos) {
            productos.Clear();
            conectar();
            MySqlDataReader reader = select("SELECT * FROM producto");
            while (reader.Read()) {
                productos.Add(new Producto(reader));
            }
            cerrar();
        }

        public DataTable getTable(String sql, List<Producto> productos) {
            llenarProductos(productos);

            conectar();
            MySqlDataAdapter returnVal = new MySqlDataAdapter(sql, con);
            DataTable dt = new DataTable("Productos");
            returnVal.Fill(dt);
            return dt;
        }

    }
}
