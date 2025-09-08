using _1W1LORENZOCOBOSROBERTNADAMAS.Domain;
using _1W1LORENZOCOBOSROBERTNADAMAS.Services;

ProductService oService = new ProductService();

//---------------  PRODUCTOS  -----------------------------------------------------------------

// Obtener todos los productos - GetAll
Console.WriteLine("Obtener todos los productos - GetAll");

try
{
    // Llamamos al Service
    List<Product> lp = oService.GetProducts();

    // Manejamos la respuesta
    if (lp.Count > 0)
        foreach (Product p in lp)
            Console.WriteLine(p);
    else
        Console.WriteLine("No hay productos...");
}
catch (Exception ex)
{
    // Aquí mostramos el error de manera amigable para el usuario.
    Console.WriteLine($"Ocurrió un error al obtener los productos: {ex.Message}");
    // El mensaje de la excepción contendrá la causa real del fallo (ej: "Input string was not in a correct format.").
}
