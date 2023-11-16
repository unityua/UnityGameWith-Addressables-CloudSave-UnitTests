using System.Collections.Generic;
using UnityEngine;

namespace PesPatron.Helpers
{
    public static class CollectionExtentions
    {
        public static bool Contains<T>(this List<T> list, T item)
        {
            return list.IndexOf(item) >= 0;
        }

        public static T RandomItem<T>(this IReadOnlyList<T> list)
        {
            return list[Random.Range(0, list.Count)];
        }
    }
}