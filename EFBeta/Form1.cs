using AccesoDatos;
using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace EFBeta
{
    public partial class Form1 : Form
    {
        private CustomerRepository cr = new CustomerRepository();
        private string imagePath; // Para almacenar la ruta de la imagen seleccionada

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Cargar información al cargar el formulario
            CargarInformacion();
        }

        private void CargarInformacion()
        {
            var categoria = cr.CargarInformacion();
            dgvCategory.DataSource = categoria;
        }
        private void btnInsertar_Click(object sender, EventArgs e)
        {
            try
            {
                var categoria = CrearCategoria(); // Creamos la categoría con la imagen convertida en bytes
                var resultado = cr.InsertarCategoria(categoria); // Insertamos la categoría en la base de datos

                if (resultado > 0)
                {
                    MessageBox.Show($"Se insertó correctamente {resultado} fila(s).");
                    CargarInformacion(); // Recargar la información después de insertar
                }
                else
                {
                    MessageBox.Show("Error: No se pudo insertar la categoría.");
                }
            }
            catch (Exception ex)
            {
                // Capturamos cualquier error inesperado y mostramos el mensaje
                MessageBox.Show($"Ocurrió un error al insertar la categoría: {ex.Message}");
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            // Implementa el código de eliminar si lo necesitas
        }

        private void btnImagen_Click(object sender, EventArgs e)
        {
            pbImage.SizeMode = PictureBoxSizeMode.Zoom;
            OpenFileDialog openFileDialog = new OpenFileDialog();

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                imagePath = openFileDialog.FileName; // Guardamos la ruta de la imagen
                pbImage.Image = Image.FromFile(imagePath); // Mostramos la imagen seleccionada
            }
        }

        private Categories CrearCategoria()
        {
            var categoria = new Categories
            {
                CategoryName = txbCategoryName.Text,
                Description = txbDescription.Text,
                CategoryID = int.TryParse(txbCategoryID.Text, out int categoryId) ? categoryId : 0,
                // Convertimos la imagen seleccionada en un arreglo de bytes si hay una imagen
                Picture = pbImage.Image != null ? ImageToByteArray(pbImage.Image) : null
            };

            return categoria;
        }

        // Método para convertir una imagen en un arreglo de bytes
        private byte[] ImageToByteArray(Image imageIn)
        {
            using (var ms = new MemoryStream())
            {
                imageIn.Save(ms, imageIn.RawFormat);
                return ms.ToArray();
            }
        }

        private void dgvCategory_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            CargarInformacion();
        }
        // Método para convertir la imagen a un arreglo de bytes (BLOB)
    }
}
