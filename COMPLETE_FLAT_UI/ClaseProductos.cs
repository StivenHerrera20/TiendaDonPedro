using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace COMPLETE_FLAT_UI
{
    public class ClaseProductos
    {
        public static string cadena = "Server=(local)\\SQLEXPRESS;Database=BD_TiendaDonPedro;User Id=Adso2501875;Password=12345;";
        public static string excepcion = "";


        //Parametro: Ninguno
        //Retorno tabla
        //Trae Toda la informacion de los productos
        public static DataTable Func_TraerTodosProductos()
        {
            DataTable Tabla = new DataTable();
            try
            {
                SqlConnection cnn = new SqlConnection(cadena);
                string consulta = "select * from TBL_PRODUCTO";
                SqlDataAdapter adap = new SqlDataAdapter(consulta, cnn);
                adap.Fill(Tabla);
                return Tabla;
            }
            catch (Exception e)
            {
                excepcion = e.Message;
                return Tabla;
            }

        }

        //Parametro: Descripcion
        //Retorno Bool
        //Inserta un producto en la tabla
        public static bool Func_InsertarProductor(string name, int precio, int stock, double iva, int idcat)
        {
            DataTable Tabla = new DataTable();
            try
            {
                SqlConnection cnn = new SqlConnection(cadena);
                string consulta = "INSERT INTO TBL_PRODUCTO VALUES ('" + name+ "',"+precio+","+stock+","+iva+ ","+idcat+");";
                SqlDataAdapter adap = new SqlDataAdapter(consulta, cnn);
                adap.Fill(Tabla);
                return true;
            }
            catch (Exception e)
            {
                excepcion = e.Message;
                return false;
            }

        }

        //Parametro: id
        //Retorno Bool
        //elimina un producto de la tabla
        public static bool Func_eliminaCategoria(int id)
        {
            DataTable Tabla = new DataTable();
            try
            {
                SqlConnection cnn = new SqlConnection(cadena);
                string consulta = "DELETE FROM TBL_PRODUCTO WHERE ID_CATEGORIA=" + id + ";";
                SqlDataAdapter adap = new SqlDataAdapter(consulta, cnn);
                adap.Fill(Tabla);
                return true;
            }
            catch (Exception e)
            {
                excepcion = e.Message;
                return false;
            }

        }


        //Parametro: usuario, nombre, apellido, contraseña, rol, id
        //Retorno tabla
        //edita la informacion del usuario
        public static bool Func_EditarProducto(string name, int precio, int stock, double iva, int idcat, int idprod)
        {
            DataTable Tabla = new DataTable();
            try
            {
                SqlConnection cnn = new SqlConnection(cadena);
                string consulta = "UPDATE TBL_PRODUCTO SET NOMBRE_PRODUCTO='" + name+ "', PRECIO_PRODUCTO='" + precio.ToString()+ "', STOCK_PRODUCTO='" + stock.ToString()+ "', IVA_PRODUCTO='" + iva.ToString()+ "', ID_CATEGORIA='" + idcat.ToString()+ "' WHERE ID_PRODUCTO=" + idprod;
                SqlDataAdapter adap = new SqlDataAdapter(consulta, cnn);
                adap.Fill(Tabla);
                return true;
            }
            catch (Exception e)
            {
                excepcion = e.Message;
                return false;
            }

        }
    }
}
