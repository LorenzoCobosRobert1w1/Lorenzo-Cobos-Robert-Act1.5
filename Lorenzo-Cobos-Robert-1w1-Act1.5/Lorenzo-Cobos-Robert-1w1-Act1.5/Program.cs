using _1W1LORENZOCOBOSROBERTNADAMAS.Domain;
using _1W1LORENZOCOBOSROBERTNADAMAS.Services;

ProductService oService = new ProductService();

// Obtener todos los productos - GetAll
Console.WriteLine("Getting all products - GetAll");
try
{
    List<Product> allProducts = oService.GetProducts();
    if (allProducts.Count > 0)
    {
        foreach (var p in allProducts)
            Console.WriteLine(p);
    }
    else
        Console.WriteLine("No products found.");
}
catch (Exception ex)
{
    Console.WriteLine($"Error getting products: {ex.Message}");
}

// -----------------------------
// Get product by Id
Console.WriteLine("\nGetting product by Id - GetById");
try
{
    Product? prod = oService.GetProductById(5);
    if (prod != null)
        Console.WriteLine(prod);
    else
        Console.WriteLine("No product found with that Id.");
}
catch (Exception ex)
{
    Console.WriteLine($"Error getting product: {ex.Message}");
}

// -----------------------------
// Create new product
Console.WriteLine("\nCreating a new product - SaveProduct");
Product newProduct = new Product
{
    Name = "Test Product",
    UnitPrice = 123.45m
};

try
{
    bool saved = oService.SaveProduct(newProduct);
    Console.WriteLine(saved ? "Product created successfully!" : "Failed to create product.");
}
catch (Exception ex)
{
    Console.WriteLine($"Error creating product: {ex.Message}");
}
//----------------------------------------------------------------
Console.WriteLine("\nGetting all products - GetAll");
try
{
    List<Product> allProducts = oService.GetProducts();
    if (allProducts.Count > 0)
    {
        foreach (var p in allProducts)
            Console.WriteLine(p);
    }
    else
        Console.WriteLine("No products found.");
}
catch (Exception ex)
{
    Console.WriteLine($"Error getting products: {ex.Message}");
}

// -----------------------------
// Delete the last created product
List<Product> updatedProducts = oService.GetProducts();
Product? lastProduct = updatedProducts.LastOrDefault(p => p.Name == "Test Product");

if (lastProduct != null)
{
    int lastProductId = lastProduct.IdProduct;
    Console.WriteLine($"\nDeleting product with Id {lastProductId} - DeleteProduct");

    try
    {
        bool deleted = oService.DeleteProduct(lastProductId);
        Console.WriteLine(deleted ? "Product deleted successfully!" : "Product could not be deleted.");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error deleting product: {ex.Message}");
    }
}

// -----------------------------
// Update product example
Console.WriteLine("\nUpdating a product - SaveProduct");
Product updateProduct = new Product
{
    IdProduct = 3, // example Id
    Name = "Updated Monitor",
    UnitPrice = 85000m
};

try
{
    bool updated = oService.SaveProduct(updateProduct);
    Console.WriteLine(updated ? $"Product {updateProduct.IdProduct} updated successfully!" : "Failed to update product.");
}
catch (Exception ex)
{
    Console.WriteLine($"Error updating product: {ex.Message}");
}

Console.WriteLine("\nPress any key to exit...");
Console.ReadKey();