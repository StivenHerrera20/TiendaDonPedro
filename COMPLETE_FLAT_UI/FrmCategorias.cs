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
    public partial class FrmCategorias : Form
    {
        public FrmCategorias()
        {
            InitializeComponent();
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
            //Validar los datos 
            if (TxtDescripcion.Text.Length > 0 )
            {
                if (txtIdCategoria.Text.Length == 0)
                {
                    //Inserta
                    if (ClaseCategoria.Func_InsertarCategoria(TxtDescripcion.Text))
                    {
                        MessageBox.Show("Categoria agregada", "Felicitaciones", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.Close();

                    }
                    else
                    {
                        MessageBox.Show("Error: " + ClaseCategoria.excepcion, "Error!!!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    //Editar
                    if (ClaseCategoria.Func_EditarCategoria(Convert.ToInt32(txtIdCategoria.Text), TxtDescripcion.Text ))
                    {
                        MessageBox.Show("Categoria Actualizada", "Felicitaciones", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtIdCategoria.Text = "";
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Error: " + ClaseCategoria.excepcion, "Error!!!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }

            }
            else
            {
                MessageBox.Show("Debes ingresar todos los datos solicitados", "Error!!!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
