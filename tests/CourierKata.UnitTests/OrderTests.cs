using System.Collections.Generic;
using CourierKata.Domain;
using CourierKata.Input;
using FluentAssertions;
using Xunit;

namespace CourierKata.UnitTests;

public class OrderTests
{
    [Fact]
    public void CreateOrder_MultipleParcels_ShouldReturnSumOfParcelsCost()
    {
        // Arrange
        var inputParcels = new List<ParcelInput>
        {
            new(9, 9, 9),
            new(49, 20, 10),
            new(50, 20, 10),
            new(200, 20, 10)
        };

        // Act
        Order order = new(inputParcels);

        // Assert
        order.TotalCost.Should().Be(51);
    }
}