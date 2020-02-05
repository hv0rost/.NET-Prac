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
            Console.Write("Enter count of elements: ");
            int count = int.Parse(Console.ReadLine());

            int[] arr = new int[count];
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

            for (int k = 0; k < n; k++)
            {
                int nomerPotoka = k;
                int chunk = count / n;
                int start = nomerPotoka * chunk;

                Console.WriteLine(nomerPotoka.ToString() + "-й поток отсортировал: ");

                for (int j = start; j < start + chunk; j++)
                {
                    Console.Write(arr[j] + " ");
                }
                Console.WriteLine();
            }

            Console.WriteLine("\nComleted Array:");

            for (int i = 0; i < arr.Length; i++)
                Console.Write($"{arr[i]} ");

            Console.ReadKey();
        }
    }
}
