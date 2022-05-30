using CourierKata.Domain;
using FluentAssertions;
using Xunit;

namespace CourierKata.UnitTests;

public class ParcelTests
{
    [Theory]
    [InlineData(1, 1, 1, 1, 3)]
    [InlineData(10, 10, 10, 3, 8)]
    [InlineData(50, 50, 50, 6, 15)]
    [InlineData(100, 100, 100, 10, 25)]
    [InlineData(101, 101, 101, 50, 50)]
    // Overweight parcels
    [InlineData(1, 1, 1, 2, 5)]
    [InlineData(101, 101, 101, 51, 51)]
    public void CreateParcel_WithInputParameters_CorrectTotalCost(int height, int width, int length, int weight, int totalCost)
    {
        // Arrange & Act
        Parcel parcel = new(height, width, length, weight);

        // Assert
        parcel.TotalCost.Should().Be(totalCost);
    }
}