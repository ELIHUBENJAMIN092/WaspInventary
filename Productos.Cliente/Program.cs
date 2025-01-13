var builder = WebApplication.CreateBuilder(args);

// Agregar servicios al contenedor.
builder.Services.AddControllersWithViews();

// Registrar un cliente HTTP que ignore la validación del certificado.
builder.Services.AddHttpClient("ClienteInseguro", client =>
{
    client.BaseAddress = new Uri("https://localhost:7170/api");
})
.ConfigurePrimaryHttpMessageHandler(() => new HttpClientHandler
{
    // Ignorar la validación de certificados SSL.
    ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true
});

var app = builder.Build();

// Configurar el pipeline de solicitudes HTTP.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Producto}/{action=Index}/{id?}");

app.Run();