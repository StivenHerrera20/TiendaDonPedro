using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using COMPLETE_FLAT_UI.Properties;

namespace COMPLETE_FLAT_UI
{
    public partial class FrmFactEnc : Form
    {
        //Calcular el consecutivo maximo del fact enc
        public long consecfe = 0;

        public FrmFactEnc()
        {
            InitializeComponent();
        }

        private void FrmFactEnc_Load(object sender, EventArgs e)
        {

        }

        private void BtnCerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void BtnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void TxtIdent_KeyPress(object sender, KeyPressEventArgs e)
        {
            //Para obligar a que sólo se introduzcan números
            if (Char.IsDigit(e.KeyChar))
            {
                e.Handled = false;
            }
            else
              if (Char.IsControl(e.KeyChar)) //permitir teclas de control como retroceso
            {
                e.Handled = false;
            }
            else
            {
                //el resto de teclas pulsadas se desactivan
                e.Handled = true;
            }
        }

        private void TxtIdent_Validated(object sender, EventArgs e)
        {
            //Buscar en la base de datos
            DataTable tabla= new DataTable();
            tabla = ClaseFactEnc.Func_TraerUnCliente(Convert.ToInt64(TxtIdent.Text));
            if (tabla.Rows.Count > 0)
            {
                string name = tabla.Rows[0]["NOMBRE_CLIENTE"].ToString();
                MessageBox.Show ("Cliente: "+name, "Informacion!!!",MessageBoxButtons.OK, MessageBoxIcon.Warning);              


            }
            else
            {
                DialogResult rpta = new DialogResult();
                rpta = MessageBox.Show("Cliente no existe!!! \n Desea crear un cliente nuevo?","Advertencia!!!",MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if (rpta == DialogResult.OK)
                {
                    this.Hide();
                    FrmClientes f = new FrmClientes();
                    f.ShowDialog();
                }
            }
        }

        private void BtnAgregar_Click(object sender, EventArgs e)
        {
            //Si hay un cliente
            if (TxtIdent.Text != string.Empty)
            {
                if (ClaseFactDetcs.DTDetalle.Columns.Count == 0)
                {
                    //inserto las columnas
                    DataColumn ColId = ClaseFactDetcs.DTDetalle.Columns.Add("ID", typeof(string));
                    DataColumn ColName= ClaseFactDetcs.DTDetalle.Columns.Add("Nombre", typeof(string));
                    DataColumn ColVlrUnit = ClaseFactDetcs.DTDetalle.Columns.Add("Precio", typeof(string));
                    DataColumn ColCant= ClaseFactDetcs.DTDetalle.Columns.Add("Cantidad", typeof(string));
                    DataColumn ColIva= ClaseFactDetcs.DTDetalle.Columns.Add("IVA", typeof(string));
                    DataColumn ColTotal= ClaseFactDetcs.DTDetalle.Columns.Add("Total", typeof(string));
                }
                //2. limpio el datatable de detalle
                ClaseFactDetcs.DTDetalle.Clear();
                //Llamo el formulario detalle
                FrmFactDet f = new FrmFactDet();
                f.ShowDialog();
            }
            else
            {
                MessageBox.Show("Debes ingresar un cliente", "ERROR!!!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void LblTituloFormCategoria_Click(object sender, EventArgs e)
        {

        }

        private void FrmFactEnc_Activated(object sender, EventArgs e)
        {
            //Pregunto si el DT detalle esta lleno o vacio
            if (ClaseFactDetcs.DTDetalle.Rows.Count > 0)
            {
                //Leo el DT
                string id = ClaseFactDetcs.DTDetalle.Rows[0]["ID"].ToString();
                string name = ClaseFactDetcs.DTDetalle.Rows[0]["Nombre"].ToString();
                string precio = ClaseFactDetcs.DTDetalle.Rows[0]["Precio"].ToString();
                string cantidad = ClaseFactDetcs.DTDetalle.Rows[0]["Cantidad"].ToString();
                string IVA = ClaseFactDetcs.DTDetalle.Rows[0]["IVA"].ToString();
                string Total = ClaseFactDetcs.DTDetalle.Rows[0]["Total"].ToString();
                //agrego la fila al datagriedview
                int filasdgw = DgvDetalle.Rows.Count;
                DgvDetalle.Rows.Insert(filasdgw,id,name,precio,cantidad,IVA,Total);
                //Limpiar el DT
                ClaseFactDetcs.DTDetalle.Clear();
                //Recorrer el DGV
                long total = 0;
                long subtotal = 0;
                foreach (DataGridViewRow row in DgvDetalle.Rows)
                {   
                    total = total + Convert.ToInt64(row.Cells["VlrTotal"].Value.ToString());
                    subtotal = subtotal + (Convert.ToInt64(row.Cells["VlrUnit"].Value.ToString()) * Convert.ToInt64(row.Cells["cantidadProd"].Value.ToString()));
                }
                TxtTotal.Text = total.ToString();
                TxtSubtotal.Text = subtotal.ToString();
                txtIva.Text = (total - subtotal).ToString();
            }
        }

        private void DgvDetalle_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void BtnEliminar_Click(object sender, EventArgs e)
        {
            string nomprod = DgvDetalle.Rows[DgvDetalle.CurrentRow.Index].Cells["NomProd"].Value.ToString();
            DialogResult rpta = new DialogResult();
            rpta = MessageBox.Show("Desea eliminar el producto: "+nomprod,"Advertencia!!!",MessageBoxButtons.OKCancel,MessageBoxIcon.Question);
            if (rpta == DialogResult.OK)
            {
                //Llamo input box y solicito la contraseña del admin
                string texto = Microsoft.VisualBasic.Interaction.InputBox("Ingrese la contraseña", "Advertencia!!!", "");
                //Traer contraseña del administrador
                string passencrip = ClaseUsuarios.Func_TraerPassAdmin();
                //desencriptar la contraseña del administrador
                string passdesc = ClaseUsuarios.DesEncriptarContraseña(passencrip);
                if (texto==passdesc)
                {
                    //Elimina la fila seleccionada
                    DgvDetalle.Rows.RemoveAt(DgvDetalle.CurrentRow.Index);
                    long total = 0;
                    long subtotal = 0;
                    foreach (DataGridViewRow row in DgvDetalle.Rows)
                    {
                        total = total + Convert.ToInt64(row.Cells["VlrTotal"].Value.ToString());
                        subtotal = subtotal + (Convert.ToInt64(row.Cells["VlrUnit"].Value.ToString()) * Convert.ToInt64(row.Cells["cantidadProd"].Value.ToString()));
                    }
                    if(DgvDetalle.Rows.Count> 0)
                    {
                        TxtTotal.Text = total.ToString();
                        TxtSubtotal.Text = subtotal.ToString();
                        txtIva.Text = (total - subtotal).ToString();
                    }
                    else
                    {
                        TxtTotal.Text = "";
                        TxtSubtotal.Text = "";
                        txtIva.Text = "";
                    }                    
                }
                else
                {
                    MessageBox.Show("Contraseña incorrecta!!!","Error",MessageBoxButtons.OK, MessageBoxIcon.Error);
                }                
            }
        }
        private void BtnGuardar_Click(object sender, EventArgs e)
        {
           consecfe=  ClaseFactEnc.Func_TraerMaxId();

            //Valido si hay cliente y productos
            if (DgvDetalle.Rows.Count>0 && TxtIdent.Text.Length > 0) 
            {
                

                foreach (DataGridViewRow row in DgvDetalle.Rows)
                 {
                    //insert en el detalle
                    long idprod = Convert.ToInt64(row.Cells["IDProd"].Value.ToString());
                    long cant = Convert.ToInt64(row.Cells["cantidadProd"].Value.ToString());
                    long precio = Convert.ToInt64(row.Cells["VlrUnit"].Value.ToString());
                    long vlriva = Convert.ToInt64(row.Cells["ivaProd"].Value.ToString());
                    ClaseFactDetcs.Func_InsertarDetalle(consecfe,idprod,cant,precio,vlriva);
                    //Actualizar stock
                    ClaseFactEnc.Func_EditarStock(idprod, cant);

                 }
                //Actualiza el encabezado
                ClaseFactEnc.Func_EditarEncabezado(Convert.ToInt64(TxtIdent.Text), Convert.ToInt64(TxtTotal.Text),"Activo", consecfe);
                MessageBox.Show("Factura Guardada","Felicitaciones",MessageBoxButtons.OK, MessageBoxIcon.Information);
                //Imprime factura
                if (ImprimirFactura.PrinterSettings.IsValid == true)
                {
                    StandardPrintController imp = new StandardPrintController();
                    ImprimirFactura.PrintController = imp;
                    ImprimirFactura.Print();
                }

                this.Close();
            }
            else
            {
                MessageBox.Show("Verifique Cliente y Productos","Error!!!",MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ImprimirFactura_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            //DataTable de la factura
            DataTable DTFactura = new DataTable();
            DTFactura = ClaseFactEnc.Func_TraerFactura(consecfe);

            //Logo tienda don pedro
            Bitmap image = new Bitmap("C:\\Users\\SENA\\Documents\\ProyectoVS_TiendaDonPedro\\COMPLETE_FLAT_UI\\Resources\\Tienda256.png");
            e.Graphics.DrawImage(image, 10, 0, 200, 200);
            //Cambiar fuente
            Font printFont = new Font("Courier New", 10, FontStyle.Bold);
            //(Texto a imprimir, Color con el que lo quiere imprimir, posicion x, posicion y)
            e.Graphics.DrawString("LA TIENDA DE DON PEDRO", printFont, Brushes.Black, 5, 200);
            e.Graphics.DrawString("Direccion: Calle 40 No.3-23 Cartago (Valle)", printFont, Brushes.Black, 5, 215);
            e.Graphics.DrawString("Telefono: 2114565-2114567", printFont, Brushes.Black, 5, 232);
            e.Graphics.DrawString("Consecutivo: " + consecfe, printFont, Brushes.Black, 5, 247);
            string fecha = DTFactura.Rows[0]["FECHA_FACTURA"].ToString();
            e.Graphics.DrawString("Fecha: " + fecha, printFont, Brushes.Black, 5, 262);
            //Datos del cliente
            e.Graphics.DrawString("Identificacion: "+ TxtIdent.Text , printFont, Brushes.Black, 5, 277);
            string nombre = DTFactura.Rows[0]["NOMBRE_CLIENTE"].ToString();
            e.Graphics.DrawString("Cliente: "+nombre, printFont, Brushes.Black, 5, 292);
            //Detalle de los productos
            e.Graphics.DrawString("---------------------------------------------" , printFont, Brushes.Black, 5, 305);
            e.Graphics.DrawString("CODIGO    NOMBRE    PRECIO   CANT   TOTAL",printFont, Brushes.Black, 5, 320);
            e.Graphics.DrawString("---------------------------------------------", printFont, Brushes.Black, 5, 335);
            long y = 350;
            long subTotalImp = 0;
            long ivaImp = 0;
            long totalImp = 0;
            for (int i = 0; i < DTFactura.Rows.Count;i++)
            {
                string cod = DTFactura.Rows[i]["ID_PRODUCTO"].ToString();
                string name = DTFactura.Rows[i]["NOMBRE_PRODUCTO"].ToString();
                string precio = DTFactura.Rows[i]["PRECIOUNIT"].ToString();
                string cant = DTFactura.Rows[i]["CANTIDAD"].ToString();
                long subTotal = Convert.ToInt64(precio)*Convert.ToInt64(cant);
                subTotalImp = subTotalImp + subTotal;
                long iva = (subTotal * Convert.ToInt64(DTFactura.Rows[i]["VALORIVA"].ToString()))/100;
                ivaImp = ivaImp + iva;
                long total = subTotal + iva;
                totalImp= totalImp + total;
                //Codigo a 6
                if(cod.Length>6)
                {
                    cod= cod.Substring(0,6);
                }
                if(name.Length>13){
                    name= name.Substring(0,13);
                    
                }
                e.Graphics.DrawString(cod,printFont, Brushes.Black, 14, y);
                e.Graphics.DrawString(name, printFont, Brushes.Black, 80, y);
                e.Graphics.DrawString(Convert.ToInt64(precio).ToString("N0"), printFont, Brushes.Black, 185, y);
                e.Graphics.DrawString(cant, printFont, Brushes.Black, 260, y);
                e.Graphics.DrawString(Convert.ToInt64(total).ToString("N0"), printFont, Brushes.Black, 315, y);
                y = y + 15;
            }
            //Indicamos que hemenos llegado al final de la pagina
            e.Graphics.DrawString("---------------------------------------------", printFont, Brushes.Black, 5, y);
            e.Graphics.DrawString("Subtotal: "+Convert.ToInt64(subTotalImp).ToString("N0"), printFont, Brushes.Black, 230, y+15);
            e.Graphics.DrawString("  IVA:    " + Convert.ToInt64(ivaImp).ToString("N0"), printFont, Brushes.Black, 230, y + 30);
            e.Graphics.DrawString(" Total:   " + Convert.ToInt64(totalImp).ToString("N0"), printFont, Brushes.Black, 230, y + 45);
            e.Graphics.DrawString("Gracias por tu compra !!!", printFont, Brushes.Black, 80, y + 70);
            e.HasMorePages = false;
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            if ( ImprimirFactura.PrinterSettings.IsValid==true )
            {
                StandardPrintController imp = new StandardPrintController();
                ImprimirFactura.PrintController = imp;
                ImprimirFactura.Print();
            }
        }
    }
}
