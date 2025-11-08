using System;

namespace Semana12_LibreriaArrays
{
    internal class Program
    {
        static void Main(string[] args)     
        {
            Libreria libreria = new Libreria();
            string opcion;

            do
            {
                Console.Clear();
                Console.WriteLine("=== SISTEMA DE LIBRERÍA (SEMANA 12) ===");
                Console.WriteLine("1) Registrar libro");
                Console.WriteLine("2) Mostrar libros");
                Console.WriteLine("3) Modificar libro");
                Console.WriteLine("4) Eliminar libro");
                Console.WriteLine("5) Salir");
                Console.Write("Seleccione una opción: ");
                opcion = Console.ReadLine();

                try
                {
                    if (opcion == "1")
                    {
                        Console.Write("Nombre del libro: ");
                        string nombre = Console.ReadLine();

                        Console.Write("Precio (coma o punto decimal): ");
                        string precioTexto = Console.ReadLine();

                        libreria.Registrar(nombre, precioTexto);
                        Console.WriteLine(" Libro registrado correctamente.");
                        Pausa();
                    }
                    else if (opcion == "2")
                    {
                        Console.WriteLine(libreria.Mostrar());
                        Pausa();
                    }
                    else if (opcion == "3")
                    {
                        Console.Write("Nombre del libro a modificar: ");
                        string actual = Console.ReadLine();

                        Console.Write("Nuevo nombre: ");
                        string nuevoNombre = Console.ReadLine();

                        Console.Write("Nuevo precio: ");
                        string nuevoPrecio = Console.ReadLine();

                        libreria.Modificar(actual, nuevoNombre, nuevoPrecio);
                        Console.WriteLine("✅ Libro modificado correctamente.");
                        Pausa();
                    }
                    else if (opcion == "4")
                    {
                        Console.Write("Nombre del libro a eliminar: ");
                        string eliminar = Console.ReadLine();

                        libreria.Eliminar(eliminar);
                        Console.WriteLine("✅ Libro eliminado correctamente.");
                        Pausa();
                    }
                    else if (opcion != "5")
                    {
                        Console.WriteLine("Opción inválida. Intente nuevamente.");
                        Pausa();
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("⚠️ " + ex.Message);
                    Pausa();
                }

            } while (opcion != "5");
        }

        static void Pausa()
        {
            Console.WriteLine();
            Console.Write("Presione ENTER para continuar...");
            Console.ReadLine();
        }
    }
}
