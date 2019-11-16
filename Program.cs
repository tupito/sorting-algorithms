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

        static int[] CreateRandomOrderTable(int size)
        {
            int[] num = new int[size];
            Random generator = new Random();
            for (int i = 0; i < num.Length; i++)
            {
                num[i] = generator.Next(size);
            }

            return num;
        }

        static int[] CreateAscendingTable(int size)
        {
            Console.WriteLine("\nNouseva taulukko:");
            int[] num = new int[size];
            num = CreateRandomOrderTable(size);
            Array.Sort(num);
            return num;
        }

        static int[] CreateDescendingTable(int size)
        {
            Console.WriteLine("\nLaskeva taulukko:");
            int[] num = new int[size];
            num = CreateRandomOrderTable(size);

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
        private static void QuickSort(int[] a, int lo, int hi)
        {
            //  lo is the lower index, hi is the upper index
            //  of the region of array a that is to be sorted
            int i = lo, j = hi, h;

            // comparison element x
            int x = a[(lo + hi) / 2];

            //  partition
            do
            {
                while (a[i] < x) i++;
                while (a[j] > x) j--;
                if (i <= j)
                {
                    h = a[i];
                    a[i] = a[j];
                    a[j] = h;
                    i++; j--;
                }
            } while (i <= j);

            //  recursion
            if (lo < j) QuickSort(a, lo, j);
            if (i < hi) QuickSort(a, i, hi);
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

        static void TeeMittaukset(SortDelegate sortDelegate)
        {
            CreateArrayDelegate randomOrder = new CreateArrayDelegate(CreateRandomOrderTable);
            CreateArrayDelegate ascOrder = new CreateArrayDelegate(CreateAscendingTable);
            CreateArrayDelegate descOrder = new CreateArrayDelegate(CreateDescendingTable);

            int[] num = new int[10];
            num = CreateRandomArray(randomOrder, 10);

            Stopwatch kello = Stopwatch.StartNew(); // ota aika, käynnistä ajastin
            sortDelegate(num);
            var elapsedTime = kello.Elapsed;        // ota aika ja tulosta se
            Console.WriteLine("aika: {0}", elapsedTime);

        }

        static void Main(string[] args)
        {
            Console.WriteLine("Deletage-version");

            SortDelegate selectionSort = new SortDelegate(SelectionSort);
            SortDelegate insertionSort = new SortDelegate(InsertionSort);
            SortDelegate quick = new SortDelegate(QuickSortMain);
            SortDelegate arraySort = new SortDelegate(ArraySort);

            TeeMittaukset(selectionSort);
            TeeMittaukset(insertionSort);
            TeeMittaukset(quick);
            TeeMittaukset(arraySort);

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
