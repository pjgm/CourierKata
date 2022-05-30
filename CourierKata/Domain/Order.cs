using CourierKata.Input;

namespace CourierKata.Domain;

public class Order
{
    public Order(IEnumerable<ParcelInput> parcels)
    {
        Parcels = parcels
            .Select(p => new Parcel(p.Height, p.Width, p.Length));
    }

    public IEnumerable<Parcel> Parcels { get; }

    public int TotalCost => Parcels.Sum(p => p.TotalCost);
}