# 🚀 Clean Architecture Template

¡Bienvenido! Este repositorio es una plantilla base (boilerplate) estructurada bajo los principios de **Clean Architecture** (Arquitectura Limpia) empleando **.NET**. Está diseñada para servir como punto de partida sólido, escalable y desacoplado para cualquier proyecto moderno de backend.

Incluye un sistema completo y preconfigurado de **Autenticación y Autorización (Login / Registro)** utilizando las abstracciones de seguridad de la plataforma.

---

## 🌟 Características Destacadas

* **Separación Estricta de Capas:** Dominio en el núcleo absoluto, sin dependencias externas, rodeado por la capa de Aplicación y la de Infraestructura/Presentación en los bordes exteriores.
* **Autenticación Out-of-the-Box:** Gestión completa del flujo de usuarios (Registro e Inicio de sesión).
* **Seguridad Robusta:** Integración nativa con componentes de identidad (`IdentityModel`) y manejo seguro de credenciales.
* **Inyección de Dependencias Limpia:** Registro modularizado de servicios para mantener el punto de entrada de la API ordenado y legible.

---

## 🏗️ Estructura del Proyecto

La solución está organizada de forma que se respete estrictamente la regla de dependencia hacia el centro:

```text
📁 CleanArchitectureTemplate/
├── 📂 Domain/          # Entidades de negocio, agregados, value objects y contratos (interfaces) base.
├── 📂 Application/     # Casos de uso de la aplicación, DTOs, validaciones y lógica de orquestación.
├── 📂 Infrastructure/  # Implementaciones concretas de persistencia, acceso a datos, repositorios y servicios externos.
└── 📂 WebApi/          # Punto de entrada del sistema. Controladores, endpoints, configuraciones de Swagger y controladores de HTTP.
```

---

## 🛠️ Stack Tecnológico
- C# / .NET
- ASP.NET Core Web API
- IdentityModel (Seguridad y Tokens)
- *¡Y más por expandir! (Entity Framework Core, MediatR, FluentValidation, etc.)*