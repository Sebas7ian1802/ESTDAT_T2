using System;
using System.Text;

Console.WriteLine("Demostración: Lista enlazada doble de Carros\n");

// Construcción de la lista principal usando AgregaDosCarros
var listaCar = new ListaEnlazadaS();

// Llamadas a AgregaDosCarros (cada llamada inserta 1 carro al inicio y 1 al final)
listaCar.AgregaDosCarros("Toyota", 4, 1800, "Honda", 2, 1200);
listaCar.AgregaDosCarros("Ford", 4, 2000, "Chevrolet", 4, 2200);
listaCar.AgregaDosCarros("BMW", 2, 3000, "Audi", 4, 2500);
listaCar.AgregaDosCarros("Kia", 4, 1600, "Hyundai", 4, 1400);

// Tras 4 llamadas hay 8 elementos. Para obtener 7 elementos (requisito) se quita el penúltimo.
listaCar.QuitaPenultimoCarro();

Console.WriteLine("ListaCar (7 elementos):");
Console.WriteLine(listaCar.ToString());
Console.WriteLine();

// Demostrar ListaSegunPuerta: devolverá los carros con puertas entre min y max (inclusive)
var filtrada = listaCar.ListaSegunPuerta(2, 2); // ejemplo: solo 2 puertas
Console.WriteLine("Carros con 2 puertas:");
Console.WriteLine(filtrada.ToString());
Console.WriteLine();

// Mostrar efecto de QuitaPenultimoCarro sobre la lista original
Console.WriteLine("Quitar penúltimo de listaCar (ejecutando otra vez):");
listaCar.QuitaPenultimoCarro();
Console.WriteLine(listaCar.ToString());
Console.WriteLine();

// Construir segunda lista para mezclar
var segunda = new ListaEnlazadaS();
segunda.AgregaDosCarros("Seat", 4, 1600, "Renault", 4, 1400);
segunda.AgregaDosCarros("Mazda", 2, 2000, "Fiat", 4, 1200);

Console.WriteLine("Segunda lista:");
Console.WriteLine(segunda.ToString());
Console.WriteLine();

// Mezclar posición impar de listaCar con posición par de segunda
var mezcla = listaCar.MezclaParImpar(segunda);
Console.WriteLine("Mezcla (impares de listaCar + pares de segunda):");
Console.WriteLine(mezcla.ToString());


// --------------------- Clases ---------------------
class Carro
{
    public string marca;
    public int puertas;
    public double ccmotor;

    public Carro(string marca, int puertas, double ccmotor)
    {
        this.marca = marca;
        this.puertas = puertas;
        this.ccmotor = ccmotor;
    }

    public override string ToString()
    {
        return $"{marca} ({ccmotor}cc)";
    }
}

class NodoS
{
    public Carro dato;
    public NodoS siguiente;
    public NodoS anterior;

    public NodoS(Carro dato)
    {
        this.dato = dato;
    }
}

class ListaEnlazadaS
{
    public NodoS primero;
    public NodoS ultimo;

    // Inserta un Carro al inicio
    private void AgregarAlInicio(Carro c)
    {
        var nodo = new NodoS(c);
        if (primero == null)
        {
            primero = ultimo = nodo;
        }
        else
        {
            nodo.siguiente = primero;
            primero.anterior = nodo;
            primero = nodo;
        }
    }

    // Inserta un Carro al final
    private void AgregarAlFinal(Carro c)
    {
        var nodo = new NodoS(c);
        if (ultimo == null)
        {
            primero = ultimo = nodo;
        }
        else
        {
            ultimo.siguiente = nodo;
            nodo.anterior = ultimo;
            ultimo = nodo;
        }
    }

    // 1. AgregaDosCarros: crea un carro e inserta al inicio y otro e inserta al final
    public void AgregaDosCarros(string marca1, int puertas1, double ccmotor1, string marca2, int puertas2, double ccmotor2)
    {
        var c1 = new Carro(marca1, puertas1, ccmotor1);
        var c2 = new Carro(marca2, puertas2, ccmotor2);
        AgregarAlInicio(c1);
        AgregarAlFinal(c2);
    }

