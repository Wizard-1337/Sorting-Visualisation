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
    public class CocktailSort : SortingAlgorithm
    {
        public CocktailSort()
        {
            name = "Cocktail Shaker Sort";
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

            bool swapped = true;
            int start = 0;
            int end = list.Count;

            while (swapped == true)
            {
                swapped = false;

                for (int i = start; i < end - 1; ++i)
                {
                    if (list[i] > list[i + 1])
                    {
                        int temp = list[i];
                        list[i] = list[i + 1];
                        list[i + 1] = temp;
                        accessed += 4;
                        swapped = true;

                        highlight.Clear();
                        highlight.Add(i);
                        highlight.Add(i+1);

                        if(Tick(tick))
                            eventHandler.Invoke(this, e);
                    }
                }
                comparisons++;

                if (swapped == false)
                    break;

                swapped = false;

                end = end - 1;

                for (int i = end - 1; i >= start; i--)
                {
                    if (list[i] > list[i + 1])
                    {
                        int temp = list[i];
                        list[i] = list[i + 1];
                        list[i + 1] = temp;
                        accessed += 4;
                        swapped = true;

                        highlight.Clear();
                        highlight.Add(i);
                        highlight.Add(i + 1);

                        if (Tick(tick))
                            eventHandler.Invoke(this, e);
                    }
                    comparisons++;
                }
                start = start + 1;
            }

            highlight.Clear();
            eventHandler.Invoke(this, e);
        }

    }
}
