using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace COMPLETE_FLAT_UI
{
    public class ClaseLogin
    {
        public static string cadena = "Server=(local)\\SQLEXPRESS;Database=BD_TiendaDonPedro;User Id=Adso2501875;Password=12345;";
        public static string excepcion = "";


        //Parametro: usuario
        //Retorno tabla
        //Trae Toda la informacion del usuario
        public static DataTable Func_TraerUsuario(string username)
        {
            DataTable Tabla = new DataTable();
            try
            {
                SqlConnection cnn = new SqlConnection(cadena);
                string consulta = "select NOMBRE_USUARIO,APELLIDO_USUARIO,PASSWORD_USUARIO,ROL_USUARIO from TBL_USUARIO where ALIAS_USUARIO ='"+username+"';";
                SqlDataAdapter adap = new SqlDataAdapter(consulta, cnn);
                adap.Fill(Tabla);
                return Tabla;
            }
            catch (Exception e) 
            { 
                excepcion= e.Message;
                return Tabla;
            }
            
        }

        
    }
}
