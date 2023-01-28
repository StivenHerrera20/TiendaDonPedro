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
    public partial class FrmUsuarios : Form
    {
        public FrmUsuarios()
        {
            InitializeComponent();
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
            //Validar los datos 
            if (TxtContraseña.Text.Length>0 && TxtApellidos.Text.Length>0 && TxtNombres.Text.Length > 0 && TxtUsuario.Text.Length > 0 && CbxRol.Text.Length >0)
            {
                if (TxtIDUsuario.Text.Length == 0)
                {
                    string encriptar = ClaseUsuarios.EncriptarContraseña(TxtContraseña.Text);
                    //Inserta
                    if (ClaseUsuarios.Func_InsertarUsuario(TxtUsuario.Text, TxtNombres.Text, TxtApellidos.Text,encriptar, CbxRol.Text))
                    {
                        MessageBox.Show("Usuario agregado", "Felicitaciones", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.Close();

                    }
                    else
                    {
                        MessageBox.Show("Error: " + ClaseUsuarios.excepcion, "Error!!!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    string encriptar = ClaseUsuarios.EncriptarContraseña(TxtContraseña.Text);
                    //Editar
                    if (ClaseUsuarios.Func_EditarUsuario(TxtUsuario.Text,TxtNombres.Text,TxtApellidos.Text,encriptar, CbxRol.Text, Convert.ToInt32(TxtIDUsuario.Text)))
                    {
                        MessageBox.Show("Usuario Actualizado", "Felicitaciones", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        TxtIDUsuario.Text = "";
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Error: " + ClaseUsuarios.excepcion, "Error!!!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                
            }
            else
            {
                MessageBox.Show("Debes ingresar todos los datos solicitados", "Error!!!",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
        }
    }
}
