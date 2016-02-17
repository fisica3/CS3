using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

using System.Linq;
using System.Xml;
using System.Xml.Linq;
using System.Xml.XPath;

using PlainConcepts.Clases;

namespace Ejemplo12_01
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        static List<Persona> Generación = new List<Persona> {
            new Persona { Nombre = "Diana", Sexo = SexoPersona.Mujer, 
                          FechaNac = new DateTime(1996, 2, 4) },
            new Persona { Nombre = "Dennis", Sexo = SexoPersona.Varón, 
                          FechaNac = new DateTime(1983, 12, 27) },
            new Persona { Nombre = "Jennifer", Sexo = SexoPersona.Mujer, 
                          FechaNac = new DateTime(1982, 8, 12) },
            new Persona { Nombre = "Claudia", Sexo = SexoPersona.Mujer, 
                          FechaNac = new DateTime(1989, 7, 26) },
            new Persona { Nombre = "Amanda", Sexo = SexoPersona.Mujer, 
                          FechaNac = new DateTime(1998, 10, 23) },
            new Persona { Nombre = "Adrián", Sexo = SexoPersona.Varón, 
                          FechaNac = new DateTime(2005, 9, 29) }
        };

        private void button1_Click(object sender, EventArgs e)
        {
            // *** API secuencial
            using (XmlTextWriter tw = new XmlTextWriter("Familia1.xml", Encoding.UTF8))
            {
                tw.Formatting = Formatting.Indented;
                tw.WriteStartDocument();
                tw.WriteStartElement("Familia");
                foreach (Persona p in Generación)
                {
                    tw.WriteStartElement("Persona");
                    tw.WriteElementString("Nombre", p.Nombre);
                    if (p.Sexo.HasValue)
                        tw.WriteElementString("Sexo", p.Sexo.Value.ToString());
                    if (p.FechaNac.HasValue)
                        tw.WriteElementString("FechaNac",
                            p.FechaNac.Value.ToString("yyyy-MM-dd"));
                    tw.WriteEndElement();
                }
                tw.WriteEndElement();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // *** API DOM
            XmlDocument doc = new XmlDocument();
            doc.AppendChild(doc.CreateXmlDeclaration("1.0", "utf-8", null));
            XmlElement raíz = doc.CreateElement("Familia");
            doc.AppendChild(raíz);
            foreach (Persona p in Generación)
            {
                XmlElement nodo = doc.CreateElement("Persona");
                XmlElement x = doc.CreateElement("Nombre");
                XmlText v = doc.CreateTextNode(p.Nombre);
                x.AppendChild(v);
                nodo.AppendChild(x);
                if (p.Sexo.HasValue)
                {
                    x = doc.CreateElement("Sexo");
                    v = doc.CreateTextNode(p.Sexo.Value.ToString());
                    x.AppendChild(v);
                    nodo.AppendChild(x);
                }
                if (p.FechaNac.HasValue)
                {
                    x = doc.CreateElement("FechaNac");
                    v = doc.CreateTextNode(p.FechaNac.Value.ToString("yyyy-MM-dd"));
                    x.AppendChild(v);
                    nodo.AppendChild(x);
                }
                raíz.AppendChild(nodo);
            }
            doc.Save("Familia2.xml");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            // *** API de LINQ To XML
            XDocument doc = new XDocument(new XDeclaration("1.0", "utf-8", null));
            XElement raíz = new XElement("Familia");
            doc.Add(raíz);
            foreach (Persona p in Generación)
            {
                XElement nodo = new XElement("Persona",    // construcción funcional
                    new XElement("Nombre", p.Nombre));
                if (p.Sexo.HasValue)
                {
                    nodo.Add(new XElement("Sexo", p.Sexo.Value));
                    // *** equivale a:
                    // nodo.Add(new XElement("Sexo", new XText(p.Sexo.Value.ToString())));
                }
                if (p.FechaNac.HasValue)
                {
                    nodo.Add(new XElement("FechaNac", p.FechaNac.Value.ToString("yyyy-MM-dd")));
                }
                raíz.Add(nodo);
            }
            doc.Save("Familia3.xml");
        }

        private void button4_Click(object sender, EventArgs e)
        {
            // carga del documento XML
            XElement doc = XElement.Load("Familia3.xml");
            // expresión de consulta
            var chicas = from x in doc.Elements()
                         where (string)x.Element("Sexo") == "Mujer"
                         orderby (string)x.Element("Nombre")
                         select new Persona()
                                {
                                    Nombre = (string)x.Element("Nombre"),
                                    Sexo = SexoPersona.Mujer
                                };
            // ejecución
            foreach (var chica in chicas)
                MessageBox.Show(chica.Nombre);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            // carga del documento XML
            XElement doc = XElement.Load("Familia3.xml");
            // extracción de fechas de cumpleaños
            lbxLista.Items.Clear();
            foreach (DateTime fechaRegalo in doc.Elements().Descendants("FechaNac"))
                lbxLista.Items.Add(fechaRegalo.ToString("dd 'de' MMMM"));
        }

        private void button6_Click(object sender, EventArgs e)
        {
            // carga del documento XML
            XElement doc = XElement.Load("Familia3.xml");
            // transformación del documento original
            XElement nuevo =
                new XElement("NombresEdades",
                        from x in doc.Elements("Persona")
                        select new XElement("Hijo",
                            x.Element("Nombre"),
                            new XElement("Edad",
                          Persona._Edad((DateTime)x.Element("FechaNac"))
                        )
                )
            );
            nuevo.Save("Familia4.xml");
        }

        private void button8_Click(object sender, EventArgs e)
        {
            // carga del documento XML
            XElement doc = XElement.Load("Familia3.xml");

            // buscar un elemento
            XElement j = doc.Elements("Persona").First(x => (string)x.Element("Nombre") == "Jennifer");

            if (j != null)
            {
                // inserción (en este caso detrás)
                j.AddAfterSelf(
                    new XElement("Persona",
                        new XElement("Nombre", "JOHN DOE"),
                        new XElement("Sexo", SexoPersona.Varón),
                        new XElement("FechaNac", new DateTime(1980, 1, 1))
                    )
                );

                // modificación de elemento
                j.SetElementValue("Nombre", "Jenny");

                // borrado
                j.NextNode.Remove();
            }

            doc.Save("Familia5.xml");
        }

        private void button10_Click(object sender, EventArgs e)
        {
            // carga del documento XML
            XElement doc = XElement.Parse(
               @"<discografia>
                 <album>
                   <nombre>Boston</nombre>
                   <fecha>1975</fecha>
                 </album>
                 <album>
                   <nombre>Don't look back</nombre>
                   <fecha>1977</fecha>
                 </album>
                 </discografia>");
            lbxLista.Items.Clear();
            var años =
                from x in doc.Elements("album")
                select (int) x.Element("fecha") ;
            foreach (var a in años)
                lbxLista.Items.Add(a);

        }

        private void button9_Click(object sender, EventArgs e)
        {
            // carga del documento XML
            XElement doc = XElement.Load("Familia3.xml");

            // obtener nodos con nombres
            IEnumerable<XElement> nombres = doc.XPathSelectElements("//Persona//Nombre");
            foreach (var n in nombres)
                MessageBox.Show(n.Value);
        }

    }
}
