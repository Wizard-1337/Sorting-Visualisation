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
    public class ShellSort : SortingAlgorithm
    {
        public ShellSort()
        {
            name = "Shell Sort";
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

            for (int gap = n / 2; gap > 0; gap /= 2)
            {
                for (int i = gap; i < n; i += 1)
                {
                    int temp = list[i];

                    int j;
                    for (j = i; j >= gap && list[j - gap] > temp; j -= gap)
                    {
                        list[j] = list[j - gap];
                        accessed += 3;
                        comparisons++;

                        highlight.Clear();
                        highlight.Add(j);
                        highlight.Add(j - gap);

                        if(Tick(tick))
                            eventHandler.Invoke(this, e);
                    }

                    list[j] = temp;
                    accessed += 2;
                }
            }

            highlight.Clear();
            eventHandler.Invoke(this, e);
        }

    }
}
