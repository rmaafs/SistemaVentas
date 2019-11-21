using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace Ventas {
    public class Producto {
        public String nombre { get; set; }
        public String descripcion { get; set; }
        public String imagen { get; set; }
        public int precio { get; set; }
        public int existencias { get; set; }
        public int id { get; set; }

        public Producto(MySqlDataReader reader) {
            id = reader.GetInt16("ID");
            nombre = reader.GetString("pNombre");
            descripcion = reader.GetString("pDescripcion");
            imagen = reader.GetString("pImagen");
            precio = reader.GetInt16("pPrecio");
            existencias = reader.GetInt16("pExistencias");
        }

    }
}
