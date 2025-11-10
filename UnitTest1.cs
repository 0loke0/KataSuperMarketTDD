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
        var producto = new Producto(1);
        var carritoCompras = new CarritoDeCompras();
        carritoCompras.AgregarProducto(producto);

        var precioTotalEnCarritoDeCompras = carritoCompras.PrecioTotal();

        precioTotalEnCarritoDeCompras.Should().Be(1);
    }

    [Fact]
    public void Si_UnProductoTieneUnValorNegativo_Debe_MostrarExepcion()
    {
        Action actionProducto = () => new Producto(-1);

        actionProducto.Should().Throw<ArgumentException>();
    }
    
    [Fact]
    public void Si_SeAgreganDosProductosAlCarrito_Debe_SumarAmbosProductos()
    {
        var producto1 = new Producto(5);
        var producto2 = new Producto(3);
        var carritoCompras = new CarritoDeCompras();
        carritoCompras.AgregarProducto(producto1);
        carritoCompras.AgregarProducto(producto2);

        var precioTotalEnCarritoDeCompras = carritoCompras.PrecioTotal();

        precioTotalEnCarritoDeCompras.Should().Be(8);
    }
}

public class Producto
{
    private int _valorUnidad { get; set; }

    public Producto(int valorUnidad)
    {
        if (valorUnidad < 0)
        {
            throw new ArgumentException("El valor del producto no puede ser negativo");
        }
        _valorUnidad = valorUnidad;
    }

    public int ObtenerValorUnidad()
    {
        return _valorUnidad;
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
}