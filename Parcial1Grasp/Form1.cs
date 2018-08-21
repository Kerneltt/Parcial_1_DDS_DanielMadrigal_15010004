using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Parcial1Grasp
{
    public partial class Form1 : Form
    {
        public List<Departamento> Ldepartamentos = new List<Departamento>();
        public List<Rol> Lroles = new List<Rol>();
        public List<Usuario> Lusuarios = new List<Usuario>();
        public string seleccionado;

        public Form1()
        {            
            InitializeComponent();
            //lets start getting the info to show latter
            if (File.Exists(System.IO.Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory.ToString(), "usuarios.txt")))
            {
                string[] usuarios = System.IO.File.ReadAllLines(System.IO.Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory.ToString(), "usuarios.txt"));
                foreach (string line in usuarios)
                {
                    string[] userdata = line.Split('|');
                    Usuario newUsuario = new Usuario();
                    newUsuario.id = userdata[0];
                    newUsuario.password = userdata[1];
                    newUsuario.rol = userdata[2];
                    Lusuarios.Add(newUsuario);
                }
            }
            if (File.Exists(System.IO.Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory.ToString(), "roles.txt")))
            {
                string[] roles = System.IO.File.ReadAllLines(System.IO.Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory.ToString(), "roles.txt"));
            }
            if (File.Exists(System.IO.Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory.ToString(), "departamentos.txt")))
            {
                string[] departamentos = System.IO.File.ReadAllLines(System.IO.Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory.ToString(), "departamentos.txt"));
                foreach (string line in departamentos)
                {
                    string[] depdata = line.Split('|');
                    Departamento newDep = new Departamento();
                    newDep.producto=depdata[0];
                    newDep.id= depdata[1];
                    newDep.costo=depdata[2];
                    newDep.cantidad=depdata[3];
                    Ldepartamentos.Add(newDep);
                }
            }
            

            
            //
        }        
        
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            dataGridView1.AllowUserToDeleteRows=true;            
        }

        private void button1_Click(object sender, EventArgs e)
        {            
            foreach (Usuario registro in Lusuarios)
            {
                if (tbUsuario.Text==registro.id && tbpass.Text==registro.password)
                {
                    MessageBox.Show("Bienvenido");
                    LogInPanel.Hide();
                    ABCPanel.Show();
                    return;
                }
            }                                
            MessageBox.Show("Nombre de usuario O contraseña incorrectos");            
        }

        private void btnUsuarios_Click(object sender, EventArgs e)
        {
            seleccionado = "usuarios.txt";
            dataGridView1.ColumnCount = 3;
            dataGridView1.RowCount = Lusuarios.Count+1;
            for (int i = 0; i < Lusuarios.Count; i++)
            {                
                dataGridView1[0, i].Value = Lusuarios[i].id;
                dataGridView1[1, i].Value = Lusuarios[i].password;
                dataGridView1[2, i].Value = Lusuarios[i].rol;
            }
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            dataGridView1.RowCount++;
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (dataGridView1.RowCount>1)
            {
                if (this.dataGridView1.SelectedRows.Count > 0)
                {
                    dataGridView1.Rows.RemoveAt(this.dataGridView1.SelectedRows[0].Index);                    
                }
            }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            string all="";
            for (int i = 0; i < dataGridView1.RowCount; i++)
            {
                for (int j = 0; j < dataGridView1.ColumnCount; j++)
                {
                    all += dataGridView1[j, i].Value+"|";
                }
                all += "\n";
            }
            System.IO.File.WriteAllText(Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory.ToString(), seleccionado), all);
            MessageBox.Show("Registro Guardado");
        }

        private void btnDepartamentos_Click(object sender, EventArgs e)
        {
            seleccionado = "departamentos.txt";
            dataGridView1.ColumnCount = 4;
            dataGridView1.RowCount = Ldepartamentos.Count+1;
            for (int i = 0; i < Ldepartamentos.Count; i++)
            {
                dataGridView1[0, i].Value = Ldepartamentos[i].producto;
                dataGridView1[1, i].Value = Ldepartamentos[i].id;
                dataGridView1[2, i].Value = Ldepartamentos[i].costo;
                dataGridView1[3, i].Value = Ldepartamentos[i].cantidad;
            }
        }
    }
}
