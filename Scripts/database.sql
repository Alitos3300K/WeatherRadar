-- Crear base de datos
IF NOT EXISTS (SELECT name FROM sys.databases WHERE name = 'WeatherRadarDB')
    CREATE DATABASE WeatherRadarDB;
GO

USE WeatherRadarDB;
GO

-- Crear tabla de ciudades favoritas
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'CiudadesFavoritas')
BEGIN
    CREATE TABLE CiudadesFavoritas (
        Id              INT IDENTITY(1,1)   NOT NULL,
        NombreCiudad    NVARCHAR(200)       NOT NULL,
        Pais            NVARCHAR(100)       NOT NULL DEFAULT '',
        FechaGuardado   DATETIME            NOT NULL DEFAULT GETDATE(),

        CONSTRAINT PK_CiudadesFavoritas PRIMARY KEY (Id)
    );
END
GO

-- Índice para búsqueda rápida por nombre de ciudad
CREATE NONCLUSTERED INDEX IX_CiudadesFavoritas_Nombre
    ON CiudadesFavoritas(NombreCiudad);
GO

-- Stored Procedure para insertar ciudad favorita
CREATE OR ALTER PROCEDURE sp_InsertarFavorita
    @NombreCiudad   NVARCHAR(200),
    @Pais           NVARCHAR(100)
AS
BEGIN
    SET NOCOUNT ON;

    -- Solo inserta si no existe ya
    IF NOT EXISTS (SELECT 1 FROM CiudadesFavoritas WHERE NombreCiudad = @NombreCiudad)
    BEGIN
        INSERT INTO CiudadesFavoritas (NombreCiudad, Pais, FechaGuardado)
        VALUES (@NombreCiudad, @Pais, GETDATE());
    END
END
GO

-- Stored Procedure para obtener todas las favoritas
CREATE OR ALTER PROCEDURE sp_ObtenerFavoritas
AS
BEGIN
    SET NOCOUNT ON;

    SELECT Id, NombreCiudad, Pais, FechaGuardado
    FROM CiudadesFavoritas
    ORDER BY FechaGuardado DESC;
END
GO