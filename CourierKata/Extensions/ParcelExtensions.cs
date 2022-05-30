using CourierKata.Domain;

namespace CourierKata.Extensions;

public static class ParcelExtensions
{
    public static ParcelType GetParcelType(this Parcel parcel)
    {
        var largestDimension = new[] { parcel.Length, parcel.Height, parcel.Width }.Max();

        ParcelType matchingDimensionType = largestDimension switch
        {
            <= Small.MaxDimension => new Small(),
            <= Medium.MaxDimension => new Medium(),
            <= Large.MaxDimension => new Large(),
            > Large.MaxDimension => new ExtraLarge()
        };

        return matchingDimensionType.MostCostEffectiveParcelSize(parcel.Weight);
    }

}