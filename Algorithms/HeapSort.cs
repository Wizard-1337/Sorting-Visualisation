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
    public class HeapSort : SortingAlgorithm
    {
        public HeapSort()
        {
            name = "Heap Sort";
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

        public new void Update(object sender, EventArgs e)
        {
            int n = list.Count;

            for (int i = n / 2 - 1; i >= 0; i--)
                heapify(n, i);

            for (int i = n - 1; i > 0; i--)
            {
                int temp = list[0];
                list[0] = list[i];
                list[i] = temp;
                accessed += 4;
                heapify(i, 0);
                highlight.Add(i);

                if (Tick(tick))
                    eventHandler.Invoke(this, e);

                highlight.Clear();
               
            }

            highlight.Clear();
            eventHandler.Invoke(this, e);
        }

        void heapify(int n, int i)
        {
            int largest = i;
            int l = 2 * i + 1;
            int r = 2 * i + 2; 

            if (l < n && list[l] > list[largest])
                largest = l;
            if (r < n && list[r] > list[largest])
                largest = r;
            comparisons += 4;
            accessed += 4;
            if (largest != i)
            {
                int swap = list[i];
                list[i] = list[largest];
                list[largest] = swap;
                accessed += 4;
                highlight.Add(i);
                heapify(n, largest);
            }
        }

    }
}
