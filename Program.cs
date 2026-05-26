using T2_ESDAT;

class Program
{
    static void Main(string[] args)
    {
        var arbolEmpleados = new ArbolBBP();

        // Se agregan 8 empleados de ejemplo 3689, 2378, 4975, 8162, 4763, 5915, 1584, 5861.
        var muestras = new List<Empleado>
            {
                new Empleado(3689, "Ana", 5),
                new Empleado(2378, "Luis", 2),
                new Empleado(4975, "Rafael", 7),
                new Empleado(8162, "Carlos", 10),
                new Empleado(4763, "Roberto", 4),
                new Empleado(5915, "Jorge", 6),
                new Empleado(1584, "Keiko", 3),
                new Empleado(5861, "Pablo", 8)
            };

        foreach (var e in muestras)
        {
            arbolEmpleados.Agrega(e);
        }

        Console.WriteLine("Se cargo 8 empleados de ejemplo (codigos: 3689, 2378, 4975, 8162, 4763, 5915, 1584, 5861).");

        bool salir = false;
        while (!salir)
        {
            Console.WriteLine();
            Console.WriteLine("Menu:");
            Console.WriteLine("1. Agregar");
            Console.WriteLine("2. Listar1 (en orden por Codigo)");
            Console.WriteLine("3. Listar2 (pos_orden)");
            Console.WriteLine("4. Mayores de cierta Antiguedad");
            Console.WriteLine("5. Penultimo (segun codigo)");
            Console.WriteLine("9. Fin");
            Console.Write("Seleccione una opcion: ");
            string opcion = Console.ReadLine();

            switch (opcion)
            {
                case "1":
                    AgregarEmpleado(arbolEmpleados);
                    break;
                case "2":
                    arbolEmpleados.MuestraInCodigo();
                    break;
                case "3":
                    arbolEmpleados.MuestraPosAntiguedad();
                    break;
                case "4":
                    Console.Write("Ingrese la antiguedad (años): ");
                    if (int.TryParse(Console.ReadLine(), out int antig))
                    {
                        int count = arbolEmpleados.MayoresA(antig);
                        Console.WriteLine($"Cantidad de empleados con antiguedad mayor a {antig}: {count}");
                    }
                    else
                    {
                        Console.WriteLine("Antiguedad invalida.");
                    }
                    break;
                case "5":
                    string resultado = arbolEmpleados.Penultimo();
                    Console.WriteLine("Penultimo (segun codigo):");
                    Console.WriteLine(resultado);
                    break;
                case "9":
                    salir = true;
                    break;
                default:
                    Console.WriteLine("Opcion no valida.");
                    break;
            }
        }

        Console.WriteLine("Fin del programa");
        Console.ReadKey();
    }

    private static void AgregarEmpleado(ArbolBBP arbol)
    {
        Console.Write("Codigo (int): ");
        if (!int.TryParse(Console.ReadLine(), out int codigo))
        {
            Console.WriteLine("Codigo invalido.");
            return;
        }

        Console.Write("Nombre: ");
        string nombre = Console.ReadLine() ?? string.Empty;

        Console.Write("Antiguedad (años, int): ");
        if (!int.TryParse(Console.ReadLine(), out int antiguedad))
        {
            Console.WriteLine("Antiguedad invalida.");
            return;
        }

        var empleado = new Empleado(codigo, nombre, antiguedad);
        arbol.Agrega(empleado);
        Console.WriteLine("Empleado agregado.");
    }
}