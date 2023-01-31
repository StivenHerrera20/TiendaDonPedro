using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace COMPLETE_FLAT_UI
{
    public partial class FrmProductos : Form
    {
        public FrmProductos()
        {
            InitializeComponent();
        }

        private void CbxCategoria_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void TxtPrecio_KeyPress(object sender, KeyPressEventArgs e)
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

        private void BtnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void BtnCerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void BtnGuardar_Click(object sender, EventArgs e)
        {
            if (TxtNombre.Text.Length> 0 && TxtPrecio.Text.Length > 0 && TxtIVA.Text.Length >0 && TxtStock.Text.Length > 0 && CbxCategoria.Text.Length > 0)
            {
                if(txtId.Text.Length == 0)
                {
                    //insertar
                    string[] arreglo = new string[2];
                    arreglo = CbxCategoria.Text.Split('-');
                    int idcat = Convert.ToInt32(arreglo[0]);
                    //insert del producto
                    if (ClaseProductos.Func_InsertarProductor(TxtNombre.Text, Convert.ToInt32(TxtPrecio.Text), Convert.ToInt32(TxtStock.Text), Convert.ToDouble(TxtIVA.Text), idcat))
                    {
                        MessageBox.Show("Producto Agregado", "Felicitaciones!!!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Error: " + ClaseProductos.excepcion, "Error!!!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    //Editar
                    string[] arreglo = new string[2];
                    arreglo = CbxCategoria.Text.Split('-');
                    int idcat = Convert.ToInt32(arreglo[0]);
                    //Editar del producto
                    if (ClaseProductos.Func_EditarProducto(TxtNombre.Text, Convert.ToInt32(TxtPrecio.Text), Convert.ToInt32(TxtStock.Text), Convert.ToDouble(TxtIVA.Text), idcat, Convert.ToInt32(txtId.Text)))
                    {
                        MessageBox.Show("Producto Editado", "Felicitaciones!!!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Error: " + ClaseProductos.excepcion, "Error!!!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                
            }
            else
            {
                MessageBox.Show("No puedes enviar datos vacios","Error!!!",MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
