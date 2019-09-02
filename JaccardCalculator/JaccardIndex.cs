using System;
using System.Collections.Generic;
using System.Linq;

namespace JaccardCalculator
{
    public static class JaccardIndex
    {
        public static double CalculateJaccardIndex<T>(ICollection<T> a, ICollection<T> b)
        {
            CheckInput(a, b);

            var intersectionCount = a.Intersect(b).Count();
            var unionCount = a.Union(b).Count();

            var jaccardIndex = (intersectionCount / (double) unionCount);
            return Math.Round(jaccardIndex, 2);
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