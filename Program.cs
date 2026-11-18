using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;

namespace CarteleraCine
{
    public class Program
    {
        public static void Main(string[] args)
        {
            FuncionRepositorio repo = new FuncionRepositorio();
            int opcion = 0;

            do
            {
                Console.WriteLine();
                Console.WriteLine("1. Registrar función");
                Console.WriteLine("2. Listar funciones");
                Console.WriteLine("3. Buscar función por Id");
                Console.WriteLine("4. Actualizar precio");
                Console.WriteLine("5. Eliminar función");
                Console.WriteLine("6. Salir");
                Console.Write("Opción: ");

                if (!int.TryParse(Console.ReadLine(), out opcion))
                {
                    Console.WriteLine("Opción inválida.");
                    continue;
                }

                try
                {
                    switch (opcion)
                    {
                        case 1:
                            Registrar(repo);
                            break;
                        case 2:
                            Listar(repo);
                            break;
                        case 3:
                            Buscar(repo);
                            break;
                        case 4:
                            ActualizarPrecio(repo);
                            break;
                        case 5:
                            Eliminar(repo);
                            break;
                        case 6:
                            Console.WriteLine("Saliendo...");
                            break;
                        default:
                            Console.WriteLine("Opción no válida.");
                            break;
                    }
                }
                catch (SqlException ex)
                {
                    Console.WriteLine("Error de base de datos: " + ex.Message);
                }

            } while (opcion != 6);
        }

        private static void Registrar(FuncionRepositorio repo)
        {
            Console.Write("Película: ");
            string pelicula = Console.ReadLine();

            Console.Write("Sala: ");
            string sala = Console.ReadLine();

            Console.Write("Precio: ");
            string textoPrecio = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(pelicula) ||
                string.IsNullOrWhiteSpace(sala) ||
                !decimal.TryParse(textoPrecio, out decimal precio) ||
                precio <= 0)
            {
                Console.WriteLine("Datos inválidos.");
                return;
            }

            Funcion nueva = new Funcion();
            nueva.Pelicula = pelicula;
            nueva.Sala = sala;
            nueva.Precio = precio;

            repo.Insertar(nueva);
            Console.WriteLine("Función registrada.");
        }

        private static void Listar(FuncionRepositorio repo)
        {
            List<Funcion> lista = repo.ListarTodos();

            if (lista.Count == 0)
            {
                Console.WriteLine("No hay funciones registradas.");
                return;
            }

            decimal total = 0;

            for (int i = 0; i < lista.Count; i++)
            {
                Console.WriteLine(lista[i].ToString());
                total = total + lista[i].Precio;
            }

            Console.WriteLine("TOTAL DE PRECIOS: Q " + total.ToString("F2"));
        }

        private static void Buscar(FuncionRepositorio repo)
        {
            Console.Write("Id: ");
            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                Console.WriteLine("Id inválido.");
                return;
            }

            Funcion encontrada = repo.BuscarPorId(id);

            if (encontrada == null)
            {
                Console.WriteLine("No existe ese Id.");
            }
            else
            {
                Console.WriteLine(encontrada.ToString());
            }
        }

        private static void ActualizarPrecio(FuncionRepositorio repo)
        {
            Console.Write("Id: ");
            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                Console.WriteLine("Id inválido.");
                return;
            }

            Funcion existente = repo.BuscarPorId(id);

            if (existente == null)
            {
                Console.WriteLine("No existe ese Id.");
                return;
            }

            Console.Write("Nuevo precio: ");
            if (!decimal.TryParse(Console.ReadLine(), out decimal nuevoPrecio) || nuevoPrecio <= 0)
            {
                Console.WriteLine("Precio inválido.");
                return;
            }

            existente.Precio = nuevoPrecio;

            bool ok = repo.Actualizar(existente);

            if (ok)
            {
                Console.WriteLine("Función actualizada.");
            }
            else
            {
                Console.WriteLine("No existe ese Id.");
            }
        }

        private static void Eliminar(FuncionRepositorio repo)
        {
            Console.Write("Id: ");
            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                Console.WriteLine("Id inválido.");
                return;
            }

            bool ok = repo.Eliminar(id);

            if (ok)
            {
                Console.WriteLine("Función eliminada.");
            }
            else
            {
                Console.WriteLine("No existe ese Id.");
            }
        }
    }
}