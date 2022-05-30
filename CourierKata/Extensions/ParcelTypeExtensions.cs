using CourierKata.Domain;

namespace CourierKata.Extensions;

public static class ParcelTypeExtensions
{
    public static ParcelType MostCostEffectiveParcelSize(this ParcelType type, int weight)
    {
        var currentParcelCost = type.TotalCost(weight);

        var heavyParcel = new Heavy();
        var heavyParcelCost = heavyParcel.TotalCost(weight);

        return currentParcelCost < heavyParcelCost ? type : heavyParcel;
    }

    public static int TotalCost(this ParcelType type, int weight) =>
        type.DeliveryCost + type.SurchargeCost(weight);

    public static int SurchargeCost(this ParcelType type, int weight) =>
        type.IsOverWeightLimit(weight)
            ? (weight - type.WeightLimit) * type.WeightLimitSurcharge
            : 0;

    public static bool IsOverWeightLimit(this ParcelType type, int weight) =>
        weight > type.WeightLimit;
}