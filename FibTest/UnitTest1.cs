using Fib;

namespace FibTest
{
    [TestClass]
    public class TestFibonacci
    {
        [TestMethod]
        public void TestGetNthNumber()
        {
            int[] expected = { 1, 1, 2, 3, 5, 8, 13, 21, 34 };
            for (int i = 0; i < expected.Length; i++)
            {
                int result = Fibonacci.GetNthNumber(i + 1);
                Assert.AreEqual(expected[i], result, string.Format("Expected for n {0}: {1}; Actual: {2}",
                                     i+1, expected[i], result));
            }
        }

        [TestMethod]
        public void TestGetNthNumberRecursive()
        {
            int[] expected = { 1, 1, 2, 3, 5, 8, 13, 21, 34 };
            for (int i = 0; i < expected.Length; i++)
            {
                int result = Fibonacci.GetNthNumberRecursive(i + 1);
                Assert.AreEqual(expected[i], result, string.Format("Expected for n {0}: {1}; Actual: {2}",
                                     i + 1, expected[i], result));
            }
        }
    }
}