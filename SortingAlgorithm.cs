using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace Sort_Visualisation
{
    public class SortingAlgorithm
    {
        public string name = "";
        public int tick;
        public List<int> highlight;
        public int highlightLen = 2;
        public int minTick = 40;
        public long elapsed = 0;
        public Stopwatch timer;
        public int accessed;
        public int comparisons;
        public int ticks;
        public List<int> list;
        public BackgroundWorker worker;
        public EventHandler eventHandler;
        public SortingAlgorithm()
        {
            
        }

        public virtual void Start(List<int> _list, int _tick = 25)
        {
           
        }

        public void Update(object sender, EventArgs e)
        {

        }

        public bool Tick(int time = 40)
        {
            bool weWait = true;
            timer.Stop();
            elapsed += timer.ElapsedMilliseconds + 1;
            if (elapsed > minTick)
            {
                elapsed = 0;
                System.Threading.Thread.Sleep(time);
            }
            else
            {
                weWait = false;
            }
            timer.Start();
            System.Threading.Thread.Sleep(time);
            return weWait;
        }

    }

}
