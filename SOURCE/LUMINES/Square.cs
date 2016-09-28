using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace LUMINES
{
    //组成方块的正方形类
    public class Square
    {
        public Point Location;
        public Size size;
        public Color ForeColor;
        public Color BackColor;
        public bool ToBeDel = false;
        public bool Delete = false;
        public bool Counted = false; //作为Block左上角Square，在新形成的单色Block中是否已记过数
        public int CountNum;

        //自定义构造函数
        public Square(Size InitialSize, Color InitialBackcolor, Color InitialForecolor)
        {
            size = InitialSize;
            BackColor = InitialBackcolor;
            ForeColor = InitialForecolor;
        }

        public void DrawFrame(System.IntPtr WinHandle)
        {
            Graphics GameGraphics = Graphics.FromHwnd(WinHandle);
            Rectangle frame;
            frame = new Rectangle(Location.X - 1, Location.Y - 1, size.Width + 1, size.Height + 1);
            GameGraphics.DrawRectangle(ScanLine.ScanLineDeactivatePen, frame);
        }

        public void DrawGridFrame(System.IntPtr WinHandle)
        {
            Graphics GameGraphics = Graphics.FromHwnd(WinHandle);
            Rectangle frame;
            frame = new Rectangle(Location.X - 1, Location.Y - 1, size.Width + 1, size.Height + 1);
            GameGraphics.DrawRectangle(new Pen(new SolidBrush(GameField.GridColor)), frame);
        }

        //用梯度路径根据以上属性画矩形
        public void Show(System.IntPtr WinHandle)
        {
            Graphics GameGraphics;
            GraphicsPath graphPath;
            PathGradientBrush brushSquare;
            Color[] surroundColor;
            Rectangle rectSquare;

            //获取Graphics对象
            GameGraphics = Graphics.FromHwnd(WinHandle);

            //创建一个由一个矩形构成的路径
            graphPath = new GraphicsPath();
            rectSquare = new Rectangle(Location.X, Location.Y, size.Width, size.Height);
            graphPath.AddRectangle(rectSquare);

            //创建用于画矩形的路径画刷
            brushSquare = new PathGradientBrush(graphPath);
            brushSquare.CenterColor = ForeColor;
            surroundColor = new Color[] { BackColor };
            brushSquare.SurroundColors = surroundColor;

            //根据以上设置画出矩形路径
            GameGraphics.FillPath(brushSquare, graphPath);

            SolidBrush BackColorBrush = new SolidBrush(GameField.BackColor);
            GameGraphics.FillRectangle(BackColorBrush, new Rectangle(Location, new Size(GameField.SquareRoundCorner, GameField.SquareRoundCorner)));
            GameGraphics.FillRectangle(BackColorBrush, new Rectangle(Location.X + size.Width - GameField.SquareRoundCorner, Location.Y, GameField.SquareRoundCorner, GameField.SquareRoundCorner));
            GameGraphics.FillRectangle(BackColorBrush, new Rectangle(Location.X, Location.Y + size.Height - GameField.SquareRoundCorner, GameField.SquareRoundCorner, GameField.SquareRoundCorner));
            GameGraphics.FillRectangle(BackColorBrush, new Rectangle(Location.X + size.Width - GameField.SquareRoundCorner, Location.Y + size.Height - GameField.SquareRoundCorner, GameField.SquareRoundCorner, GameField.SquareRoundCorner));
        }

        public void MonoColorShow(System.IntPtr WinHandle)
        {
            Graphics GameGraphics;
            GraphicsPath graphPath;
            PathGradientBrush brushSquare;
            Color[] surroundColor;
            Rectangle rectSquare;

            //获取Graphics对象
            GameGraphics = Graphics.FromHwnd(WinHandle);

            //创建一个由一个矩形构成的路径
            graphPath = new GraphicsPath();
            rectSquare = new Rectangle(Location.X, Location.Y, size.Width, size.Height);
            graphPath.AddRectangle(rectSquare);

            //创建用于画矩形的路径画刷
            brushSquare = new PathGradientBrush(graphPath);
            brushSquare.CenterColor = ForeColor;
            surroundColor = new Color[] { ForeColor };
            brushSquare.SurroundColors = surroundColor;

            //根据以上设置画出矩形路径
            GameGraphics.FillPath(brushSquare, graphPath);
        }

        //消去矩形
        public void Hide(System.IntPtr WinHandle)
        {
            Graphics GameGraphics;
            Rectangle rectSquare;

            //获取Graphics对象
            GameGraphics = Graphics.FromHwnd(WinHandle);

            //消去图形
            rectSquare = new Rectangle(Location.X, Location.Y, size.Width, size.Height);
            GameGraphics.FillRectangle(new SolidBrush(GameField.BackColor), rectSquare);
        }

        public void DeleteHide(System.IntPtr WinHandle)
        {
            DrawFrame(WinHandle);
            Hide(WinHandle);
        }

        public void ResumeNormal(System.IntPtr WinHandle)
        {
            if (this.Delete) return;
            this.ToBeDel = false;
            this.Delete = false;
            this.Counted = false;
            this.CountNum = 0;
            //this.BackColor = Block.backColors[Block.FindColorIndex(this.BackColor)];
            this.size = new Size(GameField.SquareSize, GameField.SquareSize);
            DrawFrame(WinHandle);
            Show(WinHandle);
            DrawGridFrame(WinHandle);
        }

        public void HideToDelete(System.IntPtr WinHandle)
        {
            Graphics GameGraphics;
            Rectangle rectSquare;
            GraphicsPath graphPath;
            PathGradientBrush brushSquare;
            Color[] surroundColor;

            DrawFrame(WinHandle);

            //获取Graphics对象
            GameGraphics = Graphics.FromHwnd(WinHandle);
            rectSquare = new Rectangle(Location.X, Location.Y, size.Width, size.Height);
            graphPath = new GraphicsPath();
            graphPath.AddRectangle(rectSquare);

            //创建用于画矩形的路径画刷
            brushSquare = new PathGradientBrush(graphPath);
            brushSquare.CenterColor = Color.FromArgb(133, 129, 166);
            surroundColor = new Color[] { Color.FromArgb(37,36,52) };
            brushSquare.SurroundColors = surroundColor;

            //根据以上设置画出矩形路径
            GameGraphics.FillPath(brushSquare, graphPath);
        }
    }
}
