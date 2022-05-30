using CourierKata.Domain;

namespace CourierKata.Extensions;

public static class ParcelTypeExtensions
{

    public static int TotalCost(this ParcelType type, int weight) =>
        type.DeliveryCost + type.SurchargeCost(weight);

    public static int SurchargeCost(this ParcelType type, int weight) =>
        type.IsOverWeightLimit(weight)
            ? (weight - type.WeightLimit) * 2
            : 0;

    public static bool IsOverWeightLimit(this ParcelType type, int weight) =>
        weight > type.WeightLimit;
}