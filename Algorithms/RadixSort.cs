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
    public class RadixSort : SortingAlgorithm
    {
        public RadixSort()
        {
            name = "Radix Sort";
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
            int max = list.Max();

            for (int exp = 1; max / exp > 0; exp *= 10)
                countSort(n, exp, e);

            highlight.Clear();
            eventHandler.Invoke(this, e);
        }

        public void countSort(int n, int exp, EventArgs e)
        {
            int[] output = new int[n];
            int i;
            int[] count = new int[10];

            for (i = 0; i < 10; i++)
                count[i] = 0;
            accessed += 10;

            for (i = 0; i < n; i++)
                count[(list[i] / exp) % 10]++;

            for (i = 1; i < 10; i++)
                count[i] += count[i - 1];

            accessed += 10;

            for (i = n - 1; i >= 0; i--)
            {
                output[count[(list[i] / exp) % 10] - 1] = list[i];
                count[(list[i] / exp) % 10]--;
            }

            for (i = 0; i < n; i++)
            {
                list[i] = output[i];

                accessed += 6;
                highlight.Clear();
                highlight.Add(i);

                if (Tick(tick))
                    eventHandler.Invoke(this, e);
            }
        }

    }
}
