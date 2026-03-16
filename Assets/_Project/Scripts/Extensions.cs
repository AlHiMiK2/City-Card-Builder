using System.Collections.Generic;
using UnityEngine;

namespace _Project.Scripts
{
    public static class Extensions
    {
        public static void Shuffle<T>(this IList<T> list)
        {
            int count = list.Count;
            for (int i = 0; i < count; i++)
            {
                int randomIndex = Random.Range(i, count); 
                (list[i], list[randomIndex]) = (list[randomIndex], list[i]);
            }
        }
    }
}
