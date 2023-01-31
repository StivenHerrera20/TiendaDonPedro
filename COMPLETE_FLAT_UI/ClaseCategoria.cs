using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace COMPLETE_FLAT_UI
{
    public class ClaseCategoria
    {
            public static string cadena = "Server=(local)\\SQLEXPRESS;Database=BD_TiendaDonPedro;User Id=Adso2501875;Password=12345;";
            public static string excepcion = "";


            //Parametro: Ninguno
            //Retorno tabla
            //Trae Toda la informacion de las categorias
            public static DataTable Func_TraerTodasCategorias()
            {
                DataTable Tabla = new DataTable();
                try
                {
                    SqlConnection cnn = new SqlConnection(cadena);
                    string consulta = "select * from TBL_CATEGORIA";
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
            //Inserta una categoria en la tabla
            public static bool Func_InsertarCategoria(string des)
            {
                DataTable Tabla = new DataTable();
                try
                {
                    SqlConnection cnn = new SqlConnection(cadena);
                    string consulta = "INSERT INTO TBL_CATEGORIA VALUES ('" + des + "');";
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
            //elimina un usuario en la tabla
            public static bool Func_eliminaCategoria(int id)
            {
                DataTable Tabla = new DataTable();
                try
                {
                    SqlConnection cnn = new SqlConnection(cadena);
                    string consulta = "DELETE FROM TBL_CATEGORIA WHERE ID_CATEGORIA=" + id + ";";
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
            public static bool Func_EditarCategoria(int id, string des)
            {
                DataTable Tabla = new DataTable();
                try
                {
                    SqlConnection cnn = new SqlConnection(cadena);
                    string consulta = "UPDATE TBL_CATEGORIA SET DES_CATEGORIA='" + des + "' WHERE ID_CATEGORIA=" + id;
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
