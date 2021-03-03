using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace Sort_Visualisation
{
    public partial class MainForm : Form
    {
        Stopwatch timer = new Stopwatch();
        Color backgroundColor = Color.Black;
        Color foregroundColor = Color.White;
        Color highlightColor = Color.Red;
        Graphics graphics;
        List<int> list;
        SortingAlgorithm algo;
        List<SortingAlgorithm> sortingAlgorithms;

        public MainForm()
        {
            InitializeComponent();
        }


        private void MainForm_Load(object sender, EventArgs e)
        {
            graphics = panel1.CreateGraphics();
            comboBox1.Items.Clear();

            sortingAlgorithms = new List<SortingAlgorithm>();
            sortingAlgorithms.Add(new QuickSort());
            sortingAlgorithms.Add(new CocktailSort());
            sortingAlgorithms.Add(new ShellSort());
            sortingAlgorithms.Add(new HeapSort());
            sortingAlgorithms.Add(new RadixSort());
            sortingAlgorithms.Add(new InsertionSort());
            sortingAlgorithms.Add(new BubbleSort());
            sortingAlgorithms.Add(new MergeSort());
            sortingAlgorithms.Add(new SelectionSort());

            for (var i = 0; i < sortingAlgorithms.Count; i++)
                comboBox1.Items.Add(sortingAlgorithms[i].name);

            comboBox1.SelectedItem = comboBox1.Items[0];
            list = new List<int>();
        }


        private void MainForm_Resize(object sender, EventArgs e)
        {
            graphics = panel1.CreateGraphics();
            graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighSpeed;
            Draw();
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            Shuffle();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            algo = sortingAlgorithms[comboBox1.SelectedIndex];
            algo.eventHandler += DrawCall;
            algo.Start(list, (int)numericUpDown2.Value);
            timer.Restart();
        }

        delegate void SetTextCallback(string text);

        private void SetText(string text)
        {
            if (this.textBox1.InvokeRequired)
            {
                SetTextCallback d = new SetTextCallback(SetText);
                this.Invoke(d, new object[] { text });
            }
            else
            {
                this.textBox1.Text = text;
            }
        }

        private void SetText2(string text)
        {
            if (this.textBox2.InvokeRequired)
            {
                SetTextCallback d = new SetTextCallback(SetText2);
                this.Invoke(d, new object[] { text });
            }
            else
            {
                this.textBox2.Text = text;
            }
        }

        private void SetText3(string text)
        {
            if (this.textBox3.InvokeRequired)
            {
                SetTextCallback d = new SetTextCallback(SetText3);
                this.Invoke(d, new object[] { text });
            }
            else
            {
                this.textBox3.Text = text;
            }
        }

        public void DrawCall(object sender, EventArgs e)
        {
            Draw();
            var thisAlgo = (sender as SortingAlgorithm);
            SetText("" + thisAlgo.comparisons);
            SetText2("" + thisAlgo.accessed);
            SetText3("" + timer.ElapsedMilliseconds);
        }

        public void Draw()
        {
            
            int width = panel1.Width;
            int height = panel1.Height;
            graphics.Clear(backgroundColor);
            Brush brush = new SolidBrush(foregroundColor);
            Brush highlightBrush = new SolidBrush(highlightColor);
            int maxValue = list.Count;

            float normalWidth = (float)width / (float)maxValue;
            int solidWidth = (int)(normalWidth / 5.0f);
            int spacingWidth = (int)normalWidth - solidWidth;

            for (int i = 0; i < maxValue; i++)
            {
                Brush currentBrush = brush;
                if(algo != null)
                {
                    if (algo.highlight.Contains(i))
                        currentBrush = highlightBrush;
                }
                float currentRatio = (float)list[i] / (float)maxValue;
                int currentHeight = (int)(currentRatio * (float)height);
                float currentWidth = normalWidth * (float)i;
                graphics.FillRectangle(currentBrush, currentWidth, height - currentHeight, normalWidth, currentHeight);
            }
        }

        

        public void Shuffle()
        {
            list.Clear();
            for (int i = 0; i < (int)numericUpDown1.Value; i++)
                list.Add(i + 1);
            Random rnd = new Random();
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = rnd.Next(n + 1);
                int v = list[k];
                list[k] = list[n];
                list[n] = v;
            }
            Draw();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Inform inform = new Inform();
            inform.ShowDialog();
        }
    }
}


