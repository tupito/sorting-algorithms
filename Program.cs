using System;
using System.Diagnostics; // stopwatch

namespace Kotitehtava_Delegate
{
    class Program
    {
        public delegate void SortDelegate(int[] num);
        public delegate int[] CreateArrayDelegate(int size);

        static int[] CreateRandomArray(CreateArrayDelegate createArrayDelegate, int size)
        {
            return createArrayDelegate(size);
        }

        static int[] CreateAscendingTable(int size)
        {

            int[] num = new int[size];
            Random generator = new Random();
            for (int i = 0; i < num.Length; i++)
            {
                num[i] = generator.Next(size);
            }

            Array.Sort(num);
            return num;
        }

        static int[] CreateDescendingTable(int size)
        {
            int[] num = new int[size];
            Random generator = new Random();
            for (int i = 0; i < num.Length; i++)
            {
                num[i] = generator.Next(size);
            }

            Array.Sort<int>(num, new Comparison<int>(
                        (n1, n2) => n2.CompareTo(n1)
                    ));
            return num;
        }

        static void SelectionSort(int[] num)
        {
            Console.WriteLine("\nSelectionSort: ");
            int i, j, first, temp;
            for (i = num.Length - 1; i > 0; i--)
            {
                first = 0;   //initialize to subscript of first element
                for (j = 1; j <= i; j++)   //locate smallest element between positions 1 and i.
                {
                    if (num[j] > num[first])
                        first = j;
                }
                temp = num[first];   //swap smallest found with element in position i.
                num[first] = num[i];
                num[i] = temp;
            }
        }

        static void InsertionSort(int[] num)
        {
            Console.WriteLine("\nInsertionSort");

            for (int i = 1; i < num.Length; i++)
            {
                int key = num[i];
                int j = i - 1;
                while (j >= 0 && num[j] > key)
                {
                    //loop
                    num[j + 1] = num[j];
                    j--;
                }
                num[j + 1] = key;
            }
        }

        private static int Partition(int[] num, int lo, int hi)
        {
            int pivot = num[lo];
            while (true)
            {

                while (num[lo] < pivot)
                {
                    lo++;
                }

                while (num[hi] > pivot)
                {
                    hi--;
                }

                if (lo < hi)
                {
                    if (num[lo] == num[hi]) return hi;

                    int temp = num[lo];
                    num[lo] = num[hi];
                    num[hi] = temp;
                }
                else
                {
                    return hi;
                }
            }
        }

        static void QuickSort(int[] num, int lo, int hi)
        {
            if (lo < hi)
            {
                int pivot = Partition(num, lo, hi);

                if (pivot > 1)
                {
                    QuickSort(num, lo, pivot - 1);
                }
                if (pivot + 1 < hi)
                {
                    QuickSort(num, pivot + 1, hi);
                }
            }
        }

        static void QuickSortMain(int[] num)
        {
            Console.WriteLine("\nQuicksort");
            QuickSort(num, 0, num.Length - 1);
        }

        static void ArraySort(int[] num)
        {
            Console.WriteLine("\nArray.Sort");
            Array.Sort(num);
        }

        static void Metodi(SortDelegate sortDelegate)
        {
            // CreateArrayDelegate randomOrder = new CreateArrayDelegate(RandomOrder);
            CreateArrayDelegate ascOrder = new CreateArrayDelegate(CreateAscendingTable);
            CreateArrayDelegate descOrder = new CreateArrayDelegate(CreateDescendingTable);

            //CreateAscendingTable(ascOrder);
            int[] num = new int[10000];
            num = CreateRandomArray(ascOrder, 10000);


            // luo taulukko (satunnaisluvuilla)
            //int[] num = new int[10000];
            //num = CreateRandomArray(10000);

            // ota aika, käynnistä ajastin
            Stopwatch kello = Stopwatch.StartNew();
            sortDelegate(num);
            // ota aika ja tulosta se
            var elapsedTime = kello.Elapsed;
            Console.WriteLine("aika: {0}", elapsedTime);

            /*foreach (var item in num)
            {
                Console.Write($"{item} ");
            }*/
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Deletage-version");

            SortDelegate selectionSort = new SortDelegate(SelectionSort);
            SortDelegate insertionSort = new SortDelegate(InsertionSort);
            SortDelegate quick = new SortDelegate(QuickSortMain);
            SortDelegate arraySort = new SortDelegate(ArraySort);

            // testataan sort1
            Metodi(selectionSort);
            // testataan sort2
            Metodi(insertionSort);
            // testataan sort2
            Metodi(quick);
            // Array.sort
            Metodi(arraySort);

            //testing
            /*
            int[] test = new int[10];
            test = CreateAscendingTable(10);
            Console.WriteLine("Asc table, testing");
            foreach (var item in test)
            {
                Console.WriteLine(item);
            }
            */

            //testing
            /*test = CreateDescendingTable(10);
            Console.WriteLine("desc, testing");
            foreach (var item in test)
            {
                Console.WriteLine(item);
            }*/


        }
    }
}
