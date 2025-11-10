using AwesomeAssertions;

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
}

public class CarritoDeCompras
{
    public int PrecioTotal()
    {
        return 0;        
    }
}