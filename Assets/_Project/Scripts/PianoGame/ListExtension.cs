using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public static class ListExtension 
{
    //shuffle arrays:
    public static void Shuffle<T>(this T[] array, int shuffleAccuracy)
    {
        for (int i = 0; i < shuffleAccuracy; i++)
        {
            int randomIndex = Random.Range(1, array.Length);

            T temp = array[randomIndex];
            array[randomIndex] = array[0];
            array[0] = temp;
        }
    }
    //shuffle lists:
    public static void Shuffle<T>(this List<T> list, int shuffleAccuracy)
    {
        for (int i = 0; i < shuffleAccuracy; i++)
        {
            int randomIndex = Random.Range(1, list.Count);

            T temp = list[randomIndex];
            list[randomIndex] = list[0];
            list[0] = temp;
        }
    }
    public static List<List<T>> ChunkBy<T>(this List<T> items, int sliceSize = 30)
    {
        List<List<T>> list = new List<List<T>>();
        for (int i = 0; i < items.Count; i += sliceSize)
            list.Add(items.GetRange(i, Math.Min(sliceSize, items.Count - i)));
        return list;
    }
}
