**API de Gestión de Inventarios**
- **Descripción:**
Esta API proporciona un conjunto de endpoints para gestionar inventarios en una empresa. Permite realizar operaciones CRUD (Crear, Leer, Actualizar, Eliminar) sobre productos, usuarios, categorías, y reportes, además de gestionar el control del stock en tiempo real.

- **Características:**
Gestión de productos: creación, lectura, actualización y eliminación. Control de stock: seguimiento en tiempo real de las existencias. Gestión de usuarios: autenticación y autorización. Generación de reportes sobre movimientos de inventarios. Documentación interactiva mediante Swagger.

**Instalación**

Requisitos previos

Tener instalado:

Visual Studio 2022

.NET 6.0 o superior.

SQL Server.

Endpoints principales

Autenticación

POST /api/auth/login: Iniciar sesión.

Productos

GET /api/products: Listar todos los productos.

GET /api/products/{id}: Obtener detalles de un producto.

POST /api/products: Crear un nuevo producto.

PUT /api/products/{id}: Actualizar un producto existente.

DELETE /api/products/{id}: Eliminar un producto.

Usuarios

GET /api/users: Listar todos los usuarios.

POST /api/users: Crear un nuevo usuario.

DELETE /api/users: Eliminar un nuevo usuario.

Reportes

GET /api/reports/stock: Generar un reporte de inventario.

**Tecnologías utilizadas**

Lenguaje: C#

Framework: ASP.NET Core

Base de datos: SQL Server

ORM: Entity Framework Core

Documentación: Swagger
