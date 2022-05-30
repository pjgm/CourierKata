# Implementation decisions

I decided to follow a domain driven approach from the start, encapsulating all bussiness logic in domain objects (`Order` & `Parcel`).
This proved to be a good approach as I progressed through the different phases. 

It also made testing pretty simple, even though adding new tests became a little bit cumbersome as new features where implemented.

## Phase 1: Setup & Initial implementation

I started by creating the `Order` class which essentially represents the collection of ordered parcels along with its total cost.
My assumption in this phase was that the input would be of type `IEnumerable<ParcelInput>` which I pass in on the ctor.
Then I proceeded to create the `Parcel` class along with its `ParcelType`. Even though i tried not to look ahead, I saw from a glance that there would be future changes to the definition of a `ParcelType` and as such i decided to go for a record instead of a simple enum to represent the different parcel sizes.
This way the definition of all the different available sizes are centralized in `ParcelType.cs` 

## Phase 2: Speedy delivery

This phase was pretty simple to implement since it only required a small change to the `TotalCost` of the order. I decided to introduce the `TotalCostMultiplier` at this point to anticipate future changes of this kind.
I also noticed that I was not representing the output in any way as required for this exercise and so instead of creating new `DTO` classes i decided to simply override the `ToString()` method on the `Order` class.
Tests were also modified to acomodate speedy delivery

## Phase 3: Overweight charge

The weight property was added to all the relevant classes. The weight limit of each parcel depends on its weight so this is part of the definition of each parcel type. 
`WeightLimit` property was as such added to `ParcelType` and the `TotalCost` formula was moved to `ParcelTypeExtensions`. This way all cost calculations are centralized in a single place.

## Phase 4: Heavy parcel

The Heavy parcel makes the type of a parcel not only depend on its dimensions but also its weight. Since it also has a different overweight cost, `WeightLimitSurcharge` was introduced with a default value of `2`.
One thing i noticed was that if a parcel is heavy enough (even if below 50kg), it's worth it to consider it as a `Heavy` parcel if the cost of being any other type exceeds the total cost of it being an heavy parcel.
Because of this I introduced the `MostCostEffectiveParcelSize` extension method

## Phase 5: Discounts

Discounts were the most challenging part of this excercise mainly because of all the edge cases. I believe I came up with a good enough approach, however it might not give the absolute best combination of discounts in some scenarios.
My though process for this approach was as follows:

### Naive approach
1. Group parcels by type
2. Order by descending cost
3. Go through each discount type, leaving the mixed discount for last since it should apply to the remaining parcels after all other discounts are applied
4. Return total discount value

This approach fails for scenarios where applying the mixed discount first gives a better discount.

Example:
`[Large:15, Large:15, Large:15, Small:13, Small:13, Small:3, Small:3, Small:3]`

- Applying `FourthSmallFree` discount first
    - Small discount: $3 (from group `[Small:13, Small:13, Small:3, Small:3]`)
    - Mixed discount: $0 (remaining items less than 5)
- Applying `FifthMixedFree` discount first
    - Mixed discount: $13 `[Large:15, Large:15, Large:15, Small:13, Small:13]`
    - Small discount: $0 (remaining items less than 4)

### Discount criteria approach
After some time thinking about the solution for the above edge cases, it seems like the discounts presented cannot be calculated independently since the discount to apply needs to be decided as we iterate through the parcels.
I then decided to introduce `NthParcelFreeDiscount` as the discount type containing `Criteria` which basically is the definition of a `NthParcelFree` discount.

1. Order all parcels by descending cost
2. Start with empty subset of parcels
3. For each parcel
    1. Add parcel to the subset
    2. For each criterion, check if the current subset of parcels fulfills that criterion. If so we know that this is the largest possible discount and we can use it


## Further improvements

- Testing classes got a little messy. This could be improved by creating a factory for the different types of parcel
- The `Discount` class implementation may not fit future discount requirements
- All discount edge cases may not have been taken into account
- `MostCostEffectiveParcelSize` could be improved