namespace PaymentsSharing.Collections;

internal static class EnumerableExtensionMethods
{
    public static IEnumerable<Pair<T>> TwoElementsVariations<T>(this IEnumerable<T> source)
    {
        var variations = new List<Pair<T>>();

        T[] sourceArray = source.ToArray();
        
        for (var i = 0; i < sourceArray.Length; i++)
        {
            for (var j = 0; j < sourceArray.Length; j++)
            {
                if (i != j)
                {
                    variations.Add(new Pair<T>(sourceArray[i], sourceArray[j]));
                }
            }
        }

        return variations;
    } 
}