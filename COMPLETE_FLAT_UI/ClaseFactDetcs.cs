using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace COMPLETE_FLAT_UI
{
    public class ClaseFactDetcs
    {

        public static string cadena = "Server=(local)\\SQLEXPRESS;Database=BD_TiendaDonPedro;User Id=Adso2501875;Password=12345;";
        public static string excepcion = "";
        //1. declaro un dt publico
        public static DataTable DTDetalle = new DataTable();

        //Parametro: Ninguno
        //Retorno tabla
        //Trae Toda la informacion de Un Cliente
        public static DataTable Func_TraerUnProd(long id)
        {
            DataTable Tabla = new DataTable();
            try
            {
                SqlConnection cnn = new SqlConnection(cadena);
                string consulta = "select * from TBL_PRODUCTO Where ID_PRODUCTO=" + id;
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

        //Parametro: nombre, direccion, email
        //Retorno Bool
        //Inserta un cliente en la tabla
        public static bool Func_InsertarProducto(string name, string dir, string email)
        {
            DataTable Tabla = new DataTable();
            try
            {
                SqlConnection cnn = new SqlConnection(cadena);
                string consulta = "INSERT INTO TBL_CLIENTE VALUES ('" + name + "','" + dir + "','" + email + "');";
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
        //elimina un cliente en la tabla
        public static bool Func_eliminaCliente(int id)
        {
            DataTable Tabla = new DataTable();
            try
            {
                SqlConnection cnn = new SqlConnection(cadena);
                string consulta = "DELETE FROM TBL_CLIENTE WHERE ID_CLIENTE=" + id + ";";
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
        //edita la informacion del cliente
        public static bool Func_EditarCliente(string name, string dir, string email, int id)
        {
            DataTable Tabla = new DataTable();
            try
            {
                SqlConnection cnn = new SqlConnection(cadena);
                string consulta = "UPDATE TBL_CLIENTE SET NOMBRE_CLIENTE='" + name + "' , DIRECCION_CLIENTE='" + dir + "', EMAIL_CLIENTE='" + email + "' WHERE ID_CLIENTE=" + id;
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

        public static DataTable Func_InsertarDetalle(long idfact, long idprod, long cant, long precio, long vlrIVA)
        {
            DataTable Tabla = new DataTable();
            try
            {
                SqlConnection cnn = new SqlConnection(cadena);
                string consulta = "INSERT INTO TBL_DETALLEFACTURA VALUES ("+idfact+","+idprod+","+cant+","+precio+","+vlrIVA+");";
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

    }
}
