using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Productos.Cliente.Models;
using System.Text;

public class ProductoController : Controller
{
    private readonly HttpClient _httpClient;

    public ProductoController(IHttpClientFactory httpClientFactory)
    {
        // Utilizar el cliente configurado como "ClienteInseguro"
        _httpClient = httpClientFactory.CreateClient("ClienteInseguro");
    }

    public async Task<IActionResult> Index()
    {
        var response = await _httpClient.GetAsync("api/Productos/Lista");

        if (response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadAsStringAsync();
            var productos = JsonConvert.DeserializeObject<IEnumerable<ProductoViewModel>>(content);
            return View("Index", productos);
        }

        return View(new List<ProductoViewModel>());
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(Producto producto)
    {
        if (ModelState.IsValid)
        {
            var json = JsonConvert.SerializeObject(producto);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("/api/Productos/crearProducto", content);

            if (response.IsSuccessStatusCode)
            {
                // Manejar el caso de creación exitosa.
                return RedirectToAction("Index");
            }
            else
            {
                // Manejar el caso de error en la solicitud POST, por ejemplo, mostrando un mensaje de error.
                ModelState.AddModelError(string.Empty, "Error al crear el producto.");
            }
        }
        return View(producto);
    }
}