Create database FacturacionBD4215271w1
GO
USE FacturacionBD4215271w1;
GO

-- Tablas
CREATE TABLE FormaPago (
    Id_FormaPago INT PRIMARY KEY IDENTITY(1,1),
    Nombre VARCHAR(50) NOT NULL
);
GO

CREATE TABLE Articulo (
    Id_Articulo INT PRIMARY KEY IDENTITY(1,1),
    Nombre VARCHAR(100) NOT NULL,
    PrecioUnitario DECIMAL(10,2) NOT NULL
);
GO

CREATE TABLE Factura (
    Id_Factura INT PRIMARY KEY IDENTITY(1,1),
    Fecha DATE NOT NULL,
    Cliente VARCHAR(100) NOT NULL,
    Id_FormaPago INT NOT NULL,
    FOREIGN KEY (Id_FormaPago) REFERENCES FormaPago(Id_FormaPago)
);
GO

CREATE TABLE DetalleFactura (
    Id_Detalle INT PRIMARY KEY IDENTITY(1,1),
    Id_Factura INT NOT NULL,
    Id_Articulo INT NOT NULL,
    Cantidad INT NOT NULL,
    FOREIGN KEY (Id_Factura) REFERENCES Factura(Id_Factura),
    FOREIGN KEY (Id_Articulo) REFERENCES Articulo(Id_Articulo)
);
GO

-- Datos iniciales
INSERT INTO FormaPago (Nombre) VALUES
('Efectivo'), ('Tarjeta Débito'), ('Tarjeta Crédito'), ('Transferencia'), ('MercadoPago');
GO

INSERT INTO Articulo (Nombre, PrecioUnitario) VALUES
('Mouse Logitech', 5000.00),
('Teclado Redragon', 12000.00),
('Monitor Samsung 24"', 80000.00),
('Auriculares HyperX', 25000.00),
('Notebook Lenovo', 350000.00);
GO

INSERT INTO Factura (Fecha, Cliente, Id_FormaPago) VALUES
('2025-09-01', 'Juan Pérez', 1),
('2025-09-02', 'Ana Gómez', 2),
('2025-09-03', 'Carlos López', 3),
('2025-09-04', 'María Díaz', 4),
('2025-09-05', 'Pedro Ruiz', 5);
GO

INSERT INTO DetalleFactura (Id_Factura, Id_Articulo, Cantidad) VALUES
(1, 1, 2),
(1, 2, 1),
(2, 3, 1),
(3, 4, 3),
(4, 5, 1);
GO

USE FacturacionBD4215271w1;
GO
-- SP: Facturas
CREATE OR ALTER PROCEDURE sp_Factura_GetAll
AS
BEGIN
    SELECT f.Id_Factura, f.Fecha, f.Cliente, fp.Nombre AS FormaPago
    FROM Factura f
    INNER JOIN FormaPago fp ON f.Id_FormaPago = fp.Id_FormaPago
    ORDER BY f.Id_Factura;
END;
GO

CREATE OR ALTER PROCEDURE sp_Factura_GetById
    @Id_Factura INT
AS
BEGIN
    SELECT f.Id_Factura, f.Fecha, f.Cliente, fp.Nombre AS FormaPago,
           a.Nombre AS Articulo, df.Cantidad, a.PrecioUnitario,
           (df.Cantidad * a.PrecioUnitario) AS Subtotal
    FROM Factura f
    INNER JOIN FormaPago fp ON f.Id_FormaPago = fp.Id_FormaPago
    INNER JOIN DetalleFactura df ON f.Id_Factura = df.Id_Factura
    INNER JOIN Articulo a ON df.Id_Articulo = a.Id_Articulo
    WHERE f.Id_Factura = @Id_Factura;
END;
GO


CREATE OR ALTER PROCEDURE sp_Factura_Save
    @Id_Factura INT = NULL,
    @Fecha DATE,
    @Cliente VARCHAR(100),
    @Id_FormaPago INT
AS
BEGIN
    IF @Id_Factura IS NULL
        INSERT INTO Factura (Fecha, Cliente, Id_FormaPago)
        VALUES (@Fecha, @Cliente, @Id_FormaPago);
    ELSE
        UPDATE Factura
        SET Fecha = @Fecha,
            Cliente = @Cliente,
            Id_FormaPago = @Id_FormaPago
        WHERE Id_Factura = @Id_Factura;
END;
GO

CREATE OR ALTER PROCEDURE sp_Factura_Delete
    @Id_Factura INT
AS
BEGIN
    DELETE FROM DetalleFactura WHERE Id_Factura = @Id_Factura;
    DELETE FROM Factura WHERE Id_Factura = @Id_Factura;
END;
GO

USE FacturacionBD4215271w1;
GO
-- SP: Productos
CREATE  PROCEDURE SP_RECUPERAR_PRODUCTOS
AS
BEGIN
    SELECT 
        Id_Articulo AS codigo,
        Nombre AS n_producto,
        PrecioUnitario AS precio,
        0 AS stock,
        1 AS esta_activo
    FROM Articulo
    ORDER BY Nombre;
END;
GO
USE FacturacionBD4215271w1;
GO
CREATE  PROCEDURE SP_RECUPERAR_PRODUCTO_POR_CODIGO
    @codigo INT
AS
BEGIN
    SELECT 
        Id_Articulo AS codigo,
        Nombre AS n_producto,
        PrecioUnitario AS precio,
        0 AS stock,
        1 AS esta_activo
    FROM Articulo
    WHERE Id_Articulo = @codigo;
END;
GO

CREATE  PROCEDURE SP_GUARDAR_ARTICULO
    @codigo INT,
    @nombre VARCHAR(100),
    @precio DECIMAL(10,2)
AS
BEGIN
    IF @codigo = 0
        INSERT INTO Articulo (Nombre, PrecioUnitario)
        VALUES (@nombre, @precio);
    ELSE
        UPDATE Articulo
        SET Nombre = @nombre,
            PrecioUnitario = @precio
        WHERE Id_Articulo = @codigo;
END;
GO

CREATE  PROCEDURE SP_BAJA_ARTICULO
    @codigo INT
AS
BEGIN
    DELETE FROM Articulo
    WHERE Id_Articulo = @codigo;
END;
GO

CREATE  PROCEDURE SP_GUARDAR_DETALLE_FACTURA
    @Id_Factura INT,
    @Id_Articulo INT,
    @Cantidad INT
AS
BEGIN
    INSERT INTO DetalleFactura (Id_Factura, Id_Articulo, Cantidad)
    VALUES (@Id_Factura, @Id_Articulo, @Cantidad);
END;
GO



