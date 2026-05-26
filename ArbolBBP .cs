using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace T2_ESDAT
{
    public class ArbolBBP
    {
        public NodoP Primero { get; private set; }

        public ArbolBBP()
        {
            Primero = null;
        }

        public void Agrega(Empleado empleado)
        {
            if (empleado == null)
            {
                return;
            }

            if (Primero == null)
            {
                Primero = new NodoP(empleado);
                return;
            }

            AgregaRec(Primero, empleado);
        }

        private void AgregaRec(NodoP nodo, Empleado empleado)
        {
            if (empleado.Codigo < nodo.Valor.Codigo)
            {
                if (nodo.Izqu == null)
                {
                    nodo.Izqu = new NodoP(empleado);
                }
                else
                {
                    AgregaRec(nodo.Izqu, empleado);
                }
            }
            else if (empleado.Codigo > nodo.Valor.Codigo)
            {
                if (nodo.Dere == null)
                {
                    nodo.Dere = new NodoP(empleado);
                }
                else
                {
                    AgregaRec(nodo.Dere, empleado);
                }
            }
            else
            {
                // codigo duplicado: se ignora 
                Console.WriteLine($"Ya existe un empleado con codigo {empleado.Codigo}. ignorado");
            }
        }

        public void MuestraInCodigo()
        {
            if (Primero == null)
            {
                Console.WriteLine("El arbol esta vacio");
                return;
            }

            Console.WriteLine("Listado en orden por codigo:");
            InOrden(Primero);
        }

        private void InOrden(NodoP nodo)
        {
            if (nodo == null) return;
            InOrden(nodo.Izqu);
            Console.WriteLine(nodo.Valor);
            InOrden(nodo.Dere);
        }

        public void MuestraPosAntiguedad()
        {
            if (Primero == null)
            {
                Console.WriteLine("El arbol está vacio");
                return;
            }

            Console.WriteLine("Listado PosOrden (antiguedad en cada empleado):");
            PosOrden(Primero);
        }

        private void PosOrden(NodoP nodo)
        {
            if (nodo == null) return;
            PosOrden(nodo.Izqu);
            PosOrden(nodo.Dere);
            Console.WriteLine(nodo.Valor);
        }

        public int MayoresA(int antiguedad)
        {
            return CuentaMayoresRec(Primero, antiguedad);
        }

        private int CuentaMayoresRec(NodoP nodo, int antiguedad)
        {
            if (nodo == null) return 0;
            int contador = 0;
            if (nodo.Valor.Antiguedad > antiguedad) contador = 1;
            contador += CuentaMayoresRec(nodo.Izqu, antiguedad);
            contador += CuentaMayoresRec(nodo.Dere, antiguedad);
            return contador;
        }

        public string Penultimo()
        {
            if (Primero == null)
            {
                return "El arbol está vacio";
            }

            // Si solo hay un nodo, no hay penultimo
            if (Primero.Izqu == null && Primero.Dere == null)
            {
                return "No existe un penultimo";
            }

            // Encontrar el nodo maximo (ultimo)
            NodoP node = Primero;
            NodoP parent = null;
            while (node.Dere != null)
            {
                parent = node;
                node = node.Dere;
            }

            // Si el maximo tiene subarbol izquierdo, el penultimo es el máximo de ese subarbol
            if (node.Izqu != null)
            {
                NodoP p = node.Izqu;
                while (p.Dere != null)
                {
                    p = p.Dere;
                }

                return p.Valor.ToString();
            }

            // Si no, el penultimo es el padre del nodo maximo
            if (parent != null)
            {
                return parent.Valor.ToString();
            }

            return "Error en determinar el penultimo";
        }
    }
}
