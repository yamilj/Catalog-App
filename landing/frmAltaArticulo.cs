using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Dominio;
using Negocio;  

namespace landing
{
    public partial class frmAltaArticulo : Form
    {
        private Articulo articulo = null;
        
        public frmAltaArticulo()
        {
            InitializeComponent();
        }

        public frmAltaArticulo(Articulo articulo)
        {
            InitializeComponent();
            this.articulo = articulo;
            Text = "Modificar Articulo";
        }


        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            
            ArticuloNegocio negocio = new ArticuloNegocio();
            try
            {
                if(articulo == null)
                    articulo = new Articulo();

                if (validarCarga())
                {
                    return;
                }


                articulo.Codigo = txtCodigo.Text;
                articulo.Nombre = txtNombre.Text;
                articulo.Descripcion = txtDescripcion.Text;
                articulo.UrlImagen = txtUrlImagen.Text;
                articulo.Marca = (Marca) cbxMarca.SelectedItem;
                articulo.Categoria = (Categoria) cbxCategoria.SelectedItem;
                articulo.Precio = decimal.Parse(txtPrecio.Text);

                if (articulo.Id != 0){
                    negocio.modificiar(articulo);
                    MessageBox.Show("Modificado exitosamente.");
                }
                else{
                    negocio.agregar(articulo);
                    MessageBox.Show("Agregado exitosamente.");
                }

                //No hago la opcion de archivo local, ya que creo que al no manejar las carpetas en destino podria romperse


                Close();

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }
        }

        private void frmAltaArticulo_Load(object sender, EventArgs e)
        {
            MarcaNegocio marcaNegocio = new MarcaNegocio();
            CategoriaNegocio categorianegocio = new CategoriaNegocio();
            try
            {
                cbxMarca.DataSource = marcaNegocio.listar();
                cbxMarca.ValueMember = "Id";
                cbxMarca.DisplayMember = "Descripcion";
                cbxCategoria.DataSource = categorianegocio.listar();
                cbxCategoria.ValueMember = "Id";
                cbxCategoria.DisplayMember = "Descripcion";


                if (articulo!=null)
                {
                    txtCodigo.Text = articulo.Codigo;
                    txtNombre.Text = articulo.Nombre;
                    txtDescripcion.Text = articulo.Descripcion;
                    cbxMarca.SelectedValue = articulo.Marca.Id;
                    cbxCategoria.SelectedValue = articulo.Categoria.Id;
                    txtUrlImagen.Text = articulo.UrlImagen;
                    cargarImagen(articulo.UrlImagen);
                    txtPrecio.Text = articulo.Precio.ToString();
                }

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }
        }

        private void txtUrlImagen_Leave(object sender, EventArgs e)
        {
            cargarImagen(txtUrlImagen.Text);
        }

        private void cargarImagen(string imagen)
        {
            try
            {
                pbxArticuloNuevo.Load(imagen);
            }
            catch (Exception ex)
            {
                pbxArticuloNuevo.Load("https://uning.es/wp-content/uploads/2016/08/ef3-placeholder-image.jpg");
            }

        }

        private bool validarCarga()
        {
            //Validar requeridos nombre, codigo, precio
            // validar filtro decimal en precio

            if (string.IsNullOrEmpty(txtCodigo.Text))
            {
                MessageBox.Show("Debes cargar el código del artículo.");
                return true;
            }
            if (string.IsNullOrEmpty(txtNombre.Text))
            {
                MessageBox.Show("Debes cargar el nombre del artículo.");
                return true;
            }
            if (string.IsNullOrEmpty(txtPrecio.Text))
            {
                MessageBox.Show("Debes cargar el precio del artículo.");
                return true;
            }
            // validar filtro decimal en precio
            if (!(soloNumeros(txtPrecio.Text)))
            {
                MessageBox.Show("El precio debe constar solo de números(en caso de necesitarlo, utilizar el punto como separador decimal).");
                return true;
            }

            return false;
        }

        private bool soloNumeros(string cadena)
        {
            decimal number;
            if (Decimal.TryParse(cadena, NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture, out number))
                return true;
            else
                return false;
        }


    }
}
