using CourierKata.Domain.Discounts;

namespace CourierKata;

public static class DiscountHandler
{
    public const string FourthSmallFree = nameof(FourthSmallFree);
    public const string ThirdMediumFree = nameof(ThirdMediumFree);
    public const string FifthMixedFree = nameof(FifthMixedFree);

    private static readonly IDictionary<string, NthFreeParcelCriteria> DiscountCriteria = new Dictionary<string, NthFreeParcelCriteria>
    {
        { FourthSmallFree, new FourthSmallFreeParcelCriteria() },
        { ThirdMediumFree, new ThirdMediumFreeParcelCriteria() },
        { FifthMixedFree, new FifthMixedFreeParcelCriteria() }
    };

    public static IEnumerable<Discount> GetDiscounts(IEnumerable<string> discountCodes)
    {
        var criteria = discountCodes.Select(code => DiscountCriteria[code]);
        return new List<Discount> { new NthParcelFreeDiscount(criteria) };
    }
}