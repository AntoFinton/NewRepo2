using System.Drawing;
using System.Net;

namespace Task
{
    public partial class Form1 : Form
    {

        private Point originalStartPoint = new Point(50, 50);
        private Point originalEndPoint = new Point(200, 200);
        private Point startPoint;
        private Point endPoint;
        private Point perpendicularLineStart;
        private Point perpendicularLineEnd;
        private double angle = 45;

        public Form1()
        {
            startPoint = originalStartPoint;
            endPoint = originalEndPoint;

            InitializeComponent();

        }

        // Task 1: Create a line given start point (A) and end point (B)
        private void DrawLine(Point startPoint, Point endPoint)
        {
            using (Graphics g = pictureBox1.CreateGraphics())
            {
                g.Clear(Color.White); // Clear any previous drawings

                // Draw the line between the start and end points
                Pen pen = new Pen(Color.Black, 4);
                g.DrawLine(pen, startPoint, endPoint);
            }
        }

        // Task 2: Draw a center perpendicular line on the output line of Task 1
        private void DrawPerpendicularLine(Point startPoint, Point endPoint)
        {

            Point midPoint = new Point((startPoint.X + endPoint.X) / 2, (startPoint.Y + endPoint.Y) / 2);


            double slope = (endPoint.Y - startPoint.Y) / (endPoint.X - startPoint.X);

            double perpendicularSlope = -1 / slope;

            double deltaX = Math.Sqrt(100 / (1 + Math.Pow(perpendicularSlope, 2)));
            double deltaY = perpendicularSlope * deltaX;

            perpendicularLineStart = new Point((int)(midPoint.X - deltaX), (int)(midPoint.Y - deltaY));
            perpendicularLineEnd = new Point((int)(midPoint.X + deltaX), (int)(midPoint.Y + deltaY));

            using (Graphics g = pictureBox1.CreateGraphics())
            {
                Pen pen = new Pen(Color.Red, 5);
                g.DrawLine(pen, perpendicularLineStart, perpendicularLineEnd);
            }
        }

        // Task 3: Move the center perpendicular line to either point A or point B
        private void MovePerpendicularLine(Point point)
        {
            int deltaX = point.X - (startPoint.X + endPoint.X) / 2;
            int deltaY = point.Y - (startPoint.Y + endPoint.Y) / 2;

            startPoint = new Point(startPoint.X + deltaX, startPoint.Y + deltaY);
            endPoint = new Point(endPoint.X + deltaX, endPoint.Y + deltaY);

            perpendicularLineStart = new Point(perpendicularLineStart.X + deltaX, perpendicularLineStart.Y + deltaY);
            perpendicularLineEnd = new Point(perpendicularLineEnd.X + deltaX, perpendicularLineEnd.Y + deltaY);

            //DrawLine(startPoint, endPoint);
            DrawPerpendicularLine(startPoint, endPoint);
        }

        // Task 4: Rotate the center perpendicular line like a clock needle given an Angle
        private void RotatePerpendicularLine(double angle)
        {

            Point midPoint = new Point((startPoint.X + endPoint.X) / 2, (startPoint.Y + endPoint.Y) / 2);

            double radians = angle * Math.PI / 180;

            double newX = midPoint.X + (perpendicularLineStart.X - midPoint.X) * Math.Cos(radians) - (perpendicularLineStart.Y - midPoint.Y) * Math.Sin(radians);
            double newY = midPoint.Y + (perpendicularLineStart.X - midPoint.X) * Math.Sin(radians) + (perpendicularLineStart.Y - midPoint.Y) * Math.Cos(radians);
            perpendicularLineStart = new Point((int)newX, (int)newY);

            newX = midPoint.X + (perpendicularLineEnd.X - midPoint.X) * Math.Cos(radians) - (perpendicularLineEnd.Y - midPoint.Y) * Math.Sin(radians);
            newY = midPoint.Y + (perpendicularLineEnd.X - midPoint.X) * Math.Sin(radians) + (perpendicularLineEnd.Y - midPoint.Y) * Math.Cos(radians);
            perpendicularLineEnd = new Point((int)newX, (int)newY);

            DrawLine(originalStartPoint, originalEndPoint);
            using (Graphics g = pictureBox1.CreateGraphics())
            {
                Pen pen = new Pen(Color.Red, 5);
                g.DrawLine(pen, perpendicularLineStart, perpendicularLineEnd);
            }

        }



        private void button1_Click(object sender, EventArgs e)
        {
            DrawLine(startPoint, endPoint);

        }



        private void button2_Click(object sender, EventArgs e)
        {
            DrawPerpendicularLine(startPoint, endPoint);


        }

        private void button3_Click(object sender, EventArgs e)
        {
            DrawLine(startPoint, endPoint);

            MovePerpendicularLine(endPoint);

        }

        private void button4_Click(object sender, EventArgs e)
        {


            RotatePerpendicularLine(angle);


        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            pictureBox1.Invalidate(); // Clear the contents of the PictureBox

        }
    }
}
