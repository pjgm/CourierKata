using CourierKata.Extensions;

namespace CourierKata.Domain;

public class Parcel
{
    public Parcel(int height, int width, int length, int weight)
    {
        Height = height;
        Width = width;
        Length = length;
        Weight = weight;
    }

    public int Height { get; }
    public int Width { get; }
    public int Length { get; }
    public int Weight { get; }

    public ParcelType Type => this.GetParcelType();

    public int TotalCost => Type.TotalCost(Weight);
}