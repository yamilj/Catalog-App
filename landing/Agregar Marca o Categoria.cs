using Dominio;
using Negocio;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace landing
{
    public partial class Agregar_Marca_o_Categoria : Form
    {
        public Agregar_Marca_o_Categoria()
        {
            InitializeComponent();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void Agregar_Marca_o_Categoria_Load(object sender, EventArgs e)
        {
            cbxMarcaCategoria.Items.Add("Marca");
            cbxMarcaCategoria.Items.Add("Categoría");
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            if (validarAgregar())
            {
                return;
            }

            try
            {
                
                
                
                if (cbxMarcaCategoria.Text == "Marca")
                {
                    Marca marca = new Marca();
                    MarcaNegocio negocio = new MarcaNegocio ();
                    marca.Descripcion = txtMarcaCategoria.Text;
                    negocio.agregar(marca);
                    MessageBox.Show("Marca agregada exitosamente");


                }else if (cbxMarcaCategoria.Text == "Categoría")
                {
                    Categoria categoria = new Categoria();
                    CategoriaNegocio negocio = new CategoriaNegocio();
                    categoria.Descripcion = txtMarcaCategoria.Text;
                    negocio.agregar(categoria);
                    MessageBox.Show("Categoría agregada exitosamente");

                }

                Close();


            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }





        }

        private bool validarAgregar()
        {
            if (cbxMarcaCategoria.SelectedIndex<0)
            {
                MessageBox.Show("Por favor seleccione el tipo a agregar.");
                return true;
            }
            if (string.IsNullOrEmpty(txtMarcaCategoria.Text))
            {
                MessageBox.Show("Debes cargar la marca o categoría a agregar.");
                return true;
            }
            if (!(soloLetras(txtMarcaCategoria.Text)))
            {
                MessageBox.Show("Utilizar solo letras para la marca o categoría a agregar.");
                return true;
            }
            return false;
        }


        private bool soloLetras(string cadena)
        {
            foreach (char caracter in cadena)
            {
                if (!(char.IsLetter(caracter)))
                {
                    return false;
                }
            }
            return true;
        }
    }
}
