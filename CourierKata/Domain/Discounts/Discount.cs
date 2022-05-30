namespace CourierKata.Domain.Discounts;

public abstract class Discount
{
    public abstract int CalculateDiscountValue(IEnumerable<Parcel> parcels);
}