using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace ComputerGraphics1
{
    public partial class Form1 : Form
    {
        List<int> _x, _y;
        Bitmap image;
        Graphics graphics;
        int pixelSize = 5;
        private static readonly Bitmap BitmapImage = new Bitmap("Image.bmp");

        int[] figureX = {25, 15, 30,
                         30,40,50,
                         50,65,55,
                        55,65,50,
                        50,40,30,
                        30,15,25,
                        25,30,30,
                        30,30,50,
                        50,30,50,
                        50,50,55};

        int[] figureY = {40,27,27,
                        27,10,27,
                        27,27,40,
                        40,50,50,
                        50,70,50,
                        50,50,40,
                        40,27,50,
                        27,50,50,
                        50,27,27,
                        27,50,40};
        public Form1()
        {
            InitializeComponent();
            LoadData();
            CreatePseudoPixels();
            Draw();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }

        void LoadData()
        {
            image = new Bitmap(pictureBox.Width, pictureBox.Height);
            graphics = Graphics.FromImage(image);
        }

        void Draw()
        {
            pictureBox.BackColor = Color.White;

            DrawFigure(0, "Barycentric");
            DrawFigure(100, "Gouraud");

            DrawGrid();

            pictureBox.Image = image;
            graphics.DrawImage(BitmapImage, new Point((int)(pictureBox.Width*0.17), (int)(pictureBox.Height * 0.7)));
            graphics.DrawImage(BitmapSizeChanger.EnlargeImage(BitmapImage), new Point((int)(pictureBox.Width * 0.53), (int)(pictureBox.Height * 0.55)));
        }

        void DrawGrid()
        {
            for (int i = 0; i< _x.Count; i++)
                    graphics.DrawLine(new Pen(Color.Gray), _x[i], _y[0], _x[i], _y[_y.Count - 1]);

            for (int i = 0; i < _y.Count; i++)
                    graphics.DrawLine(new Pen(Color.Gray), _x[0], _y[i], _x[_x.Count - 1], _y[i]);

            DrawCDALine(_x.Count / 2, 0, _x.Count /2, _y.Count - 1);
            DrawCDALine(0, _y.Count/2, _x.Count-1, _y.Count/2);
        }

        void CreatePseudoPixels()
        {
            _x = new List<int>();
            _y = new List<int>();

            ///заполнение стандартного окна
            for (int i = 0; i < pictureBox.Width; i += pixelSize)
                _x.Add(i);

            for (int i = 0; i < pictureBox.Height; i += pixelSize)
                _y.Add(i);
        }

        void DrawPseudoPixel(int x, int y, Color color)
        {
            graphics.FillRectangle(color != default ? new SolidBrush(color) : Brushes.Black, _x[x], _y[y], pixelSize, pixelSize);
        }

        void BarycentricCoordinates(double ax, double bx, double cx, double ay, double by, double cy)
        {
            var minX = Math.Min(ax, Math.Min(bx, cx));
            var maxX = Math.Max(ax, Math.Max(bx, cx));
            var minY = Math.Min(ay, Math.Min(by, cy));
            var maxY = Math.Max(ay, Math.Max(by, cy));

            for (var x = minX; x <= maxX; x++)
            {
                for (var y = minY; y <= maxY; y++)
                {
                    var r = ((y - cy) * (bx - cx) - (x - cx) * (by - cy)) /
                             ((ay - cy) * (bx - cx) - (ax - cx) * (by - cy));
                    var g = ((y - ay) * (cx - ax) - (x - ax) * (cy - ay)) /
                             ((by - ay) * (cx - ax) - (bx - ax) * (cy - ay));
                    var b = ((y - ay) * (bx - ax) - (x - ax) * (by - ay)) /
                             ((cy - ay) * (bx - ax) - (cx - ax) * (by - ay));

                    if (0 <= r && r <= 1 && 0 <= g && g <= 1 && 0 <= b && b <= 1)
                    {
                        var color = Color.FromArgb((int)(255 * r), (int)(255 * g), (int)(255 * b));

                        DrawPseudoPixel((int)x, (int)y, color);
                    }
                }
            }
        }

        private void GouraudMethod(double ax, double bx, double cx, double ay, double by, double cy)
        {
            var a = (ay < by && ay < cy) ? new MyPoint(ax, ay) :
                (by < cy) ? new MyPoint(bx, by) : new MyPoint(cx, cy);
            var b = (ay > by && ay > cy) ? new MyPoint(ax, ay) :
                (by > cy) ? new MyPoint(bx, by) : new MyPoint(cx, cy);
            var c = (new MyPoint(ax, ay) != a && new MyPoint(ax, ay) != b) ? new MyPoint(ax, ay) :
                (new MyPoint(bx, by) != a && new MyPoint(bx, by) != b) ? new MyPoint(bx, by) : new MyPoint(cx, cy);

            var inversed = false;
            if (Math.Abs(a.Y - c.Y) < 0.0001 && a.X > c.X)
            {
                (a, c) = (c, a);
            }
            else if (Math.Abs(c.Y - b.Y) < 0.0001 && b.X > c.X)
            {
                (b, c) = (c, b);
            }
            else if (c.X < a.X)
            {
                c = new MyPoint(2 * b.X - c.X, c.Y);
                a = new MyPoint(2 * b.X - a.X, a.Y);
                inversed = true;
            }

            var aRGB = new RGB(255, 0, 0);
            var bRGB = new RGB(0, 255, 0);
            var cRGB = new RGB(0, 0, 255);

            double dxS, x2;
            RGB dRgbS, bRgb;
            var dxT = (b.X - a.X) / (b.Y - a.Y);
            var dRgbT = (cRGB - aRGB) / (b.Y - a.Y);
            var x1 = a.X+1;
            var aRgb = aRGB;

            if (Math.Abs(a.Y - c.Y) > 0.0001)
            {
                bRgb = aRGB;
                dxS = (c.X - a.X) / (c.Y - a.Y);
                dRgbS = (bRGB - aRGB) / (c.Y - a.Y);
                x2 = a.X;
                DrawGouraudLine(a.Y, c.Y);
            }

            if (Math.Abs(b.Y - c.Y) > 0.0001)
            {
                bRgb = bRGB;
                dxS = (b.X - c.X) / (b.Y - c.Y);
                dRgbS = (cRGB - bRGB) / (b.Y - c.Y);
                x2 = c.X;
                DrawGouraudLine(c.Y, b.Y);
            }

            void DrawGouraudLine(double y1, double y2)
            {
                for (var y = y1; y <= y2; y++)
                {
                    var dRgbX = (bRgb - aRgb) / (x2 - x1);
                    var pRgb = aRgb;
                    for (var x = x1; x <= x2; x++)
                    {
                        DrawPseudoPixel((int)(inversed ? 2 * b.X - x : x), (int)y,
                            Color.FromArgb(pRgb.R < 0 ? 0 : pRgb.R > 255 ? 255 : pRgb.R,
                                pRgb.G < 0 ? 0 : pRgb.G > 255 ? 255 : pRgb.G,
                                pRgb.B < 0 ? 0 : pRgb.B > 255 ? 255 : pRgb.B));
                        pRgb += dRgbX;
                    }

                    x1 += dxT;
                    x2 += dxS;
                    aRgb += dRgbT;
                    bRgb += dRgbS;
                }
            }
        }

        void DrawBresenhamLine(int x0, int y0, int x1, int y1)
        {
            if (x0 == -1000)
                return;
            int deltaX = x1 - x0;
            int deltaY = y1 - y0;
            int absDeltaX = Math.Abs(deltaX);
            int absDeltaY = Math.Abs(deltaY);
            int accretion = 0;

            ///алгоритм для прямой, чей угол не превышает 45 градусов
            if(absDeltaX >= absDeltaY)
            {
                int y = y0;
                int direction = deltaY != 0 ? (deltaY > 0 ? 1 : -1) : 0;
                int tmp = deltaX > 0 ? 1 : -1;
                for (int x = x0; deltaX > 0 ? x <= x1 : x >= x1; x+=tmp)
                {
                    DrawPseudoPixel(x, y, default);
                    accretion += absDeltaY;

                    if(accretion >= absDeltaX)
                    {
                        accretion -= absDeltaX;
                        y += direction;
                    }
                }
            }
            ///если угол больше 45 градусов
            else
            {
                int x = x0;
                int direction = deltaX != 0 ? (deltaX > 0 ? 1 : -1) : 0;
                int tmp = deltaY > 0 ? 1 : -1;
                for (int y = y0; deltaY > 0 ? y <= y1 : y >= y1; y += tmp)
                {
                    DrawPseudoPixel(x, y, default);
                    accretion += absDeltaX;

                    if (accretion >= absDeltaY)
                    {
                        accretion -= absDeltaY;
                        x += direction;
                    }
                }
            }
        }

        void DrawCDALine(int x0, int y0, int x1, int y1)
        {
            int deltaX = x1 - x0;
            int deltaY = y1 - y0;
            int absDeltaX = Math.Abs(x1 - x0);
            int absDeltaY = Math.Abs(y1 - y0);
            int lenght = Math.Max(absDeltaX, absDeltaY);

            if(lenght == 0)
            {
                DrawPseudoPixel(x0, y0, default);
                return;
            }

            var dX = (double)deltaX / (double)lenght;
            var dY = (double)deltaY / (double)lenght;

            double x = x0;
            double y = y0;

            lenght++;

            while(lenght > 0)
            {
                DrawPseudoPixel((int)Math.Round(x), (int)Math.Round(y), default);
                x += dX;
                y += dY;
                lenght--;
            }
        }

        void DrawFigure(int x,  string type)
        {
            for(int i=0; i<figureX.Length-2; i+=1)
            {
                if (type == "Barycentric")
                {
                    BarycentricCoordinates(figureX[i], figureX[i + 1], figureX[i + 2], figureY[i], figureY[i + 1], figureY[i + 2]);
                    DrawBresenhamLine(figureX[i] +x, figureY[i], figureX[i + 1] + x, figureY[i + 1]);
                    DrawBresenhamLine(figureX[i+1] +x, figureY[i+1], figureX[i + 2] + x, figureY[i + 2]);
                    DrawBresenhamLine(figureX[i]+x, figureY[i], figureX[i + 2] + x, figureY[i + 2]);
                }
                else
                {
                    GouraudMethod(figureX[i]+x, figureX[i + 1]+x, figureX[i + 2]+x, figureY[i], figureY[i + 1], figureY[i + 2]);
                    DrawCDALine(figureX[i] + x, figureY[i], figureX[i + 1] + x, figureY[i + 1]);
                    DrawCDALine(figureX[i + 1] + x, figureY[i + 1], figureX[i + 2] + x, figureY[i + 2]);
                    DrawCDALine(figureX[i] + x, figureY[i], figureX[i + 2] + x, figureY[i + 2]);
                }
            }
        }
    }
}
