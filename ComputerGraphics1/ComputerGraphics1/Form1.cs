using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace ComputerGraphics1
{
    public partial class Form1 : Form
    {
        List<int> _x, _y, _iX, _iY;
        Bitmap image, interactiveImage;
        Graphics graphics, interactiveGraphics;
        int pixelSize = 5;
        int iCount = 0;
        int[] points = new int[4];
        int indent = 30;
        int[] boundaries = new int[4];

        int[] figureX = {25,60,
                         25,42,
                         42,60,

                         25,42,
                         42,60,
                         25,60};

        int[] figureY = {30,30,
                         30,60,
                         60,30,

                         50,15,
                         15,50,
                         50,50};
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

            lineTypeBox.SelectedItem = "BresenhamLine";

            interactiveImage = new Bitmap(interactivePictureBox.Width, interactivePictureBox.Height);
            interactiveGraphics = Graphics.FromImage(interactiveImage);
        }

        private void interactivePictureBox_MouseClick(object sender, MouseEventArgs e)
        {
            if(iCount % 2 == 0)
            {
                points[0] = e.X / pixelSize;
                points[1] = e.Y / pixelSize;
            }
            else
            {
                points[2] = e.X / pixelSize;
                points[3] = e.Y / pixelSize;
                if (lineTypeBox.SelectedItem.ToString() == "BresenhamLine")
                {
                    bool checkRect = ((points[0] & points[2]) > boundaries[2] && (points[0] & points[2]) < boundaries[0]) || ((points[1] & points[3]) > boundaries[3] && (points[1] & points[3]) < boundaries[1]);
                    if (!checkRect)
                    {
                        Cohen_Sutherland(ref points[0], ref points[1], ref points[2], ref points[3]);
                        DrawBresenhamLine(points[0], points[1], points[2], points[3], interactiveGraphics);
                        textBox1.Text = "Bresenham";
                    }
                }
                else
                {
                    DrawCDALine(points[0], points[1], points[2], points[3], interactiveGraphics);
                    textBox1.Text = "CDA";
                }
            }

            iCount++;

            interactivePictureBox.Image = interactiveImage;
        }

        private void clearButton_Click(object sender, EventArgs e)
        {
            interactiveGraphics.Clear(Color.Black);
            DrawIndents(boundaries[0], boundaries[1], boundaries[2], boundaries[3]);
            interactivePictureBox.Image = interactiveImage;
        }

        void Draw()
        {
            pictureBox.BackColor = Color.Black;
            interactivePictureBox.BackColor = Color.Black;
            MakeIndents(out boundaries[0], out boundaries[1], out boundaries[2], out boundaries[3]);

            DrawFigure(figureX, figureY, "Bresenham");
            DrawFigure(figureX, figureY, "CDA");

            pictureBox.Image = image;
            interactivePictureBox.Image = interactiveImage;
        }

        void DrawIndents(int vertical,int horizontal,int xIdent,int yIdent)
        {
            DrawBresenhamLine(vertical, horizontal, vertical, yIdent, interactiveGraphics);
            DrawBresenhamLine(xIdent, horizontal, xIdent, yIdent, interactiveGraphics);
            DrawBresenhamLine(vertical, horizontal, xIdent, horizontal, interactiveGraphics);
            DrawBresenhamLine(vertical, yIdent, xIdent, yIdent, interactiveGraphics);
        }

        void MakeIndents(out int vertical, out int horizontal, out int xIdent, out int yIdent)
        {
            vertical = (int)(_iX.Count / 200.0 * indent);
            horizontal = (int)(_iY.Count / 200.0 * indent);
            xIdent = _iX.Count - 1 - vertical;
            yIdent = _iY.Count - 1 - horizontal;

            DrawIndents(vertical, horizontal, xIdent, yIdent);
        }

        void Cohen_Sutherland(ref int ax, ref int ay, ref int bx, ref int by)
        {
            int code_a, code_b, code;
            int x, y;

            code_a = GetCode(ax, ay);  //вычисление кодов точек
            code_b = GetCode(bx, by);

            while((code_a | code_b) != 0)  //пока хотя бы одна из точке вне прямоугольника
            {
                if(code_a != 0)
                {
                    code = code_a;
                    x = ax;
                    y = ay;
                }
                else
                {
                    code = code_b;
                    x = bx;
                    y = by;
                }

                switch(code)
                {
                    case (1):
                        y += (ay - by) * (boundaries[0] - x) / (ax - bx);
                        x = boundaries[0];
                        break;
                    case (2):
                        y += (ay - by) * (boundaries[2] - x) / (ax - bx);
                        x = boundaries[2];
                        break;
                    case (4):
                        x += (ax - bx) * (boundaries[1] - y) / (ay - by);
                        y = boundaries[1];
                        break;
                    case (8):
                        x += (ax - bx) * (boundaries[3] - y) / (ay - by);
                        y = boundaries[3];
                        break;
                }

                if(code == code_a) //обновляем код
                {
                    ax = x;
                    ay = y;
                    code_a = GetCode(ax, ay);
                }
                else
                {
                    bx = x;
                    by = y;
                    code_b = GetCode(bx, by);
                }
            }
        }

        int GetCode(int x, int y)
        {
            return (x < boundaries[0] ? 1 : 0) + //точка левее
                (x > boundaries[2] ? 2 : 0) +    //точка правее
                (y < boundaries[1] ? 4 : 0) +    //точка ниже
                (y > boundaries[3] ? 8 : 0);     //точка выше
        }

        void CreatePseudoPixels()
        {
            _x = new List<int>();
            _y = new List<int>();
            _iX = new List<int>();
            _iY = new List<int>();

            ///заполнение стандартного окна
            for (int i = 0; i < pictureBox.Width; i += pixelSize)
                _x.Add(i);

            for (int i = 0; i < pictureBox.Height; i += pixelSize)
                _y.Add(i);

            ///заполнение интерактивного окна
            for (int i = 0; i < interactivePictureBox.Width; i += pixelSize)
                _iX.Add(i);

            for (int i = 0; i < interactivePictureBox.Height; i += pixelSize)
                _iY.Add(i);
        }

        void DrawPseudoPixel(int x, int y, Graphics gr)
        {
            gr.FillRectangle(Brushes.White, gr == graphics ? _x[x] : _iX[x], gr == graphics ? _y[y] : _iY[y], pixelSize, pixelSize);
        }

        void DrawBresenhamLine(int x0, int y0, int x1, int y1, Graphics g)
        {
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
                    DrawPseudoPixel(x, y, g);
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
                    DrawPseudoPixel(x, y, g);
                    accretion += absDeltaX;

                    if (accretion >= absDeltaY)
                    {
                        accretion -= absDeltaY;
                        x += direction;
                    }
                }
            }
        }

        void DrawCDALine(int x0, int y0, int x1, int y1, Graphics g)
        {
            int deltaX = x1 - x0;
            int deltaY = y1 - y0;
            int absDeltaX = Math.Abs(x1 - x0);
            int absDeltaY = Math.Abs(y1 - y0);
            int lenght = Math.Max(absDeltaX, absDeltaY);

            if(lenght == 0)
            {
                DrawPseudoPixel(x0, y0, g);
                return;
            }

            var dX = (double)deltaX / (double)lenght;
            var dY = (double)deltaY / (double)lenght;

            double x = x0;
            double y = y0;

            lenght++;

            while(lenght > 0)
            {
                DrawPseudoPixel((int)Math.Round(x), (int)Math.Round(y), g);
                x += dX;
                y += dY;
                lenght--;
            }
        }

        void DrawFigure(int[] x, int[] y, string type)
        {
            for(int i=1; i<x.Length; i+=2)
            {
                if (type == "Bresenham")
                    DrawBresenhamLine(x[i-1], y[i-1], x[i], y[i], graphics);
                else
                    DrawCDALine(x[i-1]+90, y[i-1], x[i]+90, y[i], graphics);
            }
        }
    }
}
