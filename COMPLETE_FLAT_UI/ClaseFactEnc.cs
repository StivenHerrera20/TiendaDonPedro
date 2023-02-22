using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace COMPLETE_FLAT_UI
{
    public class ClaseFactEnc
    {
        public static string cadena = "Server=(local)\\SQLEXPRESS;Database=BD_TiendaDonPedro;User Id=Adso2501875;Password=12345;";
        public static string excepcion = "";


        //Parametro: Ninguno
        //Retorno tabla
        //Trae Toda la informacion de Un Cliente
        public static DataTable Func_TraerUnCliente(long id)
        {
            DataTable Tabla = new DataTable();
            try
            {
                SqlConnection cnn = new SqlConnection(cadena);
                string consulta = "select * from TBL_CLIENTE Where ID_CLIENTE=" + id;
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
        public static bool Func_InsertarCliente(string name, string dir, string email)
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

        public static long Func_TraerMaxId()
        {
            DataTable Tabla = new DataTable();
            DataTable Tabla2 = new DataTable();
            long maxid = 0;
            try
            {
                SqlConnection cnn = new SqlConnection(cadena);
                string consulta1 = "Select Max(ID_FACTURA) AS Consec From TBL_FACTURAVENTA";
                string consulta2 = "Insert into TBL_FACTURAVENTA (FECHA_FACTURA) Values ('"+DateTime.Now.ToString("yyyyMMdd hh:mm:ss")+"')";
                SqlDataAdapter adap1 = new SqlDataAdapter(consulta1, cnn);
                adap1.Fill(Tabla);
                //Pregunto si es nulo 
                if(Tabla.Rows[0]["Consec"].ToString()== DBNull.Value.ToString())
                {
                    maxid = 1;
                }
                else
                {
                    maxid = Convert.ToInt64(Tabla.Rows[0]["Consec"].ToString());
                }
               
                bool insertado = false;
                while (insertado==false) 
                {
                    
                    try
                    {
                        SqlDataAdapter adap2 = new SqlDataAdapter(consulta2, cnn);
                        adap2.Fill(Tabla2);
                        insertado= true;
                    }
                    catch
                    {
                        insertado= false;
                        maxid++;

                    }
                    
                } 

                return maxid;
            }
            catch (Exception e)
            {
                excepcion = e.Message;
                return maxid;
            }

        }


        public static DataTable Func_EditarEncabezado(long idCliente, long total, string estado, long idFactura)
        {
            
            try
            {
                SqlConnection cnn = new SqlConnection(cadena);
                string consulta = "UPDATE TBL_FACTURAVENTA SET FECHA_FACTURA='" + DateTime.Now.ToString("yyyyMMdd hh:mm:ss") + "' , ID_CLIENTE=" + idCliente + " , TOTAL_FACTURA='"+total+"', ESTADO_FACTURA='"+estado+"' WHERE ID_FACTURA=" + idFactura;
                SqlDataAdapter adap = new SqlDataAdapter(consulta, cnn);
                DataTable Tabla = new DataTable();
                adap.Fill(Tabla);
                return Tabla;
            }
            catch (Exception e)
            {
                excepcion = e.Message;
                return null;
            }

        }

        public static DataTable Func_EditarStock(long cantidad, long id)
        {
            try
            {
                SqlConnection cnn = new SqlConnection(cadena);
                string consulta = "UPDATE TBL_PRODUCTO SER STOCK_PRODUCTO - "+cantidad+" WHERE ID_PRODUCTO="+id;
                SqlDataAdapter adap = new SqlDataAdapter(consulta, cnn);
                DataTable Tabla = new DataTable();
                adap.Fill(Tabla);
                return Tabla;
            }
            catch (Exception e)
            {
                excepcion = e.Message;
                return null;
            }

        }

        public static DataTable Func_TraerFactura( long id)
        {
            try
            {
                SqlConnection cnn = new SqlConnection(cadena);
                string consulta = "SELECT TBL_FACTURAVENTA.ID_FACTURA, TBL_FACTURAVENTA.FECHA_FACTURA,TBL_CLIENTE.NOMBRE_CLIENTE,TBL_FACTURAVENTA.TOTAL_FACTURA,TBL_DETALLEFACTURA.ID_PRODUCTO, TBL_PRODUCTO.NOMBRE_PRODUCTO,TBL_DETALLEFACTURA.PRECIOUNIT,TBL_DETALLEFACTURA.CANTIDAD,TBL_DETALLEFACTURA.VALORIVA FROM TBL_FACTURAVENTA inner join TBL_DETALLEFACTURA ON TBL_FACTURAVENTA.ID_FACTURA = TBL_DETALLEFACTURA.ID_FACTURA inner join TBL_CLIENTE on TBL_FACTURAVENTA.ID_CLIENTE=TBL_CLIENTE.ID_CLIENTE inner join TBL_PRODUCTO on TBL_DETALLEFACTURA.ID_PRODUCTO = TBL_PRODUCTO.ID_PRODUCTO WHERE TBL_FACTURAVENTA.ID_FACTURA="+id;
                SqlDataAdapter adap = new SqlDataAdapter(consulta, cnn);
                DataTable Tabla = new DataTable();
                adap.Fill(Tabla);
                return Tabla;
            }
            catch (Exception e)
            {
                excepcion = e.Message;
                return null;
            }

        }

    }
}
