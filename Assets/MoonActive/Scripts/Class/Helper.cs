using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public static class Helper
{
    public static IEnumerator InvokeAction(Action action, float delay = 0)
    {
        yield return new WaitForSeconds(delay);
        action.Invoke();
        yield return null;
    }
    
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

    public static string Invoke()
    {
        throw new System.NotImplementedException();
    }
}