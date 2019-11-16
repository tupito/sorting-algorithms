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
            int[] num = new int[size];
            num = CreateRandomOrderTable(size);
            Array.Sort(num);
            return num;
        }

        static int[] CreateDescendingTable(int size)
        {
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
        static void QuickSort(int[] a, int lo, int hi)
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

        static void MergeSortMain(int[] num)
        {
            Console.WriteLine("\nMergeSort");
            MergeSort(num, 0, num.Length-1);
        }

        static public void MergeSort(int[] arr, int p, int r)
        {
            if (p < r)
            {
                int q = (p + r) / 2;
                MergeSort(arr, p, q);
                MergeSort(arr, q + 1, r);
                Merge(arr, p, q, r);
            }
        }
        static public void Merge(int[] arr, int p, int q, int r)
        {
            int i, j, k;
            int n1 = q - p + 1;
            int n2 = r - q;
            int[] L = new int[n1];
            int[] R = new int[n2];
            for (i = 0; i < n1; i++)
            {
                L[i] = arr[p + i];
            }
            for (j = 0; j < n2; j++)
            {
                R[j] = arr[q + 1 + j];
            }
            i = 0;
            j = 0;
            k = p;
            while (i < n1 && j < n2)
            {
                if (L[i] <= R[j])
                {
                    arr[k] = L[i];
                    i++;
                }
                else
                {
                    arr[k] = R[j];
                    j++;
                }
                k++;
            }
            while (i < n1)
            {
                arr[k] = L[i];
                i++;
                k++;
            }
            while (j < n2)
            {
                arr[k] = R[j];
                j++;
                k++;
            }
        }

        static void TeeMittaukset(SortDelegate sortDelegate)
        {
            CreateArrayDelegate randomOrder = new CreateArrayDelegate(CreateRandomOrderTable);
            CreateArrayDelegate ascOrder = new CreateArrayDelegate(CreateAscendingTable);
            CreateArrayDelegate descOrder = new CreateArrayDelegate(CreateDescendingTable);

            int arrSize = 10000;

            int[] num = new int[arrSize];

            // järjestämätön taulukko
            num = CreateRandomArray(randomOrder, arrSize);
            var elapsedTime = MittaaAika(sortDelegate, num);
            Console.WriteLine("Järjestämätön taulukko, aika: {0}", elapsedTime);

            // nouseva järjestys
            num = CreateRandomArray(ascOrder, arrSize);
            elapsedTime = MittaaAika(sortDelegate, num);
            Console.WriteLine("Nouseva taulukko, aika: {0}", elapsedTime);

            // laskeva järjestys
            num = CreateRandomArray(descOrder, arrSize);
            elapsedTime = MittaaAika(sortDelegate, num);
            Console.WriteLine("Laskeva taulukko, aika: {0}", elapsedTime);
        }

        static TimeSpan MittaaAika(SortDelegate sortDelegate, int[] num)
        {
            Stopwatch kello = Stopwatch.StartNew(); // käynnistä ajastin
            sortDelegate(num);                      // kutsu järjestysalgoritmiä
            return kello.Elapsed;                   // palauta aika
        }

        static void Main(string[] args)
        {
            SortDelegate selectionSort = new SortDelegate(SelectionSort);
            SortDelegate insertionSort = new SortDelegate(InsertionSort);
            SortDelegate quick = new SortDelegate(QuickSortMain);
            SortDelegate arraySort = new SortDelegate(ArraySort);
            SortDelegate mergeSort = new SortDelegate(MergeSortMain);

            TeeMittaukset(selectionSort);
            TeeMittaukset(insertionSort);
            TeeMittaukset(quick);
            TeeMittaukset(arraySort);
            TeeMittaukset(mergeSort);
        }
    }
}
