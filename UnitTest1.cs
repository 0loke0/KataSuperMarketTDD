using AwesomeAssertions;
using System.Linq;
using Xunit.Sdk;

namespace SuperMarketTDD;

public class UnitTest1
{
    [Fact]
    public void Si_ElCarritoDeComprasEstaVacio_Debe_ElCarritoDeComprasTenerElPrecioTotalDe0()
    {
        var carritoCompras = new CarritoDeCompras();
        var precioTotalEnCarritoDeCompras = carritoCompras.PrecioTotal();

        precioTotalEnCarritoDeCompras.Should().Be(0);
    }
    
    [Fact]
    public void Si_SoloElProducctoArrozSeAgregAlCarrito_Debe_ElCarritoDeComprasTenerElPrecioTotalDe1()
    {
        var producto = new Producto("Arroz", 1,1);
        var carritoCompras = new CarritoDeCompras();
        carritoCompras.AgregarProducto(producto);

        var precioTotalEnCarritoDeCompras = carritoCompras.PrecioTotal();

        precioTotalEnCarritoDeCompras.Should().Be(1);
    }

    [Fact]
    public void Si_UnProductoTieneUnValorNegativo_Debe_MostrarExepcion()
    {
        Action actionProducto = () => new Producto("Manzanas", -1,1);

        actionProducto.Should().Throw<ArgumentException>();
    }
    
    [Fact]
    public void Si_SeAgreganDosProductosAlCarrito_Debe_SumarAmbosProductos()
    {
        var producto1 = new Producto("Arroz", 5,1);
        var producto2 = new Producto("Manzanas", 3,1);
        var carritoCompras = new CarritoDeCompras();
        carritoCompras.AgregarProducto(producto1);
        carritoCompras.AgregarProducto(producto2);

        var precioTotalEnCarritoDeCompras = carritoCompras.PrecioTotal();

        precioTotalEnCarritoDeCompras.Should().Be(8);
    }
    
    [Fact]
    public void Si_SeAgregaUnProductosAlCarrito_Debe_ElCarritoMostrarLosNombresYValoresDeLosProductosQueContiene()
    {
        var producto1 = new Producto("Arroz", 5,1);
        var producto2 = new Producto("Manzanas", 3,1);
        var carritoCompras = new CarritoDeCompras();
        carritoCompras.AgregarProducto(producto1);
        carritoCompras.AgregarProducto(producto2);

        var detalleProductos = carritoCompras.ObtenerDetalleProductos();

        detalleProductos.Should().ContainInOrder("Arroz - 5", "Manzanas - 3");
    }
    
    [Fact]
    public void Si_UnProductoTieneCantidad_Debe_MultiplicarPrecioPorCantidad()
    {
        var producto = new Producto("Leche", 2, 3);
        var carritoCompras = new CarritoDeCompras();
        carritoCompras.AgregarProducto(producto);

        var precioTotalEnCarritoDeCompras = carritoCompras.PrecioTotal();

        precioTotalEnCarritoDeCompras.Should().Be(6);
    }
    
    [Fact]
    public void Si_UnProductoTieneUnaCantidadNegativa_Debe_MostrarExepcion()
    {
        Action actionProducto = () => new Producto("Leche", 2, -1);

        actionProducto.Should().Throw<ArgumentException>();
    }
    
    
    
}

public class Producto
{
    private string _nombre { get; set; }
    private int _valorUnidad { get; set; }
    private int _cantidad { get; set; }

    public Producto(string nombre, int valorUnidad, int cantidad)
    {
        if (valorUnidad < 0)
        {
            throw new ArgumentException("El valor del producto no puede ser negativo");
        }
        if (cantidad < 0)
        {
            throw new ArgumentException("La cantidad del producto no puede ser negativa");
        }
        _nombre = nombre;
        _valorUnidad = valorUnidad;
        _cantidad = cantidad;
    }

    public int ObtenerValorUnidad()
    {
        return _valorUnidad;
    }

    public string ObtenerNombre()
    {
        return _nombre;
    }
    public int ObtenerValorTotal()
    {
        return _valorUnidad * _cantidad;
    }

}

public class CarritoDeCompras
{
    List<Producto> _productos = [];
    public int PrecioTotal()
    {
        return _productos.Any() ? _productos.Sum(p => p.ObtenerValorTotal()) : 0;
    }

    public void AgregarProducto(Producto producto)
    {
        _productos.Add(producto);
    }

    public List<string> ObtenerDetalleProductos()
    {
        return _productos.Select(p => $"{p.ObtenerNombre()} - {p.ObtenerValorUnidad()}").ToList();
    }
}