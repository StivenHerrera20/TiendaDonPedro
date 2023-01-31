using System;
using System.Collections;
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

            if (LblTitulo.Text == "Lista de Categorias")
            {
                FrmCategorias f = new FrmCategorias();
                f.txtIdCategoria.Text = DGVDatos.CurrentRow.Cells["ID_CATEGORIA"].Value.ToString();
                f.TxtDescripcion.Text = DGVDatos.CurrentRow.Cells["DES_CATEGORIA"].Value.ToString();
                f.ShowDialog();
            }

            if (LblTitulo.Text == "Lista de Productos")
            {
                FrmProductos f = new FrmProductos();
                f.txtId.Text = DGVDatos.CurrentRow.Cells["ID_PRODUCTO"].Value.ToString();
                f.TxtNombre.Text = DGVDatos.CurrentRow.Cells["NOMBRE_PRODUCTO"].Value.ToString();
                f.TxtPrecio.Text = DGVDatos.CurrentRow.Cells["PRECIO_PRODUCTO"].Value.ToString();
                f.TxtStock.Text = DGVDatos.CurrentRow.Cells["STOCK_PRODUCTO"].Value.ToString();
                f.TxtIVA.Text = DGVDatos.CurrentRow.Cells["IVA_PRODUCTO"].Value.ToString();
                //Lleno el combobox con las categorias
                DataTable tabla = ClaseCategoria.Func_TraerTodasCategorias();
                f.CbxCategoria.Items.Clear();
                for (int i = 0; i < tabla.Rows.Count; i++)
                {
                    f.CbxCategoria.Items.Add(tabla.Rows[i]["ID_CATEGORIA"].ToString() + "-" + tabla.Rows[i]["DES_CATEGORIA"].ToString());
                }

                f.ShowDialog();
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
            if (LblTitulo.Text == "Lista de Categorias")
            {
                FrmCategorias f = new FrmCategorias();
                f.ShowDialog();
            }
            if (LblTitulo.Text == "Lista de Productos")
            {
                FrmProductos f = new FrmProductos();
                //Lleno el combobox con las categorias
                DataTable tabla = ClaseCategoria.Func_TraerTodasCategorias(); 
                f.CbxCategoria.Items.Clear(); 
                for (int i = 0;i < tabla.Rows.Count; i++)
                {
                    f.CbxCategoria.Items.Add(tabla.Rows[i]["ID_CATEGORIA"].ToString()+"-"+ tabla.Rows[i]["DES_CATEGORIA"].ToString());
                }
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
            if (LblTitulo.Text == "Lista de Categorias")
            {
                string idEliminar = DGVDatos.CurrentRow.Cells["ID_CATEGORIA"].Value.ToString();
                DialogResult rpta = new DialogResult();
                rpta = MessageBox.Show("Desea eliminar el ID: " + idEliminar + "\n SE ELIMINARAN LOS PRODUCTOS DE ESTA CATEGORIA", "Advertencia!!!", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if (rpta == DialogResult.OK)
                {
                    if (ClaseCategoria.Func_eliminaCategoria(Convert.ToInt32(idEliminar)))
                    {
                        MessageBox.Show("Categoria eliminada", "Felicitaciones!!!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Error: " + ClaseCategoria.excepcion, "ERROR!!!", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            if (LblTitulo.Text == "Lista de Categorias")
            {
                //Mostrar los usuarios en DGVDatos
                this.DGVDatos.DataSource = ClaseCategoria.Func_TraerTodasCategorias();

            }
            if (LblTitulo.Text == "Lista de Productos")
            {
                //Mostrar los usuarios en DGVDatos
                this.DGVDatos.DataSource = ClaseProductos.Func_TraerTodosProductos();
            }
            

        }

        private void DGVDatos_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void LblTitulo_Click(object sender, EventArgs e)
        {

        }
    }
}
