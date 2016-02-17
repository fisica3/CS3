using System;

using PlainConcepts.Clases;

namespace Ejemplo01_01
{
    public class Empleado : Persona
    {
        // campos
        private string empresa = null;
        private decimal salario;

        // constructores
        public Empleado(string nombre, SexoPersona sexo, string empresa,
            decimal salario)
            : base(nombre, sexo)
        {
            this.Empresa = empresa;
            this.salario = salario;
        }

        // propiedades
        public string Empresa
        {
            get { return empresa; }
            set
            {
                if (value == null)
                    throw new Exception("Empresa obligatoria");
                else
                    empresa = value;
            }
        }
        public decimal Salario
        {
            get { return salario; }
            set
            {
                decimal anterior = salario;
                salario = value;
                if (salario > anterior &&
                    ExclamacionAlSubirSalario != null)
                {
                    // disparar evento
                    ExclamacionAlSubirSalario(this, new
                      CambioSalarioEventArgs(anterior, salario));
                }
            }
        }

        // métodos
        public override string ToString()
        {
            return base.ToString() + Environment.NewLine +
                  "Empresa: " + empresa + " Salario: " +
                  salario.ToString("#,##0.00");
        }

        // eventos
        public event Exclamacion_CambioSalario
            ExclamacionAlSubirSalario;
    }

    public delegate void Exclamacion_CambioSalario(object sender,
        CambioSalarioEventArgs e);

    public class CambioSalarioEventArgs : EventArgs
    {
        public decimal SalarioAntes;
        public decimal SalarioDespues;
        //
        public CambioSalarioEventArgs(decimal antes,
            decimal despues)
        {
            SalarioAntes = antes;
            SalarioDespues = despues;
        }
    }
}
