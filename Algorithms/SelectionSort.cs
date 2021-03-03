using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sort_Visualisation
{
    public class SelectionSort : SortingAlgorithm
    {
        public SelectionSort()
        {
            name = "Selection Sort";
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
            for (int i = 0; i < n-1; i++)
            {
                for (int j = i + 1; j < n; j++)
                {
                    comparisons ++;
                    accessed += 2;

                    if (list[i] > list[j])
                    {
                        int temp = list[i];
                        list[i] = list[j];
                        list[j] = temp;
                        accessed += 4;  
                    }

                    highlight.Clear();
                    highlight.Add(i);
                    highlight.Add(j);
                    if (Tick(tick))
                        eventHandler.Invoke(this, e);
                    
                }
            }

            highlight.Clear();
            eventHandler.Invoke(this, e);
        }

        

    }

    
}
