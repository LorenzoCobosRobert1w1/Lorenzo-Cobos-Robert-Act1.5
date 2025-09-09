using _1W1LORENZOCOBOSROBERTNADAMAS.Data.Implementations;
using _1W1LORENZOCOBOSROBERTNADAMAS.Domain;
using _1W1LORENZOCOBOSROBERTNADAMAS.Services;


class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("=== SELECCIONAR OPCIÓN ===");
        Console.WriteLine("0 - Probar PRODUCTOS");
        Console.WriteLine("1 - Probar FACTURAS");
        Console.Write("Ingrese opción: ");

        string? opcion = Console.ReadLine();

        if (opcion == "0")
        {
            TestProducts();
        }
        else if (opcion == "1")
        {
            TestInvoices();
        }
        else
        {
            Console.WriteLine("Opción inválida.");
        }

        Console.WriteLine("\nFin del programa. Presione cualquier tecla para salir...");
        Console.ReadKey();
    }

    // ==================== PRODUCTOS ====================
    static void TestProducts()
    {
        ProductService oService = new ProductService();

        // Obtener todos los productos
        Console.WriteLine("\n--- GetAll() ---");
        try
        {
            List<Product> allProducts = oService.GetProducts();
            if (allProducts.Count > 0)
                foreach (var p in allProducts)
                    Console.WriteLine(p);
            else
                Console.WriteLine("No products found.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error getting products: {ex.Message}");
        }

        // Obtener producto por Id
        Console.WriteLine("\n--- GetById(5) ---");
        try
        {
            Product? prod = oService.GetProductById(5);
            Console.WriteLine(prod != null ? prod.ToString() : "No product found with that Id.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error getting product: {ex.Message}");
        }

        // Crear producto
        Console.WriteLine("\n--- Save (Create) ---");
        Product newProduct = new Product { Name = "Test Product", UnitPrice = 123.45m };

        try
        {
            bool saved = oService.SaveProduct(newProduct);
            Console.WriteLine(saved ? "Product created successfully!" : "Failed to create product.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error creating product: {ex.Message}");
        }

        // Mostrar productos nuevamente
        Console.WriteLine("\n--- GetAll() ---");
        List<Product> updatedProducts = oService.GetProducts();
        foreach (var p in updatedProducts)
            Console.WriteLine(p);

        // Eliminar el último producto creado
        Product? lastProduct = updatedProducts.LastOrDefault(p => p.Name == "Test Product");
        if (lastProduct != null)
        {
            Console.WriteLine($"\n--- Delete({lastProduct.IdProduct}) ---");
            try
            {
                bool deleted = oService.DeleteProduct(lastProduct.IdProduct);
                Console.WriteLine(deleted ? "Product deleted successfully!" : "Failed to delete product.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deleting product: {ex.Message}");
            }
        }

        // Actualizar producto existente
        Console.WriteLine("\n--- Save (Update) ---");
        Product updateProduct = new Product
        {
            IdProduct = 3,
            Name = "Updated Monitor",
            UnitPrice = 85000m
        };

        try
        {
            bool updated = oService.SaveProduct(updateProduct);
            Console.WriteLine(updated ? "Product updated successfully!" : "Failed to update product.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error updating product: {ex.Message}");
        }
    }

    // ==================== FACTURAS ====================
    static void TestInvoices()
    {
        InvoiceRepository repo = new InvoiceRepository();

        // Traer todas
        Console.WriteLine("\n--- GetAll() ---");
        List<Invoice> invoices = repo.GetAll();
        foreach (var inv in invoices)
            Console.WriteLine($"Factura #{inv.InvoiceNo} - Cliente: {inv.Client} - Fecha: {inv.Date.ToShortDateString()} - FormaPago: {inv.PayType?.Name}");

        // Buscar por Id
        Console.WriteLine("\n--- GetById(1) ---");
        var invoice = repo.GetById(1);
        Console.WriteLine(invoice != null
            ? $"Factura encontrada: #{invoice.InvoiceNo} - Cliente: {invoice.Client}"
            : "No se encontró la factura con ID 1");

        // Guardar nueva
        Console.WriteLine("\n--- Save() ---");
        Invoice nuevaFactura = new Invoice
        {
            InvoiceNo = 1001,
            Date = DateTime.Now,
            Client = "Juan Pérez",
            PayType = new PaymentMethod { Id = 1, Name = "Efectivo" }
        };

        bool saved = repo.Save(nuevaFactura);
        Console.WriteLine(saved ? "Factura guardada con éxito." : "Error al guardar la factura.");

        // Eliminar
        Console.WriteLine("\n--- Delete(1001) ---");
        bool deleted = repo.Delete(1001);
        Console.WriteLine(deleted ? "Factura eliminada con éxito." : "No se pudo eliminar la factura.");
    }
}
