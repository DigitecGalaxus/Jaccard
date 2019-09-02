using System;
using System.Collections.Generic;
using System.Linq;

namespace JaccardCalculator
{
    public static class JaccardIndex
    {
        public static double CalculateJaccardDistance<T>(ICollection<T> a, ICollection<T> b)
        {
            return 1 - CalculateJaccardIndex(a, b);
        }
        
        public static double CalculateJaccardIndex<T>(ICollection<T> a, ICollection<T> b)
        {
            CheckInput(a, b);

            var intersectionCount = a.Intersect(b).Count();
            var unionCount = a.Union(b).Count();

            var jaccardIndex = (intersectionCount / (double) unionCount);
            return Math.Round(jaccardIndex, 2);
        }

        public static double CalculateMeanJaccardIndex<T>(ICollection<T> a, ICollection<T> b)
        {
            var aLength = a.Count;
            var bLength = b.Count;
            var maxIndex = Math.Max(aLength, bLength);

            var meanJaccardIndex = Enumerable
                .Range(1, maxIndex - 1)
                .Select(i =>
                {
                    var aSliced = a.Take(i).ToArray();
                    var bSliced = b.Take(i).ToArray();
                    var jaccardIndex = CalculateJaccardIndex(aSliced, bSliced);
                    return jaccardIndex;
                })
                .Average();
            
            return meanJaccardIndex;
        }
        
        private static void CheckInput<T>(ICollection<T> a, ICollection<T> b)
        {
            if (a == null)
            {
                throw new ArgumentNullException(nameof(a));
            }

            if (b == null)
            {
                throw new ArgumentNullException(nameof(b));
            }
        }
    }
}