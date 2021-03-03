using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sort_Visualisation
{
    public class QuickSort : SortingAlgorithm
    {
        public QuickSort()
        {
            name = "Quick Sort";
            highlight = new List<int>();
            timer = new Stopwatch();
            worker = new BackgroundWorker();
            worker.DoWork += Update;
            worker.WorkerReportsProgress = false;
        }

        public override void Start(List<int> _list, int _tick = 25)
        {
            list = _list;
            tick = _tick;
            comparisons = 0;
            accessed = 0;
            elapsed = 0;
            ticks = 0;
            timer.Start();
            worker.RunWorkerAsync();
        }

        int Partition(EventArgs e, int left, int right)
        {
            int pivot = list[left];
            accessed++;
            while (true)
            {

                while (list[left] < pivot)
                {
                    left++;
                    comparisons++;
                }

                while (list[right] > pivot)
                {
                    right--;
                    comparisons++;
                }

                comparisons++;

                if (left < right)
                {
                    accessed += 2;
                    comparisons++;
                    if (list[left] == list[right]) return right;

                    int temp = list[left];
                    list[left] = list[right];
                    list[right] = temp;

                    accessed += 4;

                    highlight.Clear();
                    highlight.Add(left);
                    highlight.Add(right);

                    if (Tick(tick))
                        eventHandler.Invoke(this, e);
                }
                else
                {
                    return right;
                }
            }
        }

        public void Quick_Sort(EventArgs e, int left, int right)
        {
            if (left < right)
            {
                int pivot = Partition(e, left, right);

                if (pivot > 1)
                {
                    Quick_Sort(e, left, pivot - 1);
                }
                if (pivot + 1 < right)
                {
                    Quick_Sort(e, pivot + 1, right);
                }
            }

        }



        public new void Update(object sender, EventArgs e)
        {
            int n = list.Count;

            Quick_Sort(e, 0, n - 1);

            highlight.Clear();
            eventHandler.Invoke(this, e);
        }

    }
}
