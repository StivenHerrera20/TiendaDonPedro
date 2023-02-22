using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace COMPLETE_FLAT_UI
{
    public partial class FrmClientes : Form
    {
        public FrmClientes()
        {
            InitializeComponent();
        }

        private void BtnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private Boolean email_bien_escrito(String email)
        {
            String expresion;
            expresion = "\\w+([-+.']\\w+)*@\\w+([-.]\\w+)*\\.\\w+([-.]\\w+)*";
            if (Regex.IsMatch(email, expresion))
            {
                if (Regex.Replace(email, expresion, String.Empty).Length == 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        private void TxtCorreo_Validated(object sender, EventArgs e)
        {
            if (!email_bien_escrito(TxtCorreo.Text))
            {
                MessageBox.Show("Verifique Correo Electronico", "Error!!!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                TxtCorreo.Focus();
            }

        }

        private void BtnCerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void BtnGuardar_Click(object sender, EventArgs e)
        {
            //Validar los datos 
            if (TxtNombre.Text.Length > 0 && TxtDireccion.Text.Length > 0 && TxtCorreo.Text.Length > 0 )
            {
                if (txtIdCliente.Text.Length == 0)
                {
                    //Inserta
                    if (ClaseClientes.Func_InsertarCliente(TxtNombre.Text,TxtDireccion.Text,TxtCorreo.Text))
                    {
                        MessageBox.Show("Cliente agregado", "Felicitaciones", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.Close();

                    }
                    else
                    {
                        MessageBox.Show("Error: " + ClaseClientes.excepcion, "Error!!!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                { 
                    //Editar
                    if (ClaseClientes.Func_EditarCliente(TxtNombre.Text, TxtDireccion.Text, TxtCorreo.Text, Convert.ToInt32(txtIdCliente.Text)))
                    {
                        MessageBox.Show("Cliente Actualizado", "Felicitaciones", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtIdCliente.Text = "";
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Error: " + ClaseClientes.excepcion, "Error!!!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }

            }
            else
            {
                MessageBox.Show("Debes ingresar todos los datos solicitados", "Error!!!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
