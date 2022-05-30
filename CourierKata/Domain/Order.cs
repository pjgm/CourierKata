﻿using System.Text;
using CourierKata.Input;

namespace CourierKata.Domain;

public class Order
{
    public Order(OrderInput orderInput)
    {
        Parcels = orderInput
            .Parcels
            .Select(p => new Parcel(p.Height, p.Width, p.Length));

        TotalCostMultiplier = orderInput.FastDelivery ? 2 : 1;
    }

    public IEnumerable<Parcel> Parcels { get; }

    public int TotalCostMultiplier { get; }
    public int TotalCost => Parcels.Sum(p => p.TotalCost) * TotalCostMultiplier;

    public override string ToString()
    {
        var sb = new StringBuilder();
        sb.AppendLine("* Items:");
        foreach (var parcel in Parcels)
        {
            sb.AppendLine($"\t{parcel.Type.ToString()} -> ${parcel.TotalCost}");
        }
        sb.AppendLine($"* Total Delivery Cost: ${ Parcels.Sum(p => p.TotalCost)}");
        sb.AppendLine($"* Speedy Delivery Cost: ${ TotalCost / TotalCostMultiplier}");
        sb.AppendLine($"* Total Order Cost: ${TotalCost}");

        return sb.ToString();
    }
}