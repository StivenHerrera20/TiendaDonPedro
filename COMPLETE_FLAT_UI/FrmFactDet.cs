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
    public partial class FrmFactDet : Form
    {
        public FrmFactDet()
        {
            InitializeComponent();
        }

        private void label5_Click(object sender, EventArgs e)
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

        private void BtnGuardar_Click(object sender, EventArgs e)
        {
            
            //Validar
            if (txtCantidad.Text.Length >=1)
            {
                ClaseFactDetcs.DTDetalle.Rows.Add(TxtCodigo.Text, txtNombre.Text, txtPrecio.Text, txtCantidad.Text, txtIVA.Text, txtTotal.Text);
                this.Close();
            }
            else
            {
                MessageBox.Show("Debes ingresar una cantidad","Error!!!",MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
        }

        private void TxtCodigo_Validated(object sender, EventArgs e)
        {
            //Valido si esta vacio
            if (TxtCodigo.Text.Length == 0)
            { 
                MessageBox.Show("Ingrese un Producto","Error!!!",MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                DataTable tabla = new DataTable();
                tabla = ClaseFactDetcs.Func_TraerUnProd(Convert.ToInt64(TxtCodigo.Text));
                if (tabla.Rows.Count == 0) 
                {
                    MessageBox.Show("Producto no existe", "Error!!!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    txtNombre.Text = tabla.Rows[0]["NOMBRE_PRODUCTO"].ToString();
                    txtPrecio.Text = tabla.Rows[0]["PRECIO_PRODUCTO"].ToString();
                    txtIVA.Text = tabla.Rows[0]["IVA_PRODUCTO"].ToString();
                    txtCantidad.Focus();
                }
            }          
        }

        private void txtCantidad_Validated(object sender, EventArgs e)
        {
            
            
        }

        private void txtCantidad_TextChanged(object sender, EventArgs e)
        {
            if (txtCantidad.Text.Length == 0)
            {
                MessageBox.Show("Ingrese la cantidad", "ERROR!!!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                double total = 0;
                double precio = Convert.ToDouble(txtCantidad.Text) * Convert.ToDouble(txtPrecio.Text);
                double iva = Convert.ToDouble(txtIVA.Text) / 100;
                iva = precio * iva;
                total = precio + iva;
                txtTotal.Text = total.ToString();
            }
        }
    }
}
