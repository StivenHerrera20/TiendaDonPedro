using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace COMPLETE_FLAT_UI
{
    public class ClaseUsuarios
    {
        public static string cadena = "Server=(local)\\SQLEXPRESS;Database=BD_TiendaDonPedro;User Id=Adso2501875;Password=12345;";
        public static string excepcion = "";


        //Parametro: Ninguno
        //Retorno tabla
        //Trae Toda la informacion de los usuario
        public static DataTable Func_TraerTodosUsuario()
        {
            DataTable Tabla = new DataTable();
            try
            {
                SqlConnection cnn = new SqlConnection(cadena);
                string consulta = "select * from TBL_USUARIO";
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

        //Parametro: usuario, nombre, apellido, contraseña, rol
        //Retorno Bool
        //Inserta un usuario en la tabla
        public static bool Func_InsertarUsuario(string user, string name, string ape, string pass, string rol)
        {
            DataTable Tabla = new DataTable();
            try
            {
                SqlConnection cnn = new SqlConnection(cadena);
                string consulta = "INSERT INTO TBL_USUARIO VALUES ('" + user + "','" + name + "','" + ape + "','" + pass + "','" + rol + "');";
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
        public static bool Func_eliminaUsuario(int id)
        {
            DataTable Tabla = new DataTable();
            try
            {
                SqlConnection cnn = new SqlConnection(cadena);
                string consulta = "DELETE FROM TBL_USUARIO WHERE ID_USUARIO=" + id + ";";
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
        public static bool Func_EditarUsuario(string user, string name, string ape, string pass, string rol, int id)
        {
            DataTable Tabla = new DataTable();
            try
            {
                SqlConnection cnn = new SqlConnection(cadena);
                string consulta = "UPDATE TBL_USUARIO SET ALIAS_USUARIO='"+user+"' , NOMBRE_USUARIO='"+name+"', APELLIDO_USUARIO='"+ape+"',PASSWORD_USUARIO='"+pass+"', ROL_USUARIO='"+rol+"' WHERE ID_USUARIO="+id;
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

            /// Encripta una cadena
            public static string EncriptarContraseña( string _cadenaAencriptar)
            {
                string result = string.Empty;
                byte[] encryted = System.Text.Encoding.Unicode.GetBytes(_cadenaAencriptar);
                result = Convert.ToBase64String(encryted);
                return result;
            }

            public static string DesEncriptarContraseña( string contraseñaEncriptada)
            {
            string result = string.Empty;
            byte[] decryted = Convert.FromBase64String(contraseñaEncriptada);
            result = System.Text.Encoding.Unicode.GetString(decryted);
            return result;
            }

        public static DataTable Func_TraerClientes()
        {
            DataTable Tabla = new DataTable();
            try
            {
                SqlConnection cnn = new SqlConnection(cadena);
                string consulta = "select * from TBL_CLIENTE";
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
        public static string Func_TraerPassAdmin()
        {
            string pass = "";
            try
            {
                SqlConnection cnn = new SqlConnection(cadena);
                string consulta = "select PASSWORD_USUARIO from TBL_USUARIO where ROL_USUARIO = 'Administrador'";
                SqlDataAdapter adap = new SqlDataAdapter(consulta, cnn);
                DataTable tabla = new DataTable();
                adap.Fill(tabla);
                pass = tabla.Rows[0]["PASSWORD_USUARIO"].ToString();
                return pass;
            }
            catch (Exception e)
            {
                excepcion = e.Message;
                return pass;
            }

        }
    }
}
