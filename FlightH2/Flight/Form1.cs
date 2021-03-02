using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Flight
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        const double dt = 0.01;
        const double g = 9.81;

        double a;
        double v0;
        double y0;

        double t;
        double x;
        double y;

        double maxHeight;
        double flightTime;
        double maxLenght;

        private void btStart_Click(object sender, EventArgs e)
        {
            a = (double)edAngle.Value;
            v0 = (double)edSpeed.Value;
            y0 = (double)edHeight.Value;

            t = 0;
            x = 0;
            y = y0;

            maxHeight = (Math.Pow(v0, 2) * Math.Pow(Math.Sin(a * Math.PI / 180), 2)) / (2 * g);
            flightTime = (v0 * Math.Sin(a * Math.PI / 180) + Math.Sqrt(Math.Pow(v0, 2) * Math.Pow(Math.Sin(a * Math.PI / 180), 2) + 2 * g * y0)) / g;
            maxLenght = flightTime * v0 * Math.Cos(a * Math.PI / 180);

            chart1.ChartAreas[0].AxisX.Maximum = maxLenght * 1.1;
            chart1.ChartAreas[0].AxisY.Maximum = (maxHeight + y0) * 1.2;

            chart1.Series[0].Points.Clear();
            chart1.Series[0].Points.AddXY(x, y);

            timer1.Start();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            t += dt;
            x = v0 * Math.Cos(a * Math.PI / 180) * t;
            y = y0 + v0 * Math.Sin(a * Math.PI / 180) * t - g * t * t / 2;
            chart1.Series[0].Points.AddXY(x, y);
            if (y <= 0) timer1.Stop();

            timeIndicator.Text = $"{t}";
        }

        private void chart1_Click(object sender, EventArgs e)
        {

        }

        private void continueButton_Click(object sender, EventArgs e)
        {
            if (!timer1.Enabled)
                timer1.Enabled = true;
        }

        private void pauseButton_Click(object sender, EventArgs e)
        {
            timer1.Enabled = false;
        }
    }
}
