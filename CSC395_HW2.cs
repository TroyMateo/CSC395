/*Author: Troy Mateo
 * Date: 05/24/2019
 * C# program demonstrating use of sorting methods and documentation of 
 * difference in efficiency
 */
using System;
using System.IO;

namespace CSC395_HW2
{
    class Program
    {
        static void Main(string[] args)
        {
            //Different string arrays to test out sorting methods
            //string[] arr1 = {"bubble", "apple", "car", "tesla", "swag"  };
            //string[] arr2 = { "jenkins", "robin", "leeroy", "ace", "sun" };
            string[] arr3 = { "Dog", "Cat", "Fish", "Weasel", "Zebra", "Horse" };
            //string[] arr4 = { "microsoft", "google", "amazon", "facebook", "netflix" };

            string fileName = "./input.txt";

            string[] myArr1 = File.ReadAllLines(fileName);
            string[] myArr2 = File.ReadAllLines(fileName);
            string[] myArr3 = File.ReadAllLines(fileName);
            string[] myArr4 = File.ReadAllLines(fileName);


            //initial testing of reverseSort methods
            //bubbleReverseSort(arr1);
            //selectionReverseSort(arr2);
            mergeReverseSort(arr3);
            //quickReverseSort(arr4);

            var watch = System.Diagnostics.Stopwatch.StartNew();

            //Tests on input.txt file
            //bubbleReverseSort(myArr1); //69009ms to execute
            //selectionReverseSort(myArr2); //38274 ms to execute
            //mergeReverseSort(myArr3); //7295ms to execute
            //quickReverseSort(myArr4);  //6740ms to execute

            watch.Stop();
            var elapsedMs = watch.ElapsedMilliseconds;

            Console.WriteLine("Running time in milliseconds: " + elapsedMs);

        }

        //Utility methods to see progress in sorting

        private static void printUnsorted(string[] arr)
        {
            Console.WriteLine("Array BEFORE sorting: ");
            foreach (string word in arr)
            {
                Console.Write(word + " ");
            }
            Console.WriteLine();
        }

        private static void printSorted(string[] arr)
        {
            Console.WriteLine("Array AFTER sorting: ");
            foreach (string word in arr)
            {
                Console.Write(word + " ");
            }
            Console.WriteLine();
        }

        private static void printReverse(string[] arr)
        {
            Console.WriteLine("Array REVERSED: ");
            foreach (string word in arr)
            {
                Console.Write(word + " ");
            }
            Console.WriteLine();
        }

        private static void printCount(long count)
        {
            Console.WriteLine("Number of comparisons");
            Console.WriteLine(count);
        }

        public static void bubbleReverseSort(string[] arr)
        {
            long count = 0;

            printUnsorted(arr);
            
            //Works iterations backwards to reduce number of comparisons after each passthrough
            for (int j = arr.Length - 1; j > 0; j--)
            {
                //Works through comparing elements in the array
                for (int i = 0; i < arr.Length - 1; i++)
                {
                    if (arr[i].CompareTo(arr[i + 1]) > 0)
                    {
                        //Swaps the elements at the two indeces
                        string tmp = arr[i];
                        arr[i] = arr[i + 1];
                        arr[i + 1] = tmp;
                    }
                    count++;
                }
            }

            Console.WriteLine();
            printSorted(arr);

            //Indeces start at opposite ends and work inwards towards each other
            int x = 0;
            int y = arr.Length - 1;
            while (y > x)
            {
                //Swaps the elements at the two indeces reversing the order
                string tmp2 = arr[x];
                arr[x] = arr[y];
                arr[y] = tmp2;

                //Moves the indeces along
                x++;
                y--;
            }

            Console.WriteLine();
            printReverse(arr);

            Console.WriteLine();
            printCount(count);
        }//O(n^3) because of sorting then reversing

