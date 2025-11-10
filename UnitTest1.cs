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
        var producto = new Producto("Arroz", 1);
        var carritoCompras = new CarritoDeCompras();
        carritoCompras.AgregarProducto(producto);

        var precioTotalEnCarritoDeCompras = carritoCompras.PrecioTotal();

        precioTotalEnCarritoDeCompras.Should().Be(1);
    }

    [Fact]
    public void Si_UnProductoTieneUnValorNegativo_Debe_MostrarExepcion()
    {
        Action actionProducto = () => new Producto("Manzanas", -1);

        actionProducto.Should().Throw<ArgumentException>();
    }
    
    [Fact]
    public void Si_SeAgreganDosProductosAlCarrito_Debe_SumarAmbosProductos()
    {
        var producto1 = new Producto("Arroz", 5);
        var producto2 = new Producto("Manzanas", 3);
        var carritoCompras = new CarritoDeCompras();
        carritoCompras.AgregarProducto(producto1);
        carritoCompras.AgregarProducto(producto2);

        var precioTotalEnCarritoDeCompras = carritoCompras.PrecioTotal();

        precioTotalEnCarritoDeCompras.Should().Be(8);
    }
    
    [Fact]
    public void Si_SeAgregaUnProductosAlCarrito_Debe_ElCarritoMostrarLosNombresYValoresDeLosProductosQueContiene()
    {
        var producto1 = new Producto("Arroz", 5);
        var producto2 = new Producto("Manzanas", 3);
        var carritoCompras = new CarritoDeCompras();
        carritoCompras.AgregarProducto(producto1);
        carritoCompras.AgregarProducto(producto2);

        var detalleProductos = carritoCompras.ObtenerDetalleProductos();

        detalleProductos.Should().ContainInOrder("Arroz - 5", "Manzanas - 3");
    }
}

public class Producto
{
    private string _nombre { get; set; }
    private int _valorUnidad { get; set; }

    public Producto(string nombre, int valorUnidad)
    {
        if (valorUnidad < 0)
        {
            throw new ArgumentException("El valor del producto no puede ser negativo");
        }
        _nombre = nombre;
        _valorUnidad = valorUnidad;
    }

    public int ObtenerValorUnidad()
    {
        return _valorUnidad;
    }

    public string ObtenerNombre()
    {
        return _nombre;
    }

}

public class CarritoDeCompras
{
    List<Producto> _productos = [];
    public int PrecioTotal()
    {
        return _productos.Any() ? _productos.Sum(p => p.ObtenerValorUnidad()) : 0;
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