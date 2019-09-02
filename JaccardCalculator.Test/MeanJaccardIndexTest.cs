using NUnit.Framework;

namespace JaccardCalculator.Test
{
    [TestFixture]
    public class MeanJaccardIndexTest
    {
        [Test]
        public void CalculateMeanJaccardIndex_SameArrays()
        {
            var a = new[] {1, 2, 3};
            var b = new[] {1, 2, 3};

            var result = JaccardIndex.CalculateMeanJaccardIndex(a, b);
            
            Assert.AreEqual(1, result);
        }
        
        [Test]
        public void CalculateMeanJaccardIndex_SameArraysDifferentOrder()
        {
            var a = new[] {1, 2, 3};
            var b = new[] {1, 3, 2};

            var result = JaccardIndex.CalculateMeanJaccardIndex(a, b);
            
            Assert.AreNotEqual(1, result);
        }
    }
}