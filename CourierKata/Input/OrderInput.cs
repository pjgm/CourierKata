namespace CourierKata.Input;

public record OrderInput(IEnumerable<ParcelInput> Parcels, IEnumerable<string> DiscountNames, bool FastDelivery = false);