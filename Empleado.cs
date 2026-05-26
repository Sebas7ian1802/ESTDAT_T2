using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace T2_ESDAT
{
    public class Empleado
    {
        public int Codigo { get; set; }
        public string Nombre { get; set; }
        public int Antiguedad { get; set; }

        public Empleado(int codigo, string nombre, int antiguedad)
        {
            Codigo = codigo;
            Nombre = nombre;
            Antiguedad = antiguedad;
        }

        public override string ToString()
        {
            return $"codigo: {Codigo}, nombre: {Nombre}, antiguedad: {Antiguedad}";
        }
    }
}
