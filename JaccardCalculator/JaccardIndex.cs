using System;
using System.Collections.Generic;
using System.Linq;

namespace JaccardCalculator
{
    public static class JaccardIndex
    {
        /// <summary>
        /// Inverse jaccard index/coeffiecient, based on https://www.statisticshowto.datasciencecentral.com/jaccard-index/.
        /// It doesn't consider the order of two collections, so [1,2] and [2,1] will have an index of 1.
        /// The complexity is O(n * n). 
        /// </summary>
        /// <param name="a">First collection.</param>
        /// <param name="b">Second collection. Replacing a and b returns the same result.</param>
        /// <returns>Values between 0 and 1, where 0 means the collections are identical and 1 that they are completely different with no matching elements. 
        public static double CalculateJaccardDistance<T>(ICollection<T> a, ICollection<T> b)
        {
            return 1 - CalculateJaccardIndex(a, b);
        }
        
        /// <summary>
        /// Simple jaccard index/coeffiecient, based on https://www.statisticshowto.datasciencecentral.com/jaccard-index/.
        /// It doesn't consider the order of two collections, so [1,2] and [2,1] will have an index of 1.
        /// The complexity is O(n * n). 
        /// </summary>
        /// <param name="a">First collection</param>
        /// <param name="b">Second collection. Replacing a and b returns the same result.</param>
        /// <returns>Values between 0 and 1, where 0 means the sets are completely different with no matching elements, and 1 that the sets are identical.</returns>
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

        /// <summary>
        /// It's the inverse of the mean jaccard index.
        /// 
        /// Similar to the jaccard distance, however it considers the order AND positions of the elements.
        /// For example, if two collections are identical, but have the first and second elements swapped
        /// they will have a significantly higher mean jaccard distance than two collections that have the last two elements swapped.
        ///
        /// This metric is useful for comparing f.e. two different search algorithms,
        /// where the order of elements on the first position have a higher importance than of those in the end. 
        ///
        /// The algorithmic complexity is O(n^3) so it's not ideal for larger collections or time intensive applications. 
        /// </summary>
        /// <param name="a">First collection</param>
        /// <param name="b">Second collection. Replacing a and b returns the same result.</param>
        /// <returns>A value between 0 and 1 where 0 means the collections are identical  and 1 means the collections are completely different..</returns>
        public static double CalculateMeanJaccardDistance<T>(ICollection<T> a, ICollection<T> b)
        {
            return Math.Round(1 - CalculateMeanJaccardIndex(a, b), 2);
        }

        /// <summary>
        /// Similar to the jaccard index, however it considers the order AND positions of the elements.
        /// For example, if two collections are identical, but have the first and second elements swapped
        /// they will have a significantly lower mean jaccard index than two collections that have the last two elements swapped.
        ///
        /// This metric is useful for comparing f.e. two different search algorithms,
        /// where the order of elements on the first position have a higher importance than of those in the end. 
        ///
        /// The algorithmic complexity is O(n^3) so it's not ideal for larger collections or time intensive applications. 
        /// </summary>
        /// <param name="a">First collection</param>
        /// <param name="b">Second collection. Replacing a and b returns the same result.</param>
        /// <returns>A value between 0 and 1 where 0 represents completely different collections and 1 means the collections are identical.</returns>
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