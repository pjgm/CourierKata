namespace CourierKata.Domain.Discounts;

public class NthParcelFreeDiscount : Discount
{
    public NthParcelFreeDiscount(IEnumerable<NthFreeParcelCriteria> criteria) =>
        Criteria = criteria.ToHashSet();

    private ISet<NthFreeParcelCriteria> Criteria { get; }

    public override int CalculateDiscountValue(IEnumerable<Parcel> parcels)
    {
        var orderedParcels = parcels
            .OrderByDescending(p => p.TotalCost)
            .ToArray();

        var parcelSubset = new List<Parcel>();

        var totalDiscount = 0;

        foreach (var parcel in orderedParcels)
        {
            parcelSubset.Add(parcel);
            foreach (var (parcelTypeCodes, nthFreeParcel) in Criteria)
            {
                var filteredSubset = parcelSubset
                    .Where(p => parcelTypeCodes.Contains(p.Type))
                    .ToList();

                if (filteredSubset.Count < nthFreeParcel)
                {
                    continue;
                }

                totalDiscount += parcel.TotalCost;
                parcelSubset = parcelSubset.Except(filteredSubset).ToList();

            }
        }

        return totalDiscount;
    }
}

public record NthFreeParcelCriteria(ISet<ParcelType> ParcelTypes, int NthFreeParcel);
public record FourthSmallFreeParcelCriteria() : NthFreeParcelCriteria(new HashSet<ParcelType> { new Small() }, 4);
public record ThirdMediumFreeParcelCriteria() : NthFreeParcelCriteria(new HashSet<ParcelType> { new Medium() }, 3);
public record FifthMixedFreeParcelCriteria() : NthFreeParcelCriteria(
    new HashSet<ParcelType>
    {
        new Small(),
        new Medium(),
        new Large(),
        new ExtraLarge(),
        new Heavy()
    }, 5);