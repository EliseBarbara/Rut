using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

using BibliotecaClase;
using BibliotecaControlador;


using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using MahApps.Metro.Behaviours;

namespace Vista
{
    /// <summary>
    /// Lógica de interacción para WpfCliente.xaml
    /// </summary>
    public partial class WpfCliente : MetroWindow
    {
        DaoCliente dao;

        public WpfCliente()
        {
            InitializeComponent();

            btnModificar.Visibility = Visibility.Hidden;//el botón Modificar no se ve

            //llenar el combo box con los datos del enumerador
            cbActividad.ItemsSource = Enum.GetValues(typeof
                (ActividadEmpresa));
            cbActividad.SelectedIndex = 0;

            cbTipo.ItemsSource = Enum.GetValues(typeof
                (TipoEmpresa));
            cbTipo.SelectedIndex = 0;

            dao = new DaoCliente();
        }

        //Botón limpiar
        private void btnLimpiar_Click(object sender, RoutedEventArgs e)
        {
            txtDV.Clear();
            txtRut.Clear();
            txtRazon.Clear();
            txtNombre.Clear();
            txtEmail.Clear();
            txtDireccion.Clear();
            txtTelefono.Clear();
            cbActividad.SelectedIndex = 0;
            cbTipo.SelectedIndex = 0;//Para que en el ComboBox no quede seleccionado nada
            txtRut.Focus();//Mover el cursor a la poscición Rut

        }

        //Botón '?'
        //llama el listado de clientes
        private void btnPregunta_Click(object sender, RoutedEventArgs e)
        {
            wpfListadoCliente lis = new wpfListadoCliente(this);
            lis.Show();

        }

        //Botón Cancelar/Cerrar
        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        //Botón Guardar
        private void btnGuardar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                String rut = txtRut.Text;
                String razonSocial = txtRazon.Text;
                String nombreContacto = txtNombre.Text;
                String mail = txtEmail.Text;
                String direccion = txtDireccion.Text;
                int telefono = int.Parse(txtTelefono.Text);
                ActividadEmpresa actividad = (ActividadEmpresa)cbActividad.SelectedItem;
                TipoEmpresa empresa = (TipoEmpresa)cbTipo.SelectedItem;
                Cliente c = new Cliente()
                {
                    Rut = rut,
                    RazonSocial = razonSocial,
                    NombreContacto = nombreContacto,
                    Mail = mail,
                    Direccion = direccion,
                    Telefono = telefono,
                    Actividad = actividad,
                    Empresa = empresa

                };
                bool resp = dao.Agregar(c);
                MessageBox.Show(resp ? "Guardado" : "No Guardado");

            }
            catch (ArgumentException exa)//mensajes de reglas de negocios
            {
                MessageBox.Show(exa.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error de ingreso de datos");

            }
        }

        //Botón Eliminar
        private void btnEliminar_Click(object sender, RoutedEventArgs e)
        {
            //falta validar que no tenga contratos asociados!!!!
            wpfListadoCliente lis = new wpfListadoCliente();
            Cliente cli = (Cliente)lis.dgLista.SelectedItem;
            MessageBoxResult respuesta =
                MessageBox.Show(
                    "¿Desea eliminar al Cliente?",
                    "Eliminar",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Warning);
            if (respuesta == MessageBoxResult.Yes)
            {
                bool resp = new DaoCliente().Eliminar(cli.Rut);
                if (resp)
                {
                    MessageBox.Show("Cliente eliminado");
                    lis.dgLista.ItemsSource =
                        new DaoCliente().Listar();
                }
                else
                {
                    MessageBox.Show("No se eliminó al Cliente");
                }
            }
            else
            {
                MessageBox.Show("Operación Cancelada");
            }

        }


        //buscar (método void Singleton)
        public void Buscar()
        {
            try
            {
                Cliente c = new DaoCliente().
                    Buscar(txtRut.Text);
                if (c != null)
                {
                    txtRut.Text = c.Rut;
                    txtRazon.Text = c.RazonSocial;
                    txtNombre.Text = c.NombreContacto;
                    txtEmail.Text = c.Mail;
                    txtDireccion.Text = c.Direccion;
                    txtTelefono.Text = c.Telefono.ToString();
                    cbActividad.Text = c.Actividad.ToString();
                    cbTipo.Text = c.Empresa.ToString();

                }
                else
                {
                    MessageBox.Show("No se encontraron resultados!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("error al buscar");
                //Logger.Mensaje(ex.Message);

            }
        }

        //Botón Buscar
        private void btnBuscar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Cliente c = new DaoCliente().
                    Buscar(txtRut.Text);
                if (c != null)
                {
                    txtRut.Text = c.Rut;
                    txtRazon.Text = c.RazonSocial;
                    txtNombre.Text = c.NombreContacto;
                    txtEmail.Text = c.Mail;
                    txtDireccion.Text = c.Direccion;
                    txtTelefono.Text = c.Telefono.ToString();
                    cbActividad.Text = c.Actividad.ToString();
                    cbTipo.Text = c.Empresa.ToString();

                }
                else
                {
                    MessageBox.Show("No se encontraron resultados!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("error al buscar");
                //Logger.Mensaje(ex.Message);

            }
        }

        private void btnModificar_Click(object sender, RoutedEventArgs e)
        {

        }


             


        //añadir formato al rut

        private void txtRut_LostFocus(object sender, RoutedEventArgs e)
        {
            string v = new Verificar().ValidarRut(txtRut.Text);
            txtDV.Text = v;
            try
            {
                string rutSinFormato = txtRut.Text;

                //si el rut ingresado tiene "." o "," o "-" son ratirados para realizar la formula 
                rutSinFormato = rutSinFormato.Replace(",", "");
                rutSinFormato = rutSinFormato.Replace(".", "");
                rutSinFormato = rutSinFormato.Replace("-", "");
                string rutFormateado = String.Empty;

                //obtengo la parte numerica del RUT
                //string rutTemporal = rutSinFormato.Substring(0, rutSinFormato.Length - 1);
                string rutTemporal = rutSinFormato;
                //obtengo el Digito Verificador del RUT
                //string dv = rutSinFormato.Substring(rutSinFormato.Length - 1, 1);

                Int64 rut;

                //aqui convierto a un numero el RUT si ocurre un error lo deja en CERO
                if (!Int64.TryParse(rutTemporal, out rut))
                {
                    rut = 0;
                }

                //este comando es el que formatea con los separadores de miles
                rutFormateado = rut.ToString("N0");

                if (rutFormateado.Equals("0"))
                {
                    rutFormateado = string.Empty;
                }
                else
                {
                    //si no hubo problemas con el formateo agrego el DV a la salida
                   // rutFormateado += "-" + dv;

                    //y hago este replace por si el servidor tuviese configuracion anglosajona y reemplazo las comas por puntos
                    rutFormateado = rutFormateado.Replace(",", ".");
                }

                //se pasa a mayuscula si tiene letra k
                rutFormateado = rutFormateado.ToUpper();

                //la salida esperada para el ejemplo es 99.999.999-K
                txtRut.Text = rutFormateado;
            }
            catch (Exception)
            {

            }
        }

        ////validar y/o añadir dv al rut
        //private void txtDV_TextChanged(object sender, TextChangedEventArgs e)
        //{
            
            

            
        //}

        //private void txtRut_TextChanged(object sender, TextChangedEventArgs e)
        //{
            
        //    string v = new Verificar().ValidarRut(txtRut.Text);

        //    txtDV.Text = v;
        //}
    }
}
