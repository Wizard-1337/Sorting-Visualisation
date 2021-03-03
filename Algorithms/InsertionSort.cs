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
    public class InsertionSort : SortingAlgorithm
    {
        public InsertionSort()
        {
            name = "Insertion Sort";
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

            for (int i = 1; i < n; ++i)
            {
                int key = list[i];
                int j = i - 1;
                accessed++;

                while (j >= 0 && list[j] > key)
                {
                    list[j + 1] = list[j];
                    j = j - 1;

                    accessed += 2;
                    comparisons += 2;

                    highlight.Clear();
                    highlight.Add(i);
                    highlight.Add(j + 1);

                    if (Tick(tick))
                        eventHandler.Invoke(this, e);
                }
                list[j + 1] = key;
                accessed++;
            }

            highlight.Clear();
            eventHandler.Invoke(this, e);
        }

    }
}
