# GestionUniversitaria

Aplicación web desarrollada con ASP.NET Core MVC para la gestión de información universitaria, utilizando una base de datos NoSQL embebida mediante LiteDB.

## Objetivo

El proyecto parte de una aplicación desarrollada originalmente como aplicación de consola y actualmente se encuentra migrada a ASP.NET Core MVC.

La evolución del proyecto tiene como objetivo utilizar .NET para consumir bases de datos NoSQL mediante sus respectivos drivers.

## Tecnologías

- C#
- .NET 8
- ASP.NET Core MVC
- LiteDB 5.0.21
- Razor
- Visual Studio
- Git / GitHub

## Arquitectura actual

La aplicación utiliza una arquitectura basada en:

```text
Browser
   │
   ▼
ASP.NET Core Middleware
   │
   ▼
Routing
   │
   ▼
Controller
   │
   ▼
Service
   │
   ▼
LiteDbContext
   │
   ▼
LiteDB