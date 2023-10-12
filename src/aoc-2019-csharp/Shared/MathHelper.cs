namespace aoc_2019_csharp.Shared;

public static class MathHelper
{
    public static long LeastCommonMultiple(params long[] numbers) => numbers.Aggregate(LeastCommonMultiple);

    public static long LeastCommonMultiple(long a, long b) => Math.Abs(a * b) / GreatestCommonFactor(a, b);

    public static long GreatestCommonFactor(long a, long b)
    {
        while (b != 0)
        {
            var temp = b;
            b = a % b;
            a = temp;
        }

        return a;
    }
}
