using CourierKata.Domain;
using FluentAssertions;
using Xunit;

namespace CourierKata.UnitTests;

public class ParcelTests
{
    [Theory]
    [InlineData(1, 1, 1, 3)]
    [InlineData(10, 10, 10, 8)]
    [InlineData(50, 50, 50, 15)]
    [InlineData(100, 100, 100, 25)]
    public void CreateParcel_WithInputDimensions_CorrectTotalCost(int height, int width, int length, int totalCost)
    {
        // Arrange & Act
        Parcel parcel = new(height, width, length);

        // Assert
        parcel.TotalCost.Should().Be(totalCost);
    }
}