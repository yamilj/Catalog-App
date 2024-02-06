using Dominio;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Dominio;
using Negocio;

namespace landing
{
    public partial class VerDetalle : Form
    {
        private Articulo articulo = null;
        public VerDetalle(Articulo articulo)
        {
            InitializeComponent();
            this.articulo = articulo;

        }

        private void VerDetalle_Load(object sender, EventArgs e)
        {


            try
            {

                txtCodigo.Text = articulo.Codigo;
                txtNombre.Text = articulo.Nombre;
                txtDescripcion.Text = articulo.Descripcion;
                txtMarca.Text = articulo.Marca.Descripcion;
                txtCategoria.Text = articulo.Categoria.Descripcion;
                txtPrecio.Text = articulo.Precio.ToString();


            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
