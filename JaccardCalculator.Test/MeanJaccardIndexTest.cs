using NUnit.Framework;

namespace JaccardCalculator.Test
{
    [TestFixture]
    public class MeanJaccardIndexTest
    {
        [Test]
        public void CalculateMeanJaccardIndex_OneElement()
        {
            var a = new[] {1};
            var b = new[] {2};

            var result = JaccardIndex.CalculateMeanJaccardIndex(a, b);
            
            Assert.AreEqual(0, result);
        }
        
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
        
        [Test]
        public void CalculateMeanJaccardIndex_DifferentLength()
        {
            var a = new[] {1, 2, 3};
            var b = new[] {1, 2, 3, 4};

            var result = JaccardIndex.CalculateMeanJaccardIndex(a, b);
            
            Assert.AreEqual(0.94, result);
        }
        
        [Test]
        public void CalculateMeanJaccardIndex_DifferentLength_OneEmpty()
        {
            var a = new[] {1, 2, 3};
            var b = new[] {1, 2, 3, 4};

            var result = JaccardIndex.CalculateMeanJaccardIndex(a, b);
            
            Assert.AreEqual(0.94, result);
        }
        
        [Test]
        public void CalculateMeanJaccardDistance_DifferentLength_OneEmpty()
        {
            var a = new[] {1, 2, 3};
            var b = new[] {1, 2, 3, 4};

            var result = JaccardIndex.CalculateMeanJaccardDistance(a, b);
            
            Assert.AreEqual(0.06, result);
        }
    }
}