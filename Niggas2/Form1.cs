using Niggas2.modells;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Niggas2
{
    public partial class Form1 : Form
    {
        private List<Persona> listapersonas = new List<Persona>();
        public Form1()
        {
            InitializeComponent();
            InicializarDataGridView();
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void InicializarDataGridView() 
        {
            dgvPersons.Columns.Add("Nombre", "Nombre");
            dgvPersons.Columns.Add("Edad", "Edad");
            dgvPersons.Columns.Add("Nota", "Nota");
            dgvPersons.Columns.Add("Genero", "Genero");
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (listapersonas.Count < 3)
            {
                string nombre = tbName.Text;
                int edad = int.Parse(tbAge.Text);
                int nota = int.Parse(tbGrade.Text);
                char genero = char.Parse(tbSex.Text);

                Persona persona = new Persona(nombre, edad, nota, genero);
                listapersonas.Add(persona);

                dgvPersons.Rows.Add(persona.Nombre, persona.Edad, persona.Nota, persona.Genero);

                LimpiarTextBoxes();
            }
            else 
            {
                MessageBox.Show("Solo se pueden agregar tres personas por archivo");
            }

        }

        private void LimpiarTextBoxes()
        {
            tbName.Clear();
            tbGrade.Clear();
            tbAge.Clear();
            tbSex.Clear();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (listapersonas.Count == 3)
            {
                SaveFileDialog save = new SaveFileDialog();//Stackoverflow
                save.Filter = "Archivos binarios (*.dat)|*.dat";//Stackoverflow

                if (save.ShowDialog() == DialogResult.OK) //stackoverflow
                {
                    using (FileStream nah /* lo escribi asi porque ando pereza*/ = new FileStream(save.FileName, FileMode.Create, FileAccess.Write)) //esto lo sauqe de un mae de stackoverflow, no encuentro la publicacion asi que ahi se queda
                    using (BinaryWriter wea /* aqui tambien andaba pereza*/ = new BinaryWriter(nah))
                    {
                        foreach (Persona person in listapersonas)
                        {
                            wea.Write(person.Nombre.Length);
                            wea.Write(person.Nombre.ToCharArray());
                            wea.Write(person.Edad);
                            wea.Write(person.Nota);
                            wea.Write(person.Genero);
                        }
                    }

                    MessageBox.Show("Archivo guardado correctamente.");
                    listapersonas.Clear();
                    dgvPersons.Rows.Clear();
                }
            }
            else 
            {
                MessageBox.Show("Debe agregar exactamente 3 personas antes de guardar.");
            }
        }
    }
}
