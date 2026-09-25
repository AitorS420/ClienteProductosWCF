using System;
using System.Windows.Forms;

namespace ClienteProductosWCF
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            // 1. Instanciamos el cliente
            ServicioLocal.Service1Client cliente = new ServicioLocal.Service1Client();

            try
            {
                
                var listaCompleta = cliente.ListarProductos();

               
                dataGridView1.DataSource = listaCompleta;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hubo un error: " + ex.Message);
            }
            finally
            {
                //cerramos la conexión
                cliente.Close();
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            ServicioLocal.Service1Client cliente = new ServicioLocal.Service1Client();

            try
            {
                // 2. Capturamos el número que el usuario escribió en la caja de texto
                int idBuscado = int.Parse(textBox1.Text);

                // 3. Llamamos al método ObtenerProducto pasándole ese ID
                var unProducto = cliente.ObtenerProducto(idBuscado);

                // 4. Validamos si encontró algo y lo mostramos en la tabla
                if (unProducto != null)
                {
                    // Lo envolvemos en un arreglo para que el DataGridView lo acepte
                    dataGridView1.DataSource = new[] { unProducto };
                }
                else
                {
                    MessageBox.Show("No se encontró ningún producto con ese ID.");
                    dataGridView1.DataSource = null; // Limpiamos la tabla
                }
            }
            catch (FormatException)
            {
                MessageBox.Show("Por favor, ingresa solo números en la caja de texto.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hubo un error: " + ex.Message);
            }
            finally
            {
                cliente.Close();
            }
        }
    }
    }
