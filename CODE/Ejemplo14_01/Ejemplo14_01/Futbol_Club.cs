using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ejemplo14_01
{
    partial class FutbolDataContext
    {
        partial void UpdateClub(Club actual)
        {
            int cant = this.ExecuteCommand(
				"UPDATE Club SET Nombre={1}, Ciudad={2} WHERE Codigo = {0}",
				actual.Codigo,
                actual.Nombre, 
                actual.Ciudad);
			if (cant < 1)
				throw new Exception("Error al actualizar club");
        }

        partial void InsertClub(Club p)
        {
            // funcionalidad de inserción
		}

        partial void DeleteClub(Club p)
        {
            // funcionalidad de borrado
        }
    }
}
