namespace aoc_2019_csharp.Day14;

public class Factory
{
    private readonly List<Reaction> _reactions;
    private readonly Dictionary<string, long> _inventory = new();

    public Factory(List<Reaction> reactions)
    {
        _reactions = reactions;
    }

    public long GetOreRequirementToProduce(long quantity, string chemical)
    {
        var ore = 0L;
        var reaction = _reactions.Single(r => r.Output.Name == chemical);
        var inventoryQuantity = _inventory.GetValueOrDefault(chemical);
        var quantityNeeded = quantity - inventoryQuantity;
        var quantityNeededOrZero = Math.Max(quantityNeeded, 0);
        var multiplier = (long)Math.Ceiling((double)quantityNeededOrZero / reaction.Output.Quantity);
        var actualQuantityProduced = reaction.Output.Quantity * multiplier;
        var leftOver = actualQuantityProduced - quantityNeeded;

        foreach (var input in reaction.Inputs)
        {
            var inputQuantity = input.Quantity * multiplier;

            if (input.Name == "ORE")
            {
                ore += inputQuantity;
            }
            else
            {
                ore += GetOreRequirementToProduce(inputQuantity, input.Name);
            }
        }

        _inventory[chemical] = leftOver;

        return ore;
    }
}
