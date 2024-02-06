using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Dominio;
using Negocio;


namespace landing
{
    public partial class Form1 : Form
    {
        private List<Articulo> listaArticulo;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            cargar();
            cbxCampo.Items.Add("Nombre");
            cbxCampo.Items.Add("Marca");
            cbxCampo.Items.Add("Categoría");
            cbxCampo.Items.Add("Precio");
        }

        private void cargar()
        {
            ArticuloNegocio negocio = new ArticuloNegocio();
            try
            {
                listaArticulo = negocio.listar();
                dgvArticulos.DataSource = listaArticulo;
                ocultarColumna();
                cargarImagen(listaArticulo[0].UrlImagen);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

        }
        private void ocultarColumna ()
        {
            dgvArticulos.Columns["Id"].Visible= false;
            dgvArticulos.Columns["Codigo"].Visible= false;
            dgvArticulos.Columns["Descripcion"].Visible= false;
            dgvArticulos.Columns["UrlImagen"].Visible = false;

        }

        private void dgvArticulos_SelectionChanged(object sender, EventArgs e)
        {
            if(dgvArticulos.CurrentRow != null)
            {
                Articulo seleccionado = (Articulo) dgvArticulos.CurrentRow.DataBoundItem;
                cargarImagen(seleccionado.UrlImagen);
            }
        }

        private void cargarImagen(string imagen)
        {
            try
            {
                pbxArticulo.Load(imagen);
            }
            catch (Exception ex)
            {
                pbxArticulo.Load("https://uning.es/wp-content/uploads/2016/08/ef3-placeholder-image.jpg");
            }

        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            frmAltaArticulo alta = new frmAltaArticulo();
            alta.ShowDialog();
            cargar();
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            try
            {
                Articulo seleccionado;
                if (dgvArticulos.CurrentRow == null)
                {
                    MessageBox.Show("Por favor seleccione un artículo a modificar");
                    return;
                }
                seleccionado = (Articulo)dgvArticulos.CurrentRow.DataBoundItem;
                frmAltaArticulo modificar = new frmAltaArticulo(seleccionado);
                modificar.ShowDialog();
                cargar();

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            ArticuloNegocio negocio = new ArticuloNegocio();
            Articulo seleccionado;

            try
            {
                if (dgvArticulos.CurrentRow == null)
                {
                    MessageBox.Show("Por favor seleccione un artículo a eliminar");
                    return;
                }
                DialogResult respuesta = MessageBox.Show("¿Estas seguro de eliminarlo?","Eliminando",MessageBoxButtons.YesNo,MessageBoxIcon.Warning);
                if (respuesta == DialogResult.Yes)
                {
                    seleccionado = (Articulo) dgvArticulos.CurrentRow.DataBoundItem;
                    negocio.eliminar(seleccionado.Id);
                    cargar();
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }
        }

        
        private bool validarFiltro()
        {
            if(cbxCampo.SelectedIndex < 0)
            {
                MessageBox.Show("Por favor seleccione el campo para filtrar.");
                return true;
            }
            if (cbxCriterio.SelectedIndex < 0)
            {
                MessageBox.Show("Por favor seleccione el criterio para filtrar.");
                return true;
            }
            if (cbxCampo.SelectedItem.ToString() == "Precio") 
            {
                if (string.IsNullOrEmpty(txtFiltroAvanzado.Text))
                {
                    MessageBox.Show("Debes cargar el filtro para númericos.");
                    return true;
                }
                if (!(soloNumeros(txtFiltroAvanzado.Text)))
                {
                    MessageBox.Show("Ingrese solo números por favor (en caso de necesitarlo, utilizar el punto como separador decimal).");
                    return true;
                }
            }

            return false;
        }

        private bool soloNumeros(string cadena)
        {
            //foreach (char caracter in cadena){
            //    if (!(char.IsNumber(caracter))&& caracter !='.')
            //      return false;
            //}

            decimal number;
            if (Decimal.TryParse(cadena, NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture, out number))
                return true;
            else
                return false;
        }





        private void btnFiltroAvanzado_Click(object sender, EventArgs e)
        {

            ArticuloNegocio negocio = new ArticuloNegocio();
            try
            {
                if (validarFiltro())
                        return;


                string campo = cbxCampo.SelectedItem.ToString();
                string criterio = cbxCriterio.SelectedItem.ToString();
                string filtro = txtFiltroAvanzado.Text;
                dgvArticulos.DataSource = negocio.filtrar(campo, criterio, filtro);


            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }
            



        }

        private void txtFiltroRapido_TextChanged(object sender, EventArgs e)
        {
            List<Articulo> listafiltrada;
            string filtro = txtFiltroRapido.Text;

            if (filtro.Length >= 3)
            {
                //Filtro por nombre, marca o categoria.
                listafiltrada = listaArticulo.FindAll(x => x.Nombre.ToUpper().Contains(filtro.ToUpper()) || x.Marca.Descripcion.ToUpper().Contains(filtro.ToUpper()) || x.Categoria.Descripcion.ToUpper().Contains(filtro.ToUpper()));

            }
            else
            {
                listafiltrada = listaArticulo;
            }

            dgvArticulos.DataSource = null;
            dgvArticulos.DataSource = listafiltrada;
            ocultarColumna();



        }

        private void cbxCampo_SelectedIndexChanged(object sender, EventArgs e)
        {
            string opcion = cbxCampo.SelectedItem.ToString();
            if (opcion == "Precio")
            {
                cbxCriterio.Items.Clear();
                cbxCriterio.Items.Add("Mayor a");
                cbxCriterio.Items.Add("Igual a");
                cbxCriterio.Items.Add("Menor a");
            }
            else
            {
                cbxCriterio.Items.Clear();
                cbxCriterio.Items.Add("Comienza con");
                cbxCriterio.Items.Add("Termina con");
                cbxCriterio.Items.Add("Contiene");
            }
        }

        private void btnDetalle_Click(object sender, EventArgs e)
        {
            
            Articulo seleccionado;
            if (dgvArticulos.CurrentRow == null)
            {
                MessageBox.Show("Por favor seleccione un artículo para ver el detalle");
                return;
            }
            seleccionado = (Articulo)dgvArticulos.CurrentRow.DataBoundItem;
            VerDetalle detalle = new VerDetalle(seleccionado);
            detalle.ShowDialog();
            
        }

        private void btnMarcaCategoria_Click(object sender, EventArgs e)
        {

            Agregar_Marca_o_Categoria agregar = new Agregar_Marca_o_Categoria();
            agregar.ShowDialog();
        }
    }   


}
