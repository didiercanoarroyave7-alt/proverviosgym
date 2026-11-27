# GYMFIT - Sistema de Gestión de Gimnasio

Proyecto final SENA - Análisis y Desarrollo de Software  
Ficha: 3412760 | Regional Antioquia

## Descripción

Aplicación web en **C# / ASP.NET Web Forms (.NET Framework 4.8)** con arquitectura por capas para administrar:

- Clientes y afiliación a planes
- Planes de membresía (tiquetera, quincenal, mensual, trimestral)
- Control de acceso / ingreso por documento
- Roles (Administrador, Recepcionista, Entrenador, Limpieza)
- Pagos asociados a membresías

## Estructura

```
GYMFIT/
├── CapaEntidad/          # Clases de dominio (Cliente, Plan, Membresia, Usuario...)
├── CapaDatos/            # Acceso a SQL Server (DAL)
├── CapaNegocio/          # Reglas de negocio (BL)
├── Database/GymDB.sql    # Script completo de base de datos + datos de prueba
├── Pages/                # Páginas ASPX del frontend (Login, Clientes, Planes, Acceso...)
└── README.md
```

## Requisitos

- Visual Studio 2019/2022
- SQL Server Express (o superior)
- .NET Framework 4.8

## Instalación rápida

1. **Crear la base de datos**
   - Abra SQL Server Management Studio
   - Ejecute el script `Database/GymDB.sql`
   - Se creará la base `GymDB` con roles, usuarios de prueba y 4 planes.

2. **Configurar la cadena de conexión**
   - En `Web.config` (o el archivo de configuración del proyecto web) ajuste:
   ```xml
   <connectionStrings>
     <add name="ConexionGym"
          connectionString="Server=localhost\SQLEXPRESS;Database=GymDB;Integrated Security=True;"
          providerName="System.Data.SqlClient" />
   </connectionStrings>
   ```
   (Cambie el nombre del servidor según su instalación.)

3. **Abrir el proyecto**
   - Si tiene la solución original, agregue las referencias a `CapaNegocio`.
   - O cree un nuevo proyecto ASP.NET Web Forms (.NET Framework) y referencie las tres capas.

4. **Usuarios de prueba**

   | Usuario     | Contraseña | Rol           |
   |-------------|------------|---------------|
   | admin       | admin123   | Administrador |
   | recepcion   | 1234       | Recepcionista |
   | entrenador  | 1234       | Entrenador    |

## Funcionalidades implementadas (frontend + capas)

| Módulo              | RF    | Descripción                                      |
|---------------------|-------|--------------------------------------------------|
| Login + roles       | RF01  | Inicio de sesión y restricción por rol           |
| Planes              | RF02  | Listar / crear planes                             |
| Clientes            | RF03  | Registrar y listar clientes                      |
| Afiliación + pago   | RF03  | Asignar plan, calcular vencimiento y registrar pago |
| Control de acceso   | RF04  | Validar documento → Permitido / Denegado + días restantes |

## Cómo continuar en Visual Studio

1. Abra su solución original o cree una nueva Web Application (Web Forms).
2. Agregue las carpetas `CapaEntidad`, `CapaDatos` y `CapaNegocio` como proyectos de biblioteca de clases (o copie los archivos).
3. En el proyecto web agregue referencias a las tres capas.
4. Copie las páginas de `Pages/` al proyecto web.
5. Actualice `Site.Master` para mostrar el menú según `Session["Rol"]`.
6. Compile y ejecute (F5).

## Notas importantes

- Las contraseñas se guardan en texto plano por simplicidad de la demostración académica. En producción use hash (BCrypt / PBKDF2).
- El cálculo de `FechaFin = FechaInicio + Plan.DuracionDias` está en `MembresiaBL.Afiliar`.
- El control de acceso registra cada intento en `RegistrosAcceso`.

## Autores

- Andres Felipe Gutierrez Peña
- Didier de Jesus Cano Arroyave
- Jerónimo Pelaez Rios

Instructora: Rosalba Mesa M  
Entrega: 04/12/2026
