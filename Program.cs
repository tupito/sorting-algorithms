using System;

namespace Kotitehtava_Delegate
{
    class Program
    {
        public delegate void SortDelegate(int[] num);
        public delegate int[] CreateArrayDelegate(int size);

        static int[] CreateRandomArray(int size)
        {
            // tee taulukko satunnaislukuja
            return new int[size];
        }

        static void Sort1(int[] num)
        {
            Console.WriteLine("Sort 1");
        }

        static void Sort2(int[] num)
        {
            Console.WriteLine("Sort 2");
        }

        static void QuickSort(int[] num, int lo, int hi)
        {

        }
        static void QuickSortMain(int[] num)
        {
            QuickSort(num, 0, num.Length - 1);
        }

        static void Metodi(SortDelegate del)
        {
            // luo taulukko (satunnaisluvuilla)
            int[] num = new int[10000];
            // ota aika
            del(num);
            // laske aikaero
            // tulosta
        }

        static void Main(string[] args)
        {
            SortDelegate ds1 = new SortDelegate(Sort1);
            SortDelegate ds2 = new SortDelegate(Sort2);
            SortDelegate quick = new SortDelegate(QuickSortMain);

            // testataan sort1
            Metodi(ds1);
            // testataan sort2
            Metodi(ds2);
            // testataan sort2
            Metodi(quick);
        }
    }
}
