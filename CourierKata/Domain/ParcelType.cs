namespace CourierKata.Domain;

public record ParcelType(int DeliveryCost, int WeightLimit);

public record Small() : ParcelType(3, 1)
{
    public const int MaxDimension = 9;
}

public record Medium() : ParcelType(8, 3)
{
    public const int MaxDimension = 49;
}

public record Large() : ParcelType(15, 6)
{
    public const int MaxDimension = 99;
}

public record ExtraLarge() : ParcelType(25, 10);