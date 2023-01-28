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
    public partial class FrmLista : Form
    {
        public FrmLista()
        {
            InitializeComponent();
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

  
        

        private void BtnCerrar_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (LblTitulo.Text == "Lista de Usuarios")
            {
                FrmUsuarios f = new FrmUsuarios();
                f.TxtIDUsuario.Text = DGVDatos.CurrentRow.Cells["ID_USUARIO"].Value.ToString();
                f.TxtUsuario.Text = DGVDatos.CurrentRow.Cells["ALIAS_USUARIO"].Value.ToString();
                f.TxtNombres.Text = DGVDatos.CurrentRow.Cells["NOMBRE_USUARIO"].Value.ToString();
                f.TxtContraseña.Text = ClaseUsuarios.DesEncriptarContraseña(DGVDatos.CurrentRow.Cells["PASSWORD_USUARIO"].Value.ToString());
                f.CbxRol.Text = DGVDatos.CurrentRow.Cells["ROL_USUARIO"].Value.ToString();
                f.ShowDialog();
            }
            if (LblTitulo.Text == "Lista de Clientes")
            {
                FrmClientes f = new FrmClientes();
            }
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            if (LblTitulo.Text == "Lista de Usuarios")
            {
                FrmUsuarios f = new FrmUsuarios();
                f.ShowDialog();
            }
            if (LblTitulo.Text == "Lista de Clientes")
            {
                FrmClientes f = new FrmClientes();
                f.ShowDialog();
            }

        }

        private void FrmLista_Load(object sender, EventArgs e)
        {
           
        }

        private void FrmLista_Activated(object sender, EventArgs e)
        {
            
        }

        private void BtnEliminar_Click(object sender, EventArgs e)
        {
            if (LblTitulo.Text == "Lista de Usuarios")
            {
                string idEliminar = DGVDatos.CurrentRow.Cells["ID_USUARIO"].Value.ToString();
                DialogResult rpta = new DialogResult();
                rpta = MessageBox.Show ("Desea eliminar el ID: "+idEliminar,"Advertencia!!!", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if (rpta ==DialogResult.OK)
                {
                    if (ClaseUsuarios.Func_eliminaUsuario(Convert.ToInt32(idEliminar)))
                    {
                        MessageBox.Show("Usuario eliminado", "Felicitaciones!!!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Error: " + ClaseUsuarios.excepcion, "ERROR!!!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    
                }

            }
            if (LblTitulo.Text == "Lista de Clientes")
            {
                string idEliminar = DGVDatos.CurrentRow.Cells["ID_CLIENTE"].Value.ToString();
                DialogResult rpta = new DialogResult();
                rpta = MessageBox.Show("Desea eliminar el ID: " + idEliminar, "Advertencia!!!", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if (rpta == DialogResult.OK)
                {
                    if (ClaseUsuarios.Func_eliminaCliente(Convert.ToInt32(idEliminar)))
                    {
                        MessageBox.Show("Cliente eliminado", "Felicitaciones!!!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Error: " + ClaseUsuarios.excepcion, "ERROR!!!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                }

            }
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            if (LblTitulo.Text== "Lista de Usuarios")
            {
                //Mostrar los usuarios en DGVDatos
                this.DGVDatos.DataSource = ClaseUsuarios.Func_TraerTodosUsuario();
                
            }

            if (LblTitulo.Text == "Lista de Clientes")
            {
                //Mostrar los usuarios en DGVDatos
                this.DGVDatos.DataSource = ClaseUsuarios.Func_TraerClientes();

            }

        }
    }
}
