using System;

using PlainConcepts.Clases;

namespace Ejemplo01_01
{
    class Program
    {
        static void Main(string[] args)
        {
            Exclamacion();
        }

        // gestor de evento
        private static void exclamacion(object sender,
            CambioSalarioEventArgs e)
        {
            Console.WriteLine(((Persona)sender).Nombre +
                " debe estar contento!");
            Console.WriteLine("Antes ganaba: " +
                e.SalarioAntes.ToString("#,##0.00"));
            Console.WriteLine("Ahora gana:   " +
                e.SalarioDespues.ToString("#,##0.00"));
        }

        // código de prueba
        private static void Exclamacion()
        {
            Empleado pepe = new Empleado("Juan Antonio", SexoPersona.Varón,
                "LA MEJOR EMPRESA", 1075M);
            // asociación de delegado al evento
            pepe.ExclamacionAlSubirSalario +=
                new Exclamacion_CambioSalario(exclamacion);
            // cambio a la propiedad (provoca el evento)
            pepe.Salario = decimal.Round(1.03M * pepe.Salario, 2);

            Console.ReadLine();
        }
    }
}
