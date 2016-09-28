namespace LUMINES
{
    partial class MainForm
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.picBackGround = new System.Windows.Forms.PictureBox();
            this.cmdStart = new System.Windows.Forms.Button();
            this.tmrGameClock = new System.Windows.Forms.Timer(this.components);
            this.tmrScanLine = new System.Windows.Forms.Timer(this.components);
            this.picNextBlock3 = new System.Windows.Forms.PictureBox();
            this.picNextBlock2 = new System.Windows.Forms.PictureBox();
            this.picNextBlock1 = new System.Windows.Forms.PictureBox();
            this.lblScoreValue = new System.Windows.Forms.Label();
            this.lblDeleted = new System.Windows.Forms.Label();
            this.lblLevelValue = new System.Windows.Forms.Label();
            this.tmrHold = new System.Windows.Forms.Timer(this.components);
            this.lblBonus = new System.Windows.Forms.Label();
            this.tmrBonus = new System.Windows.Forms.Timer(this.components);
            this.lblGAMEOVER = new System.Windows.Forms.Label();
            this.lblSTART = new System.Windows.Forms.Label();
            this.lblLUMINES = new System.Windows.Forms.Label();
            this.tmrBonusCheck = new System.Windows.Forms.Timer(this.components);
            this.lblPAUSED = new System.Windows.Forms.Label();
            this.lblHighScore = new System.Windows.Forms.Label();
            this.lblTime = new System.Windows.Forms.Label();
            this.lblAllDeleted = new System.Windows.Forms.Label();
            this.lblUnicolor = new System.Windows.Forms.Label();
            this.tmrTime = new System.Windows.Forms.Timer(this.components);
            this.lblHighScoreLoadFailed = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.picBackGround)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picNextBlock3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picNextBlock2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picNextBlock1)).BeginInit();
            this.SuspendLayout();
            // 
            // picBackGround
            // 
            this.picBackGround.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(102)))), ((int)(((byte)(102)))));
            this.picBackGround.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("picBackGround.BackgroundImage")));
            this.picBackGround.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.picBackGround.Location = new System.Drawing.Point(105, 39);
            this.picBackGround.Name = "picBackGround";
            this.picBackGround.Size = new System.Drawing.Size(273, 205);
            this.picBackGround.TabIndex = 0;
            this.picBackGround.TabStop = false;
            // 
            // cmdStart
            // 
            this.cmdStart.BackColor = System.Drawing.Color.Transparent;
            this.cmdStart.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.cmdStart.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cmdStart.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdStart.ForeColor = System.Drawing.SystemColors.ControlText;
            this.cmdStart.Location = new System.Drawing.Point(398, 195);
            this.cmdStart.Name = "cmdStart";
            this.cmdStart.Size = new System.Drawing.Size(67, 49);
            this.cmdStart.TabIndex = 1;
            this.cmdStart.Text = "START";
            this.cmdStart.UseVisualStyleBackColor = false;
            this.cmdStart.Click += new System.EventHandler(this.cmdStart_Click);
            // 
            // tmrGameClock
            // 
            this.tmrGameClock.Interval = 480;
            this.tmrGameClock.Tick += new System.EventHandler(this.tmrGameClock_Tick);
            // 
            // tmrScanLine
            // 
            this.tmrScanLine.Interval = 240;
            this.tmrScanLine.Tick += new System.EventHandler(this.tmrScanLine_Tick);
            // 
            // picNextBlock3
            // 
            this.picNextBlock3.BackColor = System.Drawing.Color.DimGray;
            this.picNextBlock3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picNextBlock3.Location = new System.Drawing.Point(51, 123);
            this.picNextBlock3.Name = "picNextBlock3";
            this.picNextBlock3.Size = new System.Drawing.Size(35, 35);
            this.picNextBlock3.TabIndex = 2;
            this.picNextBlock3.TabStop = false;
            // 
            // picNextBlock2
            // 
            this.picNextBlock2.BackColor = System.Drawing.Color.DimGray;
            this.picNextBlock2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picNextBlock2.Location = new System.Drawing.Point(51, 82);
            this.picNextBlock2.Name = "picNextBlock2";
            this.picNextBlock2.Size = new System.Drawing.Size(35, 35);
            this.picNextBlock2.TabIndex = 3;
            this.picNextBlock2.TabStop = false;
            // 
            // picNextBlock1
            // 
            this.picNextBlock1.BackColor = System.Drawing.Color.DimGray;
            this.picNextBlock1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picNextBlock1.Location = new System.Drawing.Point(51, 41);
            this.picNextBlock1.Name = "picNextBlock1";
            this.picNextBlock1.Size = new System.Drawing.Size(35, 35);
            this.picNextBlock1.TabIndex = 4;
            this.picNextBlock1.TabStop = false;
            // 
            // lblScoreValue
            // 
            this.lblScoreValue.AutoSize = true;
            this.lblScoreValue.BackColor = System.Drawing.Color.Transparent;
            this.lblScoreValue.Font = new System.Drawing.Font("Arial", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblScoreValue.ForeColor = System.Drawing.Color.LightGray;
            this.lblScoreValue.Location = new System.Drawing.Point(390, 91);
            this.lblScoreValue.Name = "lblScoreValue";
            this.lblScoreValue.Size = new System.Drawing.Size(16, 16);
            this.lblScoreValue.TabIndex = 5;
            this.lblScoreValue.Text = "0";
            // 
            // lblDeleted
            // 
            this.lblDeleted.AutoSize = true;
            this.lblDeleted.BackColor = System.Drawing.Color.Transparent;
            this.lblDeleted.Font = new System.Drawing.Font("Arial", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDeleted.ForeColor = System.Drawing.Color.LightGray;
            this.lblDeleted.Location = new System.Drawing.Point(390, 156);
            this.lblDeleted.Name = "lblDeleted";
            this.lblDeleted.Size = new System.Drawing.Size(16, 16);
            this.lblDeleted.TabIndex = 6;
            this.lblDeleted.Text = "0";
            // 
            // lblLevelValue
            // 
            this.lblLevelValue.AutoSize = true;
            this.lblLevelValue.BackColor = System.Drawing.Color.Transparent;
            this.lblLevelValue.Font = new System.Drawing.Font("Arial", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLevelValue.ForeColor = System.Drawing.Color.LightGray;
            this.lblLevelValue.Location = new System.Drawing.Point(153, 250);
            this.lblLevelValue.Name = "lblLevelValue";
            this.lblLevelValue.Size = new System.Drawing.Size(16, 16);
            this.lblLevelValue.TabIndex = 7;
            this.lblLevelValue.Text = "1";
            // 
            // tmrHold
            // 
            this.tmrHold.Interval = 2800;
            this.tmrHold.Tick += new System.EventHandler(this.tmrHold_Tick);
            // 
            // lblBonus
            // 
            this.lblBonus.AutoSize = true;
            this.lblBonus.BackColor = System.Drawing.Color.Transparent;
            this.lblBonus.Font = new System.Drawing.Font("Arial", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBonus.ForeColor = System.Drawing.Color.White;
            this.lblBonus.Location = new System.Drawing.Point(312, 250);
            this.lblBonus.Name = "lblBonus";
            this.lblBonus.Size = new System.Drawing.Size(0, 16);
            this.lblBonus.TabIndex = 8;
            // 
            // tmrBonus
            // 
            this.tmrBonus.Interval = 3000;
            this.tmrBonus.Tick += new System.EventHandler(this.tmrBonus_Tick);
            // 
            // lblGAMEOVER
            // 
            this.lblGAMEOVER.AutoSize = true;
            this.lblGAMEOVER.BackColor = System.Drawing.Color.Transparent;
            this.lblGAMEOVER.Font = new System.Drawing.Font("Arial", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblGAMEOVER.ForeColor = System.Drawing.Color.White;
            this.lblGAMEOVER.Location = new System.Drawing.Point(170, 129);
            this.lblGAMEOVER.Name = "lblGAMEOVER";
            this.lblGAMEOVER.Size = new System.Drawing.Size(136, 24);
            this.lblGAMEOVER.TabIndex = 9;
            this.lblGAMEOVER.Text = "GAME OVER";
            this.lblGAMEOVER.Visible = false;
            // 
            // lblSTART
            // 
            this.lblSTART.AutoSize = true;
            this.lblSTART.BackColor = System.Drawing.Color.Transparent;
            this.lblSTART.Font = new System.Drawing.Font("Arial", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSTART.ForeColor = System.Drawing.Color.White;
            this.lblSTART.Location = new System.Drawing.Point(130, 186);
            this.lblSTART.Name = "lblSTART";
            this.lblSTART.Size = new System.Drawing.Size(216, 24);
            this.lblSTART.TabIndex = 10;
            this.lblSTART.Text = "Press START button";
            // 
            // lblLUMINES
            // 
            this.lblLUMINES.AutoSize = true;
            this.lblLUMINES.BackColor = System.Drawing.Color.Transparent;
            this.lblLUMINES.Font = new System.Drawing.Font("Arial", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLUMINES.ForeColor = System.Drawing.Color.Orange;
            this.lblLUMINES.Location = new System.Drawing.Point(165, 111);
            this.lblLUMINES.Name = "lblLUMINES";
            this.lblLUMINES.Size = new System.Drawing.Size(146, 34);
            this.lblLUMINES.TabIndex = 11;
            this.lblLUMINES.Text = "LUMINES";
            // 
            // tmrBonusCheck
            // 
            this.tmrBonusCheck.Tick += new System.EventHandler(this.tmrBonusCheck_Tick);
            // 
            // lblPAUSED
            // 
            this.lblPAUSED.AutoSize = true;
            this.lblPAUSED.BackColor = System.Drawing.Color.Transparent;
            this.lblPAUSED.Font = new System.Drawing.Font("Arial", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPAUSED.ForeColor = System.Drawing.Color.White;
            this.lblPAUSED.Location = new System.Drawing.Point(182, 135);
            this.lblPAUSED.Name = "lblPAUSED";
            this.lblPAUSED.Size = new System.Drawing.Size(112, 29);
            this.lblPAUSED.TabIndex = 12;
            this.lblPAUSED.Text = "PAUSED";
            this.lblPAUSED.Visible = false;
            // 
            // lblHighScore
            // 
            this.lblHighScore.AutoSize = true;
            this.lblHighScore.BackColor = System.Drawing.Color.Transparent;
            this.lblHighScore.Font = new System.Drawing.Font("Arial", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHighScore.ForeColor = System.Drawing.Color.LightGray;
            this.lblHighScore.Location = new System.Drawing.Point(390, 123);
            this.lblHighScore.Name = "lblHighScore";
            this.lblHighScore.Size = new System.Drawing.Size(16, 16);
            this.lblHighScore.TabIndex = 13;
            this.lblHighScore.Text = "0";
            // 
            // lblTime
            // 
            this.lblTime.AutoSize = true;
            this.lblTime.BackColor = System.Drawing.Color.Transparent;
            this.lblTime.Font = new System.Drawing.Font("Arial", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTime.ForeColor = System.Drawing.Color.LightGray;
            this.lblTime.Location = new System.Drawing.Point(235, 250);
            this.lblTime.Name = "lblTime";
            this.lblTime.Size = new System.Drawing.Size(36, 16);
            this.lblTime.TabIndex = 14;
            this.lblTime.Text = "0:00";
            // 
            // lblAllDeleted
            // 
            this.lblAllDeleted.AutoSize = true;
            this.lblAllDeleted.BackColor = System.Drawing.Color.Transparent;
            this.lblAllDeleted.Font = new System.Drawing.Font("Arial", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAllDeleted.ForeColor = System.Drawing.Color.White;
            this.lblAllDeleted.Location = new System.Drawing.Point(126, 139);
            this.lblAllDeleted.Name = "lblAllDeleted";
            this.lblAllDeleted.Size = new System.Drawing.Size(224, 16);
            this.lblAllDeleted.TabIndex = 15;
            this.lblAllDeleted.Text = "ALL DELETED BONUS 10000PTS";
            this.lblAllDeleted.Visible = false;
            // 
            // lblUnicolor
            // 
            this.lblUnicolor.AutoSize = true;
            this.lblUnicolor.BackColor = System.Drawing.Color.Transparent;
            this.lblUnicolor.Font = new System.Drawing.Font("Arial", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUnicolor.ForeColor = System.Drawing.Color.White;
            this.lblUnicolor.Location = new System.Drawing.Point(139, 139);
            this.lblUnicolor.Name = "lblUnicolor";
            this.lblUnicolor.Size = new System.Drawing.Size(198, 16);
            this.lblUnicolor.TabIndex = 16;
            this.lblUnicolor.Text = "UNICOLOR BONUS 1000PTS";
            this.lblUnicolor.Visible = false;
            // 
            // tmrTime
            // 
            this.tmrTime.Interval = 1000;
            this.tmrTime.Tick += new System.EventHandler(this.tmrTime_Tick);
            // 
            // lblHighScoreLoadFailed
            // 
            this.lblHighScoreLoadFailed.AutoSize = true;
            this.lblHighScoreLoadFailed.BackColor = System.Drawing.Color.Transparent;
            this.lblHighScoreLoadFailed.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Bold);
            this.lblHighScoreLoadFailed.ForeColor = System.Drawing.Color.Red;
            this.lblHighScoreLoadFailed.Location = new System.Drawing.Point(69, 26);
            this.lblHighScoreLoadFailed.Name = "lblHighScoreLoadFailed";
            this.lblHighScoreLoadFailed.Size = new System.Drawing.Size(338, 14);
            this.lblHighScoreLoadFailed.TabIndex = 17;
            this.lblHighScoreLoadFailed.Text = "Unable to load configuration file,  HI-SCORE will not be saved.";
            this.lblHighScoreLoadFailed.Visible = false;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.DimGray;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.ClientSize = new System.Drawing.Size(477, 272);
            this.Controls.Add(this.lblHighScoreLoadFailed);
            this.Controls.Add(this.lblUnicolor);
            this.Controls.Add(this.lblAllDeleted);
            this.Controls.Add(this.lblTime);
            this.Controls.Add(this.lblHighScore);
            this.Controls.Add(this.lblPAUSED);
            this.Controls.Add(this.lblLUMINES);
            this.Controls.Add(this.lblSTART);
            this.Controls.Add(this.lblGAMEOVER);
            this.Controls.Add(this.lblBonus);
            this.Controls.Add(this.lblLevelValue);
            this.Controls.Add(this.lblDeleted);
            this.Controls.Add(this.lblScoreValue);
            this.Controls.Add(this.picNextBlock1);
            this.Controls.Add(this.picNextBlock2);
            this.Controls.Add(this.picNextBlock3);
            this.Controls.Add(this.cmdStart);
            this.Controls.Add(this.picBackGround);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "LUMINES";
            this.Activated += new System.EventHandler(this.Lumines_Activated);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.keyDown);
            this.Load += new System.EventHandler(this.lumines_Load);
            ((System.ComponentModel.ISupportInitialize)(this.picBackGround)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picNextBlock3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picNextBlock2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picNextBlock1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox picBackGround;
        private System.Windows.Forms.Button cmdStart;
        private System.Windows.Forms.Timer tmrGameClock;
        private System.Windows.Forms.Timer tmrScanLine;
        private System.Windows.Forms.PictureBox picNextBlock3;
        private System.Windows.Forms.PictureBox picNextBlock2;
        private System.Windows.Forms.PictureBox picNextBlock1;
        private System.Windows.Forms.Label lblScoreValue;
        private System.Windows.Forms.Label lblDeleted;
        private System.Windows.Forms.Label lblLevelValue;
        private System.Windows.Forms.Timer tmrHold;
        private System.Windows.Forms.Label lblBonus;
        private System.Windows.Forms.Timer tmrBonus;
        private System.Windows.Forms.Label lblGAMEOVER;
        private System.Windows.Forms.Label lblSTART;
        private System.Windows.Forms.Label lblLUMINES;
        private System.Windows.Forms.Timer tmrBonusCheck;
        private System.Windows.Forms.Label lblPAUSED;
        private System.Windows.Forms.Label lblHighScore;
        private System.Windows.Forms.Label lblTime;
        private System.Windows.Forms.Label lblAllDeleted;
        private System.Windows.Forms.Label lblUnicolor;
        private System.Windows.Forms.Timer tmrTime;
        private System.Windows.Forms.Label lblHighScoreLoadFailed;
    }
}

