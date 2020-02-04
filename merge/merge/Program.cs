using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace merge
{
    class Program
    {
        public static void Sort(object arr)
        {
            
            Array.Sort((int[])arr, 0, ((int[])arr).Length);
        }
        static void Main(string[] args)
        {
            int[] arr = new int[60];
            Random rnd = new Random();

            for (int i = 0; i < arr.Length; i++)
                arr[i] = rnd.Next(0, arr.Length);

            for (int i = 0; i < arr.Length; i++)
                Console.Write($"{arr[i]} ");

            Console.WriteLine();
            Console.WriteLine();

            int n = 5;
            Thread[] sorting = new Thread[n];
            for(int k = 0; k < n; k++)
            {
                sorting[k] = new Thread(new ParameterizedThreadStart(Sort));
                sorting[k].Start(arr);
            }

            for (int k = 0; k < n; k++)
                sorting[k].Join();

            for (int i = 0; i < arr.Length; i++)
                Console.Write($"{arr[i]} ");

            Console.ReadKey();
        }
    }
}
