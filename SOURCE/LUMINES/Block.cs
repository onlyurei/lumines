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
    //方块类
    public class Block
    {
        //每个方块由4个正方形组成
        public Square squareSW; //西南方块
        public Square squareNW; //西北方块
        public Square squareNE; //东北方块
        public Square squareSE; //东南方块
        public static int Color1 = 1;
        public static int Color2 = 2;
        public int direction;

        private const int squareSize = GameField.SquareSize; //正方形大小

        //随机数生成器
        Random random = new Random();

        //构造函数
        public Block(Point location, BlockTypes newBlockType, int newDirection)
        {
            //创建一个新方块，根据需要选择新的方块类型
            if (newBlockType == BlockTypes.Undefined)
            {
                BlockType = (BlockTypes)(random.Next(6)) + 1;
            }
            else
            {
                BlockType = newBlockType;
            }

            if (newDirection == 0) direction = random.Next(4) + 1;
            else direction = newDirection;

            //根据方块类型设置各正方形的位置（(location.X, location.Y)为该方块所在最小矩形方块的最左上角正方形的坐标）及颜色
            switch (BlockType)
            {
                case BlockTypes.full1:
                    squareSW = new Square(new Size(squareSize, squareSize), backColors[Color1], foreColors[Color1]);
                    squareNW = new Square(new Size(squareSize, squareSize), backColors[Color1], foreColors[Color1]);
                    squareNE = new Square(new Size(squareSize, squareSize), backColors[Color1], foreColors[Color1]);
                    squareSE = new Square(new Size(squareSize, squareSize), backColors[Color1], foreColors[Color1]);
                    squareNW.Location = new Point(location.X, location.Y);
                    squareNE.Location = new Point(location.X + squareSize + 1, location.Y);
                    squareSW.Location = new Point(location.X, location.Y + squareSize + 1);
                    squareSE.Location = new Point(location.X + squareSize + 1, location.Y + squareSize + 1);
                    break;

                case BlockTypes.full2:
                    squareSW = new Square(new Size(squareSize, squareSize), backColors[Color2], foreColors[Color2]);
                    squareNW = new Square(new Size(squareSize, squareSize), backColors[Color2], foreColors[Color2]);
                    squareNE = new Square(new Size(squareSize, squareSize), backColors[Color2], foreColors[Color2]);
                    squareSE = new Square(new Size(squareSize, squareSize), backColors[Color2], foreColors[Color2]);
                    squareNW.Location = new Point(location.X, location.Y);
                    squareNE.Location = new Point(location.X + squareSize + 1, location.Y);
                    squareSW.Location = new Point(location.X, location.Y + squareSize + 1);
                    squareSE.Location = new Point(location.X + squareSize + 1, location.Y + squareSize + 1);
                    break;

                case BlockTypes.Line:
                    switch (direction)
                    {
                        case 1:
                            squareSW = new Square(new Size(squareSize, squareSize), backColors[Color2], foreColors[Color2]);
                            squareNW = new Square(new Size(squareSize, squareSize), backColors[Color2], foreColors[Color2]);
                            squareNE = new Square(new Size(squareSize, squareSize), backColors[Color1], foreColors[Color1]);
                            squareSE = new Square(new Size(squareSize, squareSize), backColors[Color1], foreColors[Color1]);
                            break;

                        case 2:
                            squareSW = new Square(new Size(squareSize, squareSize), backColors[Color2], foreColors[Color2]);
                            squareNW = new Square(new Size(squareSize, squareSize), backColors[Color1], foreColors[Color1]);
                            squareNE = new Square(new Size(squareSize, squareSize), backColors[Color1], foreColors[Color1]);
                            squareSE = new Square(new Size(squareSize, squareSize), backColors[Color2], foreColors[Color2]);
                            break;

                        case 3:
                            squareSW = new Square(new Size(squareSize, squareSize), backColors[Color1], foreColors[Color1]);
                            squareNW = new Square(new Size(squareSize, squareSize), backColors[Color1], foreColors[Color1]);
                            squareNE = new Square(new Size(squareSize, squareSize), backColors[Color2], foreColors[Color2]);
                            squareSE = new Square(new Size(squareSize, squareSize), backColors[Color2], foreColors[Color2]);
                            break;

                        case 4:
                            squareSW = new Square(new Size(squareSize, squareSize), backColors[Color1], foreColors[Color1]);
                            squareNW = new Square(new Size(squareSize, squareSize), backColors[Color2], foreColors[Color2]);
                            squareNE = new Square(new Size(squareSize, squareSize), backColors[Color2], foreColors[Color2]);
                            squareSE = new Square(new Size(squareSize, squareSize), backColors[Color1], foreColors[Color1]);
                            break;
                    }
                    squareNW.Location = new Point(location.X, location.Y);
                    squareNE.Location = new Point(location.X + squareSize + 1, location.Y);
                    squareSW.Location = new Point(location.X, location.Y + squareSize + 1);
                    squareSE.Location = new Point(location.X + squareSize + 1, location.Y + squareSize + 1);
                    break;

                case BlockTypes.Dot:
                    switch (direction)
                    {
                        case 1:
                            squareSW = new Square(new Size(squareSize, squareSize), backColors[Color1], foreColors[Color1]);
                            squareNW = new Square(new Size(squareSize, squareSize), backColors[Color2], foreColors[Color2]);
                            squareNE = new Square(new Size(squareSize, squareSize), backColors[Color2], foreColors[Color2]);
                            squareSE = new Square(new Size(squareSize, squareSize), backColors[Color2], foreColors[Color2]);
                            break;

                        case 2:
                            squareSW = new Square(new Size(squareSize, squareSize), backColors[Color2], foreColors[Color2]);
                            squareNW = new Square(new Size(squareSize, squareSize), backColors[Color2], foreColors[Color2]);
                            squareNE = new Square(new Size(squareSize, squareSize), backColors[Color2], foreColors[Color2]);
                            squareSE = new Square(new Size(squareSize, squareSize), backColors[Color1], foreColors[Color1]);
                            break;

                        case 3:
                            squareSW = new Square(new Size(squareSize, squareSize), backColors[Color2], foreColors[Color2]);
                            squareNW = new Square(new Size(squareSize, squareSize), backColors[Color2], foreColors[Color2]);
                            squareNE = new Square(new Size(squareSize, squareSize), backColors[Color1], foreColors[Color1]);
                            squareSE = new Square(new Size(squareSize, squareSize), backColors[Color2], foreColors[Color2]);
                            break;

                        case 4:
                            squareSW = new Square(new Size(squareSize, squareSize), backColors[Color2], foreColors[Color2]);
                            squareNW = new Square(new Size(squareSize, squareSize), backColors[Color1], foreColors[Color1]);
                            squareNE = new Square(new Size(squareSize, squareSize), backColors[Color2], foreColors[Color2]);
                            squareSE = new Square(new Size(squareSize, squareSize), backColors[Color2], foreColors[Color2]);
                            break;
                    }
                    squareNW.Location = new Point(location.X, location.Y);
                    squareNE.Location = new Point(location.X + squareSize + 1, location.Y);
                    squareSW.Location = new Point(location.X, location.Y + squareSize + 1);
                    squareSE.Location = new Point(location.X + squareSize + 1, location.Y + squareSize + 1);
                    break;

                case BlockTypes.X:
                    if (direction > 2) direction = 1;
                    switch (direction)
                    {
                        case 1:
                            squareSW = new Square(new Size(squareSize, squareSize), backColors[Color1], foreColors[Color1]);
                            squareNW = new Square(new Size(squareSize, squareSize), backColors[Color2], foreColors[Color2]);
                            squareNE = new Square(new Size(squareSize, squareSize), backColors[Color1], foreColors[Color1]);
                            squareSE = new Square(new Size(squareSize, squareSize), backColors[Color2], foreColors[Color2]);
                            break;

                        case 2:
                            squareSW = new Square(new Size(squareSize, squareSize), backColors[Color2], foreColors[Color2]);
                            squareNW = new Square(new Size(squareSize, squareSize), backColors[Color1], foreColors[Color1]);
                            squareNE = new Square(new Size(squareSize, squareSize), backColors[Color2], foreColors[Color2]);
                            squareSE = new Square(new Size(squareSize, squareSize), backColors[Color1], foreColors[Color1]);
                            break;
                    }
                    squareNW.Location = new Point(location.X, location.Y);
                    squareNE.Location = new Point(location.X + squareSize + 1, location.Y);
                    squareSW.Location = new Point(location.X, location.Y + squareSize + 1);
                    squareSE.Location = new Point(location.X + squareSize + 1, location.Y + squareSize + 1);
                    break;

                case BlockTypes.L:
                    switch (direction)
                    {
                        case 1:
                            squareSW = new Square(new Size(squareSize, squareSize), backColors[Color2], foreColors[Color2]);
                            squareNW = new Square(new Size(squareSize, squareSize), backColors[Color1], foreColors[Color1]);
                            squareNE = new Square(new Size(squareSize, squareSize), backColors[Color1], foreColors[Color1]);
                            squareSE = new Square(new Size(squareSize, squareSize), backColors[Color1], foreColors[Color1]);
                            break;

                        case 2:
                            squareSW = new Square(new Size(squareSize, squareSize), backColors[Color1], foreColors[Color1]);
                            squareNW = new Square(new Size(squareSize, squareSize), backColors[Color1], foreColors[Color1]);
                            squareNE = new Square(new Size(squareSize, squareSize), backColors[Color1], foreColors[Color1]);
                            squareSE = new Square(new Size(squareSize, squareSize), backColors[Color2], foreColors[Color2]);
                            break;

                        case 3:
                            squareSW = new Square(new Size(squareSize, squareSize), backColors[Color1], foreColors[Color1]);
                            squareNW = new Square(new Size(squareSize, squareSize), backColors[Color1], foreColors[Color1]);
                            squareNE = new Square(new Size(squareSize, squareSize), backColors[Color2], foreColors[Color2]);
                            squareSE = new Square(new Size(squareSize, squareSize), backColors[Color1], foreColors[Color1]);
                            break;

                        case 4:
                            squareSW = new Square(new Size(squareSize, squareSize), backColors[Color1], foreColors[Color1]);
                            squareNW = new Square(new Size(squareSize, squareSize), backColors[Color2], foreColors[Color2]);
                            squareNE = new Square(new Size(squareSize, squareSize), backColors[Color1], foreColors[Color1]);
                            squareSE = new Square(new Size(squareSize, squareSize), backColors[Color1], foreColors[Color1]);
                            break;
                    }
                    squareNW.Location = new Point(location.X, location.Y);
                    squareNE.Location = new Point(location.X + squareSize + 1, location.Y);
                    squareSW.Location = new Point(location.X, location.Y + squareSize + 1);
                    squareSE.Location = new Point(location.X + squareSize + 1, location.Y + squareSize + 1);
                    break;
            }
        }

        public bool Down()
        {
            //没有方块被挡住
            if (GameField.IsEmpty((squareSW.Location.X - 1) / (squareSize + 1), (squareSW.Location.Y - 1) / (squareSize + 1) + 1) &&
                GameField.IsEmpty((squareSE.Location.X - 1) / (squareSize + 1), (squareSE.Location.Y - 1) / (squareSize + 1) + 1))
            {
                Hide(GameField.WinHandle);
                squareSW.Location = new Point(squareSW.Location.X, squareSW.Location.Y + squareSize + 1);
                squareNW.Location = new Point(squareNW.Location.X, squareNW.Location.Y + squareSize + 1);
                squareSE.Location = new Point(squareSE.Location.X, squareSE.Location.Y + squareSize + 1);
                squareNE.Location = new Point(squareNE.Location.X, squareNE.Location.Y + squareSize + 1);
                Show(GameField.WinHandle);
                return true;
            }

            //右半边方块被挡住
            else if (GameField.IsEmpty((squareSW.Location.X - 1) / (squareSize + 1), (squareSW.Location.Y - 1) / (squareSize + 1) + 1) &&
                     !GameField.IsEmpty((squareSE.Location.X - 1) / (squareSize + 1), (squareSE.Location.Y - 1) / (squareSize + 1) + 1))
            {
                GameField.StopSquare(squareSE, (squareSE.Location.X - 1) / (squareSize + 1), (squareSE.Location.Y - 1) / (squareSize + 1));
                GameField.StopSquare(squareNE, (squareNE.Location.X - 1) / (squareSize + 1), (squareNE.Location.Y - 1) / (squareSize + 1));
                //让左半边方块落地
                while (GameField.IsEmpty((squareSW.Location.X - 1) / (squareSize + 1), (squareSW.Location.Y - 1) / (squareSize + 1) + 1))
                {
                    squareSW.Hide(GameField.WinHandle);
                    squareNW.Hide(GameField.WinHandle);
                    squareSW.Location = new Point(squareSW.Location.X, squareSW.Location.Y + squareSize + 1);
                    squareNW.Location = new Point(squareNW.Location.X, squareNW.Location.Y + squareSize + 1);
                    squareSW.Show(GameField.WinHandle);
                    squareNW.Show(GameField.WinHandle);
                }
                GameField.StopSquare(squareSW, (squareSW.Location.X - 1) / (squareSize + 1), (squareSW.Location.Y - 1) / (squareSize + 1));
                GameField.StopSquare(squareNW, (squareNW.Location.X - 1) / (squareSize + 1), (squareNW.Location.Y - 1) / (squareSize + 1));
                GameField.CheckBlocks(0, GameField.Width - 1);
                if (GameField.DoSetDelCol)
                    GameField.SetDelCol();
                GameField.DoAllDeletedCheck = true;
                GameField.DoUniColorCheck = true;
                return false;
            }

            //左半边方块被挡住
            else if (!GameField.IsEmpty((squareSW.Location.X - 1) / (squareSize + 1), (squareSW.Location.Y - 1) / (squareSize + 1) + 1) &&
                     GameField.IsEmpty((squareSE.Location.X - 1) / (squareSize + 1), (squareSE.Location.Y - 1) / (squareSize + 1) + 1))
            {
                GameField.StopSquare(squareSW, (squareSW.Location.X - 1) / (squareSize + 1), (squareSW.Location.Y - 1) / (squareSize + 1));
                GameField.StopSquare(squareNW, (squareNW.Location.X - 1) / (squareSize + 1), (squareNW.Location.Y - 1) / (squareSize + 1));
                //让右半边方块落地
                while (GameField.IsEmpty((squareSE.Location.X - 1) / (squareSize + 1), (squareSE.Location.Y - 1) / (squareSize + 1) + 1))
                {
                    squareSE.Hide(GameField.WinHandle);
                    squareNE.Hide(GameField.WinHandle);
                    squareSE.Location = new Point(squareSE.Location.X, squareSE.Location.Y + squareSize + 1);
                    squareNE.Location = new Point(squareNE.Location.X, squareNE.Location.Y + squareSize + 1);
                    squareSE.Show(GameField.WinHandle);
                    squareNE.Show(GameField.WinHandle);
                }
                GameField.StopSquare(squareSE, (squareSE.Location.X - 1) / (squareSize + 1), (squareSE.Location.Y - 1) / (squareSize + 1));
                GameField.StopSquare(squareNE, (squareNE.Location.X - 1) / (squareSize + 1), (squareNE.Location.Y - 1) / (squareSize + 1));
                GameField.CheckBlocks(0, GameField.Width - 1);
                if (GameField.DoSetDelCol)
                    GameField.SetDelCol();
                GameField.DoAllDeletedCheck = true;
                GameField.DoUniColorCheck = true;
                return false;
            }
            
            //整个方块被挡住
            else
            {
                GameField.StopSquare(squareSW, (squareSW.Location.X - 1) / (squareSize + 1), (squareSW.Location.Y - 1) / (squareSize + 1));
                GameField.StopSquare(squareNW, (squareNW.Location.X - 1) / (squareSize + 1), (squareNW.Location.Y - 1) / (squareSize + 1));
                GameField.StopSquare(squareSE, (squareSE.Location.X - 1) / (squareSize + 1), (squareSE.Location.Y - 1) / (squareSize + 1));
                GameField.StopSquare(squareNE, (squareNE.Location.X - 1) / (squareSize + 1), (squareNE.Location.Y - 1) / (squareSize + 1));
                GameField.CheckBlocks(0, GameField.Width - 1);
                if (GameField.DoSetDelCol)
                    GameField.SetDelCol();
                GameField.DoAllDeletedCheck = true;
                GameField.DoUniColorCheck = true;
                return false;
            }
        }

        public bool Right()
        {
            if (GameField.IsEmpty((squareSE.Location.X - 1) / (squareSize + 1) + 1, (squareSE.Location.Y - 1) / (squareSize + 1)) &&
                GameField.IsEmpty((squareNE.Location.X - 1) / (squareSize + 1) + 1, (squareNE.Location.Y - 1) / (squareSize + 1)))
            {
                //在原位置将方块消去
                Hide(GameField.WinHandle);
                //更新方块的位置
                squareSW.Location = new Point(squareSW.Location.X + squareSize + 1, squareSW.Location.Y);
                squareNW.Location = new Point(squareNW.Location.X + squareSize + 1, squareNW.Location.Y);
                squareSE.Location = new Point(squareSE.Location.X + squareSize + 1, squareSE.Location.Y);
                squareNE.Location = new Point(squareNE.Location.X + squareSize + 1, squareNE.Location.Y);
                //在新位置显示方块
                Show(GameField.WinHandle);
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool Left()
        {
            if (GameField.IsEmpty((squareSW.Location.X - 1) / (squareSize + 1) - 1, (squareSW.Location.Y - 1) / (squareSize + 1)) &&
                GameField.IsEmpty((squareNW.Location.X - 1) / (squareSize + 1) - 1, (squareNW.Location.Y - 1) / (squareSize + 1)))
            {
                //在原位置将方块消去
                Hide(GameField.WinHandle);
                //更新方块的位置
                squareSW.Location = new Point(squareSW.Location.X - squareSize - 1, squareSW.Location.Y);
                squareNW.Location = new Point(squareNW.Location.X - squareSize - 1, squareNW.Location.Y);
                squareSE.Location = new Point(squareSE.Location.X - squareSize - 1, squareSE.Location.Y);
                squareNE.Location = new Point(squareNE.Location.X - squareSize - 1, squareNE.Location.Y);
                //在新位置显示方块
                Show(GameField.WinHandle);
                return true;
            }
            else
            {
                return false;
            }
        }

        //旋转方块（即轮换组成方块的正方形的颜色）
        public void Rotate()
        {
            Color SWForeColor = squareSW.ForeColor;
            Color NWForeColor = squareNW.ForeColor;
            Color SEForeColor = squareSE.ForeColor;
            Color NEForeColor = squareNE.ForeColor;
            Color SWBackColor = squareSW.BackColor;
            Color NWBackColor = squareNW.BackColor;
            Color SEBackColor = squareSE.BackColor;
            Color NEBackColor = squareNE.BackColor;

            Hide(GameField.WinHandle);

            squareSW.ForeColor = SEForeColor;
            squareNW.ForeColor = SWForeColor;
            squareSE.ForeColor = NEForeColor;
            squareNE.ForeColor = NWForeColor;
            squareSW.BackColor = SEBackColor;
            squareNW.BackColor = SWBackColor;
            squareSE.BackColor = NEBackColor;
            squareNE.BackColor = NWBackColor;

            Show(GameField.WinHandle);
        }

        public void Show(System.IntPtr WinHandle)
        {
            //在Game Field画出组成方块的每个正方形
            squareSW.Show(WinHandle);
            squareSE.Show(WinHandle);
            squareNE.Show(WinHandle);
            squareNW.Show(WinHandle);
        }

        public void Hide(System.IntPtr WinHandle)
        {
            //消去组成方块的每个正方形
            squareSW.Hide(WinHandle);
            squareSE.Hide(WinHandle);
            squareNE.Hide(WinHandle);
            squareNW.Hide(WinHandle);
        }

        //返回方块的TOP位置
        public int Top()
        {
            return (squareNW.Location.Y - 1) / (squareSize + 1);
        }

        public enum BlockTypes
        {
            Undefined = 0,
            full1 = 1,
            full2 = 2,
            Dot = 3,
            Line = 4,
            X = 5,
            L = 6
        };
        public BlockTypes BlockType;

        //定义每种类型方块的颜色
        public static Color[] backColors = {Color.Empty, Color.White, Color.Red, Color.SteelBlue, Color.DarkOrange,
         Color.LimeGreen, Color.DarkGray, Color.DeepPink, Color.Gold, Color.Black};
        public static Color[] foreColors = {Color.Empty, Color.WhiteSmoke, Color.IndianRed, Color.DodgerBlue, Color.SandyBrown,
        Color.LawnGreen, Color.Silver, Color.HotPink, Color.Khaki, Color.Black};

        public static int FindColorIndex(Color Color)
        {
            for (int i = 0; i <= foreColors.Length - 1; i++)
            {
                if (Color == foreColors[i])
                    return i;
            }
            return -1;
        }
    }
}
