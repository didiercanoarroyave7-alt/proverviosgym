-- =============================================
-- GYMFIT - Script de Base de Datos
-- SQL Server / SQL Server Express
-- =============================================

USE master;
GO

IF EXISTS (SELECT name FROM sys.databases WHERE name = N'GymDB')
BEGIN
    ALTER DATABASE GymDB SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
    DROP DATABASE GymDB;
END
GO

CREATE DATABASE GymDB;
GO

USE GymDB;
GO

-- Roles
CREATE TABLE Roles (
    IdRol INT IDENTITY(1,1) PRIMARY KEY,
    NombreRol NVARCHAR(50) NOT NULL UNIQUE,
    Descripcion NVARCHAR(200) NULL
);

-- Usuarios (empleados)
CREATE TABLE Usuarios (
    IdUsuario INT IDENTITY(1,1) PRIMARY KEY,
    Documento NVARCHAR(20) NOT NULL UNIQUE,
    NombreCompleto NVARCHAR(100) NOT NULL,
    Telefono NVARCHAR(20) NULL,
    NombreUsuario NVARCHAR(50) NOT NULL UNIQUE,
    ContrasenaHash NVARCHAR(100) NOT NULL, -- Por simplicidad se guarda en texto plano (mejorar con hash)
    IdRol INT NOT NULL REFERENCES Roles(IdRol),
    Estado BIT NOT NULL DEFAULT 1,
    FechaRegistro DATETIME NOT NULL DEFAULT GETDATE()
);

-- Planes de membresía
CREATE TABLE Planes (
    IdPlan INT IDENTITY(1,1) PRIMARY KEY,
    NombrePlan NVARCHAR(80) NOT NULL,
    DuracionDias INT NOT NULL CHECK (DuracionDias > 0),
    Precio DECIMAL(12,2) NOT NULL CHECK (Precio >= 0),
    Descripcion NVARCHAR(250) NULL,
    Estado BIT NOT NULL DEFAULT 1
);

-- Clientes
CREATE TABLE Clientes (
    IdCliente INT IDENTITY(1,1) PRIMARY KEY,
    Documento NVARCHAR(20) NOT NULL UNIQUE,
    NombreCompleto NVARCHAR(100) NOT NULL,
    Telefono NVARCHAR(20) NULL,
    ContactoEmergencia NVARCHAR(100) NULL,
    TelefonoEmergencia NVARCHAR(20) NULL,
    FechaRegistro DATETIME NOT NULL DEFAULT GETDATE(),
    Estado BIT NOT NULL DEFAULT 1
);

-- Membresías
CREATE TABLE Membresias (
    IdMembresia INT IDENTITY(1,1) PRIMARY KEY,
    IdCliente INT NOT NULL REFERENCES Clientes(IdCliente),
    IdPlan INT NOT NULL REFERENCES Planes(IdPlan),
    FechaInicio DATE NOT NULL,
    FechaFin DATE NOT NULL,
    Estado NVARCHAR(20) NOT NULL DEFAULT 'Activa' -- Activa / Vencida / Cancelada
);

-- Pagos
CREATE TABLE Pagos (
    IdPago INT IDENTITY(1,1) PRIMARY KEY,
    IdMembresia INT NOT NULL REFERENCES Membresias(IdMembresia),
    IdUsuarioCobro INT NOT NULL REFERENCES Usuarios(IdUsuario),
    Monto DECIMAL(12,2) NOT NULL,
    MetodoPago NVARCHAR(40) NOT NULL DEFAULT 'Efectivo',
    FechaPago DATETIME NOT NULL DEFAULT GETDATE()
);

-- Registros de acceso
CREATE TABLE RegistrosAcceso (
    IdAcceso INT IDENTITY(1,1) PRIMARY KEY,
    IdCliente INT NOT NULL REFERENCES Clientes(IdCliente),
    FechaHoraIngreso DATETIME NOT NULL DEFAULT GETDATE(),
    AccesoPermitido BIT NOT NULL,
    Observacion NVARCHAR(200) NULL
);

-- Rutinas (entrenador)
CREATE TABLE Rutinas (
    IdRutina INT IDENTITY(1,1) PRIMARY KEY,
    IdCliente INT NOT NULL REFERENCES Clientes(IdCliente),
    IdEntrenador INT NOT NULL REFERENCES Usuarios(IdUsuario),
    Objetivo NVARCHAR(100) NULL,
    DescripcionRutina NVARCHAR(MAX) NULL,
    ObservacionesFisicas NVARCHAR(300) NULL,
    FechaAsignacion DATETIME NOT NULL DEFAULT GETDATE()
);

-- Novedades del personal
CREATE TABLE NovedadesPersonal (
    IdNovedad INT IDENTITY(1,1) PRIMARY KEY,
    IdUsuario INT NOT NULL REFERENCES Usuarios(IdUsuario),
    Area NVARCHAR(80) NULL,
    Tipo NVARCHAR(50) NULL, -- Ingreso, Salida, Novedad
    Descripcion NVARCHAR(300) NULL,
    FechaHora DATETIME NOT NULL DEFAULT GETDATE()
);

-- =====================
-- Datos iniciales
-- =====================

INSERT INTO Roles (NombreRol, Descripcion) VALUES
('Administrador', 'Acceso total al sistema'),
('Recepcionista', 'Registro de clientes, ventas y control de acceso'),
('Entrenador', 'Consulta de clientes y asignación de rutinas'),
('Limpieza', 'Registro de turnos y novedades de aseo');

-- Usuario admin (contraseña: admin123)
INSERT INTO Usuarios (Documento, NombreCompleto, Telefono, NombreUsuario, ContrasenaHash, IdRol)
VALUES ('1000000001', 'Administrador GYMFIT', '3000000000', 'admin', 'admin123', 1);

-- Recepcionista de prueba
INSERT INTO Usuarios (Documento, NombreCompleto, Telefono, NombreUsuario, ContrasenaHash, IdRol)
VALUES ('1000000002', 'Ana Recepcion', '3001111111', 'recepcion', '1234', 2);

-- Entrenador de prueba
INSERT INTO Usuarios (Documento, NombreCompleto, Telefono, NombreUsuario, ContrasenaHash, IdRol)
VALUES ('1000000003', 'Carlos Entrenador', '3002222222', 'entrenador', '1234', 3);

INSERT INTO Planes (NombrePlan, DuracionDias, Precio, Descripcion) VALUES
('Tiquetera / Día', 1, 15000, 'Acceso por un día'),
('Quincenal', 15, 60000, '15 días de acceso'),
('Mensual', 30, 100000, '30 días de acceso'),
('Trimestral', 90, 270000, '90 días de acceso');

PRINT 'Base de datos GymDB creada correctamente con datos de prueba.';
PRINT 'Usuario admin / admin123';
PRINT 'Usuario recepcion / 1234';
PRINT 'Usuario entrenador / 1234';
GO
