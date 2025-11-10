using AwesomeAssertions;
using System.Linq;

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
        var producto = new Producto("Arroz",1);
        var carritoCompras = new CarritoDeCompras();
        carritoCompras.AgregarProducto(producto);

        var precioTotalEnCarritoDeCompras = carritoCompras.PrecioTotal();

        precioTotalEnCarritoDeCompras.Should().Be(1);
    }
}

public class Producto
{
    
    private string _producto { get; set; }
    private int _valorUnidad { get; set; }

    public Producto(string producto, int valorUnidad)
    {
        _producto = producto;
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