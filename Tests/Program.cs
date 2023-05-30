using System;

namespace Test
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] arr = { 1,2,3,4 };
            Do(1,2,3,4);
          
        }

        private static void Do(params int[] a)
        {
            var temp = new int[a.Length + 1];
            Console.WriteLine(temp[0]);
        }
    }

}
