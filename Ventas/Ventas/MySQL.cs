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

            conectar();
        }

        ~MySQL() {
            if (con.State == ConnectionState.Open) cerrar();
        }

        public bool conectar() {
            try {
                con.Open();
                return true;
            } catch (Exception ex) {
                MessageBox.Show(ex.Message);
            }
            return false;
        }

        public bool cerrar() {
            try {
                con.Close();
                con.Dispose();

                Debug.WriteLine("DB Cerrada.");

                return true;
            } catch (Exception ex) {
                MessageBox.Show(ex.Message);
            }
            return false;
        }

        public MySqlDataReader select(String sql) {
            MySqlCommand cmd = new MySqlCommand(sql, con);
            return cmd.ExecuteReader();
        }

        public MySqlCommand getCMD(String sql) {
            return new MySqlCommand(sql, con);
        }

        public bool query(String sql) {
            MySqlCommand cmd = new MySqlCommand(sql, con);
            try {
                cmd.ExecuteNonQuery();
                return true;
            } catch (Exception e) {
                MessageBox.Show(e.Message);
            }
            return false;
        }

        public int count(String sql) {
            int i = 0;
            MySqlDataReader reader = select(sql);
            while (reader.Read()) {
                i++;
            }
            return i;
        }

        public DataTable getTable(String sql) {
            MySqlDataAdapter returnVal = new MySqlDataAdapter(sql, con);
            DataTable dt = new DataTable("CharacterInfo");
            returnVal.Fill(dt);
            return dt;
        }

    }
}
