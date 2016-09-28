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
    class ScanLine
    {
        public static Graphics ScanLineGraphic = Graphics.FromHwnd(GameField.WinHandle);
        public static Color[] ScanLineColor = { Color.Gold, Color.Orange, Color.Yellow, Color.White, Color.LightSalmon};
        public Pen ScanLineActivatePen;
        public static Pen ScanLineDeactivatePen = new Pen(GameField.BackColor, 1);
        private string countText="";

        public ScanLine(int ScanLineColorIndex)
        {
            ScanLineActivatePen = new Pen(ScanLineColor[ScanLineColorIndex], 1);
        }

        public void Show(int x, int y0, int y1)
        {
            ScanLineGraphic.DrawLine(ScanLineActivatePen, x, y0, x, y1);
        }

        public void ShowTop(int x)
        {
            ScanLineGraphic.DrawRectangle(ScanLineActivatePen, new Rectangle(new Point(x - 28, 19), new Size(28, 14)));
            ScanLineGraphic.DrawLine(ScanLineActivatePen, new Point(x, 19), new Point(x + 7, 26));
            ScanLineGraphic.DrawLine(ScanLineActivatePen, new Point(x, 33), new Point(x + 7, 26));
            countText = GameField.DeletedBlocks.ToString();
            if (GameField.DeletedBlocks < 10)
                ScanLineGraphic.DrawString(countText,
                                            new Font("ArialBlack", 8, FontStyle.Bold), new SolidBrush(Color.White),
                                            new PointF(x - 12, 20));
            else if (GameField.DeletedBlocks >= 10 && GameField.DeletedBlocks < 100)
                ScanLineGraphic.DrawString(countText,
                                            new Font("ArialBlack", 8, FontStyle.Bold), new SolidBrush(Color.White),
                                            new PointF(x - 18, 20));
            else
                ScanLineGraphic.DrawString(countText,
                                            new Font("ArialBlack", 8, FontStyle.Bold), new SolidBrush(Color.White),
                                            new PointF(x - 24, 20));
        }

        public void HideTop(int x)
        {
            Rectangle rectangle = new Rectangle(new Point(x - 28, 19), new Size(28, 14));
            ScanLineGraphic.DrawRectangle(new Pen(GameField.BackColor, 1), rectangle);
            ScanLineGraphic.FillRectangle(new SolidBrush(GameField.BackColor), rectangle);
            ScanLineGraphic.DrawLine(new Pen(GameField.BackColor, 1), new Point(x, 19), new Point(x + 7, 26));
            ScanLineGraphic.DrawLine(new Pen(GameField.BackColor, 1), new Point(x, 33), new Point(x + 7, 26));
        }
    }
}
