namespace CourierKata.Input;

public record OrderInput(IEnumerable<ParcelInput> Parcels, bool FastDelivery = false);