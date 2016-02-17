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
    public partial class ChildForm : Form
    {
        public ChildForm()
        {
            InitializeComponent();
        }

        private void ChildForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            // quitar de la lista
            Program.listaVentanas.Remove(this);
        }

    }
}
