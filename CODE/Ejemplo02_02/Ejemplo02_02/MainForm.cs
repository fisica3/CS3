using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Ejemplo02_02
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private static int count = 0;

        private void miNueva_Click(object sender, EventArgs e)
        {
            ChildForm f = new ChildForm();
            f.Text = "Ventana " + (++count).ToString("000");
            f.MdiParent = Program.MainFormInstance;
            f.Show();
            // añadir a lista
            Program.listaVentanas.AddLast(f);
        }

        private void MainForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (Program.listaVentanas.Count > 0)
            {
                LinkedListNode<Form> activa =
                  Program.listaVentanas.Find(this.ActiveMdiChild);
                if (e.Control)
                    switch (e.KeyCode)
                    {
                        case Keys.Right:
                            Form siguiente = activa.Next != null ?
                               activa.Next.Value : 
                               Program.listaVentanas.First.Value;
                            siguiente.BringToFront();
                            break;
                        case Keys.Left:
                            Form anterior = activa.Previous !=
                                null ?
                                activa.Previous.Value : 
                                Program.listaVentanas.Last.Value;
                            anterior.BringToFront();
                            break;
                        case Keys.Home:
                            Program.listaVentanas.
                                First.Value.BringToFront();
                            break;
                        case Keys.End:
                            Program.listaVentanas.
                                Last.Value.BringToFront();
                            break;
                    }
            }
        }

        private void miSalir_Click(object sender, EventArgs e)
        {
            while (Program.listaVentanas.Count > 0)
                Program.listaVentanas.ElementAt(0).Close();
            this.Close();
        }
    }
}
