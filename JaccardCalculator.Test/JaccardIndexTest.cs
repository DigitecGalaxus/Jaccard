using System;
using System.Linq;
using NUnit.Framework;

namespace JaccardCalculator.Test
{
    [TestFixture]
    public class JaccardIndexTest
    {
        [Test]
        public void CalculateJaccardIndex_ExpectArgumentException()
        {
            Assert.Throws<ArgumentNullException>(
                () => JaccardIndex.CalculateJaccardIndex(new[] {0}, null));

            Assert.Throws<ArgumentNullException>(
                () => JaccardIndex.CalculateJaccardIndex(null, new[] {0}));

            Assert.Throws<ArgumentNullException>(
                () => JaccardIndex.CalculateJaccardIndex<int>(null, null));
        }

        [Test]
        public void CalculateJaccardIndex_HandleSpecialCases()
        {
            var aSet = new[] {1};
            var bSet = Array.Empty<int>();
            var jaccardIndex = JaccardIndex.CalculateJaccardIndex(aSet, bSet);

            Assert.AreEqual(0, jaccardIndex);

            aSet = Array.Empty<int>();
            bSet = new[] {1};
            jaccardIndex = JaccardIndex.CalculateJaccardIndex(aSet, bSet);

            Assert.AreEqual(0, jaccardIndex);

            aSet = Array.Empty<int>();
            bSet = Array.Empty<int>();
            jaccardIndex = JaccardIndex.CalculateJaccardIndex(aSet, bSet);

            Assert.AreEqual(1, jaccardIndex);
        }

        [Test]
        public void CalculateJaccardIndex_SameSets()
        {
            var aSet = new[] {1};
            var bSet = new[] {1};
            var jaccardIndex = JaccardIndex.CalculateJaccardIndex(aSet, bSet);

            Assert.AreEqual(1, jaccardIndex);
        }

        [Test]
        public void CalculateJaccardIndex_CompletelyDifferentSets()
        {
            var aSet = new[] {0};
            var bSet = new[] {1};
            var jaccardIndex = JaccardIndex.CalculateJaccardIndex(aSet, bSet);

            Assert.AreEqual(0, jaccardIndex);
        }

        [Test]
        public void CalculateJaccardIndex_HalfwayDifferent()
        {
            var aSet = new[] {0, 1, 2, 5, 6};
            var bSet = new[] {0, 2, 3, 4, 5, 7, 9};
            var jaccardIndex = JaccardIndex.CalculateJaccardIndex(aSet, bSet);

            Assert.AreEqual(0.33, jaccardIndex);
        }

        [Test]
        public void CalculateJaccardDistance_HalfwayDifferent()
        {
            var aSet = new[] {0, 1, 2, 5, 6};
            var bSet = new[] {0, 2, 3, 4, 5, 7, 9};
            var jaccardIndex = JaccardIndex.CalculateJaccardDistance(aSet, bSet);

            Assert.AreEqual(1 - 0.33, jaccardIndex);
        }

        [Test]
        public void CalculateJaccardIndex_DifferentOrder_ExpectSameJaccardIndex()
        {
            var aSet = new[] {0, 1, 2, 3, 4};
            var bSet = aSet.Reverse().ToArray();

            var jaccardIndex = JaccardIndex.CalculateJaccardIndex(aSet, bSet);

            Assert.AreEqual(1, jaccardIndex);
        }
    }
}