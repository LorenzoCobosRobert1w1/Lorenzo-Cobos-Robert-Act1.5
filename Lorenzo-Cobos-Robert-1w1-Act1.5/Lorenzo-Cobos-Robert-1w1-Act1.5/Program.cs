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

     
        Console.WriteLine("\n--- GetAll() ---");
        List<Product> updatedProducts = oService.GetProducts();
        foreach (var p in updatedProducts)
            Console.WriteLine(p);

      
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
    InvoiceService oService = new InvoiceService();

    // ------------------- GET ALL -------------------
    Console.WriteLine("\n--- GetAll() ---");
    List<Invoice> invoices = repo.GetAll();
    foreach (var inv in invoices)
    {
        Console.WriteLine($"Invoice #{inv.InvoiceNo} - Client: {inv.Client} - " +
                          $"Date: {inv.Date.ToShortDateString()} - Payment: {inv.PayType?.Name}");
    }

    // ------------------- GET BY ID -------------------
    Console.WriteLine("\n--- GetById(1) ---");
    var invoice = repo.GetById(1);
    if (invoice != null)
    {
        Console.WriteLine($"Invoice found: #{invoice.InvoiceNo} - Client: {invoice.Client}");
    }
    else
    {
        Console.WriteLine("Invoice with ID 1 not found.");
    }

    // ------------------- SAVE (Create) -------------------
    Console.WriteLine("\n--- Save (Create) ---");
    Invoice newInvoice = new Invoice
    {
        InvoiceNo = 0,
        Date = DateTime.Now,
        Client = "Juan Pérezz",
        PayType = new PaymentMethod { Id = 1 } // Efectivo
    };

    try
    {
        bool saved = oService.SaveInvoice(newInvoice);
        Console.WriteLine(saved ? "Invoice created successfully!" : "Failed to save invoice.");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error creating invoice: {ex.Message}");
    }

    // ------------------- EXECUTE TRANSACTION -------------------
    Console.WriteLine("\n--- ExecuteTransaction (Invoice with Details) ---");

    Invoice invoiceWithDetails = new Invoice
    {
        Client = "Ana Gómez",
        Date = DateTime.Now,
        PayType = new PaymentMethod { Id = 2 } // Tarjeta Débito
    };

    // Agregar detalles
    invoiceWithDetails.AddDetail(new InvoiceDetail
    {
        Product = new Product { IdProduct = 3, Name = "Monitor Samsung 24\"", UnitPrice = 80000 },
        Quantity = 1
    });

    invoiceWithDetails.AddDetail(new InvoiceDetail
    {
        Product = new Product { IdProduct = 4, Name = "Auriculares HyperX", UnitPrice = 25000 },
        Quantity = 2
    });

    bool resultTransaction = oService.ExecuteTransaction(invoiceWithDetails);
    Console.WriteLine(resultTransaction
        ? "Invoice with details created successfully!"
        : "Error: Could not create invoice with details.");

    // ------------------- GET ALL After Transaction -------------------
    Console.WriteLine("\n--- GetAll() After Transaction ---");
    invoices = repo.GetAll();
    foreach (var inv in invoices)
    {
        Console.WriteLine($"Invoice #{inv.InvoiceNo} - Client: {inv.Client} - " +
                          $"Date: {inv.Date.ToShortDateString()} - Payment: {inv.PayType?.Name}");
    }

    // ------------------- DELETE -------------------
    Console.WriteLine("\n--- Delete(5) ---");
    bool deleted = repo.Delete(5);
    Console.WriteLine(deleted ? "Invoice deleted successfully!" : "Failed to delete invoice.");
}

}
