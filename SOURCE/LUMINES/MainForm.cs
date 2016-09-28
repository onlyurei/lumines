using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace LUMINES
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private Block CurrentBlock; //当前方块
        private Block NextBlock1; //下一方块(1)
        private Block NextBlock2; //下一方块(2)
        private Block NextBlock3; //下一方块(3)
        private bool stillProcessing = false; //GameClock的Tick事件是否还在处理中
        private bool stillScanning = false; //ScanClock的Tick事件是否还在处理中
        private bool blockHold;
        private int elapsedSeconds = 0; //游戏持续的秒数
        private int highScore = 0;//最高游戏分数
        private char preMoveDirection = 'n';
        private int scanLineActivePos;
        public static string BonusInfo = "";
        private int startX;

        private void gameReset()
        {
            //参数重置
            GameField.Score = 0;
            GameField.ScoreLevel = 10000;
            GameField.DoBonus = false;
            GameField.DoAllDeletedCheck = false;
            GameField.DoUniColorCheck = false;
            GameField.Level = 1;
            GameField.DeletedBlocks = 0;
            GameField.DeletedBlocksOnePass = 0;
            GameField.DeletedBlocksTotal = 0;
            stillProcessing = false;
            stillScanning = false;
            elapsedSeconds = 0;
            GameField.DoSetDelCol = true;
            blockHold = true;
            scanLineActivePos = 0;

            //控件重置
            tmrGameClock.Enabled = false;
            tmrHold.Enabled = true;
            tmrScanLine.Enabled = true;
            tmrTime.Enabled = true;
            tmrBonusCheck.Enabled = true;
            cmdStart.Enabled = false;
            lblScoreValue.Text = "0";
            lblDeleted.Text = "0";
            lblLevelValue.Text = "1";
            lblGAMEOVER.Visible = false;
            lblSTART.Visible = false;
            lblLUMINES.Visible = false;
            lblPAUSED.Visible = false;
            lblHighScoreLoadFailed.Visible = false;
            lblBonus.Text = "";
            lblTime.Text = "0:00";

            //清屏并重置
            GameField.Reset();
            picBackGround.Invalidate();
            picNextBlock1.Invalidate();
            picNextBlock2.Invalidate();
            picNextBlock3.Invalidate();
            Application.DoEvents();

            //重置方块下落速度和扫描线移动速度
            tmrGameClock.Interval = 480;
            tmrScanLine.Interval = 14;
        }

        private string add0(int num)
        {
            if (num <= 9)
                return "0" + num.ToString();
            else
                return num.ToString();
        }

        private string getTime(int seconds)
        {
            if (seconds <= 59)
            {
                return "0:" + add0(seconds);
            }
            else if (seconds <= 3599)
            {
                int minute = (int)(seconds / 60);
                int second = seconds - minute * 60;
                return minute + ":" + add0(second);
            }
            else if (seconds <= 86399)
            {
                int hour = (int)(seconds / 3600);
                int minute = (int)((seconds - hour * 3600) / 60);
                int second = seconds - hour * 3600 - minute * 60;
                return hour + ":" + add0(minute) + ":" + add0(second);
            }
            else
            {
                tmrTime.Enabled = false;
                return "> 1 day";
            }
        }

        private void cmdStart_Click(object sender, EventArgs e)
        {
            gameReset();
            Rectangle rect = new Rectangle(new Point(0, 0), new Size(this.picBackGround.Width, 33));
            ScanLine.ScanLineGraphic.DrawRectangle(new Pen(GameField.BackColor, 1), rect);
            ScanLine.ScanLineGraphic.FillRectangle(new SolidBrush(GameField.BackColor), rect);
            //随机选择方块颜色和扫描线颜色，以及方块外形
            Random random = new Random();
            Block.Color1 = random.Next(9) + 1;
            Block.Color2 = random.Next(9) + 1;
            GameField.SquareRoundCorner = random.Next(2) + 1;
            GameField.ScanLineColorIndex = random.Next(5);
            //避免两种颜色一样或者不协调
            while ((Block.Color2 == Block.Color1) ||
                (Block.backColors[Block.Color1] == Color.LimeGreen && Block.backColors[Block.Color2] == Color.Red) ||
                (Block.backColors[Block.Color2] == Color.LimeGreen && Block.backColors[Block.Color1] == Color.Red) ||
                (Block.backColors[Block.Color1] == Color.Red && Block.backColors[Block.Color2] == Color.DeepPink) ||
                (Block.backColors[Block.Color2] == Color.Red && Block.backColors[Block.Color1] == Color.DeepPink) ||
                (Block.backColors[Block.Color1] == Color.LimeGreen && Block.backColors[Block.Color2] == Color.DeepPink) ||
                (Block.backColors[Block.Color2] == Color.LimeGreen && Block.backColors[Block.Color1] == Color.DeepPink))
                Block.Color2 = random.Next(8) + 1;

            //创建当前方块和下一方块
            CurrentBlock = new Block(new Point(GameField.SquareSize * 7 + 8,1), Block.BlockTypes.Undefined, 0);
            CurrentBlock.Show(picBackGround.Handle);
            NextBlock1 = new Block(new Point(0, 0), (Block.BlockTypes)(random.Next(6)) + 1, 0);
            NextBlock1.Show(picNextBlock1.Handle);
            NextBlock2 = new Block(new Point(0, 0), (Block.BlockTypes)(random.Next(6)) + 1, 0);
            NextBlock2.Show(picNextBlock2.Handle);
            NextBlock3 = new Block(new Point(0, 0), (Block.BlockTypes)(random.Next(6)) + 1, 0);
            NextBlock3.Show(picNextBlock3.Handle);
        }

        private bool paused = false;

        private void keyDown(object sender, KeyEventArgs e)
        {
            if (cmdStart.Enabled == false)
            {
                switch (e.KeyCode)
                {
                    case Keys.Right:
                        if (paused)
                            return;
                        CurrentBlock.Right(); 
                        break;
                    case Keys.Left:
                        if (paused)
                            return;
                        CurrentBlock.Left(); 
                        break;
                    case Keys.Up:
                        if (paused)
                            return;
                        CurrentBlock.Rotate(); 
                        break;
                    case Keys.Down:
                        if (paused)
                            return;
                        while (CurrentBlock.Down()) ;
                        if (CurrentBlock.Top() < 1)
                        {
                            tmrTime.Enabled = false;
                            tmrBonusCheck.Enabled = false;
                            tmrGameClock.Enabled = false;
                            tmrScanLine.Enabled = false;
                            cmdStart.Enabled = true;
                            lblGAMEOVER.Visible = true;
                            stillProcessing = false;
                            return;
                        }
                        //消去顶部第二行方块
                        for (int x = 0; x <= GameField.Width - 1; x++)
                            for (int y = 0; y <= 1; y++)
                            {
                                if (GameField.ArrGameField[x, y] != null)
                                {
                                    GameField.ArrGameField[x, y].Hide(GameField.WinHandle);
                                    GameField.ArrGameField[x, y] = null;
                                }
                            }
                        if (!GameField.DoAllDeletedCheck)
                            GameField.DoAllDeletedCheck = true;
                        //更换当前方块
                        CurrentBlock = new Block(new Point(GameField.SquareSize * 7 + 8, 1), NextBlock1.BlockType, NextBlock1.direction);
                        CurrentBlock.Show(picBackGround.Handle);
                        //创建下一方块
                        NextBlock1.Hide(picNextBlock1.Handle);
                        NextBlock1 = new Block(new Point(0, 0), NextBlock2.BlockType, NextBlock2.direction);
                        NextBlock1.Show(picNextBlock1.Handle);
                        NextBlock2 = new Block(new Point(0, 0), NextBlock3.BlockType, NextBlock3.direction);
                        NextBlock2.Show(picNextBlock2.Handle);
                        NextBlock3 = new Block(new Point(0, 0), Block.BlockTypes.Undefined, 0);
                        NextBlock3.Show(picNextBlock3.Handle);
                        //使方块在顶部暂留一会儿
                        tmrGameClock.Enabled = false;
                        tmrHold.Enabled = true;
                        blockHold = true;
                        break;
                    case Keys.Space:
                        tmrScanLine.Enabled = !tmrScanLine.Enabled;
                        tmrTime.Enabled = !tmrTime.Enabled;
                        lblPAUSED.Visible = !lblPAUSED.Visible;
                        if (tmrScanLine.Enabled)
                        {
                            paused = false;
                            this.Text = "LUMINES";
                            picBackGround.Invalidate();
                            picNextBlock1.Invalidate();
                            picNextBlock2.Invalidate();
                            picNextBlock3.Invalidate();
                            Application.DoEvents();
                            GameField.Redraw();
                            GameField.CheckBlocks(0, GameField.Width - 1);
                            CurrentBlock.Show(GameField.WinHandle);
                            if (NextBlock1 != null)
                                NextBlock1.Show(picNextBlock1.Handle);
                            if (NextBlock2 != null)
                                NextBlock2.Show(picNextBlock2.Handle);
                            if (NextBlock3 != null)
                                NextBlock3.Show(picNextBlock3.Handle);
                            if (blockHold == true)
                            {
                                tmrGameClock.Enabled = false;
                                tmrHold.Enabled = true;
                            }
                            else
                            {
                                tmrGameClock.Enabled = true;
                                tmrHold.Enabled = false;
                            }
                        }
                        else
                        {
                            paused = true;
                            this.Text = "LUMINES (Paused), Press 'SPACE' to Continue";
                            tmrGameClock.Enabled = false;
                            tmrHold.Enabled = false;
                        }
                        break;
                    case Keys.R:
                        tmrGameClock.Enabled = false;
                        tmrScanLine.Enabled = false;
                        tmrHold.Enabled = false;
                        tmrTime.Enabled = false;
                        if (MessageBox.Show("RESTART?", "RESTART CONFIRM", MessageBoxButtons.YesNo) == DialogResult.Yes)
                        {
                            //清屏并结束
                            GameField.Reset();
                            picBackGround.Invalidate();
                            picNextBlock1.Invalidate();
                            picNextBlock2.Invalidate();
                            picNextBlock3.Invalidate();
                            Application.DoEvents();
                            cmdStart.Enabled = true;
                            this.Text = "LUMINES";
                            lblSTART.Visible = true;
                            lblLUMINES.Visible = true;
                            lblPAUSED.Visible = false;
                            stillProcessing = false;
                            stillScanning = false;
                            lblScoreValue.Text = "0";
                            lblDeleted.Text = "0";
                            lblLevelValue.Text = "1";
                            lblTime.Text = "0:00";
                            BonusInfo = "";
                            tmrBonusCheck.Enabled = false;
                            paused = false;
                        }
                        else //返回游戏
                        {
                            paused = false;
                            this.Text = "LUMINES";
                            lblPAUSED.Visible = false;
                            picBackGround.Invalidate();
                            picNextBlock1.Invalidate();
                            picNextBlock2.Invalidate();
                            picNextBlock3.Invalidate();
                            Application.DoEvents();
                            GameField.Redraw();
                            GameField.CheckBlocks(0, GameField.Width - 1);
                            CurrentBlock.Show(GameField.WinHandle);
                            if (NextBlock1 != null)
                                NextBlock1.Show(picNextBlock1.Handle);
                            if (NextBlock2 != null)
                                NextBlock2.Show(picNextBlock2.Handle);
                            if (NextBlock3 != null)
                                NextBlock3.Show(picNextBlock3.Handle);
                            tmrScanLine.Enabled = true;
                            tmrTime.Enabled = true;
                            if (blockHold == true)
                            {
                                tmrGameClock.Enabled = false;
                                tmrHold.Enabled = true;
                            }
                            else
                            {
                                tmrGameClock.Enabled = true;
                                tmrHold.Enabled = false;
                            }
                        }
                        break;
                    default: break;
                }
                Invalidate();
            }
        }

        private void tmrScanLine_Tick(object sender, EventArgs e)
        {
            if (stillScanning) return;
            stillScanning = true;
            int x = scanLineActivePos / (GameField.SquareSize + 1);
            int dx = scanLineActivePos % (GameField.SquareSize + 1);

            if (x == 0)
            {
                if (GameField.DelCol[0])
                {
                    startX = 0;
                    if (dx == 0)
                        GameField.HideBlocks(0, 0);
                    else if (dx == GameField.SquareSize)
                    {
                        if (!GameField.DelCol[1])
                        {
                            GameField.DeleteBlocks(0, 0);
                            GameField.DoSetDelCol = true;
                            GameField.DeletedBlocksOnePass = 0;
                        }
                        else
                            GameField.DoSetDelCol = false;
                    }
                }
            }
            else if (x > 0 && x < GameField.Width - 1)
            {
                if (!GameField.DelCol[x - 1] && GameField.DelCol[x])
                {
                    startX = x;
                    if (dx == 0)
                        GameField.HideBlocks(x, x);
                    else if (dx == GameField.SquareSize)
                    {
                        if (!GameField.DelCol[x + 1])
                        {
                            GameField.DeleteBlocks(x, x);
                            GameField.DoSetDelCol = true;
                            GameField.DeletedBlocksOnePass = 0;
                        }
                        else
                        {
                            GameField.DoSetDelCol = false;
                        }
                    }
                }
                else if (GameField.DelCol[x] && !GameField.DelCol[x + 1])
                {
                    if (dx == 0)
                        GameField.HideBlocks(x, x);
                    else if (dx == GameField.SquareSize)
                    {
                        GameField.DeleteBlocks(startX, x);
                        GameField.DoSetDelCol = true;
                        GameField.DeletedBlocksOnePass = 0;
                    }
                }
                else if (GameField.DelCol[x])
                {
                    if (dx == 0)
                    {
                        GameField.HideBlocks(x, x);
                        GameField.DoSetDelCol = false;
                    }
                }
            }
            else if (x == GameField.Width - 1)
            {
                if (dx == 0)
                {
                    if (GameField.DelCol[x])
                        GameField.HideBlocks(x, x);
                    if (GameField.DelCol[x - 1])
                        startX = x - 1;
                    else startX = x;
                }
                else if (dx == GameField.SquareSize)
                {
                    if (GameField.DelCol[x])
                    {
                        GameField.DeleteBlocks(startX, x);
                        GameField.DeletedBlocksOnePass = 0;
                    }
                    GameField.DoSetDelCol = true;
                    if (GameField.DeletedBlocks < 8)
                        GameField.Score += GameField.DeletedBlocks * 40;
                    else
                        GameField.Score += GameField.DeletedBlocks * 40 * GameField.DeletedBlocks;
                    lblScoreValue.Text = GameField.Score.ToString();
                    lblDeleted.Text = GameField.DeletedBlocksTotal.ToString();
                    if (GameField.DeletedBlocks < 6)
                    { }
                    else if (GameField.DeletedBlocks <= 8)
                        BonusInfo = "GOOD!";
                    else if (GameField.DeletedBlocks <= 12)
                        BonusInfo = "EXCELLENT!";
                    else if (GameField.DeletedBlocks <= 16)
                        BonusInfo = "AWESOME!";
                    else if (GameField.DeletedBlocks <= 20)
                        BonusInfo = "SUPER AWESOME!!";
                    else if (GameField.DeletedBlocks <= 24)
                        BonusInfo = "CRAZY!!";
                    else if (GameField.DeletedBlocks <= 28)
                        BonusInfo = "IMPOSSIBLE...";
                    else
                        BonusInfo = "THAT'S CHEATING!!!";
                    if (highScore < GameField.Score)
                    {
                        highScore = GameField.Score;
                        lblHighScore.Text = highScore.ToString();
                        //保存最高分数
                        try
                        {
                            Properties.Settings.Default.HighScore = highScore.ToString();
                            Properties.Settings.Default.Save();
                        }
                        catch (Exception E)
                        {

                        }
                    }
                    //升级操作
                    if (GameField.Score >= GameField.ScoreLevel)
                    {
                        GameField.ScoreLevel += 10000;
                        GameField.Level++;
                        lblLevelValue.Text = GameField.Level.ToString();
                        //随机改变方块下落速度和扫描线移动速度
                        Random random = new Random();
                        tmrGameClock.Interval = (int)(random.NextDouble() * 300 + 240);
                        tmrScanLine.Interval = (int)(random.NextDouble() * 15 + 10);
                        //改变方块圆角大小
                        GameField.SquareRoundCorner = random.Next(2) + 1;
                        //更换扫描线颜色
                        int oldScanLineColor = GameField.ScanLineColorIndex;
                        while (GameField.ScanLineColorIndex == oldScanLineColor)
                            GameField.ScanLineColorIndex = random.Next(5);
                        //更换方块颜色
                        int oldColor1 = Block.Color1;
                        int oldColor2 = Block.Color2;
                        //保证方块的两种颜色跟变换前不一样，并且组成方块的两种颜色不相同
                        while (Block.Color1 == oldColor1 || Block.Color2 == oldColor2 || Block.Color1 == oldColor2 || Block.Color2 == oldColor1 ||
                              (Block.Color2 == Block.Color1) ||
                              (Block.backColors[Block.Color1] == Color.LimeGreen && Block.backColors[Block.Color2] == Color.Red) ||
                              (Block.backColors[Block.Color2] == Color.LimeGreen && Block.backColors[Block.Color1] == Color.Red) ||
                              (Block.backColors[Block.Color1] == Color.Red && Block.backColors[Block.Color2] == Color.DeepPink) ||
                              (Block.backColors[Block.Color2] == Color.Red && Block.backColors[Block.Color1] == Color.DeepPink) ||
                              (Block.backColors[Block.Color1] == Color.LimeGreen && Block.backColors[Block.Color2] == Color.DeepPink) ||
                              (Block.backColors[Block.Color2] == Color.LimeGreen && Block.backColors[Block.Color1] == Color.DeepPink))
                        {
                            Block.Color1 = random.Next(9) + 1;
                            Block.Color2 = random.Next(9) + 1;
                        }
                        //更换当前下落方块和三个预告方块的颜色
                        if (CurrentBlock.squareNE.ForeColor == Block.foreColors[oldColor1])
                        {
                            CurrentBlock.squareNE.ForeColor = Block.foreColors[Block.Color1];
                            CurrentBlock.squareNE.BackColor = Block.backColors[Block.Color1];
                        }
                        else
                        {
                            CurrentBlock.squareNE.ForeColor = Block.foreColors[Block.Color2];
                            CurrentBlock.squareNE.BackColor = Block.backColors[Block.Color2];
                        }
                        if (CurrentBlock.squareNW.ForeColor == Block.foreColors[oldColor1])
                        {
                            CurrentBlock.squareNW.ForeColor = Block.foreColors[Block.Color1];
                            CurrentBlock.squareNW.BackColor = Block.backColors[Block.Color1];
                        }
                        else
                        {
                            CurrentBlock.squareNW.ForeColor = Block.foreColors[Block.Color2];
                            CurrentBlock.squareNW.BackColor = Block.backColors[Block.Color2];
                        }
                        if (CurrentBlock.squareSE.ForeColor == Block.foreColors[oldColor1])
                        {
                            CurrentBlock.squareSE.ForeColor = Block.foreColors[Block.Color1];
                            CurrentBlock.squareSE.BackColor = Block.backColors[Block.Color1];
                        }
                        else
                        {
                            CurrentBlock.squareSE.ForeColor = Block.foreColors[Block.Color2];
                            CurrentBlock.squareSE.BackColor = Block.backColors[Block.Color2];
                        }
                        if (CurrentBlock.squareSW.ForeColor == Block.foreColors[oldColor1])
                        {
                            CurrentBlock.squareSW.ForeColor = Block.foreColors[Block.Color1];
                            CurrentBlock.squareSW.BackColor = Block.backColors[Block.Color1];
                        }
                        else
                        {
                            CurrentBlock.squareSW.ForeColor = Block.foreColors[Block.Color2];
                            CurrentBlock.squareSW.BackColor = Block.backColors[Block.Color2];
                        }
                        if (NextBlock1.squareNE.ForeColor == Block.foreColors[oldColor1])
                        {
                            NextBlock1.squareNE.ForeColor = Block.foreColors[Block.Color1];
                            NextBlock1.squareNE.BackColor = Block.backColors[Block.Color1];
                        }
                        else
                        {
                            NextBlock1.squareNE.ForeColor = Block.foreColors[Block.Color2];
                            NextBlock1.squareNE.BackColor = Block.backColors[Block.Color2];
                        }
                        if (NextBlock1.squareNW.ForeColor == Block.foreColors[oldColor1])
                        {
                            NextBlock1.squareNW.ForeColor = Block.foreColors[Block.Color1];
                            NextBlock1.squareNW.BackColor = Block.backColors[Block.Color1];
                        }
                        else
                        {
                            NextBlock1.squareNW.ForeColor = Block.foreColors[Block.Color2];
                            NextBlock1.squareNW.BackColor = Block.backColors[Block.Color2];
                        }
                        if (NextBlock1.squareSE.ForeColor == Block.foreColors[oldColor1])
                        {
                            NextBlock1.squareSE.ForeColor = Block.foreColors[Block.Color1];
                            NextBlock1.squareSE.BackColor = Block.backColors[Block.Color1];
                        }
                        else
                        {
                            NextBlock1.squareSE.ForeColor = Block.foreColors[Block.Color2];
                            NextBlock1.squareSE.BackColor = Block.backColors[Block.Color2];
                        }
                        if (NextBlock1.squareSW.ForeColor == Block.foreColors[oldColor1])
                        {
                            NextBlock1.squareSW.ForeColor = Block.foreColors[Block.Color1];
                            NextBlock1.squareSW.BackColor = Block.backColors[Block.Color1];
                        }
                        else
                        {
                            NextBlock1.squareSW.ForeColor = Block.foreColors[Block.Color2];
                            NextBlock1.squareSW.BackColor = Block.backColors[Block.Color2];
                        }
                        if (NextBlock2.squareNE.ForeColor == Block.foreColors[oldColor1])
                        {
                            NextBlock2.squareNE.ForeColor = Block.foreColors[Block.Color1];
                            NextBlock2.squareNE.BackColor = Block.backColors[Block.Color1];
                        }
                        else
                        {
                            NextBlock2.squareNE.ForeColor = Block.foreColors[Block.Color2];
                            NextBlock2.squareNE.BackColor = Block.backColors[Block.Color2];
                        }
                        if (NextBlock2.squareNW.ForeColor == Block.foreColors[oldColor1])
                        {
                            NextBlock2.squareNW.ForeColor = Block.foreColors[Block.Color1];
                            NextBlock2.squareNW.BackColor = Block.backColors[Block.Color1];
                        }
                        else
                        {
                            NextBlock2.squareNW.ForeColor = Block.foreColors[Block.Color2];
                            NextBlock2.squareNW.BackColor = Block.backColors[Block.Color2];
                        }
                        if (NextBlock2.squareSE.ForeColor == Block.foreColors[oldColor1])
                        {
                            NextBlock2.squareSE.ForeColor = Block.foreColors[Block.Color1];
                            NextBlock2.squareSE.BackColor = Block.backColors[Block.Color1];
                        }
                        else
                        {
                            NextBlock2.squareSE.ForeColor = Block.foreColors[Block.Color2];
                            NextBlock2.squareSE.BackColor = Block.backColors[Block.Color2];
                        }
                        if (NextBlock2.squareSW.ForeColor == Block.foreColors[oldColor1])
                        {
                            NextBlock2.squareSW.ForeColor = Block.foreColors[Block.Color1];
                            NextBlock2.squareSW.BackColor = Block.backColors[Block.Color1];
                        }
                        else
                        {
                            NextBlock2.squareSW.ForeColor = Block.foreColors[Block.Color2];
                            NextBlock2.squareSW.BackColor = Block.backColors[Block.Color2];
                        }
                        if (NextBlock3.squareNE.ForeColor == Block.foreColors[oldColor1])
                        {
                            NextBlock3.squareNE.ForeColor = Block.foreColors[Block.Color1];
                            NextBlock3.squareNE.BackColor = Block.backColors[Block.Color1];
                        }
                        else
                        {
                            NextBlock3.squareNE.ForeColor = Block.foreColors[Block.Color2];
                            NextBlock3.squareNE.BackColor = Block.backColors[Block.Color2];
                        }
                        if (NextBlock3.squareNW.ForeColor == Block.foreColors[oldColor1])
                        {
                            NextBlock3.squareNW.ForeColor = Block.foreColors[Block.Color1];
                            NextBlock3.squareNW.BackColor = Block.backColors[Block.Color1];
                        }
                        else
                        {
                            NextBlock3.squareNW.ForeColor = Block.foreColors[Block.Color2];
                            NextBlock3.squareNW.BackColor = Block.backColors[Block.Color2];
                        }
                        if (NextBlock3.squareSE.ForeColor == Block.foreColors[oldColor1])
                        {
                            NextBlock3.squareSE.ForeColor = Block.foreColors[Block.Color1];
                            NextBlock3.squareSE.BackColor = Block.backColors[Block.Color1];
                        }
                        else
                        {
                            NextBlock3.squareSE.ForeColor = Block.foreColors[Block.Color2];
                            NextBlock3.squareSE.BackColor = Block.backColors[Block.Color2];
                        }
                        if (NextBlock3.squareSW.ForeColor == Block.foreColors[oldColor1])
                        {
                            NextBlock3.squareSW.ForeColor = Block.foreColors[Block.Color1];
                            NextBlock3.squareSW.BackColor = Block.backColors[Block.Color1];
                        }
                        else
                        {
                            NextBlock3.squareSW.ForeColor = Block.foreColors[Block.Color2];
                            NextBlock3.squareSW.BackColor = Block.backColors[Block.Color2];
                        }
                        //更换已落底方块的颜色
                        for (int i = 0; i <= GameField.Width - 1; i++)
                            for (int j = 0; j <= GameField.Height - 1; j++)
                            {
                                if (GameField.ArrGameField[i, j] != null)
                                {
                                    if (GameField.ArrGameField[i, j].ForeColor == Block.foreColors[oldColor1])
                                    {
                                        GameField.ArrGameField[i, j].ForeColor = Block.foreColors[Block.Color1];
                                        GameField.ArrGameField[i, j].BackColor = Block.backColors[Block.Color1];
                                    }
                                    else
                                    {
                                        GameField.ArrGameField[i, j].ForeColor = Block.foreColors[Block.Color2];
                                        GameField.ArrGameField[i, j].BackColor = Block.backColors[Block.Color2];
                                    }
                                }
                            }
                        //重绘窗口使方块颜色更新
                        picBackGround.Invalidate();
                        picNextBlock1.Invalidate();
                        picNextBlock2.Invalidate();
                        picNextBlock3.Invalidate();
                        Application.DoEvents();
                        GameField.Redraw();
                        GameField.CheckBlocks(0, GameField.Width - 1);
                        if (CurrentBlock != null)
                            CurrentBlock.Show(GameField.WinHandle);
                        if (NextBlock1 != null)
                            NextBlock1.Show(picNextBlock1.Handle);
                        if (NextBlock2 != null)
                            NextBlock2.Show(picNextBlock2.Handle);
                        if (NextBlock3 != null)
                            NextBlock3.Show(picNextBlock3.Handle);
                    }
                }
            }

            ScanLine scanLine = new ScanLine(GameField.ScanLineColorIndex);
            scanLine.HideTop(scanLineActivePos - 1);
            scanLine.ShowTop(scanLineActivePos);

            if (scanLineActivePos == 0)
            {
                GameField.DeletedBlocks = 0;
                ScanLine.ScanLineGraphic.DrawLine(new Pen(GameField.GridColor, 1),
                        new Point(this.picBackGround.Width, 34), new Point(this.picBackGround.Width, this.picBackGround.Height));
                Rectangle rectangle = new Rectangle(new Point(this.picBackGround.Width - 28, 19), new Size(28, 14));
                ScanLine.ScanLineGraphic.DrawRectangle(new Pen(GameField.BackColor, 1), rectangle);
                ScanLine.ScanLineGraphic.FillRectangle(new SolidBrush(GameField.BackColor), rectangle);
            }
            if (dx == 0)
            {
                if (x >= 2 && x <= GameField.Width)
                {
                    bool doSth = true;
                    for (int c = 2; c <= GameField.Height - 1; c++)
                        if (GameField.ArrGameField[x - 1, c] != null)
                            if (GameField.ArrGameField[x - 1, c].Delete)
                            {
                                doSth = false;
                                break;
                            }
                    if (doSth)
                    {
                        for (int i = 0; i <= x - 2; i++)
                        {
                            if (GameField.HideCol[i])
                                GameField.DeleteBlocks(i, i);
                        }
                    }
                }
                for (int y = 2; y <= GameField.Height - 1; y++)
                {
                    if (x >= 1)
                        if (GameField.ArrGameField[x - 1, y] == null)
                            ScanLine.ScanLineGraphic.DrawLine(new Pen(new SolidBrush(this.picBackGround.BackColor), 1),
                        new Point(scanLineActivePos - 1, y * (GameField.SquareSize + 1) + 1), new Point(scanLineActivePos - 1, y * (GameField.SquareSize + 1) + 1 + GameField.SquareSize));
                }
                for (int y = 34; y <= this.picBackGround.Height; y += 17)
                {
                    ScanLine.ScanLineGraphic.DrawLine(new Pen(new SolidBrush(GameField.GridColor), 1),
                        new Point(scanLineActivePos - 1, y), new Point(scanLineActivePos, y));
                }
            }
            else if (dx == 1)
            {
                ScanLine.ScanLineGraphic.DrawLine(new Pen(new SolidBrush(GameField.GridColor), 1),
                        new Point(scanLineActivePos - 1, 34), new Point(scanLineActivePos - 1, this.picBackGround.Height));
            }
            else
            {
                for (int y = 2; y <= GameField.Height - 1; y++)
                {
                    if (x <= GameField.Width - 1)
                        if (GameField.ArrGameField[x, y] == null)
                            ScanLine.ScanLineGraphic.DrawLine(new Pen(new SolidBrush(this.picBackGround.BackColor), 1),
                        new Point(scanLineActivePos - 1, y * (GameField.SquareSize + 1) + 1), new Point(scanLineActivePos - 1, y * (GameField.SquareSize + 1) + 1 + GameField.SquareSize));
                }
                for (int y = 34; y <= this.picBackGround.Height; y += 17)
                {
                    ScanLine.ScanLineGraphic.DrawLine(new Pen(new SolidBrush(GameField.GridColor), 1),
                        new Point(scanLineActivePos - 1, y), new Point(scanLineActivePos, y));
                }
            }

            for (int y = 2; y <= GameField.Height - 1; y++)
            {
                if (x <= GameField.Width - 1)
                    if (GameField.ArrGameField[x, y] == null)
                        scanLine.Show(scanLineActivePos, y * (GameField.SquareSize + 1), y * (GameField.SquareSize + 1) + GameField.SquareSize);
            }

            if (CurrentBlock != null)
                CurrentBlock.Show(GameField.WinHandle);

            if (scanLineActivePos == this.picBackGround.Width)
            {
                for (int i = 0; i <= GameField.Width - 1; i++)
                {
                    if (GameField.HideCol[i])
                        GameField.DeleteBlocks(i, i);
                }
                scanLineActivePos = 0;
            }
            else
                scanLineActivePos++;

            if (NextBlock1 != null)
                NextBlock1.Show(picNextBlock1.Handle);
            if (NextBlock2 != null)
                NextBlock2.Show(picNextBlock2.Handle);
            if (NextBlock3 != null)
                NextBlock3.Show(picNextBlock3.Handle);
            stillScanning = false;
        }

        private void lumines_Load(object sender, EventArgs e)
        {
            GameField.WinHandle = picBackGround.Handle;
            GameField.BackColor = picBackGround.BackColor;
            try
            {
                highScore = int.Parse(Properties.Settings.Default.HighScore);
                lblHighScore.Text = highScore.ToString();
            }
            catch (Exception E)
            {
                lblHighScoreLoadFailed.Visible = true;
            }
        }

        private void tmrGameClock_Tick(object sender, EventArgs e)
        {
            //如果前一TICK的事件还在被处理，禁止代码运行
            if (stillProcessing) return;
            stillProcessing = true;
            //控制正在下落的方块
            if (preMoveDirection == 'l')
            {
                CurrentBlock.Left();
                preMoveDirection = 'n';
            }
            else if (preMoveDirection == 'r')
            {
                CurrentBlock.Right();
                preMoveDirection = 'n';
            }
            if (!CurrentBlock.Down())
            {
                if (CurrentBlock.Top() < 1)
                {
                    tmrTime.Enabled = false;
                    tmrBonusCheck.Enabled = false;
                    tmrGameClock.Enabled = false;
                    tmrScanLine.Enabled = false;
                    cmdStart.Enabled = true;
                    lblGAMEOVER.Visible = true;
                    stillProcessing = false;
                    return;
                }
                //消去顶部第二行方块
                for (int x = 0; x <= GameField.Width - 1; x++)
                    for (int y = 0; y <= 1; y++)
                    {
                        if (GameField.ArrGameField[x, y] != null)
                        {
                            GameField.ArrGameField[x, y].Hide(GameField.WinHandle);
                            GameField.ArrGameField[x, y] = null;
                        }
                    }
                //更换当前方块
                CurrentBlock = new Block(new Point(GameField.SquareSize * 7 + 8, 1), NextBlock1.BlockType, NextBlock1.direction);
                CurrentBlock.Show(picBackGround.Handle);
                //创建下一方块
                NextBlock1.Hide(picNextBlock1.Handle);
                NextBlock1 = new Block(new Point(0, 0), NextBlock2.BlockType, NextBlock2.direction);
                NextBlock1.Show(picNextBlock1.Handle);
                NextBlock2 = new Block(new Point(0, 0), NextBlock3.BlockType, NextBlock3.direction);
                NextBlock2.Show(picNextBlock2.Handle);
                NextBlock3 = new Block(new Point(0, 0), Block.BlockTypes.Undefined, 0);
                NextBlock3.Show(picNextBlock3.Handle);
                //使方块在顶部暂留一会儿
                tmrGameClock.Enabled = false;
                tmrHold.Enabled = true;
                blockHold = true;
            }
            stillProcessing = false;
        }

        private void Lumines_Activated(object sender, EventArgs e)
        {
            picBackGround.Invalidate();
            picNextBlock1.Invalidate();
            picNextBlock2.Invalidate();
            picNextBlock3.Invalidate();
            Application.DoEvents();
            GameField.Redraw();
            GameField.CheckBlocks(0, GameField.Width - 1);
            if (cmdStart.Enabled == false)
            {
                if (CurrentBlock != null)
                    CurrentBlock.Show(GameField.WinHandle);
                if (NextBlock1 != null)
                    NextBlock1.Show(picNextBlock1.Handle);
                if (NextBlock2 != null)
                    NextBlock2.Show(picNextBlock2.Handle);
                if (NextBlock3 != null)
                    NextBlock3.Show(picNextBlock3.Handle);
            }
        }

        private void tmrHold_Tick(object sender, EventArgs e)
        {
            tmrHold.Enabled = false;
            tmrGameClock.Enabled = true;
            blockHold = false;
            if (!GameField.IsEmpty((CurrentBlock.squareSW.Location.X - 1) / (GameField.SquareSize + 1), (CurrentBlock.squareSW.Location.Y - 1) / (GameField.SquareSize + 1) + 1) &&
                 GameField.IsEmpty((CurrentBlock.squareSE.Location.X - 1) / (GameField.SquareSize + 1), (CurrentBlock.squareSE.Location.Y - 1) / (GameField.SquareSize + 1) + 1))
                preMoveDirection = 'r';
            else if (!GameField.IsEmpty((CurrentBlock.squareSE.Location.X - 1) / (GameField.SquareSize + 1), (CurrentBlock.squareSE.Location.Y - 1) / (GameField.SquareSize + 1) + 1) &&
                 GameField.IsEmpty((CurrentBlock.squareSW.Location.X - 1) / (GameField.SquareSize + 1), (CurrentBlock.squareSW.Location.Y - 1) / (GameField.SquareSize + 1) + 1))
                preMoveDirection = 'l';
        }

        private void tmrBonus_Tick(object sender, EventArgs e)
        {
            tmrBonus.Enabled = false;
            lblBonus.Text = "";
            BonusInfo = "";
            lblAllDeleted.Visible = false;
            lblUnicolor.Visible = false;
        }

        private void tmrBonusCheck_Tick(object sender, EventArgs e)
        {
            if (!GameField.DoBonus)
            {
                if (GameField.Score >= 120)
                    GameField.DoBonus = true;
            }
            if (GameField.DoBonus)
            {
                if (GameField.DoAllDeletedCheck)
                {
                    if (GameField.CheckAllDeleted())
                    {
                        GameField.DoAllDeletedCheck = false;
                        lblScoreValue.Text = GameField.Score.ToString();
                    }
                }
                if (GameField.DoUniColorCheck)
                {
                    if (GameField.CheckUniColor())
                    {
                        GameField.DoUniColorCheck = false;
                        lblScoreValue.Text = GameField.Score.ToString();
                    }
                }
            }
            if (BonusInfo != "")
            {
                if (BonusInfo == "ALL DELETED BONUS 10000PTS")
                {
                    lblUnicolor.Visible = false;
                    lblAllDeleted.Visible = true;
                    tmrBonus.Enabled = true;
                }
                else if (BonusInfo == "UNICOLOR BONUS 1000PTS")
                {
                    lblAllDeleted.Visible = false;
                    lblUnicolor.Visible = true;
                    tmrBonus.Enabled = true;
                }
                else
                {
                    lblBonus.Text = BonusInfo;
                    tmrBonus.Enabled = true;
                    lblScoreValue.Text = GameField.Score.ToString();
                }
            }
        }

        private void tmrTime_Tick(object sender, EventArgs e)
        {
            elapsedSeconds++;
            lblTime.Text = getTime(elapsedSeconds);
        }
    }
}