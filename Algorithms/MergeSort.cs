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
    public class MergeSort : SortingAlgorithm
    {
        public MergeSort()
        {
            name = "Merge Sort";
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

            //if (Tick(tick))
            //    eventHandler.Invoke(this, e);
            Merge(0, list.Count, e);

            highlight.Clear();
            eventHandler.Invoke(this, e);
        }


        public void Merge(int first, int last, EventArgs e)
        {
            
            comparisons++;
            if (first + 1 == last) return;
            comparisons++;
            if (first + 2 == last)
            {
                comparisons++;
                if (list[first] > list[first + 1])
                {
                    int temp = list[first];
                    list[first] = list[first + 1];
                    list[first + 1] = temp;
                    accessed += 4;
                }
                return;
            }

            int n = list.Count;
            int mid = (first + last) / 2;

            Merge(first, mid, e);
            Merge(mid, last, e);

            int iL = first;
            int iR = mid;

            List<int> tempList = new List<int>();
            while(iL < mid && iR < last)
            {
                
                if (Tick(tick))
                    eventHandler.Invoke(this, e);
                highlight.Clear();
                highlight.Add(iL);
                highlight.Add(iR);

                if (list[iL] < list[iR])
                {
                    tempList.Add(list[iL]);
                    iL++;
                }
                else
                {
                    tempList.Add(list[iR]);
                    iR++;
                }
                comparisons += 2;
                accessed += 4;
            }

            while(iL < mid)
            {
                accessed++;
                comparisons++;
                if (Tick(tick))
                    eventHandler.Invoke(this, e);
                highlight.Clear();
                highlight.Add(iL);
                tempList.Add(list[iL]);
                iL++;
            }

            while (iR < last)
            {
                accessed++;
                comparisons++;
                if (Tick(tick))
                    eventHandler.Invoke(this, e);
                highlight.Clear();
                highlight.Add(iR);
                tempList.Add(list[iR]);
                iR++;
            }

            for (int i = 0; i < tempList.Count; i++)
            {
                list[i + first] = tempList[i];
            }

            accessed += tempList.Count;
        }



    }
}
