using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;

namespace CarteleraCine
{
    public class FuncionRepositorio
    {
        private const string CadenaConexion =
            @"Server=(localdb)\mssqllocaldb;Database=CarteleraDB;Trusted_Connection=True;TrustServerCertificate=True;";

        public void Insertar(Funcion x)
        {
            using (SqlConnection conexion = new SqlConnection(CadenaConexion))
            {
                string sql = "INSERT INTO Funciones (Pelicula, Sala, Precio) VALUES (@Pelicula, @Sala, @Precio)";

                using (SqlCommand comando = new SqlCommand(sql, conexion))
                {
                    comando.Parameters.AddWithValue("@Pelicula", x.Pelicula);
                    comando.Parameters.AddWithValue("@Sala", x.Sala);
                    comando.Parameters.AddWithValue("@Precio", x.Precio);

                    conexion.Open();
                    comando.ExecuteNonQuery();
                }
            }
        }

        public List<Funcion> ListarTodos()
        {
            List<Funcion> lista = new List<Funcion>();

            using (SqlConnection conexion = new SqlConnection(CadenaConexion))
            {
                string sql = "SELECT Id, Pelicula, Sala, Precio FROM Funciones ORDER BY Id";

                using (SqlCommand comando = new SqlCommand(sql, conexion))
                {
                    conexion.Open();

                    using (SqlDataReader lector = comando.ExecuteReader())
                    {
                        while (lector.Read())
                        {
                            Funcion f = new Funcion();
                            f.Id = (int)lector["Id"];
                            f.Pelicula = lector["Pelicula"].ToString();
                            f.Sala = lector["Sala"].ToString();
                            f.Precio = (decimal)lector["Precio"];

                            lista.Add(f);
                        }
                    }
                }
            }

            return lista;
        }

        public Funcion? BuscarPorId(int id)
        {
            using (SqlConnection conexion = new SqlConnection(CadenaConexion))
            {
                string sql = "SELECT Id, Pelicula, Sala, Precio FROM Funciones WHERE Id = @Id";

                using (SqlCommand comando = new SqlCommand(sql, conexion))
                {
                    comando.Parameters.AddWithValue("@Id", id);
                    conexion.Open();

                    using (SqlDataReader lector = comando.ExecuteReader())
                    {
                        if (lector.Read())
                        {
                            Funcion f = new Funcion();
                            f.Id = (int)lector["Id"];
                            f.Pelicula = lector["Pelicula"].ToString();
                            f.Sala = lector["Sala"].ToString();
                            f.Precio = (decimal)lector["Precio"];
                            return f;
                        }
                    }
                }
            }

            return null;
        }

        public bool Actualizar(Funcion x)
        {
            using (SqlConnection conexion = new SqlConnection(CadenaConexion))
            {
                string sql = "UPDATE Funciones SET Precio = @Precio WHERE Id = @Id";

                using (SqlCommand comando = new SqlCommand(sql, conexion))
                {
                    comando.Parameters.AddWithValue("@Precio", x.Precio);
                    comando.Parameters.AddWithValue("@Id", x.Id);

                    conexion.Open();
                    int filas = comando.ExecuteNonQuery();
                    return filas == 1;
                }
            }
        }

        public bool Eliminar(int id)
        {
            using (SqlConnection conexion = new SqlConnection(CadenaConexion))
            {
                string sql = "DELETE FROM Funciones WHERE Id = @Id";

                using (SqlCommand comando = new SqlCommand(sql, conexion))
                {
                    comando.Parameters.AddWithValue("@Id", id);

                    conexion.Open();
                    int filas = comando.ExecuteNonQuery();
                    return filas == 1;
                }
            }
        }
    }
}