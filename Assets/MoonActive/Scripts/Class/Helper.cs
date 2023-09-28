using System.Collections.Generic;
using Random = System.Random;

public static class Helper
{
    private static Random rand = new Random();
 
    public static void Shuffle<T>(this IList<T> values)
    { 
        for (int i = values.Count - 1; i > 0; i--) 
        {
            int k = rand.Next(i + 1);
            T value = values[k];
            values[k] = values[i];
            values[i] = value;
        }
    }
}