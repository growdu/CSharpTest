using System.Collections.Generic;

namespace Algorithms.Sorters
{
    /// <summary>
    /// 冒泡排序
    /// 每次通过两两比较交换位置，选出剩余无序序列里最大（小）的数据元素放到队尾
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class BubbleSorter<T> : ISorter<T>
    {       
        public  void Sort(T[] array, IComparer<T> comparer)
        {
            for (var i = 0; i < array.Length - 1; i++)
            {
                var wasChanged = false;
                for (var j = 0; j < array.Length - i - 1; j++)
                {
                    if (comparer.Compare(array[j], array[j + 1]) == 1)
                    {
                        var temp = array[j];
                        array[j] = array[j + 1];
                        array[j + 1] = temp;
                        wasChanged = true;
                    }
                }
                if (!wasChanged)
                {
                    break;
                }
            }
        }
    }

    public class IntSort : IComparer<int>
    {
        static void Main(string[] args)
        {
            var test = new int[] { 3, 2, 9, 4, 7 };
            BubbleSorter<int> bubble = new BubbleSorter<int>();
            IComparer<int> comparer = new IntSort();
            bubble.Sort(test, comparer);
        }

        public int Compare(int x, int y)
        {
            if (x > y)
                return 1;

            if (x < y)
                return -1;

            return 0;
        }
    }

}
