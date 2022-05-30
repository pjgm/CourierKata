﻿using System.Collections.Generic;
using CourierKata.Domain;
using CourierKata.Input;
using FluentAssertions;
using Xunit;
using Xunit.Abstractions;

namespace CourierKata.UnitTests;

public class OrderTests
{
    private readonly ITestOutputHelper _output;
    public OrderTests(ITestOutputHelper output)
    {
        _output = output;
    }

    [Theory]
    [InlineData(false, 3)]
    [InlineData(true, 6)]
    public void CreateOrder_SingleSmallParcel_ShouldReturnParcelCost(bool speedyDelivery, int totalCost)
    {
        // Arrange
        var inputParcels = new List<ParcelInput>
        {
            new(9, 9, 9)
        };

        var orderInput = new OrderInput(inputParcels, speedyDelivery);

        // Act
        Order order = new(orderInput);
        _output.WriteLine(order.ToString());

        // Assert
        order.TotalCost.Should().Be(totalCost);
    }

    [Theory]
    [InlineData(false, 51)]
    [InlineData(true, 102)]
    public void CreateOrder_MultipleParcels_ShouldReturnSumOfParcelsCost(bool speedyDelivery, int totalCost)
    {
        // Arrange
        var inputParcels = new List<ParcelInput>
        {
            new(9, 9, 9),
            new(49, 20, 10),
            new(50, 20, 10),
            new(200, 20, 10)
        };

        var orderInput = new OrderInput(inputParcels, speedyDelivery);

        // Act
        Order order = new(orderInput);
        _output.WriteLine(order.ToString());

        // Assert
        order.TotalCost.Should().Be(totalCost);
    }
}