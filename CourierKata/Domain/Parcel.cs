using CourierKata.Extensions;

namespace CourierKata.Domain;

public class Parcel
{
    public Parcel(int height, int width, int length)
    {
        Height = height;
        Width = width;
        Length = length;
    }

    public int Height { get; }
    public int Width { get; }
    public int Length { get; }
    public ParcelType Type => this.GetParcelType();

    public int TotalCost => Type.DeliveryCost;
}