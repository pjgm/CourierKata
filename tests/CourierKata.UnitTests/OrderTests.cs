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
            new(9, 9, 9, 1)
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
            new(9, 9, 9, 1),
            new(49, 20, 10, 3),
            new(50, 20, 10, 6),
            new(200, 20, 10, 10)
        };

        var orderInput = new OrderInput(inputParcels, speedyDelivery);

        // Act
        Order order = new(orderInput);
        _output.WriteLine(order.ToString());

        // Assert
        order.TotalCost.Should().Be(totalCost);
    }

    [Theory]
    [InlineData(false, 110)]
    [InlineData(true, 220)]
    public void CreateOrder_MultipleOverweightParcels_ShouldReturnSumOfParcelsCost(bool speedyDelivery, int totalCost)
    {
        // Arrange
        var inputParcels = new List<ParcelInput>
        {
            new(9, 9, 9, 2),
            new(49, 20, 10, 4),
            new(50, 20, 10, 7),
            new(200, 20, 10, 11),
            new(1, 1, 1, 51),
        };

        var orderInput = new OrderInput(inputParcels, speedyDelivery);

        // Act
        Order order = new(orderInput);
        _output.WriteLine(order.ToString());

        // Assert
        order.TotalCost.Should().Be(totalCost);
    }

    [Theory]
    [InlineData(false, 261)]
    [InlineData(true, 522)]
    public void CreateOrder_MultipleHeavyParcels_ShouldReturnSumOfParcelsCost(bool speedyDelivery, int totalCost)
    {
        // Arrange
        var inputParcels = new List<ParcelInput>
        {
            new(1, 2, 3, 50),
            new(1, 2, 3, 60),
            new(50, 50, 50, 100),
            new(500, 500, 500, 51),
        };

        var orderInput = new OrderInput(inputParcels, speedyDelivery);

        // Act
        Order order = new(orderInput);
        _output.WriteLine(order.ToString());

        // Assert
        order.TotalCost.Should().Be(totalCost);
    }
}