

USE FacturacionDB;
GO

-- Tabla de Formas de Pago
CREATE TABLE FormaPago (
    Id_FormaPago INT PRIMARY KEY IDENTITY(1,1),
    Nombre VARCHAR(50) NOT NULL
);

-- Tabla de Artículos
CREATE TABLE Articulo (
    Id_Articulo INT PRIMARY KEY IDENTITY(1,1),
    Nombre VARCHAR(100) NOT NULL,
    PrecioUnitario DECIMAL(10,2) NOT NULL
);

-- Tabla de Facturas
CREATE TABLE Factura (
    Id_Factura INT PRIMARY KEY IDENTITY(1,1),
    Fecha DATE NOT NULL,
    Cliente VARCHAR(100) NOT NULL,
    Id_FormaPago INT NOT NULL,
    FOREIGN KEY (Id_FormaPago) REFERENCES FormaPago(Id_FormaPago)
);

-- Tabla de Detalles de Factura
CREATE TABLE DetalleFactura (
    Id_Detalle INT PRIMARY KEY IDENTITY(1,1),
    Id_Factura INT NOT NULL,
    Id_Articulo INT NOT NULL,
    Cantidad INT NOT NULL,
    FOREIGN KEY (Id_Factura) REFERENCES Factura(Id_Factura),
    FOREIGN KEY (Id_Articulo) REFERENCES Articulo(Id_Articulo)
);
GO

-- Insertar formas de pago
INSERT INTO FormaPago (Nombre) VALUES
('Efectivo'),
('Tarjeta Débito'),
('Tarjeta Crédito'),
('Transferencia'),
('MercadoPago');

-- Insertar artículos
INSERT INTO Articulo (Nombre, PrecioUnitario) VALUES
('Mouse Logitech', 5000.00),
('Teclado Redragon', 12000.00),
('Monitor Samsung 24"', 80000.00),
('Auriculares HyperX', 25000.00),
('Notebook Lenovo', 350000.00);

-- Insertar facturas
INSERT INTO Factura (Fecha, Cliente, Id_FormaPago) VALUES
('2025-09-01', 'Juan Pérez', 1),
('2025-09-02', 'Ana Gómez', 2),
('2025-09-03', 'Carlos López', 3),
('2025-09-04', 'María Díaz', 4),
('2025-09-05', 'Pedro Ruiz', 5);

-- Insertar detalle de facturas
INSERT INTO DetalleFactura (Id_Factura, Id_Articulo, Cantidad) VALUES
(1, 1, 2), -- Juan compró 2 Mouse
(1, 2, 1), -- Juan también compró 1 Teclado
(2, 3, 1), -- Ana compró 1 Monitor
(3, 4, 3), -- Carlos compró 3 Auriculares
(4, 5, 1); -- María compró 1 Notebook

Go
CREATE PROCEDURE sp_Factura_GetAll
AS
BEGIN
    SELECT f.Id_Factura, f.Fecha, f.Cliente, fp.Nombre AS FormaPago
    FROM Factura f
    INNER JOIN FormaPago fp ON f.Id_FormaPago = fp.Id_FormaPago
    ORDER BY f.Id_Factura;
END;
GO   -- <--- importante separar el siguiente CREATE PROCEDURE

CREATE PROCEDURE sp_Factura_GetById
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

CREATE PROCEDURE sp_Factura_Save
    @Id_Factura INT = NULL,
    @Fecha DATE,
    @Cliente VARCHAR(100),
    @Id_FormaPago INT
AS
BEGIN
    IF @Id_Factura IS NULL
    BEGIN
        INSERT INTO Factura (Fecha, Cliente, Id_FormaPago)
        VALUES (@Fecha, @Cliente, @Id_FormaPago);
    END
    ELSE
    BEGIN
        UPDATE Factura
        SET Fecha = @Fecha,
            Cliente = @Cliente,
            Id_FormaPago = @Id_FormaPago
        WHERE Id_Factura = @Id_Factura;
    END
END;
GO

CREATE PROCEDURE sp_Factura_Delete
    @Id_Factura INT
AS
BEGIN
    DELETE FROM DetalleFactura WHERE Id_Factura = @Id_Factura;
    DELETE FROM Factura WHERE Id_Factura = @Id_Factura;
END;
GO

CREATE PROCEDURE SP_RECUPERAR_PRODUCTOS
AS
BEGIN
    SELECT 
        Id_Articulo AS codigo,
        Nombre AS n_producto,
        0 AS stock,           -- Ajusta si agregas un campo stock real
        PrecioUnitario AS precio,
        1 AS esta_activo      -- Siempre activo, ya que no hay campo en la tabla
    FROM Articulo
    ORDER BY Nombre;
END;
GO

CREATE PROCEDURE [dbo].[SP_GUARDAR_PRODUCTO]
    @codigo INT,
    @nombre VARCHAR(100),
    @precio DECIMAL(10,2),
    @stock INT,
    @activo BIT
AS
BEGIN
    IF @codigo = 0
    BEGIN
        INSERT INTO T_Productos (n_producto, stock, precio, esta_activo)
        VALUES (@nombre, @stock, @precio, @activo)
    END
    ELSE
    BEGIN
        UPDATE T_Productos
        SET n_producto = @nombre,
            stock = @stock,
            precio = @precio,
            esta_activo = @activo
        WHERE codigo = @codigo
    END
END
GO
