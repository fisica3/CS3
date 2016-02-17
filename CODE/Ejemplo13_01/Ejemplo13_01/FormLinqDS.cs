using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Ejemplo13_01
{
    public partial class FormLinqDS : Form
    {
        public FormLinqDS()
        {
            InitializeComponent();
        }

        private void FormLinqDS_Load(object sender, EventArgs e)
        {
            this.clubTableAdapter.Fill(this.FUTBOL2006DataSet.Club);
            this.paisTableAdapter.Fill(this.FUTBOL2006DataSet.Pais);
            this.futbolistaTableAdapter.Fill(this.FUTBOL2006DataSet.Futbolista);

            dataSet1 = FUTBOL2006DataSet;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var consulta =
                from futbolista in FUTBOL2006DataSet.Futbolista
                join club in FUTBOL2006DataSet.Club
                    on futbolista.CodigoClub equals club.Codigo
                join pais in FUTBOL2006DataSet.Pais
                    on futbolista.CodigoPaisNacimiento equals pais.Codigo
                where pais.Codigo != "ES"
                orderby pais.Nombre, club.Nombre, futbolista.Nombre
                select new
                {
                    Pais = pais.Nombre,
                    Club = club.Nombre,
                    futbolista.Nombre
                };
            bindingSource1.DataSource = consulta;
            dataGridView1.DataSource = bindingSource1;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var consulta =
                from futbolista in dataSet1.Tables["Futbolista"].AsEnumerable()
                join club in dataSet1.Tables["Club"].AsEnumerable()
                    on futbolista.Field<string>("CodigoClub") equals club.Field<string>("Codigo")
                join pais in dataSet1.Tables["Pais"].AsEnumerable()
                    on futbolista.Field<string>("CodigoPaisNacimiento") equals pais.Field<string>("Codigo")
                where pais.Field<string>("Codigo") != "ES"
                orderby pais.Field<string>("Nombre"), club.Field<string>("Nombre"),
                    futbolista.Field<string>("Nombre")
                select new
                {
                    Pais = pais.Field<string>("Nombre"),
                    Club = club.Field<string>("Nombre"),
                    Jugador = futbolista.Field<string>("Nombre")
                };

            bindingSource1.DataSource = consulta.ToList();
            dataGridView1.DataSource = bindingSource1;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            var consulta =
                from club in FUTBOL2006DataSet.Club
                orderby club.Ciudad
                select club;
               

            // actualización
            var betis = consulta.First(c => c.Nombre.Contains("BETIS"));
            betis.SetField<string>("Nombre", "BETIS manque pierda");

            // convertir en tabla
            DataTable t = consulta.CopyToDataTable();
            //
            bindingSource1.DataSource = t;
            dataGridView1.DataSource = bindingSource1;
        }

        private DataView vista = null;

        private void button4_Click(object sender, EventArgs e)
        {
            var consulta =
                from club in dataSet1.Tables["Club"].AsEnumerable() // FUTBOL2006DataSet.Club
                orderby club.Field<string>("Ciudad")  // .Ciudad
                select club;

            vista = consulta.AsDataView();
            vista.RowFilter = "Ciudad LIKE 'M%'";
            vista.Sort = "Nombre";
            dataGridView1.DataSource = vista;
        }

        private void GrabarCambios()
        {
            clubTableAdapter.Update(FUTBOL2006DataSet.Club);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            GrabarCambios();
        }
    }
}
