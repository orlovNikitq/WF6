using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Paint
{
    public partial class Form1 : Form
    {
        SolidBrush mybrush;
        Graphics my_g;
        bool isDrawing = false;
        Point startPoint;
        bool flagRec,flagEcl,filling, line = false; 
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            mybrush = new SolidBrush(panel_color.BackColor);
            my_g = panel1.CreateGraphics();
        }

        private void panel_color_DoubleClick(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                panel_color.BackColor = colorDialog1.Color;
                mybrush.Color = colorDialog1.Color;
            }
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            isDrawing = true;
            startPoint = e.Location;
        }

        private void panel1_MouseUp(object sender, MouseEventArgs e)
        {
            isDrawing = false;

            if (flagRec == true)
            {
                mybrush.Color = panel_color.BackColor;
                if (filling == true)
                {
                    my_g.FillRectangle(mybrush, startPoint.X, startPoint.Y, e.Location.X - startPoint.X, e.Location.Y - startPoint.Y);
                }
                else
                {
                    Pen myPen = new Pen(panel_color.BackColor);
                    myPen.Width = trackBar1.Value;
                    if (flagRec == true)
                        my_g.DrawRectangle(myPen, startPoint.X, startPoint.Y, e.Location.X - startPoint.X, e.Location.Y - startPoint.Y);

                    myPen.Dispose();
                }
            }
            else if (flagEcl == true)
            {
                mybrush.Color = panel_color.BackColor;
                if (filling == true)
                {
                    my_g.FillEllipse(mybrush, startPoint.X, startPoint.Y, e.Location.X - startPoint.X, e.Location.Y - startPoint.Y);
                }
                else
                {
                    Pen myPen = new Pen(panel_color.BackColor);
                    myPen.Width = trackBar1.Value;
                    if (flagEcl == true)
                        my_g.DrawEllipse(myPen, startPoint.X, startPoint.Y, e.Location.X - startPoint.X, e.Location.Y - startPoint.Y);

                    myPen.Dispose();
                }
            }
            else if(line == true)
            {
                Pen myPen = new Pen(panel_color.BackColor);
                myPen.Width = trackBar1.Value;
                my_g.DrawLine(myPen, startPoint.X, startPoint.Y, e.X, e.Y);
                myPen.Dispose();
            }
        }

        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            if (flagRec || flagEcl || line) return;
            if (isDrawing == true)
            {
                my_g.FillEllipse(mybrush, e.X, e.Y, trackBar1.Value, trackBar1.Value);
            }
          
        }
        private void button2_Click(object sender, EventArgs e)
        {
            panel1.Invalidate();
        }

        private void button1_MouseClick(object sender, MouseEventArgs e)
        {
            flagRec = flagEcl = line = false;
            mybrush.Color = panel1.BackColor;
            if (isDrawing == true)
            {
                my_g.FillEllipse(mybrush, e.X, e.Y, trackBar1.Value, trackBar1.Value);
            }
            
        }

        private void button3_MouseClick(object sender, MouseEventArgs e)
        {
            flagRec = flagEcl = line = false;
            mybrush.Color = colorDialog1.Color;
            if (isDrawing == true)
            {
                my_g.FillEllipse(mybrush, e.X, e.Y, trackBar1.Value, trackBar1.Value);
            }

        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (line == true) line = false;
            else if(line == false) line = true;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            flagRec = false;
            line = false;
            if (flagEcl == true) flagEcl = false;
            else if (flagEcl == false) flagEcl = true;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            flagEcl = false;
            line = false;
            if(flagRec == true) flagRec = false;
            else if(flagRec == false) flagRec = true;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            flagRec = false;
            flagEcl = false;
            line = false;
            if (filling == true) filling = false;
            else if(filling == false) filling = true;
        }
    }
}
