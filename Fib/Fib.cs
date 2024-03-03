namespace Fib
{
    static public class Fibonacci
    {
        public static int GetNthNumberRecursive(int n)
        {
            if (n <= 2) return 1;
            return GetNthNumberRecursive(n-1) + GetNthNumberRecursive(n-2);
        }

        public static int GetNthNumber(int n)
        {
            if (n <= 2) return 1;

            int a = 1;
            int b = 1;
            int next;
            int i = 2;
            while (true)
            {
                ++i;
                next = a + b;
                if (i == n)
                    return next;

                a = b;
                b = next;
            }
        }
    }
}