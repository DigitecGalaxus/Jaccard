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

            if ((a.Count > 0 && b.Count == 0) || (a.Count == 0 && b.Count > 0))
            {
                return 0;
            }
            
            if(a.Count == 0 && b.Count == 0)
            {
                return 1;
            }

            var intersectionCount = a.Intersect(b).Count();
            var unionCount = a.Union(b).Count();

            var jaccardIndex = (intersectionCount / (double) unionCount);
            return Math.Round(jaccardIndex, 2);
        }

        public static double CalculateMeanJaccardDistance<T>(ICollection<T> a, ICollection<T> b)
        {
            return Math.Round(1 - CalculateMeanJaccardIndex(a, b), 2);
        }

        public static double CalculateMeanJaccardIndex<T>(ICollection<T> a, ICollection<T> b)
        {
            CheckInput(a, b);
            
            var aLength = a.Count;
            var bLength = b.Count;
            var maxIndex = Math.Max(aLength, bLength);

            var meanJaccardIndex = Enumerable
                .Range(0, maxIndex)
                .Select(i =>
                {
                    var takeCount = i + 1;
                    var aSliced = a.Take(takeCount).ToArray();
                    var bSliced = b.Take(takeCount).ToArray();
                    var jaccardIndex = CalculateJaccardIndex(aSliced, bSliced);
                    return jaccardIndex;
                })
                .Average();
            
            return Math.Round(meanJaccardIndex, 2);
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