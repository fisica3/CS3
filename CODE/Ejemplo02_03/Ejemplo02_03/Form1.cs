using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Xml;
using System.Windows.Forms;

using PlainConcepts.Clases;

namespace Ejemplo02_03
{
    public partial class FormPrincipal : Form
    {
        public FormPrincipal()
        {
            InitializeComponent();
        }

        private SortedDictionary<string, Persona> dict =
            new SortedDictionary<string, Persona>();

        private void FormPrincipal_Load(object sender, EventArgs e)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load("Agenda.XML");
            XmlNodeList lista =
                doc.SelectNodes("descendant::persona");
            foreach (XmlNode n in lista)
            {
                string nombre = n.Attributes["nombre"].Value;
                SexoPersona sexo = n.Attributes["sexo"].Value == "M" ?
                    SexoPersona.Mujer :
                    SexoPersona.Varón;
                dict.Add(
                    nombre.ToUpper().Trim(),
                    new Persona(nombre, sexo));
            }
        }

        private void miSalir_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void miBuscar_Click(object sender, EventArgs e)
        {
            using (FormBusqueda f = new FormBusqueda())
                if (f.ShowDialog() == DialogResult.OK)
                {
                    string nombre = f.NombreABuscar;
                    if (dict.ContainsKey(nombre))
                        MessageBox.Show("Sí está");
                }
        }

        private void miListar_Click(object sender, EventArgs e)
        {
            lbxPersonas.Items.Clear();
            foreach (KeyValuePair<string, Persona> kv in dict)
                lbxPersonas.Items.Add(kv.Value);
        }
    }
}
