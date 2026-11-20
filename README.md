# CarteleraCine

Sistema de cartelera de cine desarrollado en C# con acceso a base de datos SQL Server LocalDB usando ADO.NET.

## Descripción

Programa de consola que permite registrar, listar, buscar, actualizar y eliminar funciones de cine.  
Los datos se almacenan en la base de datos `CarteleraDB`.

## Tecnologías usadas

- C#
- .NET 8
- ADO.NET (`Microsoft.Data.SqlClient`)
- SQL Server LocalDB

## Estructura del proyecto

- `Funcion.cs` → Clase del dominio
- `FuncionRepositorio.cs` → Acceso a datos (repositorio)
- `Program.cs` → Menú de consola
- `01_crear_CarteleraDB.sql` → Script de creación de la base de datos

## Cómo ejecutar

1. Ejecutar el script `01_crear_CarteleraDB.sql` en LocalDB.
2. Abrir el proyecto en Visual Studio.
3. Ejecutar el programa (F5).

## Funcionalidades

1. Registrar función
2. Listar funciones (con total de precios)
3. Buscar función por Id
4. Actualizar precio
5. Eliminar función
6. Salir

## Validaciones

- Película y sala no pueden estar vacías
- Precio debe ser mayor a 0
- Id debe ser un número válido
- Manejo de errores de base de datos con try/catch

  Autor: Jeison Hair Donis Martinez 
