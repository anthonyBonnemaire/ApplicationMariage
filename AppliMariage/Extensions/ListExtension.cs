using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AppliMariage.Extensions
{
    public static class ListExtension
    {

        public static void Shuffle<T>(this IList<T> list)
        {
            Random rnd = new Random();
            Shuffle(list, rnd);
        }

        public static void Shuffle<T>(this IList<T> list, Random rnd)
        {
            for (var i = 0; i < list.Count; i++)
                list.Swap(i, rnd.Next(i, list.Count));
        }

        public static void Swap<T>(this IList<T> list, int i, int j)
        {
            var temp = list[i];
            list[i] = list[j];
            list[j] = temp;
        }

        public static IList<T> GetCloneShuffle<T>(this IList<T> list)
        {
            if (list == null || list.Count == 0)
                return list;

            List<T> listeClone = new List<T>();
            foreach (var item in list)
            {
                listeClone.Add(item);
            }

            listeClone.Shuffle();
            return listeClone;
        }


    }
}