        public static void selectionReverseSort(string[] arr) 
        {
            long count = 0;

            printUnsorted(arr);

            for (int i = 0; i < arr.Length - 1; i++)
            {
                //Sets the current position to var i where the minimum value will be sorted into
                int minPos = i;
                for (int j = i + 1; j < arr.Length; j++)
                {
                    //Compares all values less than the current position
                    if (arr[j].CompareTo(arr[minPos]) < 0)
                    {
                        minPos = j;
                    }
                    //swap elements at minPos and i

                    count++;
                }
                string tmp = arr[i];
                arr[i] = arr[minPos];
                arr[minPos] = tmp;
            }

            Console.WriteLine();
            printSorted(arr);

            //Indeces start at opposite ends and work inwards towards each other
            int x = 0;
            int y = arr.Length - 1;
            while (y > x)
            {
                //Swaps the elements at the two indeces reversing the order
                string tmp2 = arr[x];
                arr[x] = arr[y];
                arr[y] = tmp2;

                //Moves the indeces along
                x++;
                y--;
            }

            Console.WriteLine();
            printReverse(arr);

            Console.WriteLine();
            printCount(count);
        }//)(n^3) because of sorting then reversing

        public static void mergeReverseSort(string[] arr)
        {
            long count = 0;

            printUnsorted(arr);

            //Calls helper methods
            string[] tmp = new string[arr.Length];
            MergeSortHelper(arr, 0, arr.Length - 1, tmp, ref count);

            Console.WriteLine();
            printSorted(arr);

            //This time using array library to reverse
            Array.Reverse(arr);

            Console.WriteLine();
            printReverse(arr);

            Console.WriteLine();
            printCount(count);
        } //Worse case is O(n^3) because of sorting then reversing

        //recursive method
        public static void MergeSortHelper(string[] arr, int first, int last, string[] tmp, ref long count)
        {
            if (first < last) //if we have at least two elements
            {
                //Finds the middle index
                int mid = (first + last) / 2;
                //Calls method on left array
                MergeSortHelper(arr, first, mid, tmp, ref count);
                //Calls method on right array
                MergeSortHelper(arr, mid + 1, last, tmp, ref count);
                //Merges the arrays together
                Merge(arr, first, mid + 1, last, tmp, ref count);
            }

        }

        public static void Merge(string[] arr, int first, int mid, int last, string[] tmp, ref long count)
        {
            //tmp is the temporary buffer
            int i = first;
            int j = mid;
            int k = first;

            //Given the elements have not reached their outer limits
            while (i < mid && j <= last) // Pick smallest elements of the two array
            {
                //If the element in the left sorted array is less then go with it otherwise pick from the right array
                if (arr[i].CompareTo(arr[j]) < 0)
                {
                    tmp[k] = arr[i];
                    i++;
                    k++;
                }
                else
                {
                    tmp[k] = arr[j];
                    j++;
                    k++;
                }
                count++;
            }

            //Put the remaining elements

            while (i < mid)
            {
                tmp[k] = arr[i];
                i++;
                k++;
            }
            while (j <= last)
            {
                tmp[k] = arr[j];
                j++;
                k++;
            }

            //Copy tmp back into arr

            for (int p = first; p <= last; p++)
            {
                arr[p] = tmp[p];
            }          
        }

        public static void quickReverseSort(string[] arr)
        {
            long count = 0;

            printUnsorted(arr);

            quickSort(arr, 0, arr.Length - 1, ref count);

            Console.WriteLine();
            printSorted(arr);

            Array.Reverse(arr);

            Console.WriteLine();
            printReverse(arr);

            Console.WriteLine();
            printCount(count);
        } //O(n^2Logn)

        public static void quickSort(string[] arr, int leftIdx, int rightIdx, ref long count)
        {
            if (leftIdx < rightIdx)
            {
                //recursive method calls itself
                //partitioning index allows for "sorting" in place
                int partIdx = Partition(arr, leftIdx, rightIdx, ref count);
                //method call on left partition
                quickSort(arr, leftIdx, partIdx - 1, ref count);
                //method all on right partition
                quickSort(arr, partIdx + 1, rightIdx, ref count);
            }
        }

        public static int Partition(string[] arr, int leftIdx, int rightIdx, ref long count)
        {
            string pivot = arr[rightIdx];

            // index of smaller element 
            int i = (leftIdx - 1);
            for (int j = leftIdx; j < rightIdx; j++)
            {
                // If current element is smaller  
                // than or equal to pivot 
                if (arr[j].CompareTo(pivot) <= 0)
                {
                    i++;

                    // swap arr[i] and arr[j] 
                    string tmp = arr[i];
                    arr[i] = arr[j];
                    arr[j] = tmp;
                }
                count++;
            }

            // swap arr[i+1] and arr[rightIdx]  
            string tmp1 = arr[i + 1];
            arr[i + 1] = arr[rightIdx];
            arr[rightIdx] = tmp1;

            return i + 1;
        }

        
    }
}
