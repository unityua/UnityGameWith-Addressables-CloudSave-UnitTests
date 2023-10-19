using System.Collections.Generic;

namespace PesPatron.Helpers
{
    public static class CollectionExtentions 
    {
        public static bool Contains<T>(this List<T> list, T item)
        {
            return list.IndexOf(item) >= 0; 
        }
    }
}