    // 2. ToString: recorre la lista mostrando marca y ccmotor de cada carro
    public override string ToString()
    {
        var sb = new StringBuilder();
        var actual = primero;
        int idx = 1;
        while (actual != null)
        {
            sb.AppendLine($"{idx}. {actual.dato.ToString()} - Puertas: {actual.dato.puertas}");
            actual = actual.siguiente;
            idx++;
        }
        return sb.Length == 0 ? "(vacía)" : sb.ToString().TrimEnd();
    }

    // Helper: cuenta nodos
    public int Count()
    {
        int c = 0;
        var a = primero;
        while (a != null) { c++; a = a.siguiente; }
        return c;
    }

    // 3. ListaSegunPuerta(min, max): devuelve una nueva lista con carros cuyas puertas estén en el rango [min,max]
    public ListaEnlazadaS ListaSegunPuerta(int min, int max)
    {
        var resultado = new ListaEnlazadaS();
        var actual = primero;
        while (actual != null)
        {
            if (actual.dato.puertas >= min && actual.dato.puertas <= max)
            {
                // Insertar al final para mantener orden
                resultado.AgregarAlFinal(new Carro(actual.dato.marca, actual.dato.puertas, actual.dato.ccmotor));
            }
            actual = actual.siguiente;
        }
        return resultado;
    }

    // 4. QuitaPenultimoCarro(): quita el penúltimo si existe, sino quita el último
    public void QuitaPenultimoCarro()
    {
        if (primero == null) return; // vacía
        if (primero == ultimo)
        {
            // un solo elemento: quitarlo
            primero = ultimo = null;
            return;
        }

        // Si sólo hay dos nodos, el penúltimo es 'primero'
        if (primero.siguiente == ultimo)
        {
            // quitar penúltimo (primero)
            primero = ultimo;
            primero.anterior = null;
            return;
        }

        // Hay al menos 3 nodos: penúltimo = ultimo.anterior
        var penultimo = ultimo.anterior;
        var antes = penultimo.anterior;

        if (antes != null)
        {
            antes.siguiente = ultimo;
            ultimo.anterior = antes;
            // penultimo queda desconectado y será recolectado
        }
        else
        {
            // Si no hay 'antes', entonces penultimo era primero (caso ya manejado, pero por seguridad)
            primero = ultimo;
            ultimo.anterior = null;
        }
    }

    // 5. MezclaParImpar(segunda): mezcla impares de esta lista con pares de la segunda lista
    public ListaEnlazadaS MezclaParImpar(ListaEnlazadaS segunda)
    {
        var resultado = new ListaEnlazadaS();

        // Extraer nodos impares de la lista original
        var nombresImpares = new System.Collections.Generic.List<Carro>();
        var a = this.primero;
        int idx = 1;
        while (a != null)
        {
            if (idx % 2 == 1)
                nombresImpares.Add(new Carro(a.dato.marca, a.dato.puertas, a.dato.ccmotor));
            a = a.siguiente;
            idx++;
        }

        // Extraer nodos pares de la segunda lista
        var nombresParesSegunda = new System.Collections.Generic.List<Carro>();
        var b = segunda?.primero;
        idx = 1;
        while (b != null)
        {
            if (idx % 2 == 0)
                nombresParesSegunda.Add(new Carro(b.dato.marca, b.dato.puertas, b.dato.ccmotor));
            b = b.siguiente;
            idx++;
        }

        // Intercalar: impar de original, par de segunda, ...
        int i = 0, j = 0;
        while (i < nombresImpares.Count || j < nombresParesSegunda.Count)
        {
            if (i < nombresImpares.Count)
            {
                resultado.AgregarAlFinal(nombresImpares[i]);
                i++;
            }
            if (j < nombresParesSegunda.Count)
            {
                resultado.AgregarAlFinal(nombresParesSegunda[j]);
                j++;
            }
        }

        return resultado;
    }
}
