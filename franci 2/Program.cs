using System;

namespace InventarioBiblioteca
{
    class Program
    {
        // Matriz de 5 libros (filas) y 3 sucursales (columnas)
        static int[,] stock = new int[5, 3] {
            { 12, 5,  0 },  // Libro 0
            { 3,  15, 8 },  // Libro 1
            { 1,  2,  4 },  // Libro 2
            { 7,  0,  12 }, // Libro 3
            { 20, 10, 5 }   // Libro 4
        };

        static void Main(string[] args)
        {
            int opcion;
            do
            {
                Console.WriteLine("\n====== INVENTARIO POR SUCURSAL ======");
                Console.WriteLine("1. Mostrar inventario completo");
                Console.WriteLine("2. Consultar disponibilidad");
                Console.WriteLine("3. Actualizar disponibilidad");
                Console.WriteLine("4. Total disponible de un libro");
                Console.WriteLine("5. Total de inventario por sucursal");
                Console.WriteLine("6. Mostrar libros con bajo inventario");
                Console.WriteLine("7. Sucursal con mayor disponibilidad");
                Console.WriteLine("0. Salir");
                Console.Write("Seleccione una opción: ");

                if (!int.TryParse(Console.ReadLine(), out opcion)) opcion = -1;

                switch (opcion)
                {
                    case 1: MostrarMatriz(); break;
                    case 2: ConsultarDisponibilidad(); break;
                    case 3: ActualizarDisponibilidad(); break;
                    case 4: TotalPorLibro(); break;
                    case 5: TotalPorSucursal(); break;
                    case 6: MostrarBajoInventario(); break;
                    case 7: MayorDisponibilidadLibro(); break;
                    case 0: Console.WriteLine("Cerrando sistema..."); break;
                    default: Console.WriteLine("Opción inválida."); break;
                }
            } while (opcion != 0);
        }

        // Complejidad O(F x C)
        static void MostrarMatriz()
        {
            Console.WriteLine("\n--- INVENTARIO COMPLETO ---");
            Console.WriteLine("\tSuc 0\tSuc 1\tSuc 2");
            for (int i = 0; i < stock.GetLength(0); i++)
            {
                Console.Write($"Libro {i}\t");
                for (int j = 0; j < stock.GetLength(1); j++)
                {
                    Console.Write($"{stock[i, j]}\t");
                }
                Console.WriteLine();
            }
        }

        // Complejidad O(1)
        static void ConsultarDisponibilidad()
        {
            Console.Write("Ingrese número de libro (0-4): ");
            int lib = int.Parse(Console.ReadLine());
            Console.Write("Ingrese número de sucursal (0-2): ");
            int suc = int.Parse(Console.ReadLine());

            if (ValidarIndices(lib, suc))
                Console.WriteLine($"\nDisponibilidad del Libro {lib} en Sucursal {suc}: {stock[lib, suc]} unidades.");
        }

        // Complejidad O(1)
        static void ActualizarDisponibilidad()
        {
            Console.Write("Ingrese número de libro (0-4): ");
            int lib = int.Parse(Console.ReadLine());
            Console.Write("Ingrese número de sucursal (0-2): ");
            int suc = int.Parse(Console.ReadLine());

            if (ValidarIndices(lib, suc))
            {
                Console.Write("Ingrese la nueva cantidad: ");
                int cant = int.Parse(Console.ReadLine());
                stock[lib, suc] = cant;
                Console.WriteLine("Inventario actualizado exitosamente.");
            }
        }

        // Complejidad O(C) - Iterar columnas
        static void TotalPorLibro()
        {
            Console.Write("Ingrese número de libro (0-4): ");
            int lib = int.Parse(Console.ReadLine());

            if (lib >= 0 && lib < stock.GetLength(0))
            {
                int total = 0;
                for (int j = 0; j < stock.GetLength(1); j++)
                {
                    total += stock[lib, j];
                }
                Console.WriteLine($"\nTotal de unidades del Libro {lib} en todas las sucursales: {total}");
            }
        }

        // Complejidad O(F) - Iterar filas
        static void TotalPorSucursal()
        {
            Console.Write("Ingrese número de sucursal (0-2): ");
            int suc = int.Parse(Console.ReadLine());

            if (suc >= 0 && suc < stock.GetLength(1))
            {
                int total = 0;
                for (int i = 0; i < stock.GetLength(0); i++)
                {
                    total += stock[i, suc];
                }
                Console.WriteLine($"\nInventario total almacenado en la Sucursal {suc}: {total} libros.");
            }
        }

        // Complejidad O(F x C)
        static void MostrarBajoInventario()
        {
            Console.WriteLine("\n--- ALERTA: BAJO INVENTARIO (<= 3 unidades) ---");
            for (int i = 0; i < stock.GetLength(0); i++)
            {
                for (int j = 0; j < stock.GetLength(1); j++)
                {
                    if (stock[i, j] <= 3)
                    {
                        Console.WriteLine($"Libro {i} en Sucursal {j} tiene {stock[i, j]} unidades.");
                    }
                }
            }
        }

        // Complejidad O(C)
        static void MayorDisponibilidadLibro()
        {
            Console.Write("Ingrese número de libro (0-4): ");
            int lib = int.Parse(Console.ReadLine());

            if (lib >= 0 && lib < stock.GetLength(0))
            {
                int max = -1;
                int sucMax = -1;

                for (int j = 0; j < stock.GetLength(1); j++)
                {
                    if (stock[lib, j] > max)
                    {
                        max = stock[lib, j];
                        sucMax = j;
                    }
                }
                Console.WriteLine($"\nLa mayor disponibilidad del Libro {lib} está en la Sucursal {sucMax} con {max} unidades.");
            }
        }

        static bool ValidarIndices(int f, int c)
        {
            if (f >= 0 && f < stock.GetLength(0) && c >= 0 && c < stock.GetLength(1))
                return true;

            Console.WriteLine("Error: Índices fuera de rango.");
            return false;
        }
    }
}