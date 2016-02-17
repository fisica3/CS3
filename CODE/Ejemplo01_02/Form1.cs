using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Threading;

using PlainConcepts.Clases;
using Ejemplo01_01;

namespace Ejemplo01_02
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            btn.Click += delegate
            {
                MessageBox.Show("Saludos desde C#");
            };
        }

        private Thread hilo = null;

        private void Hilo(object sender, EventArgs e)
        {
            hilo = new Thread(
                delegate()
                {
                    int i = 1;
                    while (true)
                    {
                        lbl.Invoke(
                            (MethodInvoker)
                            delegate
                            {
                                lbl.Text = i.ToString();
                            });
                        Thread.Sleep(1000);
                        i++;
                    }
                });
            hilo.Start();
        }

        private void btnEvento_Click(object sender, EventArgs e)
        {
            Empleado pepe = new Empleado("PEPE", SexoPersona.Varón, "XXX", 975M);

            pepe.ExclamacionAlSubirSalario +=
                delegate(object s2, CambioSalarioEventArgs e2)
                {
                    if (e2.SalarioDespues > e2.SalarioAntes)
                    {
                        MessageBox.Show(((Empleado)s2).Nombre +
                            " debe estar contento!");
                        MessageBox.Show("Antes ganaba: " +
                            e2.SalarioAntes.ToString("#,##0.00"));
                        MessageBox.Show("Ahora gana:   " +
                            e2.SalarioDespues.ToString("#,##0.00"));
                    }
                };

            pepe.Salario *= 1.05M;  // dispara el evento
        }

        private void btnHilo_Click(object sender, EventArgs e)
        {
            Hilo(sender, e);
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (hilo != null)
                hilo.Abort();
        }

        // acceso al ámbito externo
        private void button1_Click(object sender, EventArgs e)
        {
            Empleado[] arr = {
                new Empleado("Octavio", SexoPersona.Varón, "POKR", 1440M),
                new Empleado("Sergio", SexoPersona.Varón, "POKR", 1210M),
                new Empleado("Denis", SexoPersona.Varón, "POKR", 950.55M)
            };
                
            decimal suma = 0;
            foreach (Empleado emp in arr)
            {
                emp.ExclamacionAlSubirSalario +=
                    delegate (object s2, CambioSalarioEventArgs e2)
                    {
                        if (e2.SalarioDespues > e2.SalarioAntes)
                        {
                            MessageBox.Show(((Persona)s2).Nombre + 
                                " debe estar contento!");
                            MessageBox.Show("Antes ganaba: " +
                                e2.SalarioAntes.ToString("#,##0.00"));
                            MessageBox.Show("Ahora gana:   " +
                              e2.SalarioDespues.ToString("#,##0.00"));
                            // acceso a variable de ámbito externo
                            suma += e2.SalarioDespues;
                        }
                    };
                emp.Salario = decimal.Round(1.03M * emp.Salario, 2);   
            }
            MessageBox.Show("El salario total es: " +
               suma.ToString("###,##0.00"));
        }
    }
}
