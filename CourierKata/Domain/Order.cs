using System.Linq;
using System.Text;
using CourierKata.Domain.Discounts;
using CourierKata.Extensions;
using CourierKata.Input;

namespace CourierKata.Domain;

public class Order
{
    public Order(OrderInput orderInput)
    {
        Parcels = orderInput
            .Parcels
            .Select(p => new Parcel(p.Height, p.Width, p.Length, p.Weight));

        TotalCostMultiplier = orderInput.FastDelivery ? 2 : 1;

        Discounts = DiscountHandler.GetDiscounts(orderInput.DiscountNames);
    }

    public IEnumerable<Parcel> Parcels { get; }
    public IEnumerable<Discount> Discounts { get; }

    public int TotalDiscounts =>
        Discounts.Sum(discount => discount.CalculateDiscountValue(Parcels));
    public int TotalCostMultiplier { get; }
    public int TotalCost => (Parcels.Sum(p => p.TotalCost) - TotalDiscounts ) * TotalCostMultiplier;

    public override string ToString()
    {
        var sb = new StringBuilder();
        sb.AppendLine("* Items:");
        foreach (var parcel in Parcels)
        {
            sb.AppendLine($"\t{parcel.Type.ToString()} -> ${parcel.TotalCost}");
        }
        sb.AppendLine($"* Total Delivery Cost: ${ Parcels.Sum(p => p.Type.DeliveryCost)}");
        sb.AppendLine($"* Overweight Delivery Cost: ${ Parcels.Sum(p => p.Type.SurchargeCost(p.Weight))}");
        if (TotalCostMultiplier > 1)
        {
            sb.AppendLine($"* Speedy Delivery Cost: ${ TotalCost / TotalCostMultiplier}");
        }
        sb.AppendLine($"* Discounts: -${TotalDiscounts}");
        sb.AppendLine($"* Total Order Cost: ${TotalCost}");

        return sb.ToString();
    }
}