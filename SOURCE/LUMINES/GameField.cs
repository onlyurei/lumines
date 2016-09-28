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
    //游戏区类
    public class GameField
    {
        public const int Width = 16; //游戏区宽度（方块数）
        public const int Height = 12; //游戏区高度（方块数）
        public const int SquareSize = 16; //正方形边长（像素）
        public static Color GridColor = Color.FromArgb(146, 146, 146); //栅格颜色
        public static System.IntPtr WinHandle; //游戏区句柄
        public static Color BackColor; //游戏区背景颜色
        public static int Score = 0; //游戏分数
        public static int ScoreLevel = 10000; //升级分数要求
        public static int Level = 1; //当前游戏级别
        public static int DeletedBlocksTotal; //消去的总BLOCK数
        public static int DeletedBlocks; //一遍扫描消去的BLOCK数
        public static int DeletedBlocksOnePass;
        public static bool DoBonus = false; //是否进行BONUS判定
        public static bool DoAllDeletedCheck = false;
        public static bool DoUniColorCheck = false;
        public static Square[,] ArrGameField = new Square[Width, Height]; //存放已停止方块信息的数组
        public static bool[] DelCol = new bool[16];
        public static bool[] HideCol = new bool[16];
        public static bool DoSetDelCol = true;
        public static int SquareRoundCorner = 1;
        public static int ScanLineColorIndex;
        
        public static bool IsEmpty(int x, int y)
        {
            //如果x或y越过游戏边界，返回false
            if ((y < 0 || y >= Height) || (x < 0 || x >= Width))
            {
                return false;
            }
            //测试arrBitGameField的相应位是否为1，若为1，返回false
            else if (ArrGameField[x, y] != null)
            {
                return false;
            }
            //其余情况则返回true
            return true;
        }

        //检查游戏区域里的单色方块(2*2)并将他们标记，以便扫描线经过后消除它们
        public static void CheckBlocks(int x1, int x2)
        {
            for(int x = x1; x <= x2; x++)
                for (int y = 2; y <= Height - 1; y++)
                {
                    //当一个方块右半部分被删去而左半边留下时，恢复左半边两个Square为正常状态
                    if (x == 0)
                    {
                        if (ArrGameField[x, y] != null && ArrGameField[x, y].ToBeDel && !ArrGameField[x, y].Delete)
                            if (x + 1 <= Width - 1)
                                if ((ArrGameField[x + 1, y] != null && !ArrGameField[x + 1, y].ToBeDel && !ArrGameField[x + 1, y].Delete) 
                                    || ArrGameField[x + 1, y] == null)
                                {
                                    ArrGameField[x, y].ResumeNormal(WinHandle);
                                }
                    }
                    else if (x < Width - 1)
                    {
                        if (ArrGameField[x, y] != null && ArrGameField[x, y].ToBeDel && !ArrGameField[x, y].Delete)
                            if (x - 1 >= 0 && x + 1 <= Width - 1 && y >= 0 && y <= Height - 1)
                                if ((ArrGameField[x - 1, y] != null && !ArrGameField[x - 1, y].ToBeDel && !ArrGameField[x - 1, y].Delete) 
                                    || ArrGameField[x - 1, y] == null)
                                    if ((ArrGameField[x + 1, y] != null && !ArrGameField[x + 1, y].ToBeDel && !ArrGameField[x + 1, y].Delete) 
                                        || ArrGameField[x + 1, y] == null)
                                    {
                                        ArrGameField[x, y].ResumeNormal(WinHandle);
                                    }
                    }
                    else
                    {
                        if (ArrGameField[x, y] != null && ArrGameField[x, y].ToBeDel && !ArrGameField[x, y].Delete)
                            if (x - 1 >= 0)
                                if ((ArrGameField[x - 1, y] != null && !ArrGameField[x - 1, y].ToBeDel && !ArrGameField[x - 1, y].Delete) 
                                    || ArrGameField[x - 1, y] == null)
                                {
                                    ArrGameField[x, y].ResumeNormal(WinHandle);
                                }
                    }
                }

            for(int x = x1; x <= x2 - 1; x++) //横列
            　　for(int y = 2; y <= Height - 2; y++) //纵列
                {
                    //检查当前正方形是否存在
                    if (ArrGameField[x, y] != null)
                    {
                        
                        //检查当前正方形周围是否构成完整方块
                        if (x >= 0 && x <= Width - 2 && y >= 2 && y <= Height - 2)
                        {
                            if ((ArrGameField[x + 1, y] != null)
                                && (ArrGameField[x, y + 1] != null)
                                && (ArrGameField[x + 1, y + 1] != null))
                            {
                                //检查四个小正方形是否颜色相同
                                if (ArrGameField[x, y].ForeColor == ArrGameField[x + 1, y].ForeColor
                                    && ArrGameField[x, y].ForeColor == ArrGameField[x, y + 1].ForeColor
                                    && ArrGameField[x, y].ForeColor == ArrGameField[x + 1, y + 1].ForeColor)
                                {
                                    //设置arrGameField相关位，以便扫描线消除方块，并改变方块外形
                                    if (!ArrGameField[x, y].Delete)
                                    {
                                        ArrGameField[x, y].ToBeDel = true;
                                        ArrGameField[x, y].size = new Size(SquareSize + 1, SquareSize + 1);
                                        //ArrGameField[x, y].BackColor = ArrGameField[x, y].ForeColor;
                                        ArrGameField[x, y].MonoColorShow(WinHandle);
                                    }

                                    if (!ArrGameField[x + 1, y].Delete)
                                    {
                                        ArrGameField[x + 1, y].ToBeDel = true;
                                        ArrGameField[x + 1, y].size = new Size(SquareSize, SquareSize + 1);
                                        //ArrGameField[x + 1, y].BackColor = ArrGameField[x, y].ForeColor;
                                        ArrGameField[x + 1, y].MonoColorShow(WinHandle);
                                    }

                                    if (!ArrGameField[x, y + 1].Delete)
                                    {
                                        ArrGameField[x, y + 1].ToBeDel = true;
                                        ArrGameField[x, y + 1].size = new Size(SquareSize + 1, SquareSize);
                                        //ArrGameField[x, y + 1].BackColor = ArrGameField[x, y].ForeColor;
                                        ArrGameField[x, y + 1].MonoColorShow(WinHandle);
                                    }

                                    if (!ArrGameField[x + 1, y + 1].Delete)
                                    {
                                        ArrGameField[x + 1, y + 1].ToBeDel = true;
                                        ArrGameField[x + 1, y + 1].size = new Size(SquareSize, SquareSize);
                                        //ArrGameField[x + 1, y + 1].BackColor = ArrGameField[x, y].ForeColor;
                                        ArrGameField[x + 1, y + 1].MonoColorShow(WinHandle);
                                    }
                                    if (!ArrGameField[x, y].Delete && !ArrGameField[x + 1, y].Delete &&
                                        !ArrGameField[x, y + 1].Delete && !ArrGameField[x + 1, y + 1].Delete)
                                    {
                                        Color rectColor = Color.White;
                                        if (ArrGameField[x, y].ForeColor == Color.Silver || ArrGameField[x, y].ForeColor == Color.WhiteSmoke ||
                                            ArrGameField[x, y].ForeColor == Color.Khaki)
                                            rectColor = Color.FromArgb(72, 72, 72);
                                        ScanLine.ScanLineGraphic.DrawRectangle(new Pen(new SolidBrush(rectColor), 2),
                                            new Rectangle(ArrGameField[x, y].Location,
                                            new Size(ArrGameField[x, y].size.Width * 2 - 1, ArrGameField[x, y].size.Width * 2 - 1)));
                                        if (!ArrGameField[x, y].Counted)
                                        {
                                            DeletedBlocksOnePass++;
                                            ArrGameField[x, y].Counted = true;
                                            ArrGameField[x, y].CountNum = DeletedBlocksOnePass;
                                        }
                                        ScanLine.ScanLineGraphic.DrawString(ArrGameField[x, y].CountNum.ToString(),
                                            new Font("ArialBlack", 8, FontStyle.Bold), new SolidBrush(rectColor),
                                            new PointF(ArrGameField[x, y].Location.X + 1, ArrGameField[x, y].Location.Y + 1));
                                    }
                                }
                            }
                        }
                    }
                 }
        }

        public static void SetDelCol()
        {
            bool set;
            for (int x = 0; x <= Width - 1; x++)
            {
                set = false;
                for (int y = 2; y <= Height - 1; y++)
                {
                    if (ArrGameField[x, y] != null)
                        if (ArrGameField[x, y].ToBeDel)
                        {
                            DelCol[x] = true;
                            set = true;
                        }
                    if (!set)
                        DelCol[x] = false;
                }
            }
        }

        public static bool CheckUniColor()
        {
            if (DoBonus)
            {
                Color color = Color.Empty;
                bool squareExists = false;
                for (int x = 0; x <= Width - 1; x++) //横列
                    for (int y = 2; y <= Height - 1; y++) //纵列
                    {
                        if (ArrGameField[x, y] != null)
                        {
                            if (!squareExists)
                                squareExists = true;
                            if (color == Color.Empty)
                            {
                                color = ArrGameField[x, y].ForeColor;
                            }
                            else
                            {
                                if (color != ArrGameField[x, y].ForeColor)
                                    return false;
                            }
                        }
                    }
                if (squareExists)
                {
                    Score += 1000;
                    MainForm.BonusInfo = "UNICOLOR BONUS 1000PTS";
                    return true;
                }
                return false;
            }
            return false;
        }

        public static bool CheckAllDeleted()
        {
            if (DoBonus)
            {
                for (int x = 0; x <= Width - 1; x++) //横列
                    for (int y = 2; y <= Height - 1; y++) //纵列
                        if (ArrGameField[x, y] != null)
                            return false;
                Score += 10000;
                MainForm.BonusInfo = "ALL DELETED BONUS 10000PTS";
                return true;
            }
            return false;
        }

        public static void HideBlocks(int x1, int x2)
        {
            for (int x = x1; x <= x2; x++)
            {
                if (x >= 0 && x <= Width - 1)
                {
                    HideCol[x] = true;
                    for (int y = 2; y <= Height - 1; y++)
                    {
                        if (ArrGameField[x, y] != null)
                            if (ArrGameField[x, y].ToBeDel)
                            {
                                ArrGameField[x, y].HideToDelete(WinHandle);
                                ArrGameField[x, y].BackColor = Color.FromArgb(37, 36, 52);
                                ArrGameField[x, y].ForeColor = Color.FromArgb(133, 129, 166);
                                ArrGameField[x, y].Delete = true;
                                ArrGameField[x, y].ToBeDel = false;
                            }
                    }
                }
            }
        }

        public static void DeleteBlocks(int x1, int x2)
        {
            int y;
            Pen pen = new Pen(GameField.GridColor, 1);
            for (int x = x1; x <= x2; x++)
            {
                if (x >= 0 && x <= Width - 1)
                {
                    y = Height - 1;
                    while (y > 2)
                    {
                        if (ArrGameField[x, y] == null)
                            y = 0;
                        else
                            if (ArrGameField[x, y].Delete == true)
                            {
                                //ArrGameField[x, y].Delete = false;
                                ArrGameField[x, y].DeleteHide(WinHandle);
                                if (ArrGameField[x, y].Counted)
                                {
                                    DeletedBlocksTotal++;
                                    DeletedBlocks++;
                                    ArrGameField[x, y].Counted = false;
                                    ArrGameField[x, y].CountNum = 0;
                                }
                                for (int i = y; ArrGameField[x, i] != null && i >= 2; i--)
                                {
                                    if (i >= 2)
                                    {
                                        if (ArrGameField[x, i - 1] != null)
                                        {
                                            ArrGameField[x, i - 1].size = new Size(SquareSize, SquareSize);
                                            ArrGameField[x, i] = ArrGameField[x, i - 1];
                                            ArrGameField[x, i - 1].DeleteHide(WinHandle);
                                            for (int i1 = i - 1; i1 <= i + 1; i1++)
                                            {
                                                if (i1 >= 2 && i1 <= Height)
                                                    ScanLine.ScanLineGraphic.DrawLine(pen, new Point(0, (SquareSize + 1) * i1),
                                                        new Point((SquareSize + 1) * Width, (SquareSize + 1) * i1));
                                            }
                                        }
                                        else
                                        {
                                            ArrGameField[x, i].size = new Size(SquareSize, SquareSize);
                                            ArrGameField[x, i].DeleteHide(WinHandle);
                                            ArrGameField[x, i] = null;
                                            for (int i1 = i - 1; i1 <= i + 1; i1++)
                                            {
                                                if (i1 >= 2 && i1 <= Height)
                                                    ScanLine.ScanLineGraphic.DrawLine(pen, new Point(0, (SquareSize + 1) * i1),
                                                        new Point((SquareSize + 1) * Width, (SquareSize + 1) * i1));
                                            }
                                        }
                                        //更新方块位置
                                        if (ArrGameField[x, i] != null)
                                        {
                                            ArrGameField[x, i].Location = new Point(ArrGameField[x, i].Location.X,
                                                ArrGameField[x, i].Location.Y + SquareSize + 1);
                                            ArrGameField[x, i].DrawGridFrame(WinHandle);
                                            ArrGameField[x, i].Show(WinHandle);
                                        }
                                    }
                                    else
                                    {
                                        ArrGameField[x, i].size = new Size(SquareSize, SquareSize);
                                        ArrGameField[x, i].DeleteHide(WinHandle);
                                        ArrGameField[x, i] = null;
                                    }
                                }
                            }
                            else
                            {
                                ArrGameField[x, y].DrawGridFrame(WinHandle);
                                ArrGameField[x, y].Show(WinHandle);
                                y--;
                            }
                    }
                }
                ScanLine.ScanLineGraphic.DrawLine(pen, new Point((SquareSize + 1) * x, 34),
                                            new Point((SquareSize + 1) * x, (GameField.SquareSize + 1) * GameField.Width));
                HideCol[x] = false;
            }
            ScanLine.ScanLineGraphic.DrawLine(pen, new Point((SquareSize + 1) * (x2 + 1), 34),
                                            new Point((SquareSize + 1) * (x2 + 1), (GameField.SquareSize + 1) * GameField.Width));
            
            CheckBlocks(0, Width - 1);
            GameField.SetDelCol();
        }

        public static void StopSquare(Square Square, int x, int y)
        {
            ArrGameField[x, y] = Square;
        }

        public static void Redraw()
        {
            for (int y = Height - 1; y >= 0; y--)
                for (int x = Width - 1; x >= 0; x--)
                {
                    if (ArrGameField[x, y] != null)
                    {
                        if (ArrGameField[x, y].ToBeDel == true)
                        {
                            ArrGameField[x, y].MonoColorShow(WinHandle);
                        }
                        else
                            ArrGameField[x, y].Show(WinHandle);
                    }
                }
        }

        public static void Reset()
        {
            for (int x = 0; x <= Width - 1; x++)
            {
                for (int y = 0; y <= Height - 1; y++)
                {
                    ArrGameField[x, y] = null;
                }
                HideCol[x] = false;
                DelCol[x] = false;
            }
        }
    }
}
