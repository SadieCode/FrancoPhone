using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Shuffle{

    /*
     * By grenade - based on the Fisher-Yates shuffle
     *https://stackoverflow.com/revisions/1262619/1
    */
    public static void ShuffleList<T>(this IList<T> list)
    {
        System.Random rng = new System.Random();
        int n = list.Count;
        while (n > 1)
        {
            n--;
            int k = rng.Next(n + 1);
            T value = list[k];
            list[k] = list[n];
            list[n] = value;
        }
    }
}
