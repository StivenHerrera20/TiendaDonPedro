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
    public partial class FrmLogin : Form
    {
        public FrmLogin()
        {
            InitializeComponent();
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (TxtContraseña.PasswordChar == '*')
            {
                TxtContraseña.PasswordChar = '\0';
            }
            else
            {
                TxtContraseña.PasswordChar = '*';
            }
            TxtContraseña.Focus();
        }

        private void BtnSalir_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("¿Está seguro de cerrar?", "Alerta¡¡", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void BtnCerrar_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("¿Está seguro de cerrar?", "Alerta¡¡", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void BtnIniciar_Click(object sender, EventArgs e)
        {
            //validacion de datos 
            if (TxtUsuario.Text.Length>0 && TxtContraseña.Text.Length > 0)
            {
                DataTable tabla = new DataTable();
                tabla = ClaseLogin.Func_TraerUsuario(TxtUsuario.Text);
                if (tabla.Rows.Count > 0)
                {
                    //Trae algun usuario
                    string passdb =ClaseUsuarios.DesEncriptarContraseña(tabla.Rows[0]["PASSWORD_USUARIO"].ToString());
                    //comparo los 2 password
                    if (passdb ==TxtContraseña.Text)
                    {
                        //llamar al menu
                        FrmMenuPrincipal f = new FrmMenuPrincipal();
                        f.Lbl_Usuario_Nombre.Text = tabla.Rows[0]["NOMBRE_USUARIO"].ToString();
                        f.Lbl_Usuario_Apellido.Text = tabla.Rows[0]["APELLIDO_USUARIO"].ToString();
                        if (tabla.Rows[0]["ROL_USUARIO"].ToString()== "Administrador")
                        {
                            f.BtnUsuarios.Enabled = true;
                        }
                        this.Hide();
                        f.ShowDialog();
                    }
                    else
                    {
                        MessageBox.Show("Contraseña incorrecta!!!", "Error!!!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Usuario no existe","Error!!!",MessageBoxButtons.OK,MessageBoxIcon.Error);
                }
                
            }
            else
            {
                MessageBox.Show("Ingrese usuario o contraseña (No pueden haber campos vacios)","Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
        }

        private void FrmLogin_Activated(object sender, EventArgs e)
        {
            TxtUsuario.Focus();
        }
    }
}